namespace Novolis.Language.Text;

/// <summary>Source location combining source text and range.</summary>
public readonly record struct TextLocation(SourceText Source, TextSpan Span)
{
    /// <summary>Zero-based line index.</summary>
    public int Line => Source.GetLineIndex(Span.Start);

    /// <summary>Text covered by the location.</summary>
    public string Text => Source.Slice(Span);

    /// <summary>Creates a location for an offset and length.</summary>
    public static TextLocation Create(SourceText source, int start, int length) =>
        new(source, new TextSpan(start, length));
}
