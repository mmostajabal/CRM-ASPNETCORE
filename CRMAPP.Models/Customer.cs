using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMAPP.Models
{
    [Index(nameof(CustomerNo), IsUnique = true)]
    public class Customer
    {
        [Key]
        [Column(TypeName = "varchar(10)")]
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
        public DateTime LastUpdate{ get; set; } = DateTime.Now;
        [Column(TypeName = "smallint")]
        public short Status { get; set; } = 1;

        
        public string UserId { get; set; }

    }
}
