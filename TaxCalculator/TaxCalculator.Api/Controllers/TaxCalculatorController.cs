using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.Dal.Models;
using TaxCalculator.Pl.Repositories;
using Municipalities = TaxCalculator.Entities.Municipalities;

namespace TaxCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxCalculatorController : ControllerBase
    {
        private readonly TaxCalculatorDBContext _context;
        private readonly IUnitOfWork _repository;

        public TaxCalculatorController(TaxCalculatorDBContext context, IUnitOfWork repository)
        {
            _context = context;
            _repository = repository;
        }

        // POST: api/<TaxCalculatorController>
        [HttpPost]
        [Route("gettax")]
        public async Task<Municipalities> GetTax(Municipalities municipalities)
        {
            return await _repository.CalculateTax(municipalities);
        }

        // Get: api/<TaxCalculatorController>
        [HttpGet("validate/{name}")]
        public async Task<bool> Validate(string name)
        {
            return await _repository.CheckIfExists(name);
        }
    }
}
