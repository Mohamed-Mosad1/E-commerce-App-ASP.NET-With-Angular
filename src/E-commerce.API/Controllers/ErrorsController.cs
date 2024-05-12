using E_commerce.API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers
{
    [Route("errors/{statusCode}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public IActionResult Error(int statusCode)
        {
            return new ObjectResult(new BaseCommonResponse(statusCode));
        }

    }
}
