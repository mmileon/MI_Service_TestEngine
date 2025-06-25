using MI.Service.Shared.ExceptionHandling.Exceptions.ValidationExceptions;

namespace MI.Service.TestEngine.Shared.Exceptions;

/// <summary>
/// Provides exception data conflict in system.
/// </summary>
/// <seealso cref="ConflictException" />
[Serializable]
public class ArgumentMissingException : BusinessValidationException, ICustomException
{
    /// <summary>
    /// Gets the error code.
    /// </summary>
    /// <value>
    /// The error code.
    /// </value>
    public int? ErrorCode { get; private set; }

    /// <summary>
    /// Initializes a new instance of the ServiceMappingNotFoundException class.
    /// </summary>
    public ArgumentMissingException()
        : base("Entity")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
    /// </summary>
    /// <param name="entity">The entity.</param>
    public ArgumentMissingException(string entity)
        : base(entity)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
    /// </summary>
    /// <param name="errorCode">The error code.</param>
    /// <param name="entity">The entity.</param>
    public ArgumentMissingException(int errorCode, string entity)
        : base(entity)
    {
        this.ErrorCode = errorCode;
    }
}
