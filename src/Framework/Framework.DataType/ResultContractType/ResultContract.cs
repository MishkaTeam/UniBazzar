namespace Framework.DataType;

public class ResultContract
{
    public bool IsSuccessful { get; set; }
    public ErrorMessage? ErrorMessage { get; private set; }
    public string[]? InformationMessages { get; private set; }
    public string? SuccessMessage { get; private set; }
    public DateTime Timestamp { get; private set; }
    public string? RequestId { get; set; }
    public string? DebugInfo { get; set; }
    public string? CorrelationId { get; set; }

    protected ResultContract(bool isSuccessful)
    {
        Timestamp = DateTime.Now;
        IsSuccessful = isSuccessful;
    }

    public void SetSuccessMessage(string successMessage)
    {
        SuccessMessage = successMessage;
        IsSuccessful = true;
    }

    public void SetErrorMessage(ErrorMessage errorMessage)
    {
        ErrorMessage = errorMessage;
        IsSuccessful = false;
    }

    public void AddInformationMessages(string[] informationMessages)
    {
        InformationMessages = informationMessages;
    }

    public static implicit operator ResultContract(bool result)
    {
        if (result)
            return new ResultContract(true);
        else
            return new ResultContract(false);
    }

    public static implicit operator ResultContract(Exception? exception)
    {
        var result = new ResultContract(false);
        result.ErrorMessage = new ErrorMessage(exception?.Message ?? "", ErrorType.InternalError,exception);
        return result;
    }
}

public class ResultContract<TData> : ResultContract
{
    protected ResultContract(TData? data) : base(true)
    {
        Data = data;
    }

    private TData? Data { get; set; }

    public static implicit operator ResultContract<TData>(TData data) => new(default(TData));
}