using CRMAPP.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CRMAPP.ViewModel
{
    public class CustomerVM
    {
        
        [Column(TypeName = "varchar(10)")]
        [ValidateNever]
        public string CustomerNo { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string CustomerName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string CustomerSurName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Address { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(10)")]
        public string PostCode { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Country { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateOnly DateOfBirth { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime LastUpdate { get; set; } = DateTime.Now;
        [Column(TypeName = "smallint")]
        public short Status { get; set; } = 1;
        [ValidateNever]
        public string UserId { get; set; }

    }


}