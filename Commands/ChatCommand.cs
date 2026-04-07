using System;

namespace ChatCommands
{
    public class ChatCommand
    {
        public string Name { get; }
        public string Description { get; }
        public Action<string[]> Execute { get; }

        protected ChatCommand(string name, string description, Action<string[]> execute)
        {
            Name = name;
            Description = description;
            Execute = execute;
        }
    }
}