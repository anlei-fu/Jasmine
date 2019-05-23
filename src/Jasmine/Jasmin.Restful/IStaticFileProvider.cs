using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Jasmine.Restful
{
    public   interface IStaticFileProvider
    {
      Stream GetAsync(string path);
        Task WriteAsync(string path, Stream stream);
        Task RenameAsync(string path1, string newName);
        Task CopyToAsync(string path1, string path2);
        Task DeleteAsync(params string[] path);
        Task<IList<string>> ListFolderAsync();
        Task<FileInfo> GetFileInfoAsync();
        Task<DirectoryInfo> GetDirectorInfoAsync();
    }
}
