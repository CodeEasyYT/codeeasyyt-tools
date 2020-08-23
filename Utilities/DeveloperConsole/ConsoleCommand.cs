using UnityEngine;

namespace CodeEasyYT.Utilities.DeveloperConsole.Commands
{
    /// <summary>
    /// Every command should be inherited from this.
    /// </summary>
    public abstract class ConsoleCommand : ScriptableObject, IConsoleCommand
    {
        [SerializeField] private string commandWord = string.Empty;

        public string CommandWord => commandWord;

        public abstract bool Process(string[] args);
    }
}