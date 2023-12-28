using System;
using System.Collections.Generic;
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
        
        public string Load()
        {
            throw new NotImplementedException();
        }

        public string Save(string s)
        {

            throw new NotImplementedException();
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
