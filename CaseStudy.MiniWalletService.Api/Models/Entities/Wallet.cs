using CaseStudy.MiniWalletService.Api.Models.Enums;
using System.Collections.Generic;

namespace CaseStudy.MiniWalletService.Api.Models.Entities
{
    public class Wallet
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public Dictionary<CurrencyType, decimal> Money { get; set; }
    }
}
