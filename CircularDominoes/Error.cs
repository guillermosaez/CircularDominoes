namespace CircularDominoes;

public readonly record struct Error(string Description)
{
    public static readonly Error None = new(string.Empty);
}