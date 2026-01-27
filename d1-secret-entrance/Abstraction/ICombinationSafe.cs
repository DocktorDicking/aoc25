namespace d1_secret_entrance.Abstraction;

public interface ICombinationSafe
{
    CombinationSafe AddDialTurnFromFile(string filepath);
    void AddDialTurn(Direction direction, int ticks);
    List<string> GetStatistics();
    void Run();
}