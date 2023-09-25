using AutoMapper;
using CRMAPP.Models;
using CRMAPP.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMAPP.DataAccess.mapping
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Customer, CustomerVM>().ReverseMap();
            CreateMap<CustomerVM, Customer>().ReverseMap();

            CreateMap<Language, LanguageVM>().ReverseMap();
            CreateMap<LanguageVM, Language>().ReverseMap();

            CreateMap<Call, CallVM>().ReverseMap();
            CreateMap<CallVM, Call>().ReverseMap();

            CreateMap<CustomerCall, CustomerCallVM>().ReverseMap();
            CreateMap<CustomerCallVM, CustomerCall>().ReverseMap();

        }
    }
}
