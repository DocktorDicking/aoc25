namespace Aoc;

public sealed class App
{
    private readonly ICombinationSafe _safe;

    public App(ICombinationSafe safe) => _safe = safe;

    public void Run(string[] args)
    {
        try
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "input.txt");
            Console.WriteLine($"Reading file: {filePath}");
            _safe.AddDialTurnFromFile(filePath).Run();

            var stats = _safe.GetStatistics();
            
            /* 
             * This spinner is just for animation, the actual work is already finished before we even show the spinner aka lying to the user (ux).
             * A way to "really" show progress would be to use Tasks or something where we iteratively check progress, depending on the needs ofcourse. 
             */
            ShowLoadingAnimation("Processing dial turns...");
            Console.WriteLine(stats[^1]); 
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Exception thrown and caught: {0}", e);
        }
    }
    
    private static void ShowLoadingAnimation(string message)
    {
        Console.Write(message);
        for (int i = 0; i < 20; i++)
        {
            Console.Write(@"|/-\"[i % 4] + "\b");
            Thread.Sleep(50);
        }
        Console.WriteLine(" Done!");
    }
}