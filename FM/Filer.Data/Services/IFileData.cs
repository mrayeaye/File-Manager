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
        List<mDirectory> directories = new List<mDirectory>();
        public IFileData()
        {

            string[] fis = Directory.GetFiles("C:/Users/Ahmed Saad/source/repos/File-Manager/FM/Filer.Data/Data");
            string[] dirs = Directory.GetDirectories("C:/Users/Ahmed Saad/source/repos/File-Manager/FM/Filer.Data/Data");

            // Logic to get root files in "Data" folder
            foreach (var file in fis)
            {
                FileInfo fi = new FileInfo(file);
                mFile f = new mFile();
                f.Name = fi.Name; f.Size = fi.Length; f.Dir = fi.DirectoryName;
                files.Add(f);
            }
            mDirectory root = new mDirectory();
            root.Name = "Data"; root.FilesCount = files.Count; root.Files = files;
            directories.Add(root);


            // Logic to get all folders and files inside
            foreach (var dir in dirs)
            {
                DirectoryInfo di = new DirectoryInfo(dir);
                string[] dir_files = Directory.GetFiles($"C:/Users/Ahmed Saad/source/repos/File-Manager/FM/Filer.Data/Data/{di.Name}");
                mDirectory d = new mDirectory();
                List<mFile> dir_files_list = new List<mFile>();
                foreach (var file in dir_files)
                {
                    FileInfo fi = new FileInfo(file);
                    mFile f = new mFile();
                    f.Name = fi.Name; f.Size = fi.Length; f.Dir = fi.DirectoryName;
                    dir_files_list.Add(f);
                }             
                d.Name = di.Name; d.FilesCount = di.GetFiles().Length; d.Files = dir_files_list;
                directories.Add(d);
            }
        }

        public IEnumerable<mDirectory> getAllDirectories()
        {
            return directories;
        }

        public IEnumerable<mFile> getAllFiles()
        {
            
            return files;
        }
    }
}
