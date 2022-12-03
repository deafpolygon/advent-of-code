/*
 * --- Day 6: Lanternfish ---
 * Solved
 */
using System.Numerics;

List<string>? data = null;
using (var sr = new StreamReader(@"input.txt"))
    data = sr.ReadToEnd().Split($"\n", StringSplitOptions.RemoveEmptyEntries).ToList();

long[] numbers = data[0].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

BigInteger[] fishdata = new BigInteger[9];

foreach (int n in numbers)
    fishdata[n]++;

int total_days = 256;

for (int n = 0; n < total_days; n++)
{
    BigInteger newfish = fishdata[0];
    fishdata[0] = fishdata[1];
    fishdata[1] = fishdata[2];
    fishdata[2] = fishdata[3];
    fishdata[3] = fishdata[4];
    fishdata[4] = fishdata[5];
    fishdata[5] = fishdata[6];
    fishdata[6] = fishdata[7];
    fishdata[7] = fishdata[8];
    fishdata[8] = newfish;
    fishdata[6] += newfish;
}

BigInteger total = 0;

foreach (BigInteger n in fishdata)
    total += n;

Console.WriteLine(total);

