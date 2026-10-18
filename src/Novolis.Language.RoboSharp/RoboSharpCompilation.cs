using Novolis.Language.Diagnostics;
using Novolis.Language.Syntax;

namespace Novolis.Language.RoboSharp;

/// <summary>All inspectable artifacts produced by a RoboSharp compilation.</summary>
public sealed record RoboSharpCompilation(
    SyntaxTree SyntaxTree,
    RoboSharpProgram? Program,
    IReadOnlyList<RoboInstruction> TeachingInstructions,
    IReadOnlyList<LanguageDiagnostic> Diagnostics)
{
    /// <summary>Typed parser-stage artifact for RoboSharp tooling.</summary>
    public RoboSharpSyntaxTree? ParsedSyntax { get; init; }

    /// <summary>Whether compilation produced an executable program.</summary>
    public bool Succeeded => Program is not null
        && Diagnostics.All(item => item.Severity != LanguageDiagnosticSeverity.Error);
}
