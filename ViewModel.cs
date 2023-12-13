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
        private readonly CheckErrors _errors;

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
            _errors = new CheckErrors();
        }

        private void ExecuteAdd(object parameter)
        {
            string operation = "+-*/";
            string temp = parameter as string;
            if (Result == "error") Result = "";
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
                _errors.expression = Result;
                if (_errors.AvailableCharacters() == 1 && _errors.SyntacticAnalysis() == 1)
                {
                    _model.s = Result;
                    _model.i = 0;
                    double temp = _model.ProcE();
                    Result = temp.ToString();
                    this._flag = true;
                }
                else
                {
                    Result = "error";
                }
            }
        }

        private void ExecuteDelete(object parameter)
        {
            Result = "";
        }



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
