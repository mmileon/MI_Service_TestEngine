using System.Runtime.Serialization;

namespace MI.Service.TestEngine.Domain.Entities.Languages;

/// <summary>
/// Provides model for list of language keys.
/// </summary>
[DataContract]
public class LanguageKeyReference
{
    /// <summary>
    /// Gets or sets system name for entity.
    /// </summary>
    [DataMember(Name = "systemName")]
    public string SystemName { get; set; }

    /// <summary>
    /// Gets or sets display name for entity.
    /// </summary>
    [DataMember(Name = "displayName")]
    public string DisplayName { get; set; }
}