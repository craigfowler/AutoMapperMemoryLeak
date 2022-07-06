using System.Diagnostics;
using AutoMapper;

namespace AutoMapperMemoryLeak;

public class Program
{
    static readonly int[] iterationCounts = { 1, 10, 100, 1000, 10000 };

    public static void Main()
    {
        foreach(var iterationCount in iterationCounts)
            CreateMappingConfigAndMap(iterationCount);
    }

    static void CreateMappingConfigAndMap(int iterations)
    {
        var bytesBefore = GetRamUsageBytes();
        Console.WriteLine($"About to create config and map over {iterations} iterations\n  Current process bytes used = {bytesBefore}");

        for (var i = 0; i < iterations; i++)
            CreateMappingConfigAndMap();

        // Forcing GC so that transient RAM usage is removed
        GC.Collect(2);
        // Give the process 2 seconds pause so RAM usage can stabilise
        Thread.Sleep(2000);

        var bytesAfter = GetRamUsageBytes();
        Console.WriteLine($"Completed creating config and mapping over {iterations} iterations\n  Current process bytes used = {bytesAfter}\n  Process bytes growth = {bytesAfter - bytesBefore}\n");
    }

    static void CreateMappingConfigAndMap()
    {
        var config = new MapperConfiguration(x => x.CreateMap<MyFunkyClass, MyGroovyClass>());
        var mapper = config.CreateMapper();
        mapper.Map<MyGroovyClass>(new MyFunkyClass { ANumber = 5, AString = "Foo bar" });
    }

    static long GetRamUsageBytes()
    {
        Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
        return currentProcess.WorkingSet64;
    }
}
