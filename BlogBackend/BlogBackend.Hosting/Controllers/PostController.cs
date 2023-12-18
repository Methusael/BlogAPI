using BlogBackend.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogBackend.Hosting.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                var posts = await _postService.GetAllAsync(cancellationToken);
                return Ok(posts);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(499, "Request canceled");
            }
        }
    }
}
