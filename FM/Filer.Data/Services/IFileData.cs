using Filer.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Filer.Data.Services
{
    public class IFileData : IFile
    {
        List<mFile> files;

        public IFileData()
        {

            //string[] fis = Directory.GetFiles("~/Data");
            //foreach (var file in fis)
            //{
            //    FileInfo fi = new FileInfo(file);
            //    mFile f = new mFile();
            //    f.Name = fi.Name;
            //    f.Size = fi.Length;
            //    files = new List<mFile>()
            //    {
            //        new mFile{Name = fi.Name,Size = fi.Length}
            //    };
            //}
        }
        public IEnumerable<mFile> getAllFiles()
        {
            files = new List<mFile>()
                {
                    new mFile{Name = "ss",Size = 333}
                };
            return files;
        }
    }
}
