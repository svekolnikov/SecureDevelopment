using System.Runtime.CompilerServices;
using Lesson1.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using NotesApi.DAL.Interfaces.Repositories;
using NotesApi.Services.Interfaces;

namespace Lesson1.Controllers
{
    [Route("api/ef")]
    [ApiController]
    public class NotesEfController : ControllerBase
    {
        private readonly INotesManager<Note> _notesManager;
        private readonly ILogger<NotesEfController> _logger;

        public NotesEfController(INotesManager<Note> notesManager, ILogger<NotesEfController> logger)
        {
            _notesManager = notesManager;
            _logger = logger;
        }

        [HttpGet("notes")]
        public async Task<IEnumerable<Note>> GetAll()
        {
            try
            {
                var result = await _notesManager.NotesEf.GetAllAsync(CancellationToken.None);
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
                var result = await _notesManager.NotesEf.GetById(id);
                return result;
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }
        
        [HttpPost("notes/{id}")]
        public async Task AddAsync([FromBody] Note dto)
        {
            try
            {
                await _notesManager.NotesEf.AddAsync(dto);
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
                await _notesManager.NotesEf.UpdateAsync(dto);
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }
        
        [HttpDelete("notes/{id}")]
        public async Task Delete(int id)
        {
            try
            {
                await _notesManager.NotesEf.DeleteByIdAsync(id);
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
