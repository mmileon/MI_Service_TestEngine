using MI.Service.Shared.ExceptionHandling.Exceptions.ValidationExceptions;

namespace MI.Service.TestEngine.Shared.Exceptions;

/// <summary>
/// Provides exception data conflict in system.
/// </summary>
/// <seealso cref="ConflictException" />
[Serializable]
public class DataNotFoundException : NoDataException, ICustomException
{
    /// <summary>
    /// Gets the error code.
    /// </summary>
    /// <value>
    /// The error code.
    /// </value>
    public int? ErrorCode { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataNotFoundException"/> class.
    /// </summary>
    public DataNotFoundException()
        : base("Data")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
    /// </summary>
    /// <param name="message">The entity.</param>
    public DataNotFoundException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataNotFoundException"/> class.
    /// </summary>
    /// <param name="errorCode">The error code.</param>
    /// <param name="message">The message.</param>
    public DataNotFoundException(int errorCode, string message)
        : base(message)
    {
        this.ErrorCode = errorCode;
    }
}
