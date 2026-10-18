namespace Novolis.Language.Syntax;

/// <summary>Neutral token categories shared by language implementations.</summary>
public enum LanguageTokenKind
{
    /// <summary>An unclassified token.</summary>
    Unknown,
    /// <summary>The end-of-file marker.</summary>
    EndOfFile,
    /// <summary>An identifier token.</summary>
    Identifier,
    /// <summary>A numeric literal.</summary>
    Number,
    /// <summary>A string literal.</summary>
    String,
    /// <summary>A language keyword.</summary>
    Keyword,
    /// <summary>An operator token.</summary>
    Operator,
    /// <summary>A punctuation token.</summary>
    Punctuation,
    /// <summary>A comment token.</summary>
    Comment,
}
