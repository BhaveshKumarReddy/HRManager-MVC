using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HRM_Project.Models
{
    public partial class EmployeesList
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Name")]
        [StringLength(29, MinimumLength = 2, ErrorMessage = "Name must be at least 2 and maximum of 29 characters ")]
        public string EmployeeName { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Date Of Joining")]
        [DataType(DataType.Date,ErrorMessage ="Date only")]
        public DateTime? EmployeeDoj { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Role")]
        public string EmployeeRole { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Salary (CTC)")]
        [Range(0,10000000,ErrorMessage ="Choose CTC below 1cr")]
        public decimal? EmployeeSalary { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Bonus Points")]
        [Range(0, 10000000, ErrorMessage = "Can't be negative")]
        public int? EmployeePoints { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Appraisal date")]
        [DataType(DataType.Date, ErrorMessage = "Date only")]
        public DateTime? EmployeeAppraisalDate { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Location")]
        public string LocationId { get; set; }

        public virtual LocationList Location { get; set; }
    }
}
