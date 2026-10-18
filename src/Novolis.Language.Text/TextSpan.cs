namespace Novolis.Language.Text;

/// <summary>A half-open source range.</summary>
public readonly record struct TextSpan(int Start, int Length)
{
    /// <summary>Exclusive end offset.</summary>
    public int End => Start + Length;

    /// <summary>Creates a span covering two positions.</summary>
    public static TextSpan FromBounds(int start, int end) => new(start, end - start);
}
