using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class LoadDTO
    {
        public List<Load> Loads { get; set; }
        public Audit Message { get; set; }

        public LoadDTO()
        {
            Loads = new List<Load>();
            Message = new Audit();
        }
    }
}
