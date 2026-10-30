using System;
using System.Collections.Generic;
using System.Text;

namespace ChromeTracker
{
    public class ControllerSystem
    {
        private CancellationTokenSource? source;
        private InputSystem input = new InputSystem();
        private CommandPlayer cp = new CommandPlayer();
        private bool isRunning = false;
        public async Task StartAsync()
        { 
            if(isRunning) return;

            source = new CancellationTokenSource();

            isRunning = true;

            cp.ShowCommandList();

            while (!source.IsCancellationRequested)
            {
                CommandType command =  await input.GetCommandInputAsync(source.Token);

                await PlayCommand(command,source.Token);
            }
            
            isRunning = false;

            source = null;
        }

        private async Task PlayCommand(CommandType command,CancellationToken token)
        {
            switch (command)
            {
                case CommandType.None: break;
                case CommandType.CommandList: cp.ShowCommandList(); break;
                case CommandType.SearchList: await cp.SearchList(token); break;
            }
        }

        public void Stop()
        {
            if (!isRunning) return;

            source!.Cancel();
        }
    }
}
