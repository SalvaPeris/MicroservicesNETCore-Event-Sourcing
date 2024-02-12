using Microservices.Posts.Commands.Api.Commands;
using Microservices.Posts.Common.DTOs;
using Microservices.Posts.CQRS.Exceptions;
using Microservices.Posts.CQRS.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Posts.Commands.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RemoveLikePostController : ControllerBase
    {
        private readonly ILogger<RemoveLikePostController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public RemoveLikePostController(ILogger<RemoveLikePostController> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> LikePostAsync(Guid id)
        {
            try
            {
                await _commandDispatcher.SendAsync(new RemoveLikePostCommand { Id = id });

                return Ok(new BaseResponse
                {
                    Message = "Like post request completed successfully!"
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.Log(LogLevel.Warning, ex, "Client made a bad request!");
                return BadRequest(new BaseResponse
                {
                    Message = ex.Message
                });
            }
            catch (AggregateNotFoundException ex)
            {
                _logger.Log(LogLevel.Warning, ex, "Could not retrieve aggregate, client passed an incorrect post ID targetting the aggregate!");
                return BadRequest(new BaseResponse
                {
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to like a post!";
                _logger.Log(LogLevel.Error, ex, SAFE_ERROR_MESSAGE);

                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }
    }
}
