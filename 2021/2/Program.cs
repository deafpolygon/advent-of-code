/*
 * --- Day 2: Dive! ---
 * Solved
 */

using System;
using System.IO;

var watch = System.Diagnostics.Stopwatch.StartNew();
PartOne();
PartTwo();
watch.Stop();
Console.WriteLine($"This took {watch.ElapsedMilliseconds.ToString()} ms to complete.");

void PartOne()
{
    string[] instructions = Array.Empty<string>();

    try
    {
        instructions = File.ReadAllLines(@"input.txt").Where(f => !string.IsNullOrWhiteSpace(f)).ToArray();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error in importing input file:");
        Console.WriteLine(ex.Message);
    }

    int horizontal = 0;
    int depth = 0;

    foreach (var instruction in instructions)
    {
        string[] instr = instruction.Split(' ');
        try
        {
            var direction = instr[0];
            int.TryParse(instr[1], out int distance);

            if (direction.Equals("forward", StringComparison.OrdinalIgnoreCase))
            {
                horizontal += distance;
            }
            else if (direction.Equals("down", StringComparison.OrdinalIgnoreCase))
            {
                depth += distance;
            }
            else if (direction.Equals("up", StringComparison.OrdinalIgnoreCase))
            {
                depth -= distance;
            }
        }
        catch (IndexOutOfRangeException)
        { } //ignore out of bounds
    }
    Console.WriteLine("Part One Answer: {0}", depth * horizontal);
}

void PartTwo()
{

    string[] instructions = Array.Empty<string>();

    try
    {
        instructions = File.ReadAllLines(@"input.txt");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error in importing input file:");
        Console.WriteLine(ex.Message);
    }

    int horizontal = 0;
    int depth = 0;
    int aim = 0;

    foreach (var instruction in instructions)
    {
        string[] instr = instruction.Split(' ');
        try
        {
            var direction = instr[0];
            int.TryParse(instr[1], out int distance);

            if (direction.Equals("forward", StringComparison.OrdinalIgnoreCase))
            {
                horizontal += distance;
                depth += aim * distance;
            }
            else if (direction.Equals("down", StringComparison.OrdinalIgnoreCase))
            {
                aim += distance;
            }
            else if (direction.Equals("up", StringComparison.OrdinalIgnoreCase))
            {
                aim -= distance;
            }
        }
        catch (IndexOutOfRangeException)
        { } //ignore out of bounds

    }

    Console.WriteLine("Part Two Answer: {0}", depth * horizontal);


}