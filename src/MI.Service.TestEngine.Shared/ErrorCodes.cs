namespace MI.Service.TestEngine.Shared;

/// <summary>
/// Common error eodes
/// </summary>
public class ErrorCodes
{
    /// <summary>
    /// The bad request exception
    /// </summary>
    public const int BadRequestException = 4000;

    /// <summary>
    /// The not found exception
    /// </summary>
    public const int NotFoundException = 4040;

    /// <summary>
    /// The conflict exception
    /// </summary>
    public const int ConflictException = 4090;

    /// <summary>
    /// The incompatible version exception
    /// </summary>
    public const int IncompatibleVersionException = 4260;

    /// <summary>
    /// The not valid time format
    /// </summary>
    public const int NotValidTimeFormat = 4001;

    /// <summary>
    /// The invalid columns
    /// </summary>
    public const int InvalidColumns = 4002;

    /// <summary>
    /// The operator not found
    /// </summary>
    public const int OperatorNotFound = 4003;

    /// <summary>
    /// The identifier not found
    /// </summary>
    public const int IdentifierNotFound = 4004;

    /// <summary>
    /// The identifier not valid
    /// </summary>
    public const int IdentifierNotValid = 4005;

    /// <summary>
    /// The operator not valid
    /// </summary>
    public const int OperatorNotValid = 4006;

    /// <summary>
    /// The operator missing
    /// </summary>
    public const int OperatorMissing = 4007;

    /// <summary>
    /// The database settings required
    /// </summary>
    public const int DbSettingsRequired = 4008;

    /// <summary>
    /// The invalid json
    /// </summary>
    public const int InvalidJson = 4009;

    /// <summary>
    /// The invalid name length
    /// </summary>
    public const int InvalidNameLength = 4011;

    /// <summary>
    /// The application not found.
    /// </summary>
    public const int ApplicationNotFound = 100017;
    
}
