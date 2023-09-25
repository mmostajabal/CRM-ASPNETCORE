using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMAPP.ViewModel
{
    public  class CustomerCallVM
    {
        public string CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurName { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }

        public DateTime CallDate { get; set; }
        public TimeSpan CallTime { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public short Status { get; set; }
        public string StatusDesc{ get; set; }

    }
}
