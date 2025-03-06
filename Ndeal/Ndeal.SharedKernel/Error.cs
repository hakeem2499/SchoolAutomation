namespace SharedKernel;

public record Error
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);
    public static readonly Error NullValue = new(
        "General.NullValue",
        "Null Value was provided",
        ErrorType.Failure
    );
    private string v1;
    private string v2;

    public Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
    }

    public Error(string v1, string v2)
    {
        this.v1 = v1;
        this.v2 = v2;
    }

    public string Code { get; }
    public string Description { get; }
    public ErrorType Type { get; }

    public static Error Failure(string code, string description) =>
        new Error(code, description, ErrorType.Failure);

    public static Error Problem(string code, string description) =>
        new(code, description, ErrorType.Problem);

    public static Error NotFound(string code, string description) =>
        new(code, description, ErrorType.NotFound);

    public static Error Conflict(string code, string description) =>
        new(code, description, ErrorType.Conflict);
}
