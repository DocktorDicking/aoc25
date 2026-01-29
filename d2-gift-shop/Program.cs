
ShowLoadingAnimation("Loading ranges...");
string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "", "input.txt");
if (!File.Exists(filepath)) throw new FileNotFoundException("File not found", filepath);

string input = File.ReadAllText(filepath);
string[] rangeParts = input.Split(',');
long[] invalidCollection = [];

ShowLoadingAnimation("Processing ranges...");
foreach (string range in rangeParts)
{
    Range r = new Range(range);
    long[] invalidValues = GetInvalidValues(r);
    
    if (invalidValues.Length > 0)
        invalidCollection = invalidCollection.Concat(invalidValues).ToArray();
}

ShowLoadingAnimation("Summing invalid values...");
long sum = invalidCollection.Sum();

Console.WriteLine($"The sum of invalid Id's is: {sum}");

static long[] GetInvalidValues(Range range)
{
    if (range.start == 0 || range.end == 0 || range.start > range.end)
    {
        throw new ArgumentException("Invalid range");
    }

    List<long> invalidValues = [];
    for (long value = range.start; value <= range.end; value++)
    {
        if (value > 9)
        {
            string s = value.ToString();
            if (!IsOddLength(s))
            {
                int mid = s.Length / 2;
                string x = s[..mid];
                if (long.Parse(x + x) == value)
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
    public long start { get; set; }
    public long end { get; set; }

    public Range(string range)
    {
        string[] rangeParts = range.Split('-');
        start = long.Parse(rangeParts[0]);
        end = long.Parse(rangeParts[1]);
    }
}

/* TODO AOC day 2
 - parse input. Comma sepp. "x-y" x start y end of range. inclusive
 - validate range to find invalid id's
    - an ID is invalid if half id *2 equals the initial ID.
    - All ID's are integers. Leading zero's are not included.
 - SUM invalid id's'
*/