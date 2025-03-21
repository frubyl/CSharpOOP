
using System.Diagnostics;

namespace BigHW1.Commands
{
    public class TimedCommandDecorator : ICommand
    {
        private readonly ICommand _command;

        public TimedCommandDecorator(ICommand command)
        {
            _command = command;
        }

        public void Execute()
        {
            var stopwatch = Stopwatch.StartNew();
            _command.Execute();
            stopwatch.Stop();
            Console.WriteLine($"Время выполнения: {stopwatch.Elapsed.TotalMilliseconds} мс");
        }
    }
}
