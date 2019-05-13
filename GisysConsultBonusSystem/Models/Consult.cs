using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GisysConsultBonusSystem.Models
{
    public class Consult
    {
        [Key]
        public int ConsultID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EmploymentDate { get; set; }
    }

    public class Bonus
    {
        [Key]
        public int BonusID { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int ConsultID { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int ChargedHours { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int ChargedHoursWithBonus { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 0)")]
        public double BonusPot { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int NetResult { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int PeriodOfEmployment { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "datetime2")]
        public DateTime BonusDate { get; set; } 
    }
}
