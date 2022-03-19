using AutoMapper;
using CarWebAPI.Helpers;
using CarWebAPI.Models;
using CarWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CarCommandController : ControllerBase
    {
        private ICarCommandRepository _carCommandService;
        private IMapper _mapper;

        public CarCommandController(ICarCommandRepository carCommandService,
                                    IMapper mapper)
        {
            _carCommandService = carCommandService;
            _mapper = mapper;
        }

        [HttpPost("create/make")]
        public IActionResult CreateMake(string type)
        {
            try
            {
                _carCommandService.CreateMake(type);
                return Ok();
            }
            catch (ApiException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("create/model")]
        public IActionResult CreateModel(string model, int makeId)
        {
            try
            {
                _carCommandService.CreateModelForSpecificMake(model, makeId);
                return Ok();
            }
            catch (ApiException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("delete/make/id/")]
        public IActionResult DeleteMake(int makeId)
        {
            try
            {
                _carCommandService.RemoveMake(makeId);
                return Ok();
            }
            catch (ApiException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("delete/model/id/")]
        public IActionResult DeleteModel(int id)
        {
            try
            {
                _carCommandService.RemoveModel(id);
                return Ok();
            }
            catch (ApiException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update/make/")]
        public IActionResult UpdateMake([FromBody] CarMake carMake)
        {
            try
            {
                _carCommandService.UpdateCarMake(carMake);
                return Ok();
            }
            catch (ApiException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update/model/")]
        public IActionResult UpdateModel([FromBody] CarModel carModel)
        {
            try
            {
                _carCommandService.UpdateCarModel(carModel);
                return Ok();
            }
            catch (ApiException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}