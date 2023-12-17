using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groovy.Services.Helpers
{
    public class AudioBuilder
    {
        public async Task<Audio> FromBundledToLocalAsync(string fileName)
        {
            string fulpath = await CopyFileToAppDataDirectory(fileName);
            return new Audio(fulpath);
        }

        public async Task<string> CopyFileToAppDataDirectory(string filename)
        {
            using Stream inputStream = await FileSystem.Current.OpenAppPackageFileAsync(filename);

            string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, filename);

            using FileStream outputStream = File.Create(targetFile);
            await inputStream.CopyToAsync(outputStream);

            return outputStream.Name;
        }
    }
}
