using System.Runtime.Intrinsics.Arm;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CampusFeedApi.Data;
using CampusFeedApi.Dto;
using CampusFeedApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CampusFeedApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CampusFeedController : ControllerBase
    {
        private readonly ILogger<CampusFeedController> _logger;
        private readonly ICampusFeedInfoRepository _infosRepository;

        public CampusFeedController(ILogger<CampusFeedController> logger, ICampusFeedInfoRepository infosRepository)
        {
            _logger = logger;
            _infosRepository = infosRepository;
        }

        [HttpGet()]
        public async Task<IActionResult> GetList()
        {
            var Feed = await _infosRepository.GetAllAsync();
            if (Feed == null || !Feed.Any())
            {
                return Ok("Empty");
            }
            _logger.LogInformation("Getting all list");
            return Ok(Feed);
        }



        [HttpPost()]
        public async Task<IActionResult> Post(CampusFeedDto input)
        {
            try
            {
                var Feed = new CampusFeedInfo("1")
                {
                    Content = "SampleContent",
                    Category = "SampleCategory",
                    Date = DateTime.Parse("2024-01-17T12:00:00"), // Convert string to DateTime
                    Like = 10,
                    Dislike = 2
                };

                _infosRepository.Add(Feed);
                if (await _infosRepository.SaveAllChangesAsync())
                {
                    return Ok("Feed Created Successfully");
                }

                return BadRequest("Error");

            }
            catch (DbUpdateException ex)
            {
                return BadRequest("Error: A unique constraint would be violated it could be id has been used.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(CampusFeedDto input)
        {
            var Feed = await _infosRepository.GetById(input.CampusFeedId);
            Feed.CampusFeedId = input.CampusFeedId;
            Feed.Content = input.Content;
            Feed.Category = input.Category;
            Feed.Date = input.Date;
            Feed.Like = input.Like;
            Feed.Dislike = input.Dislike;

            if (Feed == null)
            {
                return NotFound($"Feed with ID {input.CampusFeedId} not found.");
            }
            if (await _infosRepository.SaveAllChangesAsync())
            {
                return Ok("Feed updated successfully.");
            }
            return BadRequest("Error updating user.");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Feed = await _infosRepository.GetById(id);

            if (Feed != null)
            {
                _infosRepository.Delete(Feed);
                if (await _infosRepository.SaveAllChangesAsync())
                {
                    return Ok($"Feed with id {id} is deleted");
                }
            }


            return BadRequest($"Feed with id {id} is not found.");
        }

    }
}

