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
        public DateTime? CurrentDate;
        private UserControl _control;
        private UserControl _logOrCreate;
        private Guid _userGuid;
        private string _email;
        private string _firstName;
        private string _lastName;
        private string _phone;
        private string _hello = "";
        private Visibility _flag = Visibility.Visible;
        private Visibility _flagSettings = Visibility.Hidden;
        private List<Place> _meetings = new List<Place>();
        private int _flat;
        private int _house;
        private string _street;
        private string _city;
        private string _country;

        public int Flat {
            get { return _flat; }
            set { _flat = value; }
        }

        public int House {
            get { return _house; }
            set { _house = value; }
        }

        public string Street {
            get { return _street; }
            set { _street = value; }
        }

        public string City {
            get { return _city; }
            set { _city = value; }
        }

        public string Country {
            get { return _country; }
            set { _country = value; }
        }

        public Guid UserGuid {
            get { return _userGuid; }
            set { _userGuid = value; }
        }

        public UserControl Control {
            get {
                return _control;
            }
            set {
                _control = value;
                OnPropertyChanged();
            }
        }

        public UserControl LogOrCreate
        {
            get
            {
                return _logOrCreate;
            }
            set
            {
                _logOrCreate = value;
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
            get { return _hello; } //_userGuid == Guid.Empty ? _hello :  $"Hello, {_firstName}"; }
            set {
                _hello = value;
                OnPropertyChanged();
            }
        }

        public ICommand SettingsClick {
            get {
                return new DelegateCommand(() => {
                    if (_userGuid == Guid.Empty) {
                        if (Flag != Visibility.Visible)
                            Hello = "Please, enter";
                    }
                    else {
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
                    if(_userGuid != Guid.Empty) {
                        var createUser = new MeetRepository().GetMeets(UserGuid);
                        createUser.Wait();
                        var w = createUser.Result;
                        Control = new MeetingControl();
                        ListBox a = Control?.Content as ListBox;
                        //TODO add via method
                        foreach (Place place in w) {
                            a?.Items.Add(place);
                        }
                        FlagSetting = Visibility.Visible;
                    }
                });
            }
        }

        public ICommand ClickEnter {
            get {
                if (_userGuid == Guid.Empty)
                    return new DelegateCommand(() => {
                        Hello = "enter your datas";
                        Flag = Visibility.Visible;
                    });
                else {
                    return new DelegateCommand(() => Hello = $"Hello {Email}");
                }
            }
        }

        public ICommand CreateMeet
        {
            get
            {
                return new DelegateCommand(() => {
                    if(_userGuid != Guid.Empty) {
                        Control = new CreateMeetControl();
                        ListBox a = Control?.Content as ListBox;
                        FlagSetting = Visibility.Visible;
                    }
                });
            }
        }
        
        public ICommand ClickLogin
        {
            get
            {
                return new DelegateCommand(() => {
                    LogOrCreate = new LoginControl();
                });
            }
        }

        public ICommand Login
        {
            get
            {
                return new DelegateCommand(() => {
                    if(!(String.IsNullOrEmpty(_email) || String.IsNullOrEmpty(_phone))) {
                        Users user = new Users {
                            email = _email,
                            contactNumber = _phone
                        };
                        var loginUser = new UserRepository().LoginUser(user);
                        loginUser.Wait();
                        UserGuid = loginUser.Result.idUser;
                        FirstName = loginUser.Result.firstName;
                        LastName = loginUser.Result.lastName;
                        Hello = $"Hello, {FirstName}";
                        Flag = Visibility.Hidden;
                        OnPropertyChanged();
                    }
                });
            }
        }

        public ICommand ClickCreate
        {
            get
            {
                return new DelegateCommand(() => {
                    LogOrCreate = new CreateControl();
                    FlagSetting = Visibility.Visible;
                });
            }
        }

        public ICommand Create
        {
            get
            {
                return new DelegateCommand(() => {
                    if(!String.IsNullOrEmpty(_email) && !String.IsNullOrEmpty(_phone) && !String.IsNullOrEmpty(_firstName) && !String.IsNullOrEmpty(_lastName)) {
                        Users user = new Users {
                            email = _email,
                            contactNumber = _phone,
                            firstName = _firstName,
                            lastName = _lastName
                        };
                        var loginUser = new UserRepository().CreateUser(user);
                        loginUser.Wait();
                        UserGuid = loginUser.Result;
                        Hello = $"Hello {Email}";
                        Flag = Visibility.Hidden;
                        OnPropertyChanged();
                    }
                });
            }
        }

        public ICommand CreateMeetClick
        {
            get
            {
                return new DelegateCommand(() => {
                    var c = Control as CreateMeetControl;
                    CurrentDate = c?.datepick.SelectedDate;
                    DateTime t = CurrentDate.Value;
                    if(!String.IsNullOrEmpty(_country) && !String.IsNullOrEmpty(_city) && !String.IsNullOrEmpty(_street) && _house >= 0 && _flat >= 0 && t >= (new DateTime())) {
                        var place = new Place {
                            city = _city,
                            country = _country,
                            street = _street,
                            house = _house,
                            flat = _flat
                        };

                        var placeId = new PlaceRepository().GetId(place);
                        placeId.Wait();
                        var idplace = placeId.Result;

                        var state = new MeetRepository().CreateMeet(place, t, UserGuid, idplace);
                        state.Wait();
                        var idMeet = state.Result;

                        var subscribeme = new SubscriptionRepository().Subscribe(_userGuid, idMeet);
                        subscribeme.Wait();
                        var result = subscribeme.Result;
                        Hello = $"Hello {Email}";
                        Flag = Visibility.Hidden;
                        OnPropertyChanged();
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
