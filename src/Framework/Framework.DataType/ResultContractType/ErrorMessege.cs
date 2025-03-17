namespace Framework.DataType;

public class ErrorMessage
{
    public ErrorMessage(string message, ErrorType errorType, Exception? exception)
    {
        Message = message;
        ErrorType = errorType;
        Exception = exception;
    }

    public string Message { get; set; }
    public ErrorType ErrorType { get; set; }
    public Exception? Exception { get; set; }
}

public enum ErrorType
{
    // Success
    None = 0,

    // General Errors
    UnknownError = 1,
    InvalidArgument = 2,
    InvalidOperation = 3,
    NullReference = 4,
    OutOfRange = 5,
    Timeout = 6,
    ResourceNotFound = 7,
    InvalidState = 8,
    InternalError = 9,

    // Network Errors
    NetworkUnavailable = 100,
    ConnectionFailed = 101,
    TimeoutError = 102,
    ServerError = 103,
    RequestTimeout = 104,
    UnauthorizedAccess = 105,

    // File System Errors
    FileNotFound = 200,
    FileAccessDenied = 201,
    FileReadError = 202,
    FileWriteError = 203,
    DirectoryNotFound = 204,

    // Database Errors
    DatabaseConnectionFailed = 300,
    QueryExecutionError = 301,
    DataIntegrityError = 302,
    RecordNotFound = 303,
    DuplicateRecord = 304,

    // Authentication/Authorization Errors
    InvalidCredentials = 400,
    AccountLocked = 401,
    SessionExpired = 402,
    AccessDenied = 403,

    // Validation Errors
    ValidationFailed = 500,
    RequiredFieldMissing = 501,
    InvalidFormat = 502,

    // Application Specific Errors
    FeatureNotImplemented = 600,
    ServiceUnavailable = 601,
    OperationNotSupported = 602,

    // Security Errors
    InvalidToken = 700,
    TokenExpired = 701,
    PermissionDenied = 702,

    // System Errors
    OutOfMemory = 800,
    StackOverflow = 801,
    DiskSpaceLow = 802,
    UnhandledException = 803,
}