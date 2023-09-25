using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMAPP.Models
{
    public class Language
    {
        [Key, Column(TypeName = "char(2)", Order = 0)]        
        public string LanguageCode { get; set; }
        [Key, Column(TypeName = "nvarchar(100)",Order = 1)]
        public string Statmentkey { get; set; }
        [Column(TypeName = "nvarchar(450)")]
        public string StatementInlang {  get; set; }
    }
}
