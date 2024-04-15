using Reddit.APIModels.RequestModels;
using Reddit.Models;

namespace Reddit.Repositories
{
    public interface IPostsRepository
    {
        public Task<PagedList<Post>> GetAll(PostsRequestModel getPostsRequest);
    }
}
