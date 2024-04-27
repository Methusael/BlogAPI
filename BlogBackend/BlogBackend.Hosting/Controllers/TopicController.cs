using BlogBackend.Application.DTOs;
using BlogBackend.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogBackend.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService;
        private readonly ILogger<TopicController> _logger;

        public TopicController(ITopicService postService, ILogger<TopicController> logger)
        {
            _topicService = postService;
            _logger = logger;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAsync([FromBody] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                var topic = await _topicService.GetByIdAsync(id, cancellationToken);
                return Ok(topic);
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

        [HttpGet("All")]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                var topics = await _topicService.GetAllAsync(cancellationToken);
                return Ok(topics);
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

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] TopicDTO topicDto, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                var guid = await _topicService.AddAsync(topicDto, cancellationToken);
                return Ok(guid);
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

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] TopicDTO topicDto, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _topicService.UpdateAsync(topicDto, cancellationToken);
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

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _topicService.DeleteByIdAsync(id, cancellationToken);
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