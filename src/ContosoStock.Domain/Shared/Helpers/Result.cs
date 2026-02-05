namespace ContosoStock.Domain.Shared.Helpers;

public class Result(bool isSuccess, string error)
{
    public bool IsSuccess { get; } = isSuccess;
    public bool IsFailure => !IsSuccess;
    public string Error { get; } = error;

    public static Result Success() => new(true, string.Empty);
    public static Result Failure(string error) => new(false, error);
}