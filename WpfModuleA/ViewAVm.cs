using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Text;

namespace WpfModuleA
{
    public class ViewAVm : BindableBase
    {
        //Fields

        private readonly StringBuilder _commandText = new StringBuilder();
        private bool _isActive;
        
        //Constructors


        public ViewAVm()
        {
            AddCommand = new DelegateCommand<object>(Save, CanSave);
            CompositeAppCommand.CompositeAppCommand.RegisterCommand(this, AddCommand, "CMD1", "Group1");
        }

        //Properties

        public DelegateCommand<object> AddCommand { get; }

        public string Text => _commandText.ToString();


        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive == value) return;
                _isActive = value;
                OnPropertyChanged(() => IsActive);
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        //Methods

        private bool CanSave(object arg)
        {
            return IsActive;
        }

        private void Save(object obj)
        {
            _commandText.AppendFormat("New command {0} at {1}", obj, DateTime.Now);
            _commandText.AppendLine();
            OnPropertyChanged(() => Text);
        }
    }
}