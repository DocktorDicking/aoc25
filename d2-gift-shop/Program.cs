// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string range = "11-22";
Range r = new Range(range);

int[] invalidValues = GetInvalidValues(r);

foreach (int value in invalidValues)
{
    Console.WriteLine(value);
}






static int[] GetInvalidValues(Range range)
{
    if (range.values.Length == 0)
    {
        Console.WriteLine("Values not loaded yet");
        return [];
    }

    int[] values = range.values;
    List<int> invalidValues = [];
    foreach (int value in values)
    {
        if (value > 9)
        {
            string s = value.ToString();
            if (!IsOddLength(s))
            {
                int mid = s.Length / 2;
                string x = s[..mid];
                if (int.Parse(x + x) == value)
                {
                    invalidValues.Add(value);
                }
            }
            
        }
    }

    return invalidValues.ToArray();
}

static bool IsOddLength(string value)
{
    return value.Length % 2 != 0;
}

struct Range
{
    public int[] values { get; set; }
    public int start { get; set; }
    public int end { get; set; }

    public Range(string range)
    {
        string[] rangeParts = range.Split('-');
        start = int.Parse(rangeParts[0]);
        end = int.Parse(rangeParts[1]);
        LoadRange();
    }
    
    private void LoadRange()
    {
        if (start > end)
        {
            Console.WriteLine("Invalid range");
        }
    
        values = Enumerable.Range(start, end - start + 1).ToArray();
    }
}

/* TODO AOC day 2
 - parse input. Comma sepp. "x-y" x start y end of range. inclusive
 - validate range to find invalid id's
    - an ID is invalid if half id *2 equals the initial ID.
    - All ID's are integers. Leading zero's are not included.
 - SUM invalid id's'
*/