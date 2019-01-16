using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace ClassLibrary1
{
    public class Database
    {
        //private string strConnection1 = string.Format("server={0};user={1};password={2};database={3};", "192.168.3.124", "root", "1234", "test");
        //private string strConnection2 = string.Format("server={0};user={1};password={2};database={3};", "192.168.3.149", "root", "1234", "gudi");

        public MySqlConnection GetConnection()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection();

                string path = "\\DBInfo.json";
                string result = new StreamReader(File.OpenRead(path)).ReadToEnd();
                JObject jo = JsonConvert.DeserializeObject<JObject>(result);
                Hashtable map = new Hashtable();
                foreach (JProperty col in jo.Properties())
                {
                    Console.WriteLine("{0} : {1}",col.Name,col.Value);
                    map.Add(col.Name, col.Value);
                }
                #region 
                string strConnection1 = 
                    string. Format("server={0};user={1};password={2};database={3};", map["server"],map["user"],map["password"],map["database"]);
                conn.ConnectionString = strConnection1;
                conn.Open();
                #endregion
                return conn;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
