﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GestionTareasALC.ViewModel
{
    class Command : ICommand
    {
        readonly Action<object> acciónAEjecutar;
        public Command(Action<object> execute)
        : this(execute, null)
        {
        }
        public Command(Action<object> execute, Predicate<object>
        canExecute)
        {
            acciónAEjecutar = execute;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            acciónAEjecutar(parameter);
        }
    }
}
