using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebApplication.DTOs;

namespace WebApplication.Controllers
{
    /// <summary>
    ///     Simple Controller which uses as an example for a reading REST API
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        /// <summary>
        ///     Example GET API which returns an entity based on a date
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns>
        ///     <see cref="DataResponseDTO" />
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(DataResponseDTO), (int) HttpStatusCode.OK)]
        public ActionResult<DataResponseDTO> GetData([FromQuery] DataRequestDTO requestDTO)
        {
            return Ok(new DataResponseDTO
            {
                //  can't be null as marked as [Required] in the DTO
                Date = requestDTO.Date!.Value,
                DataSet = new List<string>
                {
                    "chicken",
                    "dog"
                }
            });
        }
    }
}
