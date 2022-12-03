/*
 * --- Day 13: Transparent Origami ---
 * Solved
 */

using System.Text.RegularExpressions;

List<(int, int)> coords = new();
List<(string, int)> instructions = new();
(int x, int y) maxXY;

(coords, instructions, maxXY) = ParseData(@"input.txt");
int[,] points = new int[maxXY.x, maxXY.y];

foreach (var (x, y) in coords) 
{ // mark the paper
    points[x, y]++;
}

foreach ( (string axis, int position) instr in instructions)
{ // fold and count the paper
    points = FoldGrid(points, instr.axis, instr.position);
    int count = CountGrid(points);
    Console.WriteLine($"After fold {instr.axis}={instr.position}, there are {count} dots.");
}

Console.WriteLine();
DisplayGrid(points);

int CountGrid(int[,] grid)
{
    int count = 0;
    for (int y = 0; y < grid.GetLength(1); y++)
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            if (grid[x, y] > 0)
            {
                count++;
            }
        }
    }
    return count;
}


// pass it a grid, fold it along the specified axis at location
// returns a resized grid
int[,] FoldGrid(int[,] grid, string axis, int foldposition)
{
    int[,]? interim = null;

    if (axis.Equals("x")) // fold horizontally leftwards
    {
        interim = new int[grid.GetLength(0) - foldposition - 1, grid.GetLength(1)];
        for (int y = 0; y < grid.GetLength(1); y++)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                if (x > foldposition)
                {
                    int xnew = foldposition - (x - foldposition);
                    if (grid[x, y] > 0)
                    {
                        interim[xnew, y]++;
                    }
                }
                else
                {
                    if (grid[x, y] > 0)
                    {
                        interim[x, y]++;
                    }
                }
            }
        }
    }
    else if (axis.Equals("y")) // fold vertically upwards
    {
        interim = new int[grid.GetLength(0), grid.GetLength(1) - foldposition - 1];
        for (int y = 0; y < grid.GetLength(1); y++)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                if (y > foldposition)
                {
                    // take the difference
                    int ynew = foldposition - (y - foldposition);
                    if (grid[x,y] > 0)
                    {
                        interim[x, ynew]++;
                    }
                }
                else
                {
                    if (grid[x,y] > 0)
                    {
                        interim[x, y]++;
                    }
                }
            }
        }
    }
    return interim;
}

void DisplayGrid(int[,] grid)
{
    for (int y = 0; y < grid.GetLength(1); y++)
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            if (grid[x, y] > 0)
            {
                Console.Write($"█");
            }
            else
            {
                Console.Write($" ");
            }
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

( List<(int, int)>, List<(string, int)>, (int, int) ) ParseData(string filename)
{
    List<string> data = new();
    using (var sr = new StreamReader(@filename))
        data = sr.ReadToEnd().Split($"\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();

    List<(int, int)> coords = new();
    List<(string, int)> instructions = new();

    int xmax = 0;
    int ymax = 0;

    foreach (var line in data)
    {
        string coordMatch = @"^\d+,\d+$";
        string instrMatch = @"^fold along (?<instr>\w=\d+)$";
        Match mCoords = Regex.Match(line, coordMatch, RegexOptions.IgnoreCase);
        Match mInstr = Regex.Match(line, instrMatch, RegexOptions.IgnoreCase);

        if (mCoords.Success)
        {
            string[] tempcoords = mCoords.Value.Split(",");
            int xtemp = Convert.ToInt32(tempcoords[0]);
            int ytemp = Convert.ToInt32(tempcoords[1]);
            xmax = xmax < xtemp ? xtemp : xmax;
            ymax = ymax < ytemp ? ytemp : ymax;
            coords.Add((xtemp, ytemp));
        }
        else if (mInstr.Success)
        {
            string[] tempinstr = mInstr.Groups["instr"].Value.Split("=");
            instructions.Add((tempinstr[0], Convert.ToInt32(tempinstr[1])));
        }
        else
        {
            Console.WriteLine($"No match :(");
        }
    }
    return (coords, instructions, (xmax+1, ymax+1) );
}