using Filer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filer.Data.Services
{
    public interface IFile
    {
        IEnumerable<mFile> getAllFiles();
        IEnumerable<mDirectory> getAllDirectories();

        void addFiles(mFile file);
    }
}
