namespace Novolis.Language.Execution;

/// <summary>Inspectable immutable execution state.</summary>
public sealed record ExecutionSnapshot(
    int InstructionPointer,
    IReadOnlyDictionary<string, object?> Variables,
    IReadOnlyList<string> Output,
    bool IsCompleted,
    bool IsPaused);
