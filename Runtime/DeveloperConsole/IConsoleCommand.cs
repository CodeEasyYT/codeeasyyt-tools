namespace CodeEasyYT.DeveloperConsole.Commands
{
    /// <summary>
    /// If you want to see your command work use this.
    /// Prefer using "ConsoleCommand" class.
    /// </summary>
    public interface IConsoleCommand
    {
        string CommandWord { get; }
        bool Process(string[] args);
    }
}