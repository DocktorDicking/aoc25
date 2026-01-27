namespace Aoc;

public class CombinationDial : ICombinationDial
{
    private readonly DialEventListner _dialEventListner = new();
    private int MaxValue { get; set; }
    private readonly int _minValue = 0;
    private int _currentValue;
    private readonly int _startValue;

    public CombinationDial(int maxValue, int startValue)
    {
        MaxValue = maxValue;
        _currentValue = startValue;
        _startValue = startValue;
    }

    public List<string> ReadEvents()
    {
        return _dialEventListner.PrintEvents(_startValue);
    }
    
    public void TurnDial(Direction direction, int ticks)
    {
        for (int i = 0; i < ticks; i++)
        {
            switch (direction)
            {
                case Direction.Left when _currentValue == 0:
                    _currentValue = MaxValue;
                    break;
                case Direction.Left:
                    _currentValue--;
                    break;
                case Direction.Right when _currentValue == MaxValue:
                    _currentValue = _minValue;
                    break;
                case Direction.Right:
                    _currentValue++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
            // Part 2
            _dialEventListner.TrackEvent(direction, ticks, _currentValue);
        }
        // Part 1
        // _dialEventListner.TrackEvent(direction, ticks, _currentValue);
    }
}

public class DialEventListner
{
    private readonly List<DialEvent> _eventsList= [];

    public void TrackEvent(Direction direction, int ticks, int value)
    {
        _eventsList.Add(new DialEvent(direction, ticks, value));
    }

    public List<string> PrintEvents(int startValue)
    {
        var lines = new List<string>(_eventsList.Count)
        {
            $"The Dial starts by pointing at {startValue}."
        };
        lines.AddRange(_eventsList.Select(dialEvent => $"The Dial is rotated {dialEvent.Direction} {dialEvent.Ticks} to point at: {dialEvent.Value}."));
        lines.Add(PrintPassword());
        return lines;
    }

    private string PrintPassword()
    {
        int amount = _eventsList.FindAll(e => e.Value == 0).Count;
        return $"The password is: {amount}";
    }
}

/* Fun Fact:
 * Record overrides the == by default such that two records are equal if all their properties are equal.
 * Classes will check the memory address of the object when doing ==.
 * Records can be printed out of the box.
 * Records have less boilerplate code than classes.
 */
internal record DialEvent(Direction Direction, int Ticks, int Value);