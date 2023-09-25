using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMAPP.Utility
{
    public static class StaticData
    {
        public const string  ROLE_ADMIN= "ADMIN";
        public const string ROLE_EMPLOYEE = "EMPLOYEE ";
        public static string selectedLanguage { get; set; } = "en";

    }
}
