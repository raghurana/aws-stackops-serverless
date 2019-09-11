using System.IO;
using System.Reflection;

namespace StackopsCore.Utils
{
    public static class FileUtils
    {
        public static string GetAbsolutePathFromCurrentDirectory(string fileName)
        {
            var assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var configJsonPath   = Path.Combine(assemblyLocation, fileName);

            if(!File.Exists(configJsonPath))
                throw new FileNotFoundException($"Could not locate file at: {configJsonPath}.");

            return configJsonPath;
        }
    }
}