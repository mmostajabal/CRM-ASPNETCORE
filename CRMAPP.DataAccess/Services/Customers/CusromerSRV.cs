using AutoMapper;
using CRMAPP.DataAccess.Contracts.Customers;
using CRMAPP.DataAccess.Data;
using CRMAPP.DataAccess.Migrations;
using CRMAPP.Models;
using CRMAPP.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMAPP.DataAccess.Services.Customers
{
    public class CusromerSRV : ICustomer
    {
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;
        public CusromerSRV(ApplicationDBContext db, IMapper mapper)
        {
            _db= db;
            _mapper= mapper;
        }
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="customerVM"></param>
        /// <returns></returns>
        public async Task<bool> Add(CustomerVM customerVM)
        {
            
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CustomerName", customerVM.CustomerName));
            parameters.Add(new SqlParameter("@CustomerSurName", customerVM.CustomerSurName));
            parameters.Add(new SqlParameter("@Address", customerVM.Address));
            parameters.Add(new SqlParameter("@PostCode", customerVM.PostCode));
            parameters.Add(new SqlParameter("@Country", customerVM.Country));
            parameters.Add(new SqlParameter("@DateOfBirth", customerVM.DateOfBirth.ToDateTime(TimeOnly.MinValue)));
            parameters.Add(new SqlParameter("@Status", customerVM.Status));
            parameters.Add(new SqlParameter("@UserId", customerVM.UserId));

            _db.Database.ExecuteSqlRaw($"spInsert2Customer @CustomerName, @CustomerSurName, @Address, @PostCode, @Country, @DateOfBirth, @Status, @UserId", parameters.ToArray());
            return await Task.FromResult(true);
        }
        /// <summary>
        /// GetByCustomerNo
        /// </summary>
        /// <param name="customerNo"></param>
        /// <returns></returns>
        public async Task<CustomerVM?> GetByCustomerNo(string customerNo)
        {
            return (await _db.Customers.FromSqlRaw<Customer>($"spCustomerGetByCustomerNo {customerNo}").ToListAsync()).Select(customer => _mapper.Map<CustomerVM>(customer)).FirstOrDefault();            
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="customerVM"></param>
        /// <returns></returns>
        public async Task<bool> Update(CustomerVM customerVM)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@CustomerNo", customerVM.CustomerNo));
            parameters.Add(new SqlParameter("@CustomerName", customerVM.CustomerName));
            parameters.Add(new SqlParameter("@CustomerSurName", customerVM.CustomerSurName));
            parameters.Add(new SqlParameter("@Address", customerVM.Address));
            parameters.Add(new SqlParameter("@PostCode", customerVM.PostCode));
            parameters.Add(new SqlParameter("@Country", customerVM.Country));
            parameters.Add(new SqlParameter("@DateOfBirth", customerVM.DateOfBirth.ToDateTime(TimeOnly.MinValue)));
            parameters.Add(new SqlParameter("@Status", customerVM.Status));
            parameters.Add(new SqlParameter("@UserId", customerVM.UserId));

            _db.Database.ExecuteSqlRaw($"UpdateCustomer @CustomerNo, @CustomerName, @CustomerSurName, @Address, @PostCode, @Country, @DateOfBirth, @Status, @UserId", parameters.ToArray());
            return await Task.FromResult(true);
        }
        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        public async Task<List<CustomerVM>> GetAll()
        {

            return (await _db.Customers.FromSqlRaw<Customer>($"spCustomerGetAll").ToListAsync()).Select(customer => _mapper.Map<CustomerVM>(customer)).ToList();
        }


    }
}
