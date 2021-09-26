using Business.Abstract;
using Business.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FakeWebAPI.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {
        [Route("{*url}")]
        public void GeneralFunction(string url)
        {
           
        }
    }
}
