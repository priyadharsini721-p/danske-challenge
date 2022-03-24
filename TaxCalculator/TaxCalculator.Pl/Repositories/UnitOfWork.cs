using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Bl.Calculation;
using TaxCalculator.Dal.Models;
using TaxCalculator.Dal.Repositories;
using TaxCalculator.Pl.FormatException;
using Municipalities = TaxCalculator.Entities.Municipalities;

namespace TaxCalculator.Pl.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaxCalculatorDBContext _context;
        private readonly ILogger<TaxCalculatorDBContext> _logger;
        public UnitOfWork(TaxCalculatorDBContext context, ILogger<TaxCalculatorDBContext> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Municipalities> CalculateTax(Municipalities data)
        {
            Municipalities municipalities = null;
            try
            {
                DalProcessor _repo = new DalProcessor(_context);
                List<Municipalities> lstMunicipalities = await _repo.GetMunicipalityTaxDetails(data.MunicipalityId, data.Date);
                int taxRule = 0;
                double tax = TaxCalculationProcessor.CalculateTax(lstMunicipalities, out taxRule);
                municipalities = new Municipalities
                {
                    Tax = tax,
                    TaxRule = taxRule
                };
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.GetExceptionFootprints());
            }
            return municipalities;
        }

        public async Task<bool> CheckIfExists(string municipalityName)
        {
            try
            {
                DalProcessor _repo = new DalProcessor(_context);
                return await _repo.CheckIfExists(municipalityName);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.GetExceptionFootprints());
            }
            return false;
        }
    }
}
