using CRMAPP.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMAPP.ViewModel
{
    [Table("Call")]
    public class CallVM
    {
        [Key]
        [ValidateNever]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(10)")]
        public string CustomerNo {  get; set; }
        [ForeignKey(nameof(CustomerNo))]
        [ValidateNever]
        public Customer Customer { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateOnly CallDate { get; set; }  
        [Required]
        [Column(TypeName = "Time")]
        public TimeOnly CallTime { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Subject { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(MAX)")]
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastUpdate { get; set; } = DateTime.Now;
        [Column(TypeName = "smallint")]
        public short Status { get; set; } = 1;

        [Column(TypeName = "nvarchar(450)")]
        [ValidateNever]
        public string UserId { get; set; }


    }
}
