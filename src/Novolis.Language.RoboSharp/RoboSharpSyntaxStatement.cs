using Novolis.Language.Syntax;
using Novolis.Language.Text;

namespace Novolis.Language.RoboSharp;

/// <summary>Parsed RoboSharp statement before semantic binding.</summary>
public sealed record RoboSharpSyntaxStatement(
    string Command,
    string Operand,
    TextLocation Location,
    IReadOnlyList<LanguageToken> Tokens);
