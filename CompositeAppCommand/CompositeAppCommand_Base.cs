using System;
using System.Linq;
using System.Windows.Input;

namespace CompositeAppCommand
{
    public sealed partial class CompositeAppCommand : ICommand
    {
        //Delegates

        public delegate void CommandOnExecute(object parameter);
        public delegate bool CommandOnCanExecute(object parameter);

        //Constructors

        internal CompositeAppCommand() { }

        //Events

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //Methods

        public bool CanExecute(object parameter)
        {
            var parts = _registeredParts.ToArray();
            return parts.Length != 0 && parts.Aggregate(true, (current, part) => current && part.Value.Command.CanExecute(parameter));
        }

        public void Execute(object parameter)
        {
            var parts = _registeredParts.ToArray();
            foreach (var part in parts)
            {
                part.Value.Command.Execute(parameter);
            }
        }

    }
}
