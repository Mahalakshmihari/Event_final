using EventManagement_Backend.IRepository;
using EventManagement_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _review;
        
        public ReviewsController(IReviewRepository review)
        {
            _review = review;
        }

        // GET: api/<ReviewController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_review.GetAllReviews());
        }

        // GET api/<ReviewController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var reviewid = _review.GetReviewsByEventId(id);
            if (reviewid != null)
            {
                return Ok(reviewid);
            }
            else
            {
                return BadRequest();
            }
        }

        // POST api/<ReviewController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Review review)
        {
            var adding = _review.AddReview(review);
            if (adding != null)
            {
                return Ok(adding);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/<ReviewController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Review review)
        {
            var updating = _review.UpdateReview(review);
            if (updating != null)
            {
                return Ok(updating);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/<ReviewController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleting = _review.DeleteReview(id);
            return Ok(deleting);
        }
    }
}

