using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Calcul_2
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly Calculator _model;

        private string _result;
        private bool _flag = false;

        public ICommand PushEnd{get; }
        public ICommand DeleteLast { get; }
        public ICommand Equal { get; }
        public ICommand Delete {  get; }

        public string Result
        {
            get { return _result; }
            set { _result = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result)));
            }
        }
        public ViewModel()
        {
            _model = new Calculator();
            PushEnd = new RelayCommand(ExecuteAdd);
            DeleteLast = new RelayCommand(ExecuteDeleteLast);
            Equal = new RelayCommand(ExecuteEqual);
            Delete = new RelayCommand(ExecuteDelete);
        }

        private void ExecuteAdd(object parameter)
        {
            string operation = "+-*/";
            string temp = parameter as string;
            if(this._flag == true)
            {
                if (operation.IndexOf(temp) >= 0)
                {
                    Result += temp;
                }
                else
                {
                    Result = "";
                    Result += temp;

                }
                this._flag = false;
            }
            else
            {
                Result += temp;
            }
            
        }

        private void ExecuteDeleteLast(object parameter)
        {
            if(Result != "")
            {
                Result = Result.Remove(Result.Length - 1);
            }
        }

        private void ExecuteEqual(object parameter)
        {
            if(Result != "")
            {
                _model.s = Result;
                _model.i = 0;
                double temp = _model.ProcE();
                Result = temp.ToString();
                this._flag = true;
            }
        }

        private void ExecuteDelete(object parameter)
        {
            Result = "";
        }

        //private void CheckString(object parameter)
        //{
        //    string operation = "+-*/()";
        //    string a = parameter as string;
        //    for(int i = 0; i < a.Length; i++)
        //    {
        //        if ((a[i] >= '0' && a[i] <= '9') || ())
        //    }

        //}
        


    }



    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }


    





}
