using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Top20Video.Models
{
    public class ForgotPasswordModel
    {
        [DisplayName("Email ID")]
        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }

        public TransactionMessage TransMessage { get; set; }
    }

    public class ChangePasswordModel
    {
        [Required]
        [MaxLength(20)]
        [DisplayName("Current Password")]
        public string CurrentPassword { get; set; }

        [Required]
        [MaxLength(20)]
        [DisplayName("New Password")]
        public string NewPassword { get; set; }

        [Required]
        [MaxLength(20)]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

       public TransactionMessage TransMessage { get; set; }

    }
}