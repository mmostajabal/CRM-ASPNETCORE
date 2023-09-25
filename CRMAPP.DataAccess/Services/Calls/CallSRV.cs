using AutoMapper;
using CRMAPP.DataAccess.Contracts.Calls;
using CRMAPP.DataAccess.Data;
using CRMAPP.DataAccess.Migrations;
using CRMAPP.Models;
using CRMAPP.ViewModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMAPP.DataAccess.Services.Calls
{
    public  class CallSRV : ICalls
    {
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;

        public CallSRV(ApplicationDBContext db, IMapper mapper) {
            _db = db;
            _mapper = mapper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="callVM"></param>
        /// <returns></returns>
        public async Task<bool> Add(CallVM callVM)
        {

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CustomerNo", callVM.CustomerNo));
            parameters.Add(new SqlParameter("@CallDate", callVM.CallDate.ToDateTime(TimeOnly.MinValue)));
            parameters.Add(new SqlParameter("@CallTime", callVM.CallTime.ToTimeSpan()));
            parameters.Add(new SqlParameter("@Subject", callVM.Subject));
            parameters.Add(new SqlParameter("@Description", callVM.Description));            
            parameters.Add(new SqlParameter("@Status", callVM.Status));
            parameters.Add(new SqlParameter("@UserId", callVM.UserId));

            _db.Database.ExecuteSqlRaw($"spInsert2Call @CustomerNo, @CallDate, @CallTime, @Subject, @Description, @Status, @UserId", parameters.ToArray());
            return await Task.FromResult(true);
            
        }

        public Task<List<CallVM>> GetAll()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerNo"></param>
        /// <returns></returns>
        public async Task<List<CallVM>> GetByCustomerNo(string customerNo, int status)
        {
                       
            return (await _db.Calls.FromSqlRaw<Call>($"spGetCalls4Customer {customerNo}, {status}").ToListAsync()).Select(call => _mapper.Map<CallVM>(call)).ToList();
        }

        public async Task<CallVM?> GetById(int id)
        {
            return (await _db.Calls.FromSqlRaw<Call>($"spGetCallById {id}").ToListAsync()).Select(call => _mapper.Map<CallVM>(call)).FirstOrDefault();
        }

        public async Task<List<CustomerCallVM>> GetCustomerCalls()
        {

            return (await _db.customerCalls.FromSqlRaw<CustomerCall>($"spCustomerCall").ToListAsync()).Select(item => _mapper.Map<CustomerCallVM>(item)).ToList();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="callVM"></param>
        /// <returns></returns>
        public async Task<bool> Update(CallVM callVM)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            
            parameters.Add(new SqlParameter("@Id", callVM.Id));
            parameters.Add(new SqlParameter("@CallDate", callVM.CallDate.ToDateTime(TimeOnly.MinValue)));
            parameters.Add(new SqlParameter("@CallTime", callVM.CallTime.ToTimeSpan()));
            parameters.Add(new SqlParameter("@Subject", callVM.Subject));
            parameters.Add(new SqlParameter("@Description", callVM.Description));
            parameters.Add(new SqlParameter("@Status", callVM.Status));
            parameters.Add(new SqlParameter("@UserId", callVM.UserId));

            _db.Database.ExecuteSqlRaw($"spUpdateCall @Id, @CallDate, @CallTime, @Subject, @Description, @Status, @UserId", parameters.ToArray());
            return await Task.FromResult(true);

        }
    }
}
