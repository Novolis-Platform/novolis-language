namespace Novolis.Language.RoboSharp;

/// <summary>Inspectable mutable world state for a RoboSharp execution session.</summary>
public sealed class RoboSharpWorld
{
    private readonly List<string> output = [];

    /// <summary>Current X coordinate.</summary>
    public int X { get; private set; }

    /// <summary>Current Y coordinate.</summary>
    public int Y { get; private set; }

    /// <summary>Current cardinal heading, where zero is north.</summary>
    public int Heading { get; private set; }

    /// <summary>Text emitted by print instructions.</summary>
    public IReadOnlyList<string> Output => output;

    /// <summary>Current named values exposed to inspection tools.</summary>
    public IReadOnlyDictionary<string, object?> Variables =>
        new Dictionary<string, object?>(StringComparer.Ordinal)
        {
            ["x"] = X,
            ["y"] = Y,
            ["heading"] = Heading,
        };

    /// <summary>Moves the robot according to its current heading.</summary>
    public void Move(int distance)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(distance);
        var (dx, dy) = Heading switch
        {
            0 => (0, distance),
            1 => (distance, 0),
            2 => (0, -distance),
            _ => (-distance, 0),
        };
        X += dx;
        Y += dy;
    }

    /// <summary>Turns the robot left or right.</summary>
    public void Turn(string direction)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(direction);
        var delta = direction.Equals("right", StringComparison.OrdinalIgnoreCase)
            ? 1
            : direction.Equals("left", StringComparison.OrdinalIgnoreCase)
                ? -1
                : throw new ArgumentException("Direction must be left or right.", nameof(direction));
        Heading = (Heading + delta + 4) % 4;
    }

    /// <summary>Appends one line to the world output.</summary>
    public void Print(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        output.Add(text);
    }
}
