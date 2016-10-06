using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cti.Seller.Data
{
    public class SQL
    {
        public List<SqlParameter> SqlParams { get; set; }
        public string SqlQuery { get; set; }
    }
}
    