namespace CircularDominoes;

public readonly record struct Domino
{
    public int Left { get; }
    public int Right { get; }
    
    public Domino(int left, int right)
    {
        Left = left;
        Right = right;
    }

    public Domino Flip()
    {
        return new(Right, Left);
    }

    public override string ToString()
    {
        return $"[{Left}|{Right}]";
    }
}