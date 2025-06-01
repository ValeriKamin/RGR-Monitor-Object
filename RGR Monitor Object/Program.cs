using System;
using System.Threading;

public class CounterMonitor
{
    private int counter = 0;
    private readonly object lockObj = new object();

    public void Increment()
    {
        lock (lockObj)
        {
            counter++;
            Console.WriteLine($"Потiк {Thread.CurrentThread.ManagedThreadId}: {counter}");
        }
    }

    public int GetValue()
    {
        lock (lockObj)
        {
            return counter;
        }
    }
}

class Program
{
    static void Main()
    {
        var monitor = new CounterMonitor();
        Thread[] threads = new Thread[5];

        for (int i = 0; i < threads.Length; i++)
        {
            threads[i] = new Thread(() =>
            {
                for (int j = 0; j < 10; j++)
                {
                    monitor.Increment();
                    Thread.Sleep(10);
                }
            });
            threads[i].Start();
        }

        foreach (var t in threads)
            t.Join();

        Console.WriteLine($"Загальне значення: {monitor.GetValue()}");
    }
}
