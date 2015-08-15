using System;
using System.Windows.Input;

namespace CompositeAppCommand
{
    internal class RegisteredPart
    {
        //Constructors

        public RegisteredPart(object owner, ICommand command)
        {
            if ((owner == null) || (command == null))
            {
                throw new ArgumentNullException();
            }
            Owner = owner;
            Command = command;
        }

        //Properties

        public object Owner { get; }

        public ICommand Command { get; }

    }
}
