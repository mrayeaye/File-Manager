using Filer.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Web;



namespace Filer.Data.Services
{
    public class IFileData : IFile
    {
        List<mFile> files = new List<mFile>();

        public IFileData()
        {

            string[] fis = Directory.GetFiles("C:/Users/Ahmed Saad/source/repos/File-Manager/FM/Filer.Data/Data");
            foreach (var file in fis)
            {
                FileInfo fi = new FileInfo(file);
                
                //files = new List<mFile>()
                //{
                //    new mFile{Name = fi.Name,Size = fi.Length}
                //};

                mFile f = new mFile();
                f.Name = fi.Name; f.Size = fi.Length;
                files.Add(f);
            }
        }
        public IEnumerable<mFile> getAllFiles()
        {
            
            return files;
        }
    }
}
