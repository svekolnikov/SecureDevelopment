using Microsoft.AspNetCore.Mvc;

namespace Lesson1.Controllers
{
    [Route("api/ef")]
    [ApiController]
    public class NotesEFController : ControllerBase
    {
        [HttpGet("notes")]
        public IEnumerable<string> GetAll()
        {
            return new string[] { "value1", "value2" };
        }
        
        [HttpGet("notes/{id}")]
        public string GetById(int id)
        {
            return "value";
        }
        
        [HttpPost]
        public void AddAsync([FromBody] string value)
        {
        }
        
        [HttpPut("notes/{id}")]
        public void Update(int id, [FromBody] string value)
        {
        }
        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
