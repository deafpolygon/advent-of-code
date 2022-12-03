/*
 * --- Day 8: Seven Segment Search ---
 * Solved
 */

List<string>? data = null;
using (var sr = new StreamReader(@"input.txt"))
    data = sr.ReadToEnd().Split($"\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();

List<(string[] signalPattern, string[] outputValue)> crypto = new();

foreach (var value in data)
{
    string[] pipeSplit = value.Split('|', StringSplitOptions.RemoveEmptyEntries);
    string[] sp = pipeSplit[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
    string[] ov = pipeSplit[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
    (string[], string[]) tdata = (sp, ov);
    crypto.Add(tdata);
}


var watch = System.Diagnostics.Stopwatch.StartNew();
PartOne(crypto);
PartTwo(crypto);
watch.Stop();
Console.WriteLine($"This took {watch.ElapsedMilliseconds} ms to complete.");


static void PartOne(List<(string[] signalPattern, string[] outputValue)> crypto)
{
    int digitcount = 0;
    foreach (var entry in crypto)
    {
        (string[] sp, string[] ov) = entry;
        foreach (string s in ov) 
        {
            int slen = s.Length;
            if (slen == 2 || slen == 3 || slen == 4 || slen == 7)
                digitcount++;
        }
    }
    Console.WriteLine($"Part One: They appear {digitcount} times.");
}

static void PartTwo(List<(string[] signalPattern, string[] outputValue)> crypto)
{
    int outputTotal = 0;

    foreach (var entry in crypto)
    {
        (string[] sp, string[] ov) = entry;
        List<string> spw = new();
        spw.AddRange(sp);
        spw.Sort((a,b) => a.Length.CompareTo(b.Length)); // sort so it's quicker
        HashSet<char>[]? dcrypto = new HashSet<char>[10];

        while (spw.Count > 0)
        {
            if (spw[0].Length == 2) // this is a 1
            {
                HashSet<char> spwset = new(spw[0].ToCharArray());
                dcrypto[1] = spwset;
                spw.RemoveAt(0);
            }
            else if (spw[0].Length == 4) // this is a 4
            {
                HashSet<char> spwset = new(spw[0].ToCharArray());
                dcrypto[4] = spwset;
                spw.RemoveAt(0);
            } 
            else if (spw[0].Length == 3) // this is a 7
            {
                HashSet<char> spwset = new(spw[0].ToCharArray());
                dcrypto[7] = spwset;
                spw.RemoveAt(0);
            }
            else if (spw[0].Length == 7) // tihs is a 8
            {
                HashSet<char> spwset = new(spw[0].ToCharArray());
                dcrypto[8] = spwset;
                spw.RemoveAt(0);
            }
            else if (spw[0].Length == 5) // deal with the set of 5 sequences
            {
                HashSet<char> spwset = new(spw[0].ToCharArray());
                if (dcrypto[1].Count == 2 && dcrypto[1].IsSubsetOf(spwset)) // this is 3
                {
                    dcrypto[3] = spwset;
                    spw.RemoveAt(0);
                }
                else
                {
                    spwset.ExceptWith(dcrypto[4]); // removes '4'
                    if (spwset.Count == 3) // this is 2
                    {
                        dcrypto[2] = new HashSet<char>(spw[0].ToCharArray()); // spwset is modified
                        spw.RemoveAt(0);
                    }
                    else // this is 5
                    {
                        dcrypto[5] = new HashSet<char>(spw[0].ToCharArray());
                        spw.RemoveAt(0);
                    }
                }
            }
            else if (spw[0].Length == 6)
            {
                HashSet<char> spwset = new(spw[0].ToCharArray());
                // deal with set of 6
                if (dcrypto[3].Count == 5 && dcrypto[3].IsSubsetOf(spwset)) // this is a 9
                {
                    dcrypto[9] = spwset;
                    spw.RemoveAt(0);
                }
                else if (dcrypto[1].Count == 2 && dcrypto[1].IsSubsetOf(spwset)) // this is a 0
                {
                    dcrypto[0] = spwset;
                    spw.RemoveAt(0);
                }
                else // this is 6
                {
                    dcrypto[6] = spwset;
                    spw.RemoveAt(0);
                }
            }
        }

        List<string> decrypted = new();
        foreach(var n in ov)
        {
            HashSet<char> seq = new(n.ToCharArray());
            for (int i = 0; i < dcrypto.Length; i++)
            {
                if (seq.SetEquals(dcrypto[i]))
                {
                    decrypted.Add(i.ToString());
                    break;
                }
            }
        }
        int decryptednumber = Convert.ToInt32(String.Join("", decrypted));
        outputTotal += decryptednumber;
    }
    Console.WriteLine($"Part Two: The total is {outputTotal}.");
}
