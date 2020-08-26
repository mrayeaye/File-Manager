using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filer.Data.Models
{
    public class mDirectory
    {
        public string Name { get; set; }
        public int FilesCount { get; set; }
       public  IEnumerable<mFile> Files { get; set; }
    }
}
