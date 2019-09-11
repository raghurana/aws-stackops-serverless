using System.IO;
using Newtonsoft.Json;
using StackopsCore.Models;

namespace StackopsCore.Factories
{
    public static class StackFactory
    {
        public static Stack[] CreateStacksFromJson(string configPath)
        {
            var jsonData = File.ReadAllText(configPath);
            return JsonConvert.DeserializeObject<Stack[]>(jsonData);
        }
    }
}