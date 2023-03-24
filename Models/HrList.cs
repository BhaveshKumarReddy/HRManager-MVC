using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace HRM_Project.Models
{
    public partial class HrList
    {
        public int HrId { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Name")]
        [StringLength(29,MinimumLength =2,ErrorMessage = "Name must be at least 2 and maximum of 29 characters ")]
        public string HrName { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Email")]
        [StringLength(29, MinimumLength = 5, ErrorMessage = "Email must be at least 5 and maximum of 29 characters ")]
        [Remote("IsEmailAvailable", "HrList", HttpMethod = "POST", ErrorMessage = "Email already in use")]
        public string HrEmail { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be at least 6 and maximum of 29 characters ")]
        public string HrPassword { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Location")]
        public string LocationId { get; set; }

        public virtual LocationList Location { get; set; }

    }
}
