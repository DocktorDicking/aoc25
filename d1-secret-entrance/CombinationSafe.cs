namespace Aoc;

public class CombinationSafe : ICombinationSafe
{
    private readonly CombinationDial _dial;
    private const int MaxValue = 99;
    private const int StartValue = 50;
    private readonly List<(Direction Direction, int Ticks)> _turns;

    public CombinationSafe()
    {
        _dial = new CombinationDial(MaxValue, StartValue);
        _turns = [];
    }
    
    public void AddDialTurn(Direction direction, int ticks)
    {
        _turns.Add((direction, ticks));
    }
    
    public CombinationSafe AddDialTurnFromFile(string filepath)
    {
        if (!File.Exists(filepath)) throw new FileNotFoundException("File not found", filepath);
        
        foreach (string line in File.ReadAllLines(filepath))
        {
            char directionChar = line[0];
            Direction direction = MapDirection(directionChar);
            if (int.TryParse(line[1..], out int ticks))
            {
                AddDialTurn(direction, ticks);
                continue;
            }
            
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Unable to parse line: {line}");
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Added { _turns.Count } dial turns from file: {filepath}");
        return this;
    }

    public void Run()
    {
        _turns.ForEach(turn => _dial.TurnDial(turn.Direction, turn.Ticks));
    }

    public List<string> GetStatistics()
    { 
        return _dial.ReadEvents();
    }
    
    private static Direction MapDirection(char direction) => direction switch
    {
        'L' => Direction.Left,
        'R' => Direction.Right,
        _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
    };
}