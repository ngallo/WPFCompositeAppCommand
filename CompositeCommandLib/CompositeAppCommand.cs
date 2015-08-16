using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace CompositeCommandLib
{
    public class CompositeAppCommand : CompositeCommand
    {
        //Fields

        private static readonly Dictionary<string, CompositeAppCommand> Commands = new Dictionary<string, CompositeAppCommand>();
        private readonly Dictionary<object, IList<ICommand>> _composisteParts = new Dictionary<object, IList<ICommand>>();

        //Methods

        #region Internal Helpers

        private static string GetKey(string name, string family)
        {
            return string.IsNullOrWhiteSpace(name) ? null : $"{family}::{name}";
        }

        internal static ICommand GetCommand(string name, string family)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            var key = GetKey(name, family);
            lock (Commands)
            {
                if (!Commands.ContainsKey(key))
                {
                    Commands.Add(key, new CompositeAppCommand());
                }
                return Commands[key];
            }
        }

        #endregion

        public static void RegisterCommand(object owner, ICommand command, string name, string family = null)
        {
            if ((owner == null) || (command == null) || string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException();
            }
            lock (Commands)
            {
                var appCmd = GetCommand(name, family) as CompositeAppCommand;
                if (appCmd == null)
                {
                    throw new InvalidOperationException();
                }
                appCmd.RegisterCommand(owner, command);
            }
        }

        public static void UnregisterCommand(object owner, string name, string family = null)
        {
            if ((owner == null) || string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException();
            }
            var key = GetKey(name, family);
            lock (Commands)
            {
                if (!Commands.ContainsKey(key))
                {
                    throw new InvalidOperationException("Command doesn't exist.");
                }
                Commands[key].UnregisterCommand(owner);
            }
        }

        private void RegisterCommand(object owner, ICommand command)
        {
            lock (_composisteParts)
            {
                if (!_composisteParts.ContainsKey(owner))
                {
                    _composisteParts.Add(owner, new List<ICommand>());
                }
                var commands = _composisteParts[owner];
                commands.Add(command);
                RegisterCommand(command);
            }
        }

        private void UnregisterCommand(object owner)
        {
            lock (_composisteParts)
            {
                if (!_composisteParts.ContainsKey(owner))
                {
                    return;
                }
                var commands = _composisteParts[owner];
                if (commands != null)
                {
                    foreach (var command in commands)
                    {
                        UnregisterCommand(command);
                    }
                }
                _composisteParts.Remove(owner);
            }
        }

    }
}
