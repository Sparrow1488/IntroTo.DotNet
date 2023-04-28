# Синхронизация потоков

Языковая среда C# предоставляет возможность использовать различные инструменты для синхронизации работы с потоками, такие как `lock`, `Monitor`, `Mutex`, `Semaphore` (и некоторые другие). Каждый обладает своими особенностями, что делает их не совсем одинаковыми (иначе бы все так и использовали `lock`).

## lock (locker)

`lock` - это синтаксический сахар, используемый в языке C# для синхронизации любого числа потоков. Используется следующим образом:

```C#
public class Program
{
    private static int _value = 0;
    private static readonly object _lock = new object();
    public static void Main()
    {
        for (int i = 0; i < 5; i++)
        {
            var t = new Thread(Count);
            t.Name = "Thread-" + i;
            t.Start();
        }
    }
    private static void Count()
    {
        lock (_lock)
        {
            for (; _value < 10; _value++)
            Console.WriteLine(Thread.CurrentThread.Name + "  " + _value);
            _value = 0;
            Console.WriteLine("\n");
        }
    }
}
```

Для его использования требуется объект `var locker = new object();`, который выполняет роль блокировщика. Когда первый поток начинает выполнение, все последующие встают в очередь.

## Monitor

Статический класс `Monitor` является нутрянкой синтаксического сахара `lock`. Для синхронизации по прежнему используется объект `var locker = new object();` 

```C#
internal class Program
{
    private static object locker = new object();
    private static int _blockIteratorFactor = -1;

    public static void Main()
    {
        Console.WriteLine($"Int32.MaxValue = {Int32.MaxValue}");
        for (int i = 0; i < 5; i++)
        {
            var t = new Thread(() => ElevateInt32((i + 1) * 5));
            t.Name = $"[Thread {i}]";
            t.Start();
        }
    }

    /// <summary>
    /// Поочередное возведение в степень числа с последующим уведомлением параллельных вычислительных потоков о невозможном продолжении вычислений из-за переполнения буфера (используется Int32)
    /// </summary>
    /// <param name="iterations">Кол-во интераций перемножения</param> 
    private static void ElevateInt32(int iterations)
    {
        var threadName = Thread.CurrentThread.Name + " ";
        bool taken = false;
        int res = 1;
        try
        {
            Monitor.Enter(locker, ref taken);

            for (int i = 0; i < iterations; i++)
            {
                if (_blockIteratorFactor > -1 && _blockIteratorFactor == i)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(threadName + "Thread warned about impossible calculations. Thread work Stoped");
                    Console.ResetColor();
                    Monitor.Pulse(locker);
                    return;
                }
                var factor = i == 0 ? 1 : i;
                var buffer = res * factor;
                if (buffer < 0)
                {
                    Console.WriteLine(threadName + $"Imposible calculate using Int32 (BUFFER = {buffer})");
                    Console.WriteLine(threadName + $"Thread was blocked");
                    _blockIteratorFactor = i;
                    Monitor.Wait(locker);
                    Console.WriteLine(threadName + $"Thread continue calculation after block. Thread work Finished");
                    return;
                }
                else
                {
                    Console.WriteLine(threadName + $"Calculation equals ({buffer} = {res} * {factor})");
                    res = buffer;
                    if (i == 5)
                    {
                        Console.WriteLine(threadName + "Waiting");
                        Monitor.Wait(locker, 500);
                        Console.WriteLine(threadName + "Continue calculate");
                    }
                }
            }
            Monitor.Pulse(locker);
        }
        finally
        {
            if (taken) Monitor.Exit(locker);
        }
    }
}
```

Использование `Monitor` позволяет гибче построить логику программы, при этом настроить синхронизацию под свои нужды.

## Mutex

`Mutex` - это класс, объект которого позволяет выстроить очередь потоков для их синхронизации.

```C#
internal class Program
{
    public static Mutex Mutex = new Mutex();
    public static void Main()
    {
        for (int i = 0; i < 5; i++)
            new Example(i);
    }
}

internal class Example
{
    private int Id { get; set; }
    public Example(int id)
    {
        Id = id;
        new Thread(Foo).Start();
    }

    private void Foo()
    {
        Program.Mutex.WaitOne();

        Console.WriteLine($"[{Id}]Thread started Foo");
        Thread.Sleep(900);
        Console.WriteLine($"[{Id}]Thread finished Foo");
        Console.WriteLine();

        Program.Mutex.ReleaseMutex();
    }
}
```

Отличием `Mutex` от предыдущих инструментов будет то, что мы при помощи объекта управляем потоками, не прибегая к использованию статических методов и конструкций. Это дает нам некоторый простор для построения логики синхронизации.

## Semaphore

`Semaphore` - это инструмент для синхронизации потоков, позволяющий указывать число одновременно работающих потоков, тем самым выстраивая несколько очередей.

```C#
internal class Program
{
    public static void Main()
    {
        for (int i = 0; i < 10; i++)
            new Queue(i);
    }
}

internal class Queue
{
    public static Semaphore Semaphore = new Semaphore(2, 2);
    public int Id { get; set; }
    public Queue(int id)
    {
        Id = id;
        new Thread(Handle).Start();
    }

    private void Handle()
    {
        Semaphore.WaitOne();

        Console.WriteLine($"[{Id}]Thread: занял очередь");
        Thread.Sleep(900);
        Console.WriteLine($"[{Id}]Thread: освободил очередь");
        Console.WriteLine();

        Semaphore.Release();
    }
}
```

