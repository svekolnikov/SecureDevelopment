using System.Runtime.CompilerServices;
using Lesson1.DAL.Interfaces.Repositories;
using Lesson1.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lesson1.Controllers
{
    [Route("api/ef")]
    [ApiController]
    public class NotesEfController : ControllerBase
    {
        private readonly IRepository<Note> _repository;
        private readonly ILogger<NotesEfController> _logger;

        public NotesEfController(IRepository<Note> repository, ILogger<NotesEfController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("notes")]
        public async Task<IEnumerable<Note>> GetAll()
        {
            try
            {
                var result = await _repository.GetAllAsync(CancellationToken.None);
                return result;
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }
        
        [HttpGet("notes/{id}")]
        public async Task<Note?> GetById(int id)
        {
            try
            {
                var result = await _repository.GetById(id);
                return result;
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }
        
        [HttpPost]
        public async Task AddAsync([FromBody] Note dto)
        {
            try
            {
                await _repository.AddAsync(dto);
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }
        
        [HttpPut("notes/{id}")]
        public async Task Update([FromBody] Note dto)
        {
            try
            {
                await _repository.UpdateAsync(dto);
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }
        
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            try
            {
                await _repository.DeleteByIdAsync(id);
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }

        private void LogError(Exception e, [CallerMemberName] string methodName = null!)
        {
            _logger.LogError(e, "Error at {0}", methodName);
        }
    }
}
