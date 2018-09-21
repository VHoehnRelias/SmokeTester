using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cmt.Online.Web.TestUi.Selenium.Enums
{
    public class Environment
    {
        public Environment(EnvironmentType type, string server, string database, string clientCode, string userId, string password, string webSite)
        {
            Type = type;
            Server = server;
            Database = database;
            ClientCode = clientCode;
            UserId = userId;
            Password = password;
            WebSite = webSite;
        }

        private EnvironmentType type;
        public EnvironmentType Type
        {
            get { return type; }
            set { type = value; }
        }

        private string server;
        public string Server
        {
            get { return server; }
            set { server = value; }
        }

        private string database;
        public string Database
        {
            get { return database; }
            set { database = value; }
        }

        private string clientCode;
        public string ClientCode
        {
            get { return clientCode; }
            set { clientCode = value; }
        }

        private string userId;
        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string webSite;
        public string WebSite
        {
            get { return webSite; }
            set { webSite = value; }
        }

        public override string ToString()
        {
            return this.Type.ToString();
        }

        public string ConnectionString
        {
            get{ return string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", Server, Database ); }
        }

        public string ConnectionStringUsers
        {
            get { return string.Format("Data Source={0};Initial Catalog=AspNetDB;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", Server); }
        }
        public string ConnectionStringLookup
        {
            get { return string.Format("Data Source={0};Initial Catalog=LookupLocal;User ID=CMTOnline;Password=CMTOnline", Server);
        }
    }
    }
}
