using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Municipalities = TaxCalculator.Entities.Municipalities;

namespace TaxCalculator.Pl.Repositories
{
    public interface IUnitOfWork
    {
        Task<Municipalities> CalculateTax(Municipalities data);
        Task<bool> CheckIfExists(string municipalityName);
    }
}
