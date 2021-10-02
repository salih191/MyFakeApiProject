using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Utilities.Business;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSourceController:Controller
    {
        private readonly string _path = Helper.TryGetSolutionDirectoryInfo().FullName+"/Data/json/";
        
        
        [HttpGet("getFields")]
        public IActionResult GetFields()
        {
            var files = Directory.GetFiles(_path).Where(f=>f.EndsWith("json"));
            var fields = files.Select(file => new FileInfo(file)).Select(fileInfo => fileInfo.Name.Replace(".json", ""));
            
            return Ok(fields);
        }
    }
}