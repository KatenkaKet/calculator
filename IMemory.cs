using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calcul_2
{
    public interface IMemory
    {

        void Load();
        string Save(string s);
    }

    class DataBase : IMemory
    {
        
        public void Load()
        {
            throw new NotImplementedException();
        }

        public string Save(string s)
        {
            throw new NotImplementedException();
        }
    }

    class File : IMemory
    {
        
        public void Load()
        {
            throw new NotImplementedException();
        }

        public string Save(string s)
        {
            throw new NotImplementedException();
        }
    }

    class RAM : IMemory
    {
        string history = "";
        int i = 0;
        public string History {get {return history; }}
        public void Load()
        {
            throw new NotImplementedException();
        }

        public string Save(string s)
        {
            if (i < 5)
            {
                history += s + "\n";
                i++;
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
