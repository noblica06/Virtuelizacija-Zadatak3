using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class IMDatabase : IDisposable
    {
        public static Dictionary<int, Load> LoadDatabase = new Dictionary<int, Load>();
        public static Dictionary<int, DateTime> LoadArrivalBase = new Dictionary<int, DateTime>();
        
        private bool disposedValue;

        public static List<Load> ReadByDate(DateTime date)
        {
            List<Load> ret = new List<Load>();
            foreach(Load l in LoadDatabase.Values)
            {
                if(l.Timestamp == date)
                {
                    ret.Add(l);
                }
            }
            return ret;
        }

       

        public IMDatabase()
        {
            LoadDatabase = new Dictionary<int, Load>();
            
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)

                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                LoadDatabase.Clear();
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
         ~IMDatabase()
         {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
         }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
