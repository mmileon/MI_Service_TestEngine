using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MI.Service.TestEngine.Business.Applications.Models;

/// <summary>
/// Application model.
/// </summary>
[DataContract]
public class ApplicationModuleModel
{
    /// <summary>
    /// Gets or sets application name.
    /// </summary>
    [Required]
    [StringLength(255)]
    [DataMember(Name = "application")]
    public string Application { get; set; }

    /// <summary>
    /// Gets or sets rule module name.
    /// </summary>
    [DataMember(Name = "modules")]
    public List<string> Modules { get; set; }
}