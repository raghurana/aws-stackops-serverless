using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public static class StacksFactory
{
    public static IReadOnlyList<Stack> GetStacksFromJsonConfig(string configPath)
    {
        var jsonData = File.ReadAllText(configPath);
        return JsonConvert.DeserializeObject<Stack[]>(jsonData);
    }
}