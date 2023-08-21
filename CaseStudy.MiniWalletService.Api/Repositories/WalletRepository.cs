using CaseStudy.Data;
using CaseStudy.Data.DbContexts;
using CaseStudy.MiniWalletService.Api.Models.Entities;

namespace CaseStudy.MiniWalletService.API.Repositories
{
    public class WalletRepository : Repository<Wallet>
    {
        public WalletRepository() : base(new CaseStudyDbContext())
        {
        }
    }
}
