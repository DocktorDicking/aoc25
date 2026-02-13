
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
        //TODO: Update this code such that is can detect sequences that occure more then twice.
        /*
         * For example 11 (1 1) is invalid but now also 111 (1 1 1).
         * The way we check the rule needs to be changed completly since this code below does not allow flexible checking of sequences.
         */
        
        
        /*
        "121"
        "111"
        "11"
        "12"
        
        We need to update the code in whole.
        We need some way of keeping track of a sequence and break as soon as the sequence changes. 
        
        So we have to keep checking for a while to be sure the sequence is not repeating?
        
        123456 <-- not repeating BUT
        123456123456 <-- suddenly repeating
        
        when do we stop checking? 
        
        okeoke so each sequence has a set length.
        So we check untill we passed half of the sequence and IF it does not start repeating we break. 
        
        so if we have the same sequence we keep checking untill "123456 | " and if the next character is not a 1, we break?
        Sot it shall not alway be half since a seq (sequence) can be odd. So we keep track of the sequence we know of...
        So basically the ID is invalid untill proven it is not. 
        
        A valid ID does not repeat contain a repeting sequence.
        
        Psudo:
        
        string x (parsed);
        .....
        
        
        
         */
        
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

/* TODO part 2
New rule, ID is invalid when sequence is repeated atleast twice. 123123 or 123123123 are both invalid.

*/