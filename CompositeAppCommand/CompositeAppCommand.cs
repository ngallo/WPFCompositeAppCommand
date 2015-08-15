using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CompositeAppCommand
{
    public partial class CompositeAppCommand
    {        
        //Fields

        private static readonly Dictionary<string, CompositeAppCommand> Commands = new Dictionary<string, CompositeAppCommand>();
        private readonly ConcurrentDictionary<string, RegisteredPart> _registeredParts = new ConcurrentDictionary<string, RegisteredPart>();

        //Methods

        private static string GetKey(string name, string family)
        {
            return string.IsNullOrWhiteSpace(name) ? null : $"{family}::{name}";
        }
        
        private static void Command_CanExecuteChanged(object sender, System.EventArgs e)
        {
            CommandManager.InvalidateRequerySuggested();
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

        public static bool UnregisterCommand(object owner, string name, string family = null)
        {
            if ((owner == null) || string.IsNullOrWhiteSpace(name))
            {
                return false;
            }
            var key = GetKey(name, family);
            if (!Commands.ContainsKey(key))
            {
                return false;
            }
            Commands[key].UnregisterCommand(owner);
            return true;
        }

        private void RegisterCommand(object owner, ICommand command)
        {
            var part = new RegisteredPart(owner, command);
            if (_registeredParts.TryAdd(owner.GetHashCode().ToString(), part))
            {
                command.CanExecuteChanged += Command_CanExecuteChanged;
            }
        }

        private void UnregisterCommand(object owner)
        {
            var parts = _registeredParts.ToArray();
            foreach (var part in parts.Where(part => part.Value.Owner == owner))
            {
                part.Value.Command.CanExecuteChanged -= Command_CanExecuteChanged;
                part.Value.Dispose();
                RegisteredPart partRemoved;
                _registeredParts.TryRemove(part.Key, out partRemoved);
            }
        }

    }
}
