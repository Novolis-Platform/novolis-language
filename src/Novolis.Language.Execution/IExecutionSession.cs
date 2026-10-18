namespace Novolis.Language.Execution;

/// <summary>Steppable runtime session contract.</summary>
public interface IExecutionSession
{
    /// <summary>Current state.</summary>
    ExecutionSnapshot Snapshot { get; }

    /// <summary>Advances execution by one instruction.</summary>
    ExecutionStep Step();
}
