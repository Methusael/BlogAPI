using BlogBackend.Application.Features.Topics.Dtos;
using BlogBackend.Application.Interfaces;
using BlogBackend.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogBackend.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TopicController : ControllerBase
    {
        private readonly IReadService<Topic,TopicDto> _topicReadService;
        private readonly ILogger<TopicController> _logger;

        public TopicController(IReadService<Topic, TopicDto> topicReadService, ILogger<TopicController> logger)
        {
            _topicReadService = topicReadService;
            _logger = logger;
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                var topic = await _topicReadService.GetByIdAsync(id);
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
                var topics = await _topicReadService.GetAllAsync();
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

        //[Authorize(Roles = "Admin")]
        //[HttpPost("Create")]
        //public async Task<IActionResult> CreateAsync([FromBody] CreateTopicDto topicDto, CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        cancellationToken.ThrowIfCancellationRequested();
        //        var guid = await _topicReadService.AddAsync(topicDto, cancellationToken);
        //        return Ok(guid);
        //    }
        //    catch (OperationCanceledException)
        //    {
        //        return StatusCode(499, "Request canceled");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[Authorize(Roles = "Admin")]
        //[HttpPut("Edit")]
        //public async Task<IActionResult> Edit([FromBody] UpdateTopicDto updateTopicDto, CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        cancellationToken.ThrowIfCancellationRequested();
        //        await _topicReadService.UpdateAsync(updateTopicDto);
        //        return Ok();
        //    }
        //    catch (OperationCanceledException)
        //    {
        //        return StatusCode(499, "Request canceled");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[Authorize(Roles = "Admin")]
        //[HttpDelete("Delete")]
        //public async Task<IActionResult> Delete([FromBody] Guid id, CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        cancellationToken.ThrowIfCancellationRequested();
        //        await _topicReadService.DeleteByIdAsync(id, cancellationToken);
        //        return Ok();
        //    }
        //    catch (OperationCanceledException)
        //    {
        //        return StatusCode(499, "Request canceled");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}