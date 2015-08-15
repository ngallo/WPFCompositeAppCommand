using System;
using System.Windows.Input;

namespace CompositeAppCommand
{
    internal class RegisteredPart : IDisposable
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

        public object Owner { get; private set; }

        public ICommand Command { get; private set; }

        //Methods

        public void Dispose()
        {
            Owner = null;
            Command = null;
        }
    }
}
