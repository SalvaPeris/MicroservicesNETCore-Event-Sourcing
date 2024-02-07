using Microservices.Posts.Common.DTOs;
using Microservices.Posts.Queries.Domain.Entities;

namespace Microservices.Posts.Queries.Api.DTOs
{
    public class PostLookupResponse : BaseResponse
    {
        public List<PostEntity>? Posts { get; set; }
    }
}
