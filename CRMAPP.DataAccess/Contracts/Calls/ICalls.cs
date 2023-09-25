using CRMAPP.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMAPP.DataAccess.Contracts.Calls
{
    public  interface ICalls
    {
        Task<List<CallVM>> GetAll();
        Task<List<CallVM>> GetByCustomerNo(string customerNo, int status);
        Task<CallVM?> GetById(int id);
        Task<bool> Add(CallVM callVM);
        Task<bool> Update(CallVM callVM);

        Task<List<CustomerCallVM>> GetCustomerCalls();
    }
}
