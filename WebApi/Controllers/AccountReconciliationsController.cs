﻿using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountReconciliationsController : ControllerBase
    {
        private readonly IAccountReconciliationService _accountReconciliationService;

        public AccountReconciliationsController(IAccountReconciliationService accountReconciliationService)
        {
            _accountReconciliationService = accountReconciliationService;
        }

        [HttpPost("addFromExcel")]
        public IActionResult AddFromExcel(IFormFile file, int companyId)
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

                var result = _accountReconciliationService.AddToExcel(filePath, companyId);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result.Message);
            }

            return BadRequest("Dosya seçimi yapmadınız");
        }

        [HttpPost("add")]
        public IActionResult Add(AccountReconciliation accountReconciliation)
        {
            var result = _accountReconciliationService.Add(accountReconciliation);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update(AccountReconciliation accountReconciliation)
        {
            var result = _accountReconciliationService.Update(accountReconciliation);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(AccountReconciliation accountReconciliation)
        {
            var result = _accountReconciliationService.Delete(accountReconciliation);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _accountReconciliationService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("GetList")]
        public IActionResult GetList(int companyId)
        {
            var result = _accountReconciliationService.GetList(companyId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
    }
}
