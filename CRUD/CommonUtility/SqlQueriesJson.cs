using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.CommonUtility
{
    public class SqlQueriesJson
    {
        static IConfiguration _sqlQueryConfiguration = new ConfigurationBuilder()
            .AddJsonFile("SqlQueries.json", true, true)
            .Build();

        public static string CreateInformationQuery { get { return _sqlQueryConfiguration["Queries:CreateInformation"]; } }
        public static string ReadInformation { get { return _sqlQueryConfiguration["Queries:ReadInformation"]; } }
        public static string UpdateInformation { get { return _sqlQueryConfiguration["Queries:UpdateInformation"]; } }
        public static string SearchInformationById { get { return _sqlQueryConfiguration["Queries:SearchInformationById"]; } }
        public static string DeleteInformation { get { return _sqlQueryConfiguration["Queries:DeleteInformation"]; } }
    }
}
