var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var timer = new PeriodicTimer(TimeSpan.FromSeconds(10));
_ = Task.Run(async () =>
{
    while (await timer.WaitForNextTickAsync())
    {
        Console.WriteLine("Starting CPU-intensive task");
        var start = DateTime.UtcNow;
        while ((DateTime.UtcNow - start).TotalMilliseconds < 500)
        {
            // Busy-wait loop
            double _ = Math.Sqrt(start.Millisecond);
        }
        Console.WriteLine("Completed CPU-intensive task");
    }
});

app.MapGet("/", () => "Hello from .NET CPU app!");
app.Run();
