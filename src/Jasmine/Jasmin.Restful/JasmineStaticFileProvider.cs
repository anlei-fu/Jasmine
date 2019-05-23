using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Jasmine.Restful
{
    public class JasmineStaticFileProvider : IStaticFileProvider
    {
        public Task CopyToAsync(string path1, string path2)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(params string[] path)
        {
            throw new System.NotImplementedException();
        }

        public Stream GetAsync(string path)
        {
            if(File.Exists(path))
            {
                return new FileStream(path, FileMode.Open, FileAccess.Read);
            }
            else
            {
                return null;
            }
        }

        public Task<DirectoryInfo> GetDirectorInfoAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<FileInfo> GetFileInfoAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<string>> ListFolderAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task RenameAsync(string path1, string newName)
        {
            throw new System.NotImplementedException();
        }

        public Task WriteAsync(string path, Stream stream)
        {
            throw new System.NotImplementedException();
        }
    }
}
