using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBUtility
{
    public interface IFillable
    {
        string TableName { get; set; }

        Dictionary<string, object> Extract();

        void Fill(SqlDataReader reader);
    }
}
