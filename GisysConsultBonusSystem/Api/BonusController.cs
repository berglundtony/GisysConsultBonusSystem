using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using NodaTime;
using GisysConsultBonusSystem.Models;
using GisysConsultBonusSystem.Functions;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GisysConsultBonusSystem.Api
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class BonusController : ControllerBase
    {
        private readonly ConsultContext _context;

        public BonusController(ConsultContext consultContext)
        {
            _context = consultContext;
        }
        // GET: api/Consults
        [HttpGet]
        [Route("~/api/Bonus/")]
        [EnableCors("DevelopmentCorsPolicy")]
        public async Task<ActionResult<IEnumerable<GetBonus>>> GetBonus()
        {
            List<GetBonus> _bonuses = new List<GetBonus>();
            List<Bonus> _bonuslist = await _context.Bonus.ToListAsync();
            Consult consult = new Consult();
            string message = "";

            foreach (var i in _bonuslist)
            {
                try
                {
                    consult = _context.Consults.Find(i.ConsultID);
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    return NotFound();
                }
                GetBonus getbonus = Functions.BonusFunction.GetBonus(i.BonusDate, i.ChargedHoursWithBonus, i.BonusPot, i.ConsultID, _context);
                if (consult != null)
                {
                    _bonuses.Add(new GetBonus
                    {
                        BonusID = i.BonusID,
                        ConsultID = i.ConsultID,
                        Fullname = consult.FirstName + " " + consult.LastName,
                        NetResult = i.NetResult,
                        BonusPot = i.BonusPot,
                        ChargedHoursWithBonus = i.ChargedHoursWithBonus,
                        ChargedHours = i.ChargedHours,
                        PeriodOfEmployment = i.PeriodOfEmployment,
                        TotalHoursWithBonus = getbonus.TotalHoursWithBonus,
                        BonusInMoney = getbonus.BonusInMoney,
                        BonusDate = i.BonusDate
                    });
                }
                else
                {
                    _bonuses.Add(new GetBonus
                    {
                        BonusID = i.BonusID,
                        ConsultID = i.ConsultID,
                        Fullname = "Denna konsult är inte längre registrerad",
                        NetResult = i.NetResult,
                        BonusPot = i.BonusPot,
                        ChargedHoursWithBonus = i.ChargedHoursWithBonus,
                        ChargedHours = i.ChargedHours,
                        PeriodOfEmployment = i.PeriodOfEmployment,
                        TotalHoursWithBonus = getbonus.TotalHoursWithBonus,
                        BonusInMoney = getbonus.BonusInMoney,
                        BonusDate = i.BonusDate
                    });
                }
            }
            return _bonuses;
        }


        // GET: api/Bonus/
        [HttpGet("{id}")]
        [EnableCors("DevelopmentCorsPolicy")]
        public async Task<ActionResult<GetBonus>> GetBonus(int id, Bonus bonusmodel)
        {
            GetBonus bonusname = new GetBonus();
            var consult = await _context.Consults.FindAsync(bonusmodel.ConsultID);
            if (consult == null)
            {
                return NotFound();
            }
            var bonus = await _context.Bonus.FindAsync(id);
            if (bonus == null)
            {
                return NotFound();
            }

            bonusname.BonusID = bonus.BonusID;
            bonusname.ConsultID = bonusmodel.ConsultID;
            bonusname.Fullname = consult.FirstName + " " + consult.LastName;
            bonusname.NetResult = bonusmodel.NetResult;
            bonusname.BonusPot = bonusmodel.NetResult * 0.05;
            bonusname.ChargedHours = bonusmodel.ChargedHours;
            bonusname.ChargedHoursWithBonus = bonus.ChargedHoursWithBonus;
            bonusname.PeriodOfEmployment = bonus.PeriodOfEmployment;
            GetBonus getbonus = Functions.BonusFunction.GetBonus(bonus.BonusDate, bonus.ChargedHoursWithBonus, bonus.BonusPot, bonusmodel.ConsultID, _context);
            bonusname.TotalHoursWithBonus = getbonus.TotalHoursWithBonus;
            bonusname.BonusInMoney = getbonus.BonusInMoney;
            bonusname.BonusDate = bonus.BonusDate;

            return bonusname;
        }

        // POST: api/Bonus
        [HttpPost]
        [EnableCors("DevelopmentCorsPolicy")]
        public async Task<ActionResult<Bonus>> PostBonus([FromBody]Bonus postBonus)
        {
            List<Bonus> _bonusList = new List<Bonus>();
            var consult = await _context.Consults.FindAsync(postBonus.ConsultID);
            if (consult == null)
            {
                return NotFound();
            }

            DateTime employmentDate = consult.EmploymentDate;
            int periodOfEmployment = BonusFunction.GetEmpoymentTime(employmentDate);
            double chargedHoursWithBonus = BonusFunction.GetLojalityFactor(periodOfEmployment, postBonus.ChargedHours);
            Bonus bonus = new Bonus();
            bonus.ConsultID = postBonus.ConsultID;
            bonus.NetResult = postBonus.NetResult;
            bonus.BonusPot = postBonus.NetResult * 0.05;
            bonus.PeriodOfEmployment = periodOfEmployment;
            bonus.ChargedHours = postBonus.ChargedHours;
            bonus.ChargedHoursWithBonus = Convert.ToInt32(chargedHoursWithBonus);
            bonus.BonusDate = DateTime.Now;

            if (_context.Bonus.Any(x => x.ConsultID == bonus.ConsultID && x.BonusDate.Month == bonus.BonusDate.Month))
            {
                return NotFound("Den här konsulten är redan tillagd");
            }
            else
            {
                _context.Bonus.Add(bonus);
                await _context.SaveChangesAsync();
                var result = (from i in _context.Bonus where i.ConsultID == bonus.ConsultID select i.BonusID).Last();
                bonus.BonusID = result;
            }

            return CreatedAtAction("GetBonus", new { id = bonus.BonusID }, bonus);
        }

 

        // Delete: api/Bonus/5
        [HttpDelete("{id}")]
        [EnableCors("DevelopmentCorsPolicy")]
        public async Task<IActionResult> DeleteBonus(int id)
        {
            var bonus = await _context.Bonus.FindAsync(id);
            _context.Bonus.Remove(bonus);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;

            }

            return NoContent();
        }

        private bool BonusDetailExists(int id)
        {
            return _context.Bonus.Any(e => e.BonusID == id);
        }

    }
}