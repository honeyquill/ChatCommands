Chatcommands read me
--------------------

To link your bb mod up with chat commands include this line [assembly: MelonAdditionalDependencies("ChatCommands")] 
and of courser referance the DLL in releases. Then to find chatCommands include this in your OnInitializeMelon()
```
var chatCommands = MelonMod.RegisteredMelons
                .OfType<ChatCommands.Main>()
                .FirstOrDefault(); 
if (chatCommands != null)
            {
                chatCommands.RegisterCommand("Ping", new Ping()); //Example.
            }
            else
            {
                MelonLogger.Warning("ChatCommands could not be found.");
            }
```
You should make a commands folder, this is what a command looks like:
```
using ChatCommands;

namespace OBSIntegration
{
    public class Ping : ChatCommand
    {
        public Ping()
            : base("ping", "Replies with Pong!", ExecutePing)
        {
        } 
        private static void ExecutePing(string[] args)
        {
            BeetleUtils.SendChatMessage("Pong!");
        }
    }
}
```
        
