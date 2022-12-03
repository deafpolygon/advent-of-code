/*
 * --- Day 9: Smoke Basin ---
 * Solved
 */

List<string>? data = null;
using (var sr = new StreamReader(@"input.txt"))
    data = sr.ReadToEnd().Split($"\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();

int x = data[0].Length;
int y = data.Count();

int[,] heightmap = new int[x, y];

for(int i = 0; i < y; i++) // iterate over row
{
    for (int j = 0; j < x; j++)
    {
        heightmap[j, i] = Convert.ToInt32(data[i][j].ToString());
    }
}

PartOne(heightmap);
PartTwo(heightmap);

void PartOne(int[,] map)
{
    int xmax = map.GetLength(0);
    int ymax = map.GetLength(1);

    List<int> lowPoints = new();

    for (int y = 0; y < ymax; y++)
    {
        for (int x = 0; x < xmax; x++)
        {
            int currentNumber = map[x, y];
            List<(int, int)> proximity = new List<(int, int)> { (0, -1), (-1, 0), (1, 0), (0, 1) };
            List<int> surrounding = new();
            foreach (var (p1, p2) in proximity)
            {
                int cx = x + p1;
                int cy = y + p2;

                if (cx >= 0 && cy >= 0 && cx < map.GetLength(0) && cy < map.GetLength(1))
                {
                    surrounding.Add(map[x + p1, y + p2]);
                }
            }
            if (currentNumber < surrounding.Min())
            {
                lowPoints.Add(currentNumber);
            }
        }
    }

    long total = lowPoints.Sum() + (lowPoints.Count);
    Console.WriteLine($"Part One. The total is {total}.");
}

void PartTwo(int[,] map)
{

    int xmax = map.GetLength(0);
    int ymax = map.GetLength(1);

    List<(int p1, int p2)> lowPoints = new();
    for (int y = 0; y < ymax; y++)
    {
        for (int x = 0; x < xmax; x++)
        {
            int currentNumber = map[x, y];          
            List<(int, int)> proximity = new List<(int, int)> { (0, -1), (-1, 0), (1, 0), (0, 1) };
            List<int> surrounding = new();
            foreach (var (p1, p2) in proximity)
            {
                int cx = x + p1;
                int cy = y + p2;

                if (cx >= 0 && cy >= 0 && cx < map.GetLength(0) && cy < map.GetLength(1))
                {
                    surrounding.Add(map[x + p1, y + p2]);
                }
            }
            if (currentNumber < surrounding.Min())
            {
                lowPoints.Add( (x, y) );
            }
        }
    }

    List<(int size, (int px, int py))> lowPointSizes = new();
    foreach ( (int lpx, int lpy) lp in lowPoints)
    {
        HashSet<(int, int)> done = new();
        List<(int, int)> stack = new(); 

        foreach ( (int, int) neighbor in GetValidNeighbors(map, lp))
        {
            if( !done.Contains(neighbor) )
                stack.Add(neighbor); 
        }
        while (stack.Count > 0)
        {
            (int, int) p = stack.Last();
            stack.Remove(p);

            foreach ( (int,int) neighbor in GetValidNeighbors(map, p))
            {
                if ( !done.Contains( neighbor ))
                    stack.Add( neighbor );
            }
            done.Add(p);
        }
        lowPointSizes.Add( (done.Count, lp) );
    }
    lowPointSizes.Sort((s, t) => t.size.CompareTo(s.size));
    int productOfThreeLargest = lowPointSizes[0].size * lowPointSizes[1].size * lowPointSizes[2].size;
    Console.WriteLine($"Part Two.  The product of the three largest basins is {productOfThreeLargest}.");

}

List<(int, int)> GetValidNeighbors(int[,] map, (int x, int y) sp )
{
    List<(int, int)> proximity = new List<(int, int)> { (0, -1), (-1, 0), (1, 0), (0, 1) };
    List<(int, int)> gn = new();
    foreach (var (p1, p2) in proximity)
    {
        int cx = sp.x + p1;
        int cy = sp.y + p2;
        if (cx >= 0 && cy >= 0 && cx < map.GetLength(0) && cy < map.GetLength(1))
        {
            if (map[sp.x + p1, sp.y + p2] < 9)
                gn.Add((sp.x + p1, sp.y + p2));
        }
    }
    return gn;
}

/*
static void PrintMap(int[,] map)
{
    for (int y = 0; y < map.GetLength(1); y++)
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            if (map[x, y] == 0)
            {
                Console.Write("  ");
            }
            else
            {
                Console.Write($"{map[x, y]} ");
            }
            
        }
        Console.WriteLine();
    }
}
*/