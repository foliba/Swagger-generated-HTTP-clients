using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    /// <summary>
    ///     Just one controller to show how errors are handled when using the generated HTTP client
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class Broken : Controller
    {
        /// <summary>
        ///     Always returns a Problem ActionResult which results in a 500
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ProblemDetails), (int) HttpStatusCode.InternalServerError)]
        public ActionResult Get()
        {
            return Problem();
        }
    }
}