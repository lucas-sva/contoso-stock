namespace ContosoStock.Domain.Shared.Helpers;

public class Result
{
    protected Result(bool isSuccess, string error)
    {
        if (isSuccess && error != string.Empty || !isSuccess && error == string.Empty)
            throw new InvalidOperationException();

        IsSuccess = isSuccess;
        Error = error;
    }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; }
    
    public static Result Success() => new(true, string.Empty);
    public static Result Failure(string error) => new(false, error);
    
    public static Result<T> Success<T>(T value) => new(value, true, string.Empty);
    public static Result<T> Failure<T>(string error) => new(default, false, error);
}

public class Result<T> : Result
{
    private readonly T? _value;
    
    internal Result(T? value, bool isSuccess, string error) : base(isSuccess, error)
    {
        _value = value;
    }
    
    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("O valor de um resultado de falha não pode ser acessado.");

    public static implicit operator Result<T>(T value) => Success(value);
}