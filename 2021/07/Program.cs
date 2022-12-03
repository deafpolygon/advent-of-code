/*
 * --- Day 7: The Treachery of Whales ---
 * Solved
 */

List<string>? data = null;
using (var sr = new StreamReader(@"input.txt"))
    data = sr.ReadToEnd().Split($"\n", StringSplitOptions.RemoveEmptyEntries).ToList();

int[] crabs = data[0].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

int high = crabs.Max();
int low = crabs.Min();

int[] fuelcosts = new int[high]; // store costs per position
for (int i = 0; i < fuelcosts.Length; i++)
{
    foreach (var c in crabs)
    {
        int steps = Math.Abs(c - i);
        int additional = 0;
        for (int j = 0; j < steps; j++)
        {
            additional += j;
        }
        int fuelcost = steps + additional;
        fuelcosts[i] += fuelcost;


        //part 1
        //fuelcosts[i] += Math.Abs(c - i);
    }
}

int minfuel = fuelcosts[0];
int minposition = 0;
for (int i = 0;i < fuelcosts.Length;i++)
{
    if (minfuel > fuelcosts[i])
    {
        minfuel = fuelcosts[i];
        minposition = i;
    }
}

Console.WriteLine($"Position {minposition} with {minfuel}");