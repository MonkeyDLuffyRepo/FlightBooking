using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightBooking.BR.Interfaces;
using FlightBooking.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FlightBooking.MVVM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightInterface _flightService;
        private readonly ILogger<FlightController> _logger;
        public FlightController(IFlightInterface flightInterface, ILogger<FlightController> logger)
        {
            _flightService = flightInterface;
            _logger = logger;
        }
        /// <summary>
        /// Get All Planes
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-planes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllPlanes()
        {
            _logger.LogDebug("FlightController: GetAllPlanes() called");
            return Ok(_flightService.GetAllPlane());
        }
        /// <summary>
        /// Get All Airports
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-airport")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllAirports()
        {
            _logger.LogDebug("FlightController: GetAllAirports() called");
            return Ok(_flightService.GetAllAirport());
        }
        /// <summary>
        /// Get All Flights
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-flight")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllFlights()
        {
            _logger.LogDebug("FlightController: GetAllFlights() called");
            return Ok(_flightService.GetAllFlight());
        }
        /// <summary>
        /// Get Flight By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get-flight-by-id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetFlightById(int id)
        {
            _logger.LogDebug("FlightController: GetCompanyById() called");
            return Ok(_flightService.GetFlightById(id));
        }
        /// <summary>
        /// Create Flight
        /// </summary>
        /// <param name="companyModel"></param>
        /// <returns></returns>
        [HttpPost("create-flight")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateFlight([FromBody] Flight flightModel)
        {
            _logger.LogDebug("FlightController: CreateFlight() called");
            if (flightModel == null)
            {
                return BadRequest();
            }

            return Ok(_flightService.CreateFlight(flightModel));
        }
        /// <summary>
        /// Update Flight
        /// </summary>
        /// <param name="flightModel"></param>
        /// <returns></returns>
        [HttpPut("update-flight")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateFlight([FromBody] Flight flightModel)
        {
            _logger.LogDebug("FlightController: UpdateFlight() called");
            if (flightModel == null)
                return BadRequest();

            return Ok(_flightService.UpdateFlight(flightModel));
        }
        /// <summary>
        /// Delete Flight
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete-flight/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteFlight(int id)
        {
            _logger.LogDebug("FlightController: DeleteFlight() called");
            try
            {

                return Ok(_flightService.DeleteFlight(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw new Exception("An error occurred on the server.");
            }
        }
    }
}