using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace apiestudo110319.Controllers
{
    [Route("api/images")]
    public class ImagesController : Controller
    {

        public async Task<ActionResult> GetImage(
           [FromQuery] string name,
           [FromServices]IHostingEnvironment env
            )
        {
            var filePath = Path.Combine(env.ContentRootPath, "Images", name);
            var fileExists = System.IO.File.Exists(filePath);
            if (fileExists == false)
            {
                return NotFound();
            }
            else




        }


    }
}