using CodeEasyYT.DeveloperConsole.Commands;
using System.Collections.Generic;
using System.Linq;

namespace CodeEasyYT.DeveloperConsole
{
    /// <summary>
    /// This is the developer console itself. However to use this attach "DeveloperConsoleBehaviour" to a GameObject.
    /// </summary>
    public class DeveloperConsole
    {
        private readonly string prefix;
        private readonly IEnumerable<IConsoleCommand> commands;

        public DeveloperConsole(string prefix, IEnumerable<IConsoleCommand> commands)
        {
            this.prefix = prefix;
            this.commands = commands;
        }
        public DeveloperConsole(IEnumerable<IConsoleCommand> commands)
        {
            this.prefix = string.Empty;
            this.commands = commands;
        }

        public void ProcessCommand(string commandInput, string[] args)
        {
            foreach (var command in commands)
            {
                if (!commandInput.Equals(command.CommandWord, System.StringComparison.Ordinal))
                {
                    continue;
                }

                if(command.Process(args))
                {
                    return;
                }
            }
        }

        public void ProcessCommand(string inputValue)
        {
            if (!inputValue.StartsWith(prefix) && prefix != string.Empty) return;

            if (prefix != string.Empty)
                inputValue = inputValue.Remove(0, prefix.Length);

            string[] inputSplit = inputValue.Split(' ');

            string commandInput = inputSplit[0];
            string[] args = inputSplit.Skip(1).ToArray();

            ProcessCommand(commandInput, args);
        }
    }
}