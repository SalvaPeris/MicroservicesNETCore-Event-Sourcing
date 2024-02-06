using Microservices.Posts.Common.DTOs;

namespace Microservices.Posts.Commands.Api.DTOs
{
    public class NewPostResponse : BaseResponse
    {
        public Guid Id { get; set; }
    }
}
