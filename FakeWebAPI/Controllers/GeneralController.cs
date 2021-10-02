using System;
using System.IO;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace FakeWebAPI.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {
        private readonly IUrlDataService _urlDataService;

        public GeneralController(IUrlDataService urlDataService)
        {
            _urlDataService = urlDataService;
        }
       
        [Route("{*url}")]
        public void GeneralFunction(string url)
        {
            var token = Request.Headers["fakeApiToken"];
            
            
            var result = _urlDataService.UrlResult(url,token );
            // if (result.Success)
            // {
            //     return Ok(result.Data);
            // }
            //
            // return BadRequest(result.Message);
        }
       
    }
}
