/*
 * --- Day 15: Chiton ---
 * In progress?
 */

using System.Collections.Generic;

int[,] riskmap = LoadMatrix2D("test_input.txt");
PrintMatrix2D(riskmap);

(int x, int y) start = (0, 0);
(int x, int y) end = (riskmap.GetLength(0)-1, riskmap.GetLength(1)-1);

Console.WriteLine($"Value of {start} - {riskmap[start.x, start.y]}");
Console.WriteLine($"Value of {end} - {riskmap[end.x, end.y]}");

//PriorityQueue<(int, int), int> queue = new();

Queue<(int, int)> queue = new();
queue.Append(start);

//iterate over paths (using priority queue and tuple coordinates??)
//assign a risk score in a List<> paths once we reach the 'end', add that to best path,
//a path being an ordered list of coordinates, and the resulting score
//any new paths calculated can be compared to best path's score

//TODO: save this for the future someplace...
int[,] LoadMatrix2D(string filename)
{
    List<string>? data = null;
    using (var sr = new StreamReader(@filename))
        data = sr.ReadToEnd().Split($"\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();

    int xwidth = data[0].Length;
    int ywidth = data.Count();
    int[,] matrix2d = new int[xwidth, ywidth];
    for (int y = 0; y < ywidth; y++)
    {
        int[] rowdata = new int[xwidth];
        rowdata = Array.ConvertAll(data[y].ToCharArray(), c => (int)Char.GetNumericValue(c));
        for (int x = 0; x < xwidth; x++)
        {
            matrix2d[x, y] = rowdata[x]; 
        }
    }
    return matrix2d;
}

void PrintMatrix2D(int[,] matrix)
{
    for (int y = 0; y < matrix.GetLength(1); y++)
    {
        for (int x = 0; x < matrix.GetLength(0); x++)
            Console.Write($"{matrix[x, y]} ");
        Console.WriteLine();
    }

    Console.WriteLine();
}