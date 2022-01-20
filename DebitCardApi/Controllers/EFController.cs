using System.Runtime.CompilerServices;
using DebitCardApi.DAL.Models;
using DebitCardApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DebitCardApi.Controllers
{
    [Route("api/ef")]
    [ApiController]
    public class EfController : ControllerBase
    {
        private readonly IDebitCardsManager<DebitCard> _debitCardsManager;
        private readonly ILogger<EfController> _logger;

        public EfController(IDebitCardsManager<DebitCard> debitCardsManager, ILogger<EfController> logger)
        {
            _debitCardsManager = debitCardsManager;
            _logger = logger;
        }

        [HttpGet("cards")]
        public async Task<IEnumerable<DebitCard>> GetAll()
        {
            try
            {
                var result = await _debitCardsManager.DebitCardsEf.GetAllAsync(CancellationToken.None);
                return result;
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }
        
        [HttpGet("card/{id}")]
        public async Task<DebitCard?> GetById(int id)
        {
            try
            {
                var result = await _debitCardsManager.DebitCardsEf.GetById(id);
                return result;
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }
        
        [HttpPost("card")]
        public async Task AddAsync([FromBody] DebitCard dto)
        {
            try
            {
                await _debitCardsManager.DebitCardsEf.AddAsync(dto);
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }
        
        [HttpPut("card")]
        public async Task Update([FromBody] DebitCard dto)
        {
            try
            {
                await _debitCardsManager.DebitCardsEf.UpdateAsync(dto);
            }
            catch (Exception e)
            {
                LogError(e);
                throw;
            }
        }
        
        [HttpDelete("card/{id}")]
        public async Task Delete(int id)
        {
            try
            {
                await _debitCardsManager.DebitCardsEf.DeleteByIdAsync(id);
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
