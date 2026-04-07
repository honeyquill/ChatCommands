using System;
using System.Collections.Generic;
using System.Linq;
using static BeetleUtils.BeetleUtils;
using MelonLoader;
using Debug = System.Diagnostics.Debug;
using Main = ChatCommands.Main;

[assembly: MelonPriority(-100)]
[assembly: MelonInfo(typeof(Main), "ChatCommands", "1.0.1", "Bee")]


namespace ChatCommands
{
    public class Main : MelonMod
    {
        private string _newestMsg;
        private const char CommandPrefix = '!';
        private readonly Dictionary<string, ChatCommand> _commands =
            new Dictionary<string, ChatCommand>();
        
        public IReadOnlyDictionary<string, ChatCommand> Commands => _commands;
        
        public override void OnUpdate()
        {
            CommandHandler();
        }

        public void RegisterCommand(string command, ChatCommand chatCommand)
        {
            _commands.Add(command.ToLower(), chatCommand); 
            MelonLogger.Msg("Command " + chatCommand.Name + " registered!");
        }

        private string GetNewestCommand()
        {
            var history = GetChatHistory();

            if (history == null || history.Count == 0)
            {
                return null;
            }
                
            
            var newestIndex = history.Count - 1;
            var lastMessage = _newestMsg;
            _newestMsg = history[newestIndex].message?.ToLower();
            Debug.Assert(_newestMsg != null, nameof(_newestMsg) + " != null");
            
            if (_newestMsg == lastMessage) return null;
            if (_newestMsg.ToCharArray()[0] != CommandPrefix) return null;
            
            _newestMsg = _newestMsg.Remove(0, 1);
            MelonLogger.Msg("Command identified:" + _newestMsg);
            return _newestMsg;
        }

        private void CommandHandler()
        {
            var commandName = GetNewestCommand();
            switch (commandName)
            {
                case null:
                    return;
                case "help":
                {
                    SendChatMessage("----------HELP----------<size=15>"); //Sets Size to 15 (default is 30)
                    foreach (var command in _commands.Select(kvp => kvp.Value))
                    {
                        SendChatMessage($"{command.Name} - {command.Description}");
                    }

                    SendChatMessage("</size>----------HELP----------"); //Resets size
                    return;
                }
            }

            if (_commands.TryGetValue(commandName, out var commandAction))
            {
                commandAction.Execute(Array.Empty<string>());
            }
            else
            {
                SendChatMessage("Unknown command: " + commandName);
            }
        }
    }
}