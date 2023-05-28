using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<ILoadService> servisLoad = new ChannelFactory<ILoadService>("ServiceLoad");

            ILoadService proxy = servisLoad.CreateChannel();

            

            Interface.InterfaceMenu(proxy);
        }
    }
}
