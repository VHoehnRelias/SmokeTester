using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeTestDBClassLibrary
{
    public class HandCoded
    {
        public string CnnString { get; set; }

        public List<Report> GetStraightReports()
        {
            var strSQL = "SELECT * FROM dbo.Reports";
            var dt = new DataTable();
            using (var cnn = new SqlConnection(CnnString))
            {
                using (var cmd = new SqlCommand(strSQL, cnn))
                {
                    using(var sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }
            foreach (DataRow dr in dt.Rows)
            {

            }
            return null;
        }
    }
}
