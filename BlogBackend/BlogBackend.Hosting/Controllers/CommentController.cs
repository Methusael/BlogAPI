using BlogBackend.Application.Features.Comments.Dtos;
using BlogBackend.Application.Interfaces;
using BlogBackend.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace BlogBackend.Hosting.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly IReadService<Comment, CommentDto> _commentReadService;
        private readonly IWriteService<Comment, CreateCommentDto, UpdateCommentDto> _commentWriteService;
        private readonly ILogger<CommentController> _logger;

        public CommentController(
            IReadService<Comment, CommentDto> postService,
            IWriteService<Comment, CreateCommentDto, UpdateCommentDto> postWriteService,
            ILogger<CommentController> logger)
        {
            _commentReadService = postService;
            _commentWriteService = postWriteService;
            _logger = logger;
        }

        [HttpGet("CommentsOnPost/{id}")]
        public async Task<IActionResult> GetCommentsOnPostAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                var filter = (Expression<Func<Comment, bool>>)(c => c.PostId == id);
                var comments = await _commentReadService.GetAsync(filter);
                return Ok(comments.ToList());
            }
            catch (OperationCanceledException)
            {
                return StatusCode(499, "Request canceled");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                var comment = await _commentReadService.GetByIdAsync(id);
                return Ok(comment);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(499, "Request canceled");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCommentDto createCommentDto, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _commentWriteService.CreateAsync(createCommentDto);
                return Ok();
            }
            catch (OperationCanceledException)
            {
                return StatusCode(499, "Request canceled");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> EditAsync(Guid id, [FromBody] UpdateCommentDto updateCommentDto, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _commentWriteService.UpdateAsync(id, updateCommentDto);
                return Ok();
            }
            catch (OperationCanceledException)
            {
                return StatusCode(499, "Request canceled");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync([FromBody] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _commentWriteService.DeleteAsync(id);
                return Ok();
            }
            catch (OperationCanceledException)
            {
                return StatusCode(499, "Request canceled");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
