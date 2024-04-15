using Microsoft.AspNetCore.Mvc;

namespace Reddit.APIModels.RequestModels
{
    public class PostsRequestModel
    {
        [FromQuery]
        public string? SearchKey { get; set; }

        [FromQuery]
        public bool? IsAscending { get; set; }

        [FromQuery]
        public string? SortKey { get; set; }

        [FromQuery]
        public int Page { get; set; } = 1;

        [FromQuery]
        public int PageSize { get; set; } = 10;
    }
}
