using CaseStudy.MiniWalletService.API.Repositories;
using CaseStudy.MiniWalletService.Api.Models.Entities;
using CaseStudy.MiniWalletService.Api.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace CaseStudy.MiniWalletService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly WalletRepository _repository;
        private readonly ILogger<WalletController> _logger;

        public WalletController(ILogger<WalletController> logger, WalletRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        [Route("api/Wallet/GetWallet/{id:int}")]
        [HttpGet]
        public Wallet GetWallet(int id)
        {
            try
            {
                var wallet = _repository.Get(x => x.Id == id).FirstOrDefault();
                if (wallet == null)
                    throw new NullReferenceException("Wallet was not found");

                return wallet;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);
                throw new NullReferenceException(ex.Message);
            }
        }

        [Route("api/Wallet/AddMoney/{id:int}/{currency}/{amount:int}")]
        [HttpPost]
        public ActionResult<bool> AddMoney(int id, CurrencyType currency, decimal amount)
        {
            try
            {
                var wallet = _repository.Get(x => x.Id == id).FirstOrDefault();
                if (wallet == null)
                    throw new NullReferenceException("Wallet was not found");

                if (wallet.Money[currency] != 0)
                    wallet.Money[currency] += amount;
                else
                    wallet.Money.Add(currency, amount);

                var updateResult = _repository.Update(wallet);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);
                throw new NullReferenceException(ex.Message);
            }
        }

        [Route("api/Wallet/WithdrawMoney/{id:int}/{currency}/{amount:int}")]
        [HttpPost]
        public ActionResult<bool> WithdrawMoney(int id, CurrencyType currency, decimal amount)
        {
            try
            {
                var wallet = _repository.Get(x => x.Id == id).FirstOrDefault();
                if (wallet == null)
                    throw new NullReferenceException("Wallet was not found");

                if (wallet.Money[currency] >= amount)
                {
                    wallet.Money[currency] -= amount;
                }
                else
                    throw new NullReferenceException("Insufficient balance");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, ex.Message);
                throw new NullReferenceException(ex.Message);
            }
        }

    }
}
