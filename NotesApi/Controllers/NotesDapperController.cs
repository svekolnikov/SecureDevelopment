using System.Runtime.CompilerServices;
using Lesson1.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using NotesApi.Services.Interfaces;

namespace NotesApi.Controllers
{
    [Route("api/dapper")]
    [ApiController]
    public class NotesDapperController : ControllerBase
    {
        private readonly INotesManager<Note> _notesManager;
        private readonly ILogger<NotesDapperController> _logger;

        public NotesDapperController(INotesManager<Note> notesManager, ILogger<NotesDapperController> logger)
        {
            _notesManager = notesManager;
            _logger = logger;
        }

        [HttpGet("notes")]
        public async Task<IEnumerable<Note>> GetAll()
        {
            try
            {
                var result = await _notesManager.NotesDapper.GetAllAsync(CancellationToken.None);
                return result;
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }

        [HttpGet("note/{id}")]
        public async Task<Note?> GetById(int id)
        {
            try
            {
                var result = await _notesManager.NotesDapper.GetById(id);
                return result;
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }

        [HttpPost("note")]
        public async Task AddAsync([FromBody] Note dto)
        {
            try
            {
                await _notesManager.NotesDapper.AddAsync(dto);
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }

        [HttpPut("note")]
        public async Task Update([FromBody] Note dto)
        {
            try
            {
                await _notesManager.NotesDapper.UpdateAsync(dto);
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }

        [HttpDelete("note/{id}")]
        public async Task Delete(int id)
        {
            try
            {
                await _notesManager.NotesDapper.DeleteByIdAsync(id);
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
