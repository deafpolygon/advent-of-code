/*
 * --- Day 3: Binary Diagnostic ---
 * Completed
 */

using System;
using System.IO;

string[] data = Array.Empty<string>();
List<int[]> diagnostics = new();

try
{
    data = File.ReadAllLines(@"input.txt").Where(f => !string.IsNullOrWhiteSpace(f)).ToArray();
}
catch (Exception ex)
{
    Console.WriteLine("Error in importing input file:" + ex.Message);
}

foreach (var d in data)
{
    diagnostics.Add(Array.ConvertAll(d.ToCharArray(), c => (int)Char.GetNumericValue(c)));
}

var watch = System.Diagnostics.Stopwatch.StartNew();
PartOne(diagnostics);
PartTwo(diagnostics);
watch.Stop();
Console.WriteLine($"This took {watch.ElapsedMilliseconds.ToString()} ms to complete.");

void PartOne(List<int[]> diag)
{

    int width = diag[0].Length; // determine column width
    int[] totals = new int[width];
    for (int i = 0; i < width; i++)
    {
        totals[i] = GetColSum(diag, i);
    }

    int[] gammaBinary = new int[width];
    int[] epsilonBinary = new int[width];

    for (int i = 0; i < width; i++)
    {
        if(totals[i] > (diag.Count / 2))
        {
            gammaBinary[i] = 1;
            epsilonBinary[i] = 0;
        }
        else
        {
            gammaBinary[i] = 0;
            epsilonBinary[i] = 1;
        }

    }

    int gammaDecimal = Convert.ToInt32(String.Join("", gammaBinary), 2);
    int epsilonDecimal = Convert.ToInt32(String.Join("", epsilonBinary), 2);

    Console.WriteLine("Part One.  The power consumption is {0}.", gammaDecimal * epsilonDecimal);

}

void PartTwo(List<int[]> diag)
{
    int width = diag[0].Length;
    List<int[]> o2Intermediate = new(diag);
    List<int[]> co2Intermediate = new(diag);

    for (int pos = 0; pos < width; pos++)
    {
        if (o2Intermediate.Count > 1)
        {
            int common = GetCommonBitAtPosition(o2Intermediate, pos);
            List<int[]> r1 = GetOnlyBitsAtPosition(o2Intermediate, pos, common);
            o2Intermediate = r1;
        }

        if (co2Intermediate.Count > 1)
        {
            int common = GetCommonBitAtPosition(co2Intermediate, pos);
            List<int[]> r2 = GetOnlyBitsAtPosition(co2Intermediate, pos, Math.Abs(common - 1));
            co2Intermediate = r2;
        }
    }

    int o2gen = Convert.ToInt32(string.Join("", o2Intermediate[0]), 2);
    int co2scrub = Convert.ToInt32(string.Join("", co2Intermediate[0]), 2);

    Console.WriteLine("Part Two. The Life Support Rating is: {0}", o2gen * co2scrub);
}

/*
 * Filter the list based on bit at position
 * Return the list
 */
List<int[]> GetOnlyBitsAtPosition(List<int[]> diag, int position, int bit)
{
    List<int[]> result = new();
    foreach (var d in diag)
    {
        if (d[position] == bit)
        {
            result.Add(d);
        }
    }
    return result;
}

/*
 * Returns the most common bit at the given position
 */
int GetCommonBitAtPosition(List<int[]> diag, int position)
{
    int sum = GetColSum(diag, position);
    double half = diag.Count / 2.0;
    int common = 0;
    if (sum >= half)
    {
        common = 1;
    }
    return common;
}

/*
 * Return the sum of the column of a List of integers
 */
int GetColSum(List<int[]> diag, int position)
{
    int total = 0;
    foreach (int[] d in diag)
    {
        total += d[position];
    }
    return total;
}