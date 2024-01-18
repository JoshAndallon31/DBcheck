using Microsoft.AspNetCore.Mvc;
using CampusFeedApi.Data;
using CampusFeedApi.Dto;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("by-Feed/{CampusFeedId}")]
        public async Task<IActionResult> GetListByFeed(string CampusFeedId)
        {
            var Feed = await _infosRepository.GetById(CampusFeedId);

            if (Feed == null)
            {
                return NotFound("Feed not found");
            }

            _logger.LogInformation("Getting feed by ID");

            // convert to DTO
            var feedReturn = new CampusFeedDto();
            feedReturn.CampusFeedId = Feed.CampusFeedId;
            feedReturn.Content = Feed.Content;
            feedReturn.Category = Feed.Category;
            feedReturn.Date = Feed.Date;
            feedReturn.Like = Feed.Like;
            feedReturn.Dislike = Feed.Dislike;

            return Ok(feedReturn);
        }



        [HttpPost()]
        public async Task<IActionResult> Post(CampusFeedDto input)
        {
            try
            {
                var feed = new CampusFeedDto
                {
                    CampusFeedId = input.CampusFeedId,
                    Content = input.Content,
                    Category = input.Category,
                    Date = input.Date,
                    Like = input.Like,
                    Dislike = input.Dislike
                };

                _infosRepository.Add(feed);

                if (await _infosRepository.SaveAllChangesAsync())
                {
                    return Ok("Feed Created Successfully");
                }

                return BadRequest("Error");
            }
            catch (DbUpdateException ex)
            {
                return BadRequest("Error: A unique constraint would be violated, it could be the ID has already been used.");
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