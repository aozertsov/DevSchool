using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPFClient.Annotations;
using System.Windows.Input;

namespace WPFClient {
    using System.Windows;
    using ServerLogic.Map;

    public class Binding : INotifyPropertyChanged {
        private Guid _userGuid;
        private string _email;
        private string _name;
        private string _hello;
        private int _columnCount;
        private Visibility _flag = Visibility.Hidden;
        

        public Visibility Flag {
            get { return _flag; }
            set {
                _flag = value;
         //       OnPropertyChanged();
            }
        }

        public string Hello {
            get { return _userGuid == Guid.Empty ? _hello :  $"Hello, {_name}"; }
            set {
                _hello = value;
                OnPropertyChanged();
            }
        }

        public int ColumnCount {
            get { return _columnCount; }
            set {
                _columnCount = value;
                //OnPropertyChanged(nameof(ColumnCount));
            }
        }
        
        public ICommand ClickEnter {
            get     {
                if (_userGuid == Guid.Empty)
                    return new DelegateCommand(() => Hello = "Enterrrrrr");
                else {
                    return new DelegateCommand(() => Hello = "Тут нужно будет отправить запрос");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
            
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }

    public class DelegateCommand : ICommand {
        private readonly Action _action;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action action) {
            _action = action;
        }

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            _action();
        }
    }
}
