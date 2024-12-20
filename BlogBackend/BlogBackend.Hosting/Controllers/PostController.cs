﻿using BlogBackend.Application.Features.Posts.Dtos;
using BlogBackend.Application.Interfaces;
using BlogBackend.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogBackend.Hosting.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IReadService<Post, PostDto> _postReadService;
        private readonly IWriteService<Post, CreatePostDto, UpdatePostDto> _postWriteService;
        private readonly ILogger<PostController> _logger;

        public PostController(
            IReadService<Post, PostDto> postService,
            IWriteService<Post, CreatePostDto, UpdatePostDto> postWriteService,
            ILogger<PostController> logger)
        {
            _postReadService = postService;
            _postWriteService = postWriteService;
            _logger = logger;
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                var post = await _postReadService.GetByIdAsync(id);
                return Ok(post);
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

        //[HttpGet("UserPosts")]
        //public async Task<IActionResult> GetUserPostsAsync(CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        cancellationToken.ThrowIfCancellationRequested();
        //        var posts = await _postReadService.GetAllAsync();
        //        return Ok(posts);
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

        [HttpGet("All")]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                var posts = await _postReadService.GetAllAsync();
                return Ok(posts);
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
        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePostDto createPostDto, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _postWriteService.CreateAsync(createPostDto);
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
        public async Task<IActionResult> Edit(Guid id, [FromBody] UpdatePostDto updatePostDto, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _postWriteService.UpdateAsync(id, updatePostDto);
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
        public async Task<IActionResult> Delete([FromBody] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                await _postWriteService.DeleteAsync(id);
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

        //[Authorize(Roles = "Admin,User")]
        //[HttpGet("Like")]
        //public async Task<IActionResult> LikePostAsync(CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        cancellationToken.ThrowIfCancellationRequested();
        //        var posts = await _postService.GetAllAsync(cancellationToken);
        //        return Ok(posts);
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

        //[Authorize(Roles = "Admin,User")]
        //[HttpGet("CommentOnPost")]
        //public async Task<IActionResult> CommentOnPostAsync(CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        cancellationToken.ThrowIfCancellationRequested();
        //        var posts = await _postService.GetAllAsync(cancellationToken);
        //        return Ok(posts);
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

        //[HttpGet("Comments")]
        //public async Task<IActionResult> GetCommentsAsync(CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        cancellationToken.ThrowIfCancellationRequested();
        //        var posts = await _postService.GetAllAsync(cancellationToken);
        //        return Ok(posts);
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
