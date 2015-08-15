using System;
using System.Text;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;

namespace WpfModuleB
{
    public class ViewBVm : BindableBase
    {
        //Fields

        private readonly StringBuilder _commandText = new StringBuilder();

        //Properties

        public DelegateCommand<object> AddCommand => new DelegateCommand<object>(Save, CanSave);

        public string Text => _commandText.ToString();

        public ViewBVm()
        {
            CompositeAppCommand.CompositeAppCommand.RegisterCommand(this, AddCommand, "CMD1", "Group1");
        }

        //Methods
        
        private bool CanSave(object arg)
        {
            return true;
        }

        private void Save(object obj)
        {
            _commandText.AppendFormat("New command {0} at {1}", obj, DateTime.Now);
            _commandText.AppendLine();
            OnPropertyChanged(() => Text);
        }
    }
}
