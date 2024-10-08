﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlicundeApi.Context;
using AlicundeApi.Models;
using System.Xml.Linq;
using Microsoft.Build.Framework;
using AlicundeApi.Interfaces;

namespace AlicundeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IBank _iBank;
        private readonly IData _iData;

        public BanksController(ApplicationDbContext context, IBank iBank, IData iData)
        {
            _context = context;
            _iBank = iBank;
            _iData = iData;
        }

        // GET: api/Banks/ConsumeApi
        [HttpGet]
        [Route("ConsumeApi")]
        public async Task<ActionResult<List<Banks>>> ConsumeApi()
        {
            var banks = await _iBank.GetAllBanks();
            await _iData.SaveBanksAsync(banks);
            return banks;
        }

        // GET: api/Banks/GetByPrimaryKey/5
        [HttpGet]
        [Route("GetByPrimaryKey/{id}")]
        public async Task<ActionResult<List<Banks>>> GetByPrimaryKey(int id)
        {
            Bank bank = await _iData.GetBankByPrimaryKey(id);
            if (bank == null)
            {
                return NoContent();
            }
            return Ok(bank);
        }

        // GET: api/Banks/5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bank>>> GetBank()
        {
            return await _context.Bank.ToListAsync();
        }

        // GET: api/Banks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bank>> GetBank(int id)
        {
            var bank = await _context.Bank.FindAsync(id);

            if (bank == null)
            {
                return NotFound();
            }

            return bank;
        }

        // PUT: api/Banks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBank(int id, Bank bank)
        {
            if (id != bank.id)
            {
                return BadRequest();
            }

            _context.Entry(bank).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BankExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Banks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bank>> PostBank(Bank bank)
        {
            _context.Bank.Add(bank);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBank", new { id = bank.id }, bank);
        }

        // DELETE: api/Banks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBank(int id)
        {
            var bank = await _context.Bank.FindAsync(id);
            if (bank == null)
            {
                return NotFound();
            }

            _context.Bank.Remove(bank);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BankExists(int id)
        {
            return _context.Bank.Any(e => e.id == id);
        }
    }
}
