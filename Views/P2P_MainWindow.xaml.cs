using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.PeerToPeer;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WebCrawlerWPF.P2P;

namespace WebCrawlerWPF.Views
{
    public partial class P2P_MainWindow : Window
    {
        private P2PService localService;
        private string serviceUrl;
        private ServiceHost host;
        private PeerName peerName;
        private PeerNameRegistration peerNameRegistration;

        public P2P_MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Получение конфигурационной информации из app.config
            string port = ConfigurationManager.AppSettings["port"];
            string username = ConfigurationManager.AppSettings["username"];
            string machineName = Environment.MachineName;
            string serviceUrl = null;

            // Установка заголовка окна
            this.Title = string.Format("P2P приложение - {0}", username);

            //  Получение URL-адреса службы с использованием адресаIPv4 
            //  и порта из конфигурационного файла
            foreach (IPAddress address in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    serviceUrl = string.Format("net.tcp://{0}:{1}/P2PService", address, port);
                    break;
                }
            }

            // Выполнение проверки, не является ли адрес null
            if (serviceUrl == null)
            {
                // Отображение ошибки и завершение работы приложения
                MessageBox.Show(this, "Не удается определить адрес конечной точки WCF.", "Networking Error",
                    MessageBoxButton.OK, MessageBoxImage.Stop);
                Application.Current.Shutdown();
            }

            // Регистрация и запуск службы WCF
            localService = new P2PService(this, username);
            host = new ServiceHost(localService, new Uri(serviceUrl));
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Mode = SecurityMode.None;
            host.AddServiceEndpoint(typeof(IP2PService), binding, serviceUrl);
            try
            {
                host.Open();
            }
            catch (AddressAlreadyInUseException)
            {
                // Отображение ошибки и завершение работы приложения
                MessageBox.Show(this, "Не удается начать прослушивание, порт занят.", "WCF Error",
                   MessageBoxButton.OK, MessageBoxImage.Stop);
                Application.Current.Shutdown();
            }

            // Создание имени равноправного участника (пира)
            peerName = new PeerName("P2P Sample", PeerNameType.Unsecured);

            // Подготовка процесса регистрации имени равноправного участника в локальном облаке
            if (port != null)
            {
                peerNameRegistration = new PeerNameRegistration(peerName, int.Parse(port));
                peerNameRegistration.Cloud = Cloud.AllLinkLocal;

                // Запуск процесса регистрации
                peerNameRegistration.Start();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Остановка регистрации
            peerNameRegistration.Stop();

            // Остановка WCF-сервиса
            host.Close();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            // Создание распознавателя и добавление обработчиков событий
            PeerNameResolver resolver = new PeerNameResolver();
            resolver.ResolveProgressChanged +=
                new EventHandler<ResolveProgressChangedEventArgs>(resolver_ResolveProgressChanged);
            resolver.ResolveCompleted +=
                new EventHandler<ResolveCompletedEventArgs>(resolver_ResolveCompleted);

            // Подготовка к добавлению новых пиров
            PeerList.Items.Clear();
            RefreshButton.IsEnabled = false;

            // Преобразование незащищенных имен пиров асинхронным образом
            resolver.ResolveAsync(new PeerName("0.P2P Sample"), 1);
        }

        void resolver_ResolveCompleted(object sender, ResolveCompletedEventArgs e)
        {
            // Сообщение об ошибке, если в облаке не найдены пиры
            if (PeerList.Items.Count == 0)
            {
                PeerList.Items.Add(
                   new PeerEntry
                   {
                       DisplayString = "Пиры не найдены.",
                       ButtonsEnabled = false
                   });
            }
            // Повторно включаем кнопку "обновить"
            RefreshButton.IsEnabled = true;
        }

        void resolver_ResolveProgressChanged(object sender, ResolveProgressChangedEventArgs e)
        {
            PeerNameRecord peer = e.PeerNameRecord;

            foreach (IPEndPoint ep in peer.EndPointCollection)
            {
                if (ep.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    try
                    {
                        string endpointUrl = string.Format("net.tcp://{0}:{1}/P2PService", ep.Address, ep.Port);
                        NetTcpBinding binding = new NetTcpBinding();
                        binding.Security.Mode = SecurityMode.None;
                        IP2PService serviceProxy = ChannelFactory<IP2PService>.CreateChannel(
                            binding, new EndpointAddress(endpointUrl));
                        PeerList.Items.Add(
                           new PeerEntry
                           {
                               PeerName = peer.PeerName,
                               ServiceProxy = serviceProxy,
                               DisplayString = serviceProxy.GetName(),
                               ButtonsEnabled = true
                           });
                    }
                    catch (EndpointNotFoundException)
                    {
                        PeerList.Items.Add(
                           new PeerEntry
                           {
                               PeerName = peer.PeerName,
                               DisplayString = "Неизвестный пир",
                               ButtonsEnabled = false
                           });
                    }
                }
            }
        }

        private void PeerList_Click(object sender, RoutedEventArgs e)
        {
            // Убедимся, что пользователь щелкнул по кнопке с именем MessageButton
            if (((Button)e.OriginalSource).Name == "MessageButton")
            {
                // Получение пира и прокси, для отправки сообщения
                PeerEntry peerEntry = ((Button)e.OriginalSource).DataContext as PeerEntry;
                if (peerEntry != null && peerEntry.ServiceProxy != null)
                {
                    try
                    {
                        peerEntry.ServiceProxy.SendMessage("Привет друг!", ConfigurationManager.AppSettings["username"]);
                    }
                    catch (CommunicationException)
                    {

                    }
                }
            }
        }

        internal void DisplayMessage(string message, string from)
        {
            // Показать полученное сообщение (вызывается из службы WCF)
            MessageBox.Show(this, message, string.Format("Сообщение от {0}", from),
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

}
