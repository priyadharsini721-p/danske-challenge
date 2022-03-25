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
using TaxCalculator.Entities;
using TaxCalculator.Pl.FormatException;
using Entity = TaxCalculator.Entities;

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

        public async Task<Entity.Municipalities> CalculateTax(Entity.Municipalities data)
        {
            Entity.Municipalities municipalities = null;
            try
            {
                DalProcessor _repo = new DalProcessor(_context);
                List<Entity.Municipalities> lstMunicipalities = await _repo.GetMunicipalityTaxDetails(data.MunicipalityName, data.Date);
                int taxRule = 0;
                double tax = TaxCalculationProcessor.CalculateTax(lstMunicipalities, out taxRule);
                if (tax > 0)
                {
                    municipalities = new Entity.Municipalities
                    {
                        Tax = tax,
                        TaxRule = taxRule
                    };
                }
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

        public async Task<Entity.Municipalities> GetDetails(string municipalityName, string taxRuleId)
        {
            try
            {
                DalProcessor _repo = new DalProcessor(_context);
                return await _repo.GetDetails(municipalityName, taxRuleId);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.GetExceptionFootprints());
            }
            return null;
        }

        public async Task<Lookup> GetLookup()
        {
            try
            {
                DalProcessor _repo = new DalProcessor(_context);
                return await _repo.GetLookup();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.GetExceptionFootprints());
            }
            return null;
        }

        public async Task<bool> UpdateDetails(Entity.Municipalities data)
        {
            try
            {
                DalProcessor _repo = new DalProcessor(_context);
                return await _repo.UpdateDetails(data);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.GetExceptionFootprints());
            }
            return false;
        }

        public async Task<bool> AddDetails(Entity.Municipalities data)
        {
            try
            {
                DalProcessor _repo = new DalProcessor(_context);
                return await _repo.AddDetails(data);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.GetExceptionFootprints());
            }
            return false;
        }
    }
}
