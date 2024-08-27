using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;


namespace Entities.DTO
{
    public record LinkParameters
    {
        public BookParameters BookParameters { get; init; }
        public HttpContext HttpContext { get; init; }
    }
}
