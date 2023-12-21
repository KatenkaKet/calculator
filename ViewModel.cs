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
        private readonly IMemory _currentmemory;

        private string _result;
        private bool _flag = false;
        private string s;
        private string history;

        public ICommand PushEnd {get; }
        public ICommand DeleteLast { get; }
        public ICommand Equal { get; }
        public ICommand Delete {  get; }
        public string CalcMemory { get { return history; } set { history = _currentmemory.Save(value); PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CalcMemory))); } }

        public string Result
        {
            get { return _result; }
            set { _result = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result))); }
        }
        public ViewModel(IMemory memory)
        {
            _model = new Calculator();
            PushEnd = new RelayCommand(ExecuteAdd);
            DeleteLast = new RelayCommand(ExecuteDeleteLast);
            Equal = new RelayCommand(ExecuteEqual);
            Delete = new RelayCommand(ExecuteDelete);
            _errors = new CheckErrors();
            _currentmemory = memory;
        }

        // запись входных параметров
        private void ExecuteAdd(object parameter)
        {
            string operation = "+-*/";
            string temp = parameter as string;
            if (Result == "error") Result = "";
            if (_flag == true)
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
                _flag = false;
            }
            else
            {
                Result += temp;
            }
            
        }

        // удаление последнего элемента
        private void ExecuteDeleteLast(object parameter)
        {
            if(Result != "")
            {
                Result = Result.Remove(Result.Length - 1);
            }
        }

        // Вчисление выражения
        private void ExecuteEqual(object parameter)
        {
            if(!String.IsNullOrEmpty(Result))
            {
                s = Result;
                _errors.expression = Result;
                if (_errors.SyntacticAnalysis() == 1)
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
                s += "=" + Result;
                CalcMemory = s;
            }
        }

        // Oчистка
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
