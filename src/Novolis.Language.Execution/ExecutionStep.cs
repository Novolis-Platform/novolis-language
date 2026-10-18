using Novolis.Language.Diagnostics;
using Novolis.Language.Text;

namespace Novolis.Language.Execution;

/// <summary>One transition in an inspectable execution session.</summary>
public sealed record ExecutionStep(
    ExecutionStepKind Kind,
    ExecutionSnapshot Snapshot,
    string? Description = null,
    LanguageDiagnostic? Diagnostic = null,
    TextLocation? Location = null);
