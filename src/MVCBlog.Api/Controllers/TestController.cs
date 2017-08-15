using MVCBlog.Core.Commands;
using MVCBlog.Core.Database;
using MVCBlog.Core.Entities;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace MVCBlog.Api.Controllers
{
  public class TestController : ApiController
  {
    /// <summary>
    /// The repository.
    /// </summary>
    private readonly IRepository repository;

    /// <summary>
    /// The add blog entry comment command handler.
    /// </summary>
    private readonly ICommandHandler<AddBlogEntryCommentCommand> addBlogEntryCommentCommandHandler;

    /// <summary>
    /// The delete blog entry comment command hander.
    /// </summary>
    private readonly ICommandHandler<DeleteCommand<BlogEntryComment>> deleteBlogEntryCommentCommandHander;

    /// <summary>
    /// The delete blog entry command hander.
    /// </summary>
    private readonly ICommandHandler<DeleteBlogEntryCommand> deleteBlogEntryCommandHander;

    /// <summary>
    /// The update blog entry command handler.
    /// </summary>
    private readonly ICommandHandler<UpdateCommand<BlogEntry>> updateBlogEntryCommandHandler;

    /// <summary>
    /// The update blog entry file command handler.
    /// </summary>
    private readonly ICommandHandler<UpdateCommand<BlogEntryFile>> updateBlogEntryFileCommandHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlogController" /> class.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="addBlogEntryCommentCommandHandler">The add blog entry comment command handler.</param>
    /// <param name="deleteBlogEntryCommentCommandHander">The delete blog entry comment command hander.</param>
    /// <param name="deleteBlogEntryCommandHander">The delete blog entry command hander.</param>
    /// <param name="updateBlogEntryCommandHandler">The update blog entry command handler.</param>
    /// <param name="updateBlogEntryFileCommandHandler">The update blog entry file command handler.</param>
    public TestController(
      IRepository repository,
      ICommandHandler<AddBlogEntryCommentCommand> addBlogEntryCommentCommandHandler,
      ICommandHandler<DeleteCommand<BlogEntryComment>> deleteBlogEntryCommentCommandHander,
      ICommandHandler<DeleteBlogEntryCommand> deleteBlogEntryCommandHander,
      ICommandHandler<UpdateCommand<BlogEntry>> updateBlogEntryCommandHandler,
      ICommandHandler<UpdateCommand<BlogEntryFile>> updateBlogEntryFileCommandHandler)
    {
      this.repository = repository;

      this.addBlogEntryCommentCommandHandler = addBlogEntryCommentCommandHandler;
      this.deleteBlogEntryCommentCommandHander = deleteBlogEntryCommentCommandHander;
      this.deleteBlogEntryCommandHander = deleteBlogEntryCommandHander;
      this.updateBlogEntryCommandHandler = updateBlogEntryCommandHandler;
      this.updateBlogEntryFileCommandHandler = updateBlogEntryFileCommandHandler;
    }


    [HttpGet]
    public IHttpActionResult Get()
    {
      return Ok(new
      {
        request = "PopularBlogs",
        data = repository.BlogEntries
          .AsNoTracking()
          .OrderByDescending(b => b.Visits)
          .Take(5)
      });
    }
  }
}
