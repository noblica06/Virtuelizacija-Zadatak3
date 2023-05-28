using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service
{
    internal class Program
    {
        public static Publisher pub = new Publisher();
        public static Subscriber sub;
        static void Main(string[] args)
        {
            XMLDatabase xd = new XMLDatabase();
            //xd.AddLoadElement(new Load(), "Load.xml");

            //CreateThread();
            using (ServiceHost host = new ServiceHost(typeof(LoadService)))
            {
                
                host.Open();

                CreateThread();
                Console.WriteLine("Servis je uspesno pokrenut ");
                Console.ReadKey();
                host.Close();
            }

        }
        private static void CreateThread()
        {
            sub = new Subscriber("1", pub);
            var backgroundThread = new Thread(() => {

                while (true)
                {
                    Thread.Sleep(1000);
                    pub.DoSomething();
                }
            });
            
            backgroundThread.Start();
        }
    }
    public class CustomEventArgs : EventArgs
    {
        public CustomEventArgs(DateTime ts)
        {
            Timestamp = ts;
        }
        public DateTime Timestamp { get; set; }
    }
    public class Publisher
    {
        public event EventHandler<CustomEventArgs> RaiseCustomEvent;

        public void DoSomething()
        {
            OnRaiseCustomEvent(new CustomEventArgs(DateTime.Now));
        }

        public virtual void OnRaiseCustomEvent(CustomEventArgs e)
        {
            EventHandler<CustomEventArgs> raiseEvent = RaiseCustomEvent;
            if(raiseEvent != null)
            {
                e.Timestamp = DateTime.Now;
                raiseEvent(this, e);
            }
        }
    }
    public class Subscriber
    {
        private readonly string _id;

        public Subscriber(string id, Publisher pub)
        {
            _id = id;
            pub.RaiseCustomEvent += HandleCustomEvent;
        }

        void HandleCustomEvent(object sender, CustomEventArgs e)
        {
            
            DateTime date = e.Timestamp;
            List<int> ObjectsToBeRemoved = new List<int>();
            foreach(KeyValuePair<int, DateTime> t in IMDatabase.LoadArrivalBase)
            {
                int time = Int32.Parse(ConfigurationManager.AppSettings["DataTimeout"]);
                if ((date - t.Value) >= new TimeSpan(0,0,time))
                {
                    Console.WriteLine($"Uklonjen Load Id: {IMDatabase.LoadDatabase[t.Key]}");
                    ObjectsToBeRemoved.Add(t.Key);
                    
                }
            }
            foreach(int i in ObjectsToBeRemoved)
            {
                IMDatabase.LoadDatabase.Remove(i);
                IMDatabase.LoadArrivalBase.Remove(i);
            }
        }
    }
}
