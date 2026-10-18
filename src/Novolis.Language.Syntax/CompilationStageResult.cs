using Novolis.Language.Diagnostics;

namespace Novolis.Language.Syntax;

/// <summary>Result of one inspectable compilation stage.</summary>
public sealed record CompilationStageResult<T>(
    T? Value,
    IReadOnlyList<LanguageDiagnostic> Diagnostics)
{
    /// <summary>Whether the stage completed without errors.</summary>
    public bool Succeeded => Diagnostics.All(item => item.Severity != LanguageDiagnosticSeverity.Error);

    /// <summary>Creates a successful result.</summary>
    public static CompilationStageResult<T> Success(T value) => new(value, []);

    /// <summary>Creates a failed result.</summary>
    public static CompilationStageResult<T> Failure(params LanguageDiagnostic[] diagnostics) =>
        new(default, diagnostics);
}
