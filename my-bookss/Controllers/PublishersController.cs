using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_bookss.ActionResult;
using my_bookss.Data.Models;
using my_bookss.Data.Services;
using my_bookss.Data.ViewModels;
using my_bookss.Exceptions;

namespace my_bookss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        PublishersService _publishersService;
        public PublishersController(PublishersService publishersService)
        {
            _publishersService=publishersService;
        }

        //Get all publishers - used for sorting,filtering and paging as well
        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy,string searchString)
        {
            try
            {
                var _result = _publishersService.GetAllPublishers(sortBy,searchString);
                return Ok(_result);
            }
            catch (Exception )
            {
                return BadRequest("We could not load the publishers");
                
            }
        }
        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody]PublisherVM publisher)
        {
            try
            {
                var newPublisher = _publishersService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), newPublisher);

            }
            catch(PublisherNameException ex)
            {
                return BadRequest($"{ex.Message},Publisher name : {ex.PublisherName}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }
            
        }

        [HttpGet("get-publisher-books-with-author/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var _response = _publishersService.GetPublisherData(id);
            return Ok(_response);
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            
            var _response = _publishersService.GetPublisherById(id);
            if (_response != null)
            {
                return Ok(_response);
                
            }
            else
            {
               return NotFound();
            }
            
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
                _publishersService.DeletePublisherById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }
        }

    }
}
