using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Interface
    {
        public static void  InterfaceMenu(ILoadService proxy)
        {
            string input = "";
            do
            {
                
                switch (input)
                {
                    case "1":
                        //Metoda za trazenje po datumu
                        Console.WriteLine("Unesite datum:");
                        int godina = 0;
                        int mjesec = 0;
                        int dan = 0;
                        try
                        {
                            Console.WriteLine("Unesite godinu:");
                            godina = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Unesite mesec:");
                            mjesec = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Unesite dan:");
                            dan = Int32.Parse(Console.ReadLine());
                        }
                        catch
                        {

                            Console.WriteLine("Nepravilan unos");
                            //Console.WriteLine("Pritisnite bilo koji taster za nastavak...");
                            Console.WriteLine("#############################");
                            Console.WriteLine("1-Pretraga po Datumu");
                            Console.WriteLine("2-Pokreni nova merenja");
                            Console.WriteLine("X-Izlaz");
                            Console.WriteLine("#############################");
                            continue;
                        }
                        
                        
                        DateTime testTime = DateTime.MinValue;
                        
                            testTime = new DateTime(godina, mjesec, dan);
                        
                        
                            LoadDTO dto = proxy.SendByDate(testTime);
                            if(dto.Message.MessageType == MessageType.Error)
                            {
                                
                                Console.WriteLine(dto.Message.Message);
                                Console.WriteLine("#############################");
                            }
                            else
                            {
                                string sourcePath = ConfigurationManager.AppSettings["Source"];
                                string path = sourcePath + "/Load.csv";
                                Console.WriteLine($"Primljeno je {dto.Loads.Count} objekata");
                                string separator = ",";
                                StringBuilder output = new StringBuilder();
                                if (!File.Exists(path))
                                {
                                    File.Create(path).Close();
                                    
                                    string[] header = { "ID", "Date", "Forecast Value", "Measured Value" };
                                    output.AppendLine(string.Join(separator, header));
                                    File.AppendAllText(path, output.ToString());
                                }
                                output = new StringBuilder();
                                
                                foreach (Load l in dto.Loads)
                                {
                                    output = new StringBuilder();
                                    Console.WriteLine(l);

                                    
                                    
                                    string[] newLine = { l.Id.ToString(), l.Timestamp.ToString(), l.ForecastValue.ToString(), l.MeasuredValue.ToString() };
                                    output.AppendLine(string.Join(separator, newLine));
                                    File.AppendAllText(path, output.ToString());
                                }
                                Console.WriteLine($"Upisano {dto.Loads.Count} objekata u datoteku sa putanjom {path}");
                            }
                        

                        break;
                    case "2":
                        
                            //pozovi metodu
                            proxy.AddNewLoads();                           
                        
                        Console.WriteLine("Uspesno je dodato novo merenje");
                        break;
                    case "x":
                        return;
                    default:
                        break;

                        
                }
                
                Console.WriteLine("1-Pretraga po Datumu");
                Console.WriteLine("2-Pokreni nova merenja");
                Console.WriteLine("X-Izlaz");
                Console.WriteLine("#############################");
            }
            while ( (input = Console.ReadLine().ToString().ToLower()) != "x");
        }
    }
}
