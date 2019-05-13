using GisysConsultBonusSystem.Models;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GisysConsultBonusSystem.Functions
{
    public static class BonusFunction
    {
        public static int GetEmpoymentTime(DateTime employmentDate)
        {
            var year = employmentDate.Year;
            var month = employmentDate.Month;
            var day = employmentDate.Day;

            DateTime currentDate = DateTime.Now;
            var yearToday = currentDate.Year;
            var monthToday = currentDate.Month;
            var dayToday = currentDate.Day;

            LocalDate start = new LocalDate(year, month, day);
            LocalDate now = new LocalDate(yearToday, monthToday, dayToday);
            Period period = Period.Between(start, now, PeriodUnits.Years | PeriodUnits.Months);

            return period.Years;
        }

        public static double GetLojalityFactor(int periodOfEmployment, int debitHours)
        {
            double lojalityFactor = 0.0;

            Dictionary<int, double> bonusYears = new Dictionary<int, double>();

            bonusYears.Add(0, 1);
            bonusYears.Add(1, 1.1);
            bonusYears.Add(2, 1.2);
            bonusYears.Add(3, 1.3);
            bonusYears.Add(4, 1.4);
            bonusYears.Add(5, 1.5);

            int years = periodOfEmployment;

            if (years == 0)
            {
                bonusYears.TryGetValue(0, out lojalityFactor);
                return lojalityFactor * debitHours;
            }
            else if (years == 1)
            {
                bonusYears.TryGetValue(1, out lojalityFactor);
                return lojalityFactor * debitHours;
            }
            else if (years == 2)
            {
                bonusYears.TryGetValue(2, out lojalityFactor);
                return lojalityFactor * debitHours;
            }
            else if (years == 3)
            {
                bonusYears.TryGetValue(3, out lojalityFactor);
                return lojalityFactor * debitHours;
            }
            else if (years == 4)
            {
                bonusYears.TryGetValue(4, out lojalityFactor);
                return lojalityFactor * debitHours;
            }
            else
            {
                bonusYears.TryGetValue(5, out lojalityFactor);
                return lojalityFactor * debitHours;
            }
        }
        public static GetBonus GetBonus(DateTime bonusDate, double chargedHoursWithBonus, double bonusPot, int consultID, ConsultContext _context)
        {
            GetBonus bonus = new GetBonus();
            try
            {
                var totalBonus = (from i in _context.Bonus where i.BonusDate.Month == bonusDate.Month orderby i.BonusDate descending select i).ToList();

                foreach (var i in totalBonus)
                {
                    bonus.TotalHoursWithBonus += i.ChargedHoursWithBonus;
                }
                decimal partOfBonusHours = Convert.ToDecimal(chargedHoursWithBonus) / bonus.TotalHoursWithBonus;
                decimal bonusinmoney = Convert.ToDecimal(bonusPot) * partOfBonusHours;
                bonus.BonusInMoney = Math.Round(bonusinmoney);
            }
            catch (Exception ex)
            {

            }
            return bonus;
        }
    }
}
