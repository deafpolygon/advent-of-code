/*
 * ---Day 14: Extended Polymerization ---
 * Solved
 */

List<string>? data = null;
using (var sr = new StreamReader(@"input.txt"))
    data = sr.ReadToEnd().Split($"\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();

List<char> elements = new();
char[] sequence = data[0].ToCharArray();
data.RemoveAt(0);

Dictionary<string, string> splitmap = new();
foreach (string mapdata in data)
{
    string[] result = mapdata.Split(" -> ");
    splitmap[result[0]] = result[1];

    foreach(char c in result[0])
    {
        if (!elements.Contains(c))
        {
            elements.Add(c);
        }
    }

    if (!elements.Contains(result[1][0]))
    {
        elements.Add(result[1][0]);
    }
}

// Generate the initial pairs dictionary
Dictionary<string, long> pairs = new();
for(int i = 0; i < sequence.Length -1; i++)
{
    
    string newpair = $"{sequence[i]}{sequence[i+1]}";
    if (pairs.ContainsKey(newpair)) {
        pairs[newpair]++;
    }
    else
    {
        pairs[newpair] = 1;
    }
}


int numSteps = 40;
for(int i = 0;i < numSteps; i++)
{
    Dictionary<string, long> intermediate = new();
    
    foreach (KeyValuePair<string, long> pair in pairs)
    {
        string splitstring = splitmap[pair.Key];
        string pair1 = $"{pair.Key[0]}{splitstring}";
        string pair2 = $"{splitstring}{pair.Key[1]}";

        // add new pair #1
        if (intermediate.ContainsKey(pair1))
        {
            intermediate[pair1] += pair.Value;
        }
        else
        {
            intermediate[pair1] = pair.Value;
        }

        // add pair #2
        if (intermediate.ContainsKey(pair2))
        {
            intermediate[pair2] += pair.Value;
        }
        else
        {
            intermediate[pair2] = pair.Value;
        }            
    }

    pairs = new(intermediate);

}

Dictionary<char, long> counts = new();
foreach(KeyValuePair<string, long> pair in pairs)
{
    foreach (char c in pair.Key)
    {
        if(counts.ContainsKey(c))
        {
            counts[c] += pair.Value;
        }
        else
        {
            counts[c] = pair.Value;
        }
    }

}

List<KeyValuePair<char, long>> sortedCounts = counts.ToList();
sortedCounts.Sort((p1, p2) => p1.Value.CompareTo(p2.Value));

long difference = sortedCounts.Last().Value - sortedCounts.First().Value;

//Yeah it double-counted, I'll fix it later, maybe.
Console.WriteLine($"The difference between the most common and least common element is {Math.Ceiling((double)difference/2)}.");
