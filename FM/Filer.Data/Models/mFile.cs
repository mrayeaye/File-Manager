using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filer.Data.Models
{
    public class mFile
    {
       
        public string Name { get; set; }
        public double Size { get; set; }
        public int Dir_Id { get; set; }
       
        public Byte[] bytes { get; set; }
    }
}
