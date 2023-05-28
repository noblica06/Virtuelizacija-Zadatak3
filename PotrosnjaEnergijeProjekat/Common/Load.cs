using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class Load :IDisposable
    {
        int id;
        DateTime timestamp;
        double forecastValue;
        double measuredValue;
        private bool disposedValue;

        public Load()
        {
            id = 0;
            timestamp = DateTime.Now;
            forecastValue = 0;
            measuredValue = 0;
        }
        public Load(int id, DateTime timestamp, double forecastValue, double measuredValue)
        {
            this.id = id;
            this.timestamp = timestamp;
            this.forecastValue = forecastValue;
            this.measuredValue = measuredValue;
        }

        

        [DataMember]
        public int Id { get => id; set => id = value; }
        [DataMember]
        public DateTime Timestamp { get => timestamp; set => timestamp = value; }
        [DataMember]
        public double ForecastValue { get => forecastValue; set => forecastValue = value; }
        [DataMember]
        public double MeasuredValue { get => measuredValue; set => measuredValue = value; }

        public override string ToString()
        {
            return $"Id:{id}, Date:{timestamp}, Measured Value: {measuredValue}, Forecasted Value: {forecastValue}";    
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
                id = 0;
                timestamp = DateTime.MinValue;
                measuredValue = 0;
                forecastValue = 0;


                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
         ~Load()
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
