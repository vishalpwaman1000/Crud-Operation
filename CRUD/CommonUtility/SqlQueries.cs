using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.CommonUtility
{
    public class SqlQueries
    {

        static IConfiguration _sqlQueryConfiguration = new ConfigurationBuilder()
            .AddXmlFile("SqlQueries.xml", true, true)
            .Build();

        public static string CreateInformationQuery { get { return _sqlQueryConfiguration["CreateInformationQuery"]; } }
        public static string ReadInformation { get { return _sqlQueryConfiguration["ReadInformation"]; } }
        public static string UpdateInformation { get { return _sqlQueryConfiguration["UpdateInformation"]; } }
        public static string SearchInformationById { get { return _sqlQueryConfiguration["SearchInformationById"]; } }//
        public static string DeleteInformation { get { return _sqlQueryConfiguration["DeleteInformation"]; } }
    }
}
