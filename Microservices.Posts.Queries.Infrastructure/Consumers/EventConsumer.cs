﻿using Confluent.Kafka;
using Microservices.Posts.CQRS.Consumers;
using Microservices.Posts.CQRS.Events;
using Microservices.Posts.Queries.Infrastructure.Converters;
using Microservices.Posts.Queries.Infrastructure.Handlers;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Microservices.Posts.Queries.Infrastructure.Consumers
{
    public class EventConsumer(IOptions<ConsumerConfig> config, IEventHandler eventHandler) : IEventConsumer
    {
        private readonly ConsumerConfig _config = config.Value;
        private readonly IEventHandler _eventHandler = eventHandler;

        public void Consume(string topic)
        {
            using var consumer = new ConsumerBuilder<string, string>(_config)
                .SetKeyDeserializer(Deserializers.Utf8)
                .SetValueDeserializer(Deserializers.Utf8)
                .Build();

            consumer.Subscribe(topic);

            while (true)
            {
                var consumerResult = consumer.Consume();

                if (consumerResult?.Message == null) continue;

                var options = new JsonSerializerOptions { Converters = { new EventJsonConverter() } };
                var @event = JsonSerializer.Deserialize<BaseEvent>(consumerResult.Message.Value, options);

                var handlerMethod = _eventHandler.GetType().GetMethod("On", [@event.GetType()]);

                if (handlerMethod == null)
                {
                    throw new ArgumentNullException(nameof(handlerMethod), "Could not find event handler.");
                }

                handlerMethod.Invoke(_eventHandler, new object[] { @event });
                consumer.Commit(consumerResult);
            }
        }
    }
}
