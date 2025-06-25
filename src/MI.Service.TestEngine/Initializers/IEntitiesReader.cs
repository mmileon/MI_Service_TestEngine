namespace MI.Service.TestEngine.Initializers;

/// <summary>
/// Json parser.
/// </summary>
public interface IEntityReader
{
    /// <summary>
    /// Get initial entities from json.
    /// </summary>
    /// <param name="path">Path to json file.</param>
    /// <returns><see cref="InitialEntities"/>.</returns>
    Task<InitialEntities> Read(string path);
}