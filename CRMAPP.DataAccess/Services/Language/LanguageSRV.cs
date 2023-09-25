using AutoMapper;
using CRMAPP.DataAccess.Contracts.Language;
using CRMAPP.DataAccess.Data;
using CRMAPP.DataAccess.Migrations;
using CRMAPP.Models;
using CRMAPP.Utility;
using CRMAPP.ViewModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMAPP.DataAccess.Services.Language
{

    public class LanguageSRV : ILanguage
    {
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;
        //private static readonly object lockObj = new object();
        //private static LanguageSRV instance = null;

        public LanguageSRV(ApplicationDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="languageCode"></param>
        /// <param name="StatementLanguageLey"></param>
        /// <returns></returns>
        public string TranslateSrv(string statementLanguagekey, string languageCode = "")
        {
            try
            {
                if (string.IsNullOrWhiteSpace(languageCode)) languageCode = StaticData.selectedLanguage;

                List<SqlParameter> parameters = new List<SqlParameter>();

                parameters.Add(new SqlParameter("@language", languageCode));
                parameters.Add(new SqlParameter("@Statmentkey", statementLanguagekey));
                LanguageVM languageVM = _db.languages.FromSqlRaw($"spGetStatement @language, @Statmentkey", parameters.ToArray()).ToList().Select(lang => _mapper.Map<LanguageVM>(lang)).FirstOrDefault();

                return languageVM != null ? languageVM.StatementInlang : statementLanguagekey;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "";
            }
        }

    }
}
