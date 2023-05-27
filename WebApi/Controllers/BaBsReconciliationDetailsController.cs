using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaBsReconciliationDetailsController : ControllerBase
    {
        private readonly IBaBsReconciliationDetailService _baBsReconciliationDetailService;

        public BaBsReconciliationDetailsController(IBaBsReconciliationDetailService baBsReconciliationDetailService)
        {
            _baBsReconciliationDetailService = baBsReconciliationDetailService;
        }

        [HttpPost("addFromExcel")]
        public IActionResult AddFromExcel(IFormFile file, int baBsReconciliationId)
        {
            if (file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + ".xlsx"; //dosya adı belirledik
                var filePath = $"{Directory.GetCurrentDirectory()}/Content/{fileName}"; //dosya yolunu aldık
                using (FileStream stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                    stream.Flush();
                }

                var result = _baBsReconciliationDetailService.AddToExcel(filePath, baBsReconciliationId);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result.Message);
            }

            return BadRequest("Dosya seçimi yapmadınız");
        }

        [HttpPost("add")]
        public IActionResult Add(BaBsReconciliationDetail baBsReconciliationDetail)
        {
            var result = _baBsReconciliationDetailService.Add(baBsReconciliationDetail);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update(BaBsReconciliationDetail baBsReconciliationDetail)
        {
            var result = _baBsReconciliationDetailService.Update(baBsReconciliationDetail);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(BaBsReconciliationDetail baBsReconciliationDetail)
        {
            var result = _baBsReconciliationDetailService.Delete(baBsReconciliationDetail);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _baBsReconciliationDetailService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("GetList")]
        public IActionResult GetList(int baBsReconciliationId)
        {
            var result = _baBsReconciliationDetailService.GetList(baBsReconciliationId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
    }
}
