using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MI.Service.TestEngine.Initializers;

/// <inheritdoc />
public class EntityReader : IEntityReader
{
    private const string Json = ".json";
    private const string Yaml = ".yaml";
    private const string All = "*";

    /// <summary>
    /// Get initial entities from json.
    /// </summary>
    /// <param name="path">Path to json file.</param>
    /// <returns>
    ///   <see cref="InitialEntities" />.
    /// </returns>
    public async Task<InitialEntities> Read(string path)
    {
        var jObject = new JObject();
        var jsonMergeSettings = new JsonMergeSettings
        {
            PropertyNameComparison = StringComparison.InvariantCultureIgnoreCase,
            MergeArrayHandling = MergeArrayHandling.Union,
        };

        var filePaths = GetFilePaths(path);
        foreach (var filePath in filePaths)
        {
            var entity = await GetInitialEntity(filePath);
            if (entity is not null)
            {
                jObject.Merge(entity, jsonMergeSettings);
            }
        }

        return jObject.ToObject<InitialEntities>();
    }

    private static async Task<JObject> GetInitialEntity(string filePath)
    {
        using var reader = new StreamReader(filePath);
        var result = await reader.ReadToEndAsync();

        return JsonConvert.DeserializeObject<JObject>(result, new JsonSerializerSettings());
    }

    private static IEnumerable<string> GetFilePaths(string path)
    {
        return Directory.GetFiles(path, All, SearchOption.AllDirectories)
            .Where(s => s.EndsWith(Json, StringComparison.InvariantCultureIgnoreCase) ||
                        s.EndsWith(Yaml, StringComparison.InvariantCultureIgnoreCase));
    }
}
