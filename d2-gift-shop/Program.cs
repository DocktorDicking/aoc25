
ShowLoadingAnimation("Loading ranges...");
string ranges = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,\n1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";
string[] rangeParts = ranges.Split(',');
int[] invalidCollection = [];

ShowLoadingAnimation("Processing ranges...");
foreach (string range in rangeParts)
{
    Range r = new Range(range);
    int[] invalidValues = GetInvalidValues(r);
    
    if (invalidValues.Length > 0)
        invalidCollection = invalidCollection.Concat(invalidValues).ToArray();
}

ShowLoadingAnimation("Summing invalid values...");
int sum = invalidCollection.Sum();
Console.WriteLine($"The sum of invalid Id's is: {sum}");

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

static void ShowLoadingAnimation(string message)
{
    Console.Write(message);
    for (int i = 0; i < 20; i++)
    {
        Console.Write(@"|/-\"[i % 4] + "\b");
        Thread.Sleep(50);
    }
    Console.WriteLine(" Done!");
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