using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using WebCrawlerWPF.Views;

namespace WebCrawlerWPF.P2P
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class P2PService : IP2PService
    {
        private P2P_MainWindow hostReference;
        private string username;

        public P2PService(P2P_MainWindow hostReference, string username)
        {
            this.hostReference = hostReference;
            this.username = username;
        }

        public string GetName()
        {
            return username;
        }

        public void SendMessage(string message, string from)
        {
            hostReference.DisplayMessage(message, from);
        }
    }
}
