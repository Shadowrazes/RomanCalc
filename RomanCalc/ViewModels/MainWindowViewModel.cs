using System;
using System.Collections.Generic;
using System.Text;
using System.Reactive;
using ReactiveUI;
using RomanCalc.Models;

namespace RomanCalc.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string expression = "";

        public ReactiveCommand<string, string> OnClickCommand { get; }

        public MainWindowViewModel()
        {
            OnClickCommand = ReactiveCommand.Create<string, string>((str) => Expression = str);
        }
        public string Expression
        {
            set
            {
                if (expression == "Ошибка")
                    expression = "";
                if (value != "=")
                    this.RaiseAndSetIfChanged(ref expression, expression + value);
                else
                {
                    try
                    {
                        if (expression.IndexOf('+') == 0 || expression.IndexOf('+') == expression.Length - 1 ||
                            expression.IndexOf('-') == 0 || expression.IndexOf('-') == expression.Length - 1 ||
                            expression.IndexOf('*') == 0 || expression.IndexOf('*') == expression.Length - 1 ||
                            expression.IndexOf('/') == 0 || expression.IndexOf('/') == expression.Length - 1)
                            throw new RomanNumberException("Ошибка");
                        if (expression.IndexOf('+') != -1)
                        {
                            RomanNumberExtend a = new RomanNumberExtend(expression.Substring(0, expression.IndexOf('+')));
                            RomanNumberExtend b = new RomanNumberExtend(expression.Substring(expression.IndexOf('+')));
                            this.RaiseAndSetIfChanged(ref expression, (a + b).ToString());
                        }
                        if (expression.IndexOf('-') != -1)
                        {
                            RomanNumberExtend a = new RomanNumberExtend(expression.Substring(0, expression.IndexOf('-')));
                            RomanNumberExtend b = new RomanNumberExtend(expression.Substring(expression.IndexOf('-')));
                            this.RaiseAndSetIfChanged(ref expression, (a - b).ToString());
                        }
                        if (expression.IndexOf('*') != -1)
                        {
                            RomanNumberExtend a = new RomanNumberExtend(expression.Substring(0, expression.IndexOf('*')));
                            RomanNumberExtend b = new RomanNumberExtend(expression.Substring(expression.IndexOf('*')));
                            this.RaiseAndSetIfChanged(ref expression, (a * b).ToString());
                        }
                        if (expression.IndexOf('/') != -1)
                        {
                            RomanNumberExtend a = new RomanNumberExtend(expression.Substring(0, expression.IndexOf('/')));
                            RomanNumberExtend b = new RomanNumberExtend(expression.Substring(expression.IndexOf('/')));
                            this.RaiseAndSetIfChanged(ref expression, (a / b).ToString());
                        }
                    }
                    catch (RomanNumberException)
                    {
                        this.RaiseAndSetIfChanged(ref expression, "Ошибка");
                    }
                }
            }
            get
            { 
                return expression;
            }
        }
    }
}
