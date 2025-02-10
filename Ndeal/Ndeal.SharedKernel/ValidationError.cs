namespace SharedKernel;

public sealed record ValidationError : Error
{
    public ValidationError(Error[] errors)
        : base(
            "Validation.General",
            "One or More Validation Error Occured",
            ErrorType.Validation
        ) => Errors = errors;

    public Error[] Errors { get; }

    public static ValidationError FromResults(IEnumerable<Result> results) =>
        new(results.Where(result => result.IsFailure).Select(result => result.Error).ToArray());
}
