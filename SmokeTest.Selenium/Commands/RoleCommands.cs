using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmt.Online.Web.TestUi.Selenium.Commands
{
    public class RoleCommands
    {
        public static List<string> GetUserRoles()
        {
            var roles = new List<string>();
            string cnnStr = Driver.TheEnvironment.ConnectionStringUsers;
            string userId = Driver.TheEnvironment.UserId;
            string appId = Driver.TheEnvironment.ClientCode;
            string sql = string.Format(format: "SELECT RoleName FROM [dbo].[aspnet_Roles] WHERE ApplicationId = (SELECT[ApplicationId] FROM[dbo].[aspnet_Applications] WHERE[ApplicationName] = '{0}') ", arg0: appId);
            using (SqlConnection cnn = new SqlConnection(cnnStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                {
                    cnn.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        foreach(DbDataRecord c in rd.Cast<DbDataRecord>())
                        {
                            roles.Add((string)c[0]);
                        }
                    }
                }
            }

            return roles;
        }
    }
}
