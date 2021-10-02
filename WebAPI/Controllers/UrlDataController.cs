using Business.Abstract;
using Business.Concrete;
using Core.Extensions;
using Core.Utilities.IoC;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlDataController : Controller
    {
        private readonly IUrlDataService _urlDataService;
        private readonly IDataService _dataService;

        public UrlDataController(IUrlDataService urlDataService, IDataService dataService)
        {
            _urlDataService = urlDataService;
            _dataService = dataService;
        }

        [HttpPost("add")]
        public IActionResult Add(UrlDataAddDto urlDataAddDto)
        {
            var u = User.Claims("id");
            var data = _dataService.ReadJsonFromString(urlDataAddDto.Json).Data;
            var urlData = new UrlData() { Url = urlDataAddDto.Url, DataBytes = _dataService.DataToBinary(data).Data,
                Token = User.Claims("token")[0],Content_Type = urlDataAddDto.Content_Type,Method = urlDataAddDto.Method};
            _urlDataService.AddUrl(urlData);
            return Ok(urlData);
        }
    }
}