using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace nota
{
    public class data
    {

        SqlConnection con = new SqlConnection();

        private string dbname = "", dbuser = "", dbpass = "";

        private string curDir = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + Path.DirectorySeparatorChar;

        public void carrega_cfg()
        {

            try
            {

                string line = "";

                string nFile = curDir + "cfgdb.txt";

                string[] info;

                StreamReader sReade = new StreamReader(nFile);

                line = sReade.ReadLine();

                info = line.Split(';');

                dbname = info[0];

                dbuser = info[1];

                dbpass = info[2];

            }
            catch (Exception e)
            {

                dataLog(e.ToString());

                dbname = "false";

            }

        }


        public void dataLog(string info)
        {

            String text = "";

            String data = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");

            String nFile = curDir + "dataLog.log";

            StreamWriter wLog = new StreamWriter(nFile, true);

            text = data + "  -  " + info + "\r\n";
            
            wLog.Write(text);

            wLog.Close();

        }

        public Boolean conDB(string dbcatalog)
        {

            Boolean ret = false;

            carrega_cfg();

            try
            {

                if (!dbname.Equals("false"))
                {

                    if (con.State == ConnectionState.Open)
                    {
                        closeDB();
                    }

                    con.ConnectionString =
                        @"Data Source=" + dbname + ";" +
                        "Initial Catalog=" + dbcatalog + ";" +
                        "User id=" + dbuser + ";" +
                        "Password=" + dbpass + ";";

                    con.Open();

                }

                ret = true;

            }
            catch (Exception e)
            {

                dataLog(e.ToString());

                ret = false;

            }

            return ret;

        }

        public void closeDB()
        {

            con.Close();

        }

        public int execQuery(string query)
        {

            int ret = 0;

            string content = "";
            
            if (conDB("nfse"))
            {

                SqlCommand cmd = con.CreateCommand();

                cmd.CommandText = query;

                SqlTransaction sqlT = con.BeginTransaction();

                try
                {

                    cmd.Transaction = sqlT;

                    ret = cmd.ExecuteNonQuery();

                    sqlT.Commit();

                }
                catch (Exception e)
                {

                    sqlT.Rollback();

                    ret = -1;

                    content = query + "\n" + e.ToString();

                    dataLog(content);

                }

            }
            else
            {

                ret = -5;

            }

            closeDB();

            return ret;

        }

        public SqlDataReader rQuery(string query)
        {
            
            SqlDataReader reader = null;

            Boolean retorno = false;

            if (conDB("nfse"))
            {

                SqlCommand cmd = con.CreateCommand();

                cmd.CommandText = query;

                try
                {

                    SqlTransaction sqlT = con.BeginTransaction();

                    cmd.Transaction = sqlT;

                    cmd.ExecuteNonQuery();

                    reader = cmd.ExecuteReader();

                    retorno = reader.HasRows;
                    
                }
                catch (Exception e)
                {

                    dataLog(query + "\r\n" + e.ToString());

                    retorno = false;

                }

            }
            else
            {

                retorno = false;

            }

            return reader;

        }

        public int login(string user, string pass)
        {

            int ret = 0;

            if (conDB("nfse"))
            {

                using (SqlCommand cmd = new SqlCommand("Validate_User"))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", user);
                    cmd.Parameters.AddWithValue("@Password", pass);
                    cmd.Connection = con;
                    ret = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();

                }

            }

            return ret;

        }

    }

}