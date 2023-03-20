using System.ComponentModel;

namespace TCPClient.ViewModel {
    public class ViewModelBase : INotifyPropertyChanged
    {
        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        

        string _ipAddr = "1"; // 기본 
        public string IPAddr
        {
            get
            {
                return _ipAddr;
            }
            set
            {
                if (_ipAddr == value)
                    return;
                _ipAddr = value;
                OnPropertyChanged("IPAddr");
            }
        }

        int _port = 9999; // 기본
        public int Port
        {
            get
            {
                return _port;
            }
            set
            {
                if (_port == value)
                    return;
                _port = value;
                OnPropertyChanged("Port");
            }
        }

        string _str;
        public string Str
        {
            get
            {
                return _str;
            }
            set
            {
                if (_str == value)
                    return;
                _str = value;
                OnPropertyChanged("Str");
            }
        }

        string _msgBox;
        public string MsgBox
        {
            get
            {
                return _msgBox;
            }
            set
            {
                if (_msgBox == value)
                    return;
                _msgBox = value;
                OnPropertyChanged("MsgBox");
            }
        }

        string _state;
        public string State
        {
            get
            {
                return _state;
            }
            set
            {
                if (_state == value)
                    return;
                _state = value;
                OnPropertyChanged("State");
            }
        }

        System.Windows.Media.Brush _foregroundColor = System.Windows.Media.Brushes.Red;
        public System.Windows.Media.Brush ForegroundColor
        {
            get
            {
                return _foregroundColor;
            }
            set
            {
                if (_foregroundColor == value)
                    return;
                _foregroundColor = value;
                OnPropertyChanged("ForegroundColor");
            }
        }

    }
}
