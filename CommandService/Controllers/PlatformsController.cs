using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        #region Private Members



        #endregion

        #region Contructor

        public PlatformsController()
        {

        }

        #endregion

        #region Endpoints

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inbound POST # Command Service");
            return Ok("Inbound test OK from Platforms Controller");
        }

        #endregion
    }
}
