using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MI.Service.TestEngine.Domain.Entities.Languages;

/// <summary>
/// Provides model for language configuration.
/// </summary>
[DataContract]
public class LanguageConfiguration
{
    /// <summary>
    /// Gets or sets system name for language.
    /// </summary>
    [Required]
    [DataMember(Name = "systemName")]
    public string SystemName { get; set; }

    /// <summary>
    /// Gets or sets display name for language.
    /// </summary>
    [Required]
    [DataMember(Name = "displayName")]
    public string DisplayName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this language is default.
    /// </summary>
    [Required]
    [DataMember(Name = "isDefault")]
    public bool IsDefault { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this language is system.
    /// </summary>
    [Required]
    [DataMember(Name = "isSystem")]
    public bool IsSystem { get; set; }
}