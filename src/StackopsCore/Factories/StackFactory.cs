using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using StackopsCore.Models;

namespace StackopsCore.Factories
{
    public static class StackFactory
    {
        public static IList<Stack> CreateStacksFromJson(string configPath)
        {
            var jsonData = File.ReadAllText(configPath);
            return JsonConvert.DeserializeObject<Stack[]>(jsonData);
        }
    }
}