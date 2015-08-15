using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Windows.Input;

namespace CompositeAppCommand
{
    public partial class CompositeAppCommand
    {        
        //Fields

        private static readonly Dictionary<string, CompositeAppCommand> Commands = new Dictionary<string, CompositeAppCommand>();
        private readonly ConcurrentBag<RegisteredPart> _registeredParts = new ConcurrentBag<RegisteredPart>();

        //Methods

        private static string GetKey(string name, string family)
        {
            return string.IsNullOrWhiteSpace(name) ? null : $"{family}::{name}";
        }

        internal static CompositeAppCommand GetCommand(string name, string family)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            var key = GetKey(name, family);
            if (!Commands.ContainsKey(key))
            {
                Commands.Add(key, new CompositeAppCommand());
            }
            return Commands[key];
        }

        public static bool RegisterCommand(object owner, ICommand command, string name, string family = null)
        {
            if ((owner == null) ||(command == null) || string.IsNullOrWhiteSpace(name))
            {
                return false;
            }
            var key = GetKey(name, family);
            if (!Commands.ContainsKey(key))
            {
                return false;
            }
            Commands[key].RegisterCommand(owner, command);
            return true;
        }

        private void RegisterCommand(object owner, ICommand command)
        {
            _registeredParts.Add(new RegisteredPart(owner, command));
            command.CanExecuteChanged += Command_CanExecuteChanged;
        }

        private void Command_CanExecuteChanged(object sender, System.EventArgs e)
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
