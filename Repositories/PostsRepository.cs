using Reddit.APIModels.RequestModels;
using Reddit.Models;
using System.Linq.Expressions;

namespace Reddit.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly ApplicationDBContext _context;

        public PostsRepository(ApplicationDBContext applicationDBContext)
        {
            this._context = applicationDBContext;
        }

        public async Task<PagedList<Post>> GetAll(PostsRequestModel getPostsRequest)
        {
            IQueryable<Post> postsQueryable = _context.Posts;

            if (!string.IsNullOrWhiteSpace(getPostsRequest.SearchKey))
            {
                postsQueryable = postsQueryable.Where(p => p.Title.Contains(getPostsRequest.SearchKey) || p.Content.Contains(getPostsRequest.SearchKey));
            }

            bool isAscending = getPostsRequest.IsAscending ?? false;

            switch (getPostsRequest.SortKey)
            {
                case "createdAt":
                    postsQueryable = !isAscending ? postsQueryable.OrderByDescending(p => p.CreateAt) : postsQueryable.OrderBy(p => p.Upvotes - p.Downvotes);
                    break;
                case "positivity":
                    postsQueryable = !isAscending ? postsQueryable.OrderByDescending(p => p.Upvotes - p.Downvotes) : postsQueryable.OrderBy(p => p.Upvotes - p.Downvotes);
                    break;
                case "id":
                    postsQueryable = !isAscending ? postsQueryable.OrderByDescending(p => p.Id) : postsQueryable.OrderBy(p => p.Id);
                    break;
            }


            return await PagedList<Post>.CreateAsync(postsQueryable, getPostsRequest.Page, getPostsRequest.PageSize);
        }
    }
}
