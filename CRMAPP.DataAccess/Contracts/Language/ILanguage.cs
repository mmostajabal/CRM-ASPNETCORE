using CRMAPP.Models;
using CRMAPP.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMAPP.DataAccess.Contracts.Language
{
    public interface ILanguage
    {
        string TranslateSrv(string StatementLanguagekey, string languageCode = "");
    }
}
