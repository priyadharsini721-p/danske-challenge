using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.Dal.Models;
using TaxCalculator.Entities;
using TaxCalculator.Pl.Repositories;
using Municipalities = TaxCalculator.Entities.Municipalities;

namespace TaxCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxCalculatorController : ControllerBase
    {
        private readonly IUnitOfWork _repository;
        public TaxCalculatorController(IUnitOfWork repository)
        {
            _repository = repository;
        }

        // POST: api/<TaxCalculatorController>
        [HttpPost]
        [Route("gettax")]
        public async Task<Municipalities> GetTax(Municipalities municipalities)
        {
            return await _repository.CalculateTax(municipalities);
        }

        // Get: api/<TaxCalculatorController/validate>
        [HttpGet("validate/{name}")]
        public async Task<bool> Validate(string name)
        {
            return await _repository.CheckIfExists(name);
        }

        // Get: api/<TaxCalculatorController/getlookup>
        [HttpGet("getlookup")]
        public async Task<Lookup> GetLookup()
        {
            return await _repository.GetLookup();
        }

        // Get: api/<TaxCalculatorController/getdetails>
        [HttpGet("getdetails/{name}/{taxRuleId}")]
        public async Task<Municipalities> GetDetails(string name, string taxRuleId)
        {
            return await _repository.GetDetails(name, taxRuleId);
        }

        // POST: api/<TaxCalculatorController/AddDetails>
        [HttpPost]
        [Route("adddetails")]
        public async Task<bool> AddDetails(Municipalities municipalities)
        {
            return await _repository.AddDetails(municipalities);
        }

        // POST: api/<TaxCalculatorController/UpdateDetails>
        [HttpPost]
        [Route("updatedetails")]
        public async Task<bool> UpdateDetails(Municipalities municipalities)
        {
            return await _repository.UpdateDetails(municipalities);
        }
    }
}
