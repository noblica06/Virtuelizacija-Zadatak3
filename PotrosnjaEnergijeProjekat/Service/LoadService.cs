using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class LoadService : ILoadService
    {
        
        private static XMLDatabase xmld = new XMLDatabase();
        public void AddNewLoads()
        {
            Random rand = new Random();
            int id = XMLDatabase.LastLoadID() + 1;
            double mv = rand.NextDouble() * 10000;
            double fv = rand.NextDouble() * 200 + mv - 100;

            Load load = new Load(id, DateTime.Now, fv, mv);
            

            XMLDatabase.AddLoadElement(load);
        }

        public LoadDTO SendByDate(DateTime requestDate)
        {
            List<Load> ret = new List<Load>();

            LoadDTO loadDTO = new LoadDTO();

            foreach(Load l in IMDatabase.LoadDatabase.Values)
            {
                if(l.Timestamp.Date == requestDate.Date)
                {
                    ret.Add(l);
                }
            }
            List<Load> xmlSpisak = new List<Load>();
            if (ret.Count < 1)
            {
                xmlSpisak = XMLDatabase.ReadLoadElements(requestDate);
                if (xmlSpisak.Count < 1)
                {
                    //Audit error
                    
                    loadDTO.Loads = xmlSpisak;
                    loadDTO.Message.Id = XMLDatabase.LastAuditID() + 1;
                    loadDTO.Message.MessageType = MessageType.Error;
                    loadDTO.Message.Message = "Ne postoji merenje sa zadatim datumom.";
                    loadDTO.Message.Timestamp = DateTime.Now;
                    XMLDatabase.SaveAudit(loadDTO.Message);
                    return loadDTO;
                }
                else
                {
                    loadDTO.Loads = xmlSpisak;
                    loadDTO.Message.Id = XMLDatabase.LastAuditID() + 1;
                    loadDTO.Message.MessageType = MessageType.Info;
                    loadDTO.Message.Message = "Uspesno pronadjena merenja";
                    loadDTO.Message.Timestamp = DateTime.Now;
                    foreach (Load l in xmlSpisak)
                    {
                        IMDatabase.LoadDatabase.Add(l.Id, l);
                        IMDatabase.LoadArrivalBase.Add(l.Id, DateTime.Now);
                    }
                    XMLDatabase.SaveAudit(loadDTO.Message);
                    return loadDTO;
                }

            }
            else
            {
                
                loadDTO.Loads = ret;
                loadDTO.Message.Id = XMLDatabase.LastAuditID() + 1;
                loadDTO.Message.MessageType = MessageType.Info;
                loadDTO.Message.Message = "Uspesno pronadjena merenja";
                loadDTO.Message.Timestamp = DateTime.Now;
                XMLDatabase.SaveAudit(loadDTO.Message);
                return loadDTO;
            }

            
        }
    }
}
