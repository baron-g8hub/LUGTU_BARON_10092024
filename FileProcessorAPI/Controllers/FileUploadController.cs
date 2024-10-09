using FileProcessorAPI.DataProcess;
using FileProcessorAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FileProcessorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {

        private readonly ILogger<FileUploadController> _logger;

        public FileUploadController(ILogger<FileUploadController> logger)
        {
            _logger = logger;
        }

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
            var list = new UploadFileHandler().GetSalesByFilter("BRANCH",branch);
            return list.ToArray();
        }

        // POST api/<FileUploadController>
        [HttpPost] 
        public IActionResult UploadCSV(IFormFile file)
        {
            try
            {
                _logger.LogInformation("File successfully uploaded. " + file.FileName);
                return Ok(new UploadFileHandler().UploadFile(file));
            }
            catch (Exception e)
            {
              return BadRequest(e.Message);
            }
        }
    }
}
