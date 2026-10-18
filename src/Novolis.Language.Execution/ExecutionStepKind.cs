namespace Novolis.Language.Execution;

/// <summary>Kind of an execution transition.</summary>
public enum ExecutionStepKind
{
    /// <summary>Execution has started.</summary>
    Started,
    /// <summary>An instruction was processed.</summary>
    Instruction,
    /// <summary>Execution produced output.</summary>
    Output,
    /// <summary>Execution completed successfully.</summary>
    Completed,
    /// <summary>Execution failed with a fault.</summary>
    Faulted,
    /// <summary>Execution is paused at a resumable point.</summary>
    Paused,
}
