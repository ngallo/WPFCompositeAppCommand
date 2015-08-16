using System;
using System.Windows.Input;
using System.Windows.Markup;

namespace CompositeAppCommand.MarkupExtensions
{
    [MarkupExtensionReturnType(typeof(ICommand))]
    public class AppCommand : MarkupExtension
    {
        //Constructors

        public AppCommand() { }
        
        public AppCommand(string name)
        {
            Name = name;
        }

        public AppCommand(string name, string family)
        {
            Name = name;
            Family = family;
        }

        // Properties

        public string Family { get; set; }

        public string Name { get; set; }

        //Methods

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Name == null)
            {
                throw new InvalidOperationException("Cannot use command without setting Name.");
            }
            return CompositeAppCommand.GetCommand(Name, Family);
        }

    }
}
