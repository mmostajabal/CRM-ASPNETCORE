using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMAPP.ViewModel
{
    public class LanguageVM
    {
        public string LanguageCode { get; set; }
        public string Statmentkey { get; set; }       
        public string StatementInlang {  get; set; }
    }
}
