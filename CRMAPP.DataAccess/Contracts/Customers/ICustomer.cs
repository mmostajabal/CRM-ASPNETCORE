using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMAPP.Models;
using CRMAPP.ViewModel;

namespace CRMAPP.DataAccess.Contracts.Customers
{
    public interface ICustomer
    {
        Task<List<CustomerVM>> GetAll();
        Task<CustomerVM> GetByCustomerNo(string customerNo);
        Task<bool> Add(CustomerVM customerVM);
        Task<bool> Update(CustomerVM customerVM);        
    }
}
