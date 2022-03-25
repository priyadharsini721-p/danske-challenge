using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entity = TaxCalculator.Entities;

namespace TaxCalculator.Pl.Repositories
{
    public interface IUnitOfWork
    {
        Task<Entity.Municipalities> CalculateTax(Entity.Municipalities data);
        Task<bool> CheckIfExists(string municipalityName);
        Task<Entity.Municipalities> GetDetails(string municipalityName, string taxRuleId);
        Task<Entity.Lookup> GetLookup();
        Task<bool> AddDetails(Entity.Municipalities data);
        Task<bool> UpdateDetails(Entity.Municipalities data);
    }
}
