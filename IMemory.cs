using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calcul_2
{
    public interface IMemory
    {

        string Load();
        string Save(string s);
    }

    class DataBase : IMemory
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=calc_history");
        string history = "";
        int j = 0;

        public void openConnection()
        {
            if(connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void closeConnection()
        {
            if(connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public string Load()
        {
            
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM `calculations`", connection);
            openConnection();
            //DataSet ds = new DataSet();
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                for (int i = table.Rows.Count-5; i < table.Rows.Count; i++)
                {
                    if(i < 0) { continue; }
                    history += table.Rows[i][1].ToString()+"="+ table.Rows[i][2].ToString() + "\n";
                    j++;
                }
            }
            return history;
        }

        public string Save(string s)
        {
            openConnection();
            string[] partofs = s.Split('=');
            MySqlCommand command = new MySqlCommand("INSERT INTO `calculations` (`expression`, `decision`) VALUES('"+ partofs[0] + "', '"+ partofs[1] + "');", connection);
            command.ExecuteNonQuery();
            closeConnection();
            if (j < 5)
            {
                history += s + "\n";
                j++;
            }
            else
            {
                string[] partofhistory = history.Split('\n');
                history = "";
                for (int i = 1; i < 5; i++)
                {
                    history += partofhistory[i] + "\n";
                }
                history += s + "\n";
            }
            return history;
        }
    }

    class FileMemory : IMemory
    {
        string file_name = "File_History.txt";
        string history = "";
        int j = 0;
        public string Load()
        {
            history = File.ReadAllText(file_name);
            string[] partofhistory = history.Split('\n');
            history = "";
            j = partofhistory.Length;
            if (j > 6)
            {
                for (int i = j - 6; i < j; i++)
                {
                    history += partofhistory[i] + "\n";
                }
            }
            else
            {
                for (int i = 0; i < j; i++)
                {
                    if (partofhistory[i] != "")
                        history += partofhistory[i] + "\n";
                }
            }
            return history;
        }

        public string Save(string s)
        {
            File.AppendAllText(file_name, s+"\n");
            if (j < 5)
            {
                history += s + "\n";
                j++;
            }
            else
            {
                string[] partofhistory = history.Split('\n');
                history = "";
                for (int i = 1; i < 5; i++)
                {
                    history += partofhistory[i] + "\n";
                }
                history += s + "\n";
            }
            return history;
        }
    }

    class RAM : IMemory
    {
        string history = "";
        int j = 0;
        public string History {get {return history; }}
        public string Load()
        {
            return history;
        }

        public string Save(string s)
        {
            if (j < 5)
            {
                history += s + "\n";
                j++;
            }
            else
            {
                string[] partofhistory = history.Split('\n');
                history = "";
                for(int i = 1; i < 5; i++)
                {
                    history += partofhistory[i] + "\n";
                }
                history += s + "\n";
            }
            return history;
        }
    }
}
