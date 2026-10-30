using System;
using System.Collections.Generic;
using System.Text;

namespace ChromeTracker
{
    public class InputSystem
    {
        public async Task<CommandType> GetCommandInputAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                string? an = await Console.In.ReadLineAsync(token);
                if (!Enum.IsDefined(typeof(CommandType),an)||int.TryParse(an,out int bf))
                {
                    Console.WriteLine("Not valid command\nShow command List : CommandList");
                    continue;
                }
                return (CommandType)Enum.Parse(typeof(CommandType),an,true);
            }

            return CommandType.None;
        }
    }
}
