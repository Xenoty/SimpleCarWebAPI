using AutoMapper;
using CarWebAPI.Helpers;
using CarWebAPI.Models;
using CarWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CarWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CarQueryController : ControllerBase
    {
        private ICarQueryService _carQueryService;
        private IMapper _mapper;

        public CarQueryController(ICarQueryService carQueryService,
                                  IMapper mapper)
        {
            _carQueryService = carQueryService;
            _mapper = mapper;

        }

        /// <summary>
        /// Gets all Car Makes
        /// </summary>
        /// <returns>List of Car Makes</returns>
        [HttpGet("GetAllMakes")]
        public IActionResult GetAllMakes()
        {
            try
            {
                List<CarMake> carMakesList = _carQueryService.GetAllMakes();
                var model = _mapper.Map<IList<CarMake>>(carMakesList);
                return Ok(model);
            }
            catch (ApiException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Gets all Car Models
        /// </summary>
        /// <returns>List of Car Models</returns>
        [HttpGet("GetAllModels")]
        public IActionResult GetAllModels()
        {
            try
            {
                List<CarModel> carModelsList = _carQueryService.GetAllModels();
                var model = _mapper.Map<IList<CarModel>>(carModelsList);
                return Ok(model);
            }
            catch (ApiException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Gets all Car Make by ID
        /// </summary>
        [HttpGet("make/id/")]
        public IActionResult GetMakeById(int makeId)
        {
            try
            {
                CarMake carMake = _carQueryService.GetMakeById(makeId);
                return Ok(carMake);
            }
            catch (ApiException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Gets all Car Model by ID
        /// </summary>
        [HttpGet("model/id/")]
        public IActionResult GetModelById(int modelId)
        {
            try
            {
                CarModel carModel = _carQueryService.GetModelById(modelId);
                return Ok(carModel);
            }
            catch (ApiException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
