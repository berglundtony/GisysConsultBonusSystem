using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GisysConsultBonusSystem.Models
{
    public class GetBonus
    {
        public int BonusID { get; set; }
        public int ConsultID { get; set; }
        public string Fullname { get; set; }
        public int NetResult { get; set; }
        public double BonusPot { get; set; }
        public int PeriodOfEmployment { get; set; }
        public int ChargedHours { get; set; }
        public int ChargedHoursWithBonus { get; set; }
        public int TotalHoursWithBonus { get; set; }
        public decimal BonusInMoney { get; set; }
        public DateTime BonusDate { get; set; }
    }
}
