using Novolis.Language.Text;

namespace Novolis.Language.Syntax;

/// <summary>A lexical token with source location and optional value.</summary>
public sealed record LanguageToken(
    LanguageTokenKind Kind,
    string Text,
    TextLocation Location,
    object? Value = null);
