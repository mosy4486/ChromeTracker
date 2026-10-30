namespace ChromeTracker
{
    internal class Program
    {
        static async Task Main()
        {
            ControllerSystem ct = new ControllerSystem();
            Task ctTask = ct.StartAsync();
            await ctTask;
        }
    }
}
