using FileProcessorAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FileProcessorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        // GET: api/<FileUploadController>
        [HttpGet]
        public IEnumerable<SALE> GetSales(string fileName)
        {
            var sales = new UploadFileHandler().ReadSalesCSV(fileName);
            return sales.ToArray();
        }

        // GET api/<FileUploadController>
        [HttpGet("{branch}")]
        public IEnumerable<SALE> GetSalesByBranch(string branch)
        {
            var list = new UploadFileHandler().GetSalesByBranch(branch);
            return list.ToArray();
        }




        // POST api/<FileUploadController>
        [HttpPost] 
        public IActionResult UploadCSV(IFormFile file)
        {
            return Ok(new UploadFileHandler().UploadFile(file));
        }

        //// PUT api/<FileUploadController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<FileUploadController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
