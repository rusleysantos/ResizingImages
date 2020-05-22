using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageMagick;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace ResizingImages.Controllers
{
    [Route("api")]
    public class ImagesController : Controller
    {
        public IHostEnvironment Env { get; set; }

        public ImagesController(IHostEnvironment env)
        {
            Env = env;
        }

        /// <summary>
        /// Retorna imagem em seu tamanho normal
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{action}")]
        public async Task<ActionResult> GetImage([FromQuery] string name)
        {
            var filePath = Path.Combine(Env.ContentRootPath, "img", name);
            var fileExists = System.IO.File.Exists(filePath);

            if (fileExists == false)
            {
                return NotFound();
            }

            var file = await System.IO.File.ReadAllBytesAsync(filePath);

            return File(file, "image/jpeg");
        }

        /// <summary>
        /// Faz o resize da imagem
        /// </summary>
        /// <param name="name">Arquivo</param>
        /// <param name="width">Largura</param>
        /// <param name="height">Altura</param>
        /// <param name="quality">Qualidade</param>
        /// <returns></returns>
        [HttpGet("{action}")]
        public async Task<ActionResult> ResizeImage([FromQuery] string name, int width, int height, int quality)
        {
            var filePath = Path.Combine(Env.ContentRootPath, "img", name);
            var fileExists = System.IO.File.Exists(filePath);

            if (fileExists == false)
            {
                return NotFound();
            }

            using (var memory = new MemoryStream())
            using (var image = new MagickImage(filePath))
            {
                image.Resize(width, height);
                image.Strip();
                image.Quality = quality;
                image.Write(memory);
                var file = memory.ToArray();

                return File(file, "image/jpeg");
            }
        }
    }
}