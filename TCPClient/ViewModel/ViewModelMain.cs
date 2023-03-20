using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TCPClient.Helpers;

namespace TCPClient.ViewModel {
    class ViewModelMain : ViewModelBase {
        TcpClient client;
        Thread receiveThr;
        public RelayCommand ConnectCommand { get; set; }
        public RelayCommand FileSendCommand { get; set; }
        public RelayCommand OrderCommand { get; set; }
        public RelayCommand ClearCommand { get; set; }
        public RelayCommand OpenFileCommand { get; set; }

        private const int ORDER_LIMIT = 2;
        private const int CLEAR_LIMIT = 1;

        private int _limitValue = 0;

        private string _openFileText;
        public string OpenFileText {
            get { return _openFileText; }
            set {
                _openFileText = value;
                OnPropertyChanged("OpenFileText");
            }
        }
        public ViewModelMain() {
            ConnectCommand = new RelayCommand(Connect);
            OrderCommand = new RelayCommand(Order);
            ClearCommand = new RelayCommand(Clear);
            State = "Disconnected";
        }

        private void Order(object obj) {
            if (client == null) {
                return;
            }
            if (_limitValue != ORDER_LIMIT) {
                try {
                    NetworkStream stream = client.GetStream();
                    if (stream.CanWrite) {
                        Str = "Order";
                        if (Str != null && Str != "") {
                            string clientMsg = Str;
                            byte[] clientMsgAsByteArray = Encoding.UTF8.GetBytes(clientMsg);
                            stream.Write(clientMsgAsByteArray, 0, clientMsgAsByteArray.Length);
                            //MsgBox += "Client] " + clientMsg + Environment.NewLine;
                        }
                    }
                } catch (SocketException se) {
                    //MessageBox.Show("Socket exception: " + se);
                } catch (ArgumentNullException ae) {

                }
                Str = "";
                _limitValue = ORDER_LIMIT;
            }
            
        }
        //Test Branch

        private void Clear(object obj) {
            if (client == null) {
                return;
            }
            if (_limitValue != CLEAR_LIMIT) {
                try {
                    NetworkStream stream = client.GetStream();
                    if (stream.CanWrite) {
                        Str = "Clear";
                        if (Str != null && Str != "") {
                            string clientMsg = Str;
                            byte[] clientMsgAsByteArray = Encoding.UTF8.GetBytes(clientMsg);
                            stream.Write(clientMsgAsByteArray, 0, clientMsgAsByteArray.Length);
                            //MsgBox += "Client] " + clientMsg + Environment.NewLine;
                        }
                    }
                } catch (SocketException se) {
                    //MessageBox.Show("Socket exception: " + se);
                } catch (ArgumentNullException ae) {

                }
                Str = "";
                _limitValue = CLEAR_LIMIT;
            }
            
        }

        private void Connect(object obj) {
            try {
                receiveThr = new Thread(new ThreadStart(ListenData));
                receiveThr.IsBackground = true;
                receiveThr.Start();
            } catch (Exception e) {
                //MessageBox.Show("Client connection exception " + e);
            }
        }

        private void ListenData() {
            try {
                client = new TcpClient();
                IPAddress ip = IPAddress.Parse("127.0.0." + IPAddr);
                client.Connect(ip, Port);
                if (client.Connected == true) {
                    MsgBox += "client connected!" + Environment.NewLine;
                    State = "Connected";
                    ForegroundColor = System.Windows.Media.Brushes.Blue;
                    Byte[] bytes = new Byte[1024];
                    while (true) {
                        using (NetworkStream stream = client.GetStream()) {
                            int length;
                            try {
                                while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) {
                                    var incommingData = new byte[length];
                                    Array.Copy(bytes, 0, incommingData, 0, length);
                                    string serverMsg = Encoding.UTF8.GetString(incommingData);
                                    MsgBox += "Server] " + serverMsg + Environment.NewLine;
                                }
                            } catch (Exception e) {
                                client.Close();
                                MsgBox += "connection close" + Environment.NewLine;
                            }
                        }
                    }
                }
            } catch (SocketException se) {
                //MessageBox.Show("Socket exception: " + se);
            }
        }

    }
}
