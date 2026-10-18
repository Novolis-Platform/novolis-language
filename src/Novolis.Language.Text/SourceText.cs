namespace Novolis.Language.Text;

/// <summary>Immutable source text with line lookup support.</summary>
public sealed class SourceText
{
    private readonly string[] _lines;

    /// <summary>Creates source text from a string.</summary>
    public SourceText(string text)
    {
        Text = text ?? throw new ArgumentNullException(nameof(text));
        _lines = text.Replace("\r\n", "\n", StringComparison.Ordinal).Split('\n');
    }

    /// <summary>Complete source text.</summary>
    public string Text { get; }

    /// <summary>Source length in UTF-16 code units.</summary>
    public int Length => Text.Length;

    /// <summary>Number of source lines.</summary>
    public int LineCount => _lines.Length;

    /// <summary>Returns a line by zero-based index.</summary>
    public string GetLine(int index) => _lines[index];

    /// <summary>Returns a source slice.</summary>
    public string Slice(TextSpan span) => Text.Substring(span.Start, span.Length);

    /// <summary>Gets the line containing an offset.</summary>
    public int GetLineIndex(int position)
    {
        var offset = 0;
        for (var index = 0; index < _lines.Length; index++)
        {
            var end = offset + _lines[index].Length;
            if (position <= end)
            {
                return index;
            }

            offset = end + 1;
        }

        return _lines.Length - 1;
    }
}
