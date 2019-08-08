using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REST_Demo_CSharp;

namespace REST_Demo_CSharp
{
    public class DatasourceHelper
    {
        public static Datasource GenerateObject()
        {
            Datasource datasource = new Datasource
            {
                externalId = Constants.DATASOURCE_ID,
                description = Constants.DATASOURCE_DESCRIPTION
            };

            return datasource;
        }
    }
}
