using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPFClient.Annotations;
using System.Windows.Input;

namespace WPFClient {
    using System.Collections;
    using System.Windows;
    using System.Windows.Controls;
    using Repository;
    using ServerLogic.Map;
    using WPFClient.UserControls;

    public class Binding : INotifyPropertyChanged {
        private UserControl _control;
        private Guid _userGuid;
        private string _email;
        private string _firstName;
        private string _lastName;
        private string _phone;
        private string _hello = "Enter";
        private Visibility _flag = Visibility.Hidden;
        private Visibility _flagSettings = Visibility.Hidden;
        private List<Place> _meetings = new List<Place>();
        
        public UserControl Control {
            get {
                return _control;
            }
            set {
                _control = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged();}
        }

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; OnPropertyChanged();}
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }
        
        public Visibility Flag {
            get { return _flag; }
            set {
                _flag = value;
                OnPropertyChanged();
            }
        }

        public Visibility FlagSetting
        {
            get { return _flagSettings; }
            set
            {
                _flagSettings = value;
                OnPropertyChanged();
            }
        }

        public string Hello {
            get { return _userGuid == Guid.Empty ? _hello :  $"Hello, {_firstName}"; }
            set {
                _hello = value;
                OnPropertyChanged();
            }
        }

        public ICommand SettingsClick {
            get {
                return new DelegateCommand(() => {
                    if (_userGuid == Guid.Empty) {
                        Control = new SettingControl();
                        ListBox a = Control?.Content as ListBox;
                        a?.Items.Add(new Users {
                            firstName = _firstName,
                            lastName = _lastName,
                            email = _email,
                            contactNumber = _phone
                        });
                        FlagSetting = Visibility.Visible;
                    }
                });
            }
        }

        public ICommand MeetingClick
        {
            get
            {
                return new DelegateCommand(() => {
                    if(_userGuid == Guid.Empty) {
                        Control = new MeetingControl();
                        ListBox a = Control?.Content as ListBox;
                        //TODO add via method
                        a?.Items.Add(new Place { city = "city" });
                        a?.Items.Add(new Place { city = "ГОрод" });
                        FlagSetting = Visibility.Visible;
                    }
                });
            }
        }

        public ICommand ClickEnter {
            get {
                if (_userGuid == Guid.Empty)
                    return new DelegateCommand(() => {
                        Hello = "Enterrrrrr";
                        Flag = Visibility.Visible;
                    });
                else {
                    return new DelegateCommand(() => Hello = "Тут нужно будет отправить запрос");
                }
            }
        }

        public ICommand CreateOrLogin {
            get {
                return new DelegateCommand(() => {
                    if (_email != "" && _firstName != null && _lastName != null && _phone != null) {
                        Users user = new Users {
                            email = _email,
                            firstName = _firstName,
                            lastName = _firstName,
                            contactNumber = _phone
                        };
                        var createUser = new UserRepository().CreateUser(user);
                        createUser.Wait();
                    }
                });
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
