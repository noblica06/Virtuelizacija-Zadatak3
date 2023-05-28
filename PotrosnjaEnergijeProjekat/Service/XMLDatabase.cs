using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Service
{
    internal class XMLDatabase : IDisposable
    {
        private bool disposedValue;

        public static void AddLoadElement(Load load)
        {
            if (!File.Exists("Load.xml"))
            {
                using(XmlWriter xw = XmlWriter.Create("Load.xml"))
                {
                    xw.WriteStartDocument();
                    xw.WriteStartElement("rows");
                    xw.WriteEndElement();
                    xw.WriteEndDocument();
                    xw.Flush();
                    xw.Close();
                    XDocument xd = XDocument.Load("Load.xml");
                    var rows = xd.Element("rows");
                    var row = new XElement("row");
                    var IdElement = new XElement("ID", load.Id);
                    var TsElement = new XElement("TIME_STAMP", load.Timestamp);
                    var FVElement = new XElement("FORECAST_VALUE", load.ForecastValue);
                    var MVElement = new XElement("MEASURED_VALUE", load.MeasuredValue);
                    row.Add(IdElement);
                    row.Add(TsElement);
                    row.Add(FVElement);
                    row.Add(MVElement);
                    rows.Add(row);
                    xd.Save("Load.xml");
                }
            }
            else
            {
                XDocument xd = XDocument.Load("Load.xml");
                var rows = xd.Element("rows");
                var row = new XElement("row");
                var IdElement = new XElement("ID", load.Id);
                var TsElement = new XElement("TIME_STAMP", load.Timestamp);
                var FVElement = new XElement("FORECAST_VALUE", load.ForecastValue);
                var MVElement = new XElement("MEASURED_VALUE", load.MeasuredValue);
                row.Add(IdElement);
                row.Add(TsElement);
                row.Add(FVElement);
                row.Add(MVElement);
                rows.Add(row);
                xd.Save("Load.xml");
            }
        }

        public static List<Load> ReadLoadElements(DateTime time)
        {
            XDocument xd = XDocument.Load("Load.xml");
            var rows = xd.Element("rows");
           
            List<Load> ret = new List<Load>();
            
            Load l = new Load();
            foreach(XElement xe in rows.Descendants("row"))
            {
                List<XElement> le = xe.Descendants().ToList();
                DateTime timestamp = DateTime.Parse(le[1].Value);
                if (timestamp.Date == time.Date)
                {
                    int id = Int32.Parse(le[0].Value);
                    double forecast = double.Parse(le[2].Value);
                    double measured = double.Parse(le[3].Value);
                     l = new Load(id, timestamp, forecast, measured);
                    ret.Add(l);
                }
                
            }
            
            return ret;
        }

        public static void SaveAudit(Audit audit)
        {
            if (!File.Exists("Audit.xml"))
            {
                using (XmlWriter xw = XmlWriter.Create("Audit.xml"))
                {
                    xw.WriteStartDocument();
                    xw.WriteStartElement("STAVKE");
                    xw.WriteEndElement();
                    xw.WriteEndDocument();
                    xw.Flush();
                    xw.Close();
                    XDocument xd = XDocument.Load("Audit.xml");
                    var stavke = xd.Element("STAVKE");
                    var row = new XElement("row");
                    var idElement = new XElement("ID", audit.Id);
                    var TsElement = new XElement("TIME_STAMP", audit.Timestamp);
                    var MTElement = new XElement("MESSAGE_TYPE", audit.MessageType);
                    var MessageElement = new XElement("MESSAGE", audit.Message);
                    row.Add(idElement);
                    row.Add(TsElement);
                    row.Add(MTElement);
                    row.Add(MessageElement);
                    stavke.Add(row);
                    xd.Save("Audit.xml");
                    
                }
            }
            else
            {
                XDocument xd = XDocument.Load("Audit.xml");
                var stavke = xd.Element("STAVKE");
                var row = new XElement("row");
                var idElement = new XElement("ID", audit.Id);
                var TsElement = new XElement("TIME_STAMP", audit.Timestamp);
                var MTElement = new XElement("MESSAGE_TYPE", audit.MessageType);
                var MessageElement = new XElement("MESSAGE", audit.Message);
                row.Add(idElement);
                row.Add(TsElement);
                row.Add(MTElement);
                row.Add(MessageElement);
                stavke.Add(row);
                xd.Save("Audit.xml");
            }
        }

        public static int LastAuditID()
        {
            
            if (!File.Exists("Audit.xml"))
            {
                return 100;
            }
            else
            {
                XDocument xd = XDocument.Load("Audit.xml");
                var stavke = xd.Element("STAVKE");
                List<XElement> list = stavke.Descendants("row").ToList();
                List<XElement> list2 = list[list.Count - 1].Descendants().ToList();
                return Int32.Parse(list2[0].Value);
            }
        }
        public static int LastLoadID()
        {
            if (!File.Exists("Load.xml"))
            {
                return 0;
            }
            else
            {
                XDocument xd = XDocument.Load("Load.xml");
                var stavke = xd.Element("rows");
                List<XElement> list = stavke.Descendants("row").ToList();
                List<XElement> list2 = list[list.Count - 1].Descendants().ToList();
                return Int32.Parse(list2[0].Value);
                
            }
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
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
         ~XMLDatabase()
         {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
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
