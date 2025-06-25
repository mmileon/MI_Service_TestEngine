using System.Runtime.Serialization;

namespace MI.Service.TestEngine.Contracts;

/// <summary>
/// The error data transfer object.
/// </summary>
[DataContract]
public sealed class ErrorResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorResponse"/> class.
    /// </summary>
    public ErrorResponse()
    {
        Messages = new List<string>();
    }

    /// <summary>
    /// Gets or sets the error messages.
    /// </summary>
    /// <value>
    /// The error messages.
    /// </value>
    [DataMember(Name = "message")]
    public IReadOnlyCollection<string> Messages { get; set; }

    /// <summary>
    /// Gets or sets the error messages.
    /// </summary>
    /// <value>
    /// The error messages.
    /// </value>
    [DataMember(Name = "code", EmitDefaultValue = false)]
    public int? Code { get; set; }

    /// <summary>
    /// Gets or sets the developer error message.
    /// Should not be set in production.
    /// </summary>
    /// <value>
    /// The developer error message.
    /// </value>
    [DataMember(Name = "developerMessage", EmitDefaultValue = false)]
    public string DeveloperMessage { get; set; } = null;
}