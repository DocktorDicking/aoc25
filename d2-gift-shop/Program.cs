// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

static Range LoadRange(Range range)
{
    if (range.start < range.end)
    {
        Console.WriteLine("Invalid range");
        return range;
    }
    
    range.values = Enumerable.Range(range.start, range.end - range.start + 1).ToArray();
    return range;
}

static int[] GetInvalidValues(Range range)
{
    if (range.values.Length == 0)
    {
        Console.WriteLine("Values not loaded yet");
        return [];
    }

    var values = range.values;
    foreach (var value in values)
    {
        //TODO
        return [];
    }

    return [];
}

string range = "11-22";

string[] rangeParts = range.Split('-');
int start = int.Parse(rangeParts[0]);
int end = int.Parse(rangeParts[1]);

struct Range
{
    public int[] values { get; set; }
    public int start { get; set; }
    public int end { get; set; }

    public Range(int start, int end)
    {
        this.start = start;
        this.end = end;
    }
}