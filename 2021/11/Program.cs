/*
 * --- Day 11: Dumbo Octopus ---
 * Solved
 * 
 * Solution process
 * 
 * For each STEP *
 * 1; raise every octopus's energy level by 1
 * 2; mark all octopus coordinates above a 9 in a queue
 * 3; go through the queue and 'flash' the ones that are above 9 
 *    (set to 0), raise the energy level of all surrounding octopi 
 *    by 1, add the newly above 9's to the stack
 * 4; continue going through the queue until complete
 * 
 */

/*
Console.Write("How many steps do you want to simulate: ");
int totalsteps = Convert.ToInt32(Console.ReadLine());
Console.Write("What step do you want total flash count: ");
int flashstep = Convert.ToInt32(Console.ReadLine());
Console.WriteLine();
*/

int totalsteps = 500;
int flashstep = 100;

var watch = System.Diagnostics.Stopwatch.StartNew();

int[,] octopi = LoadOctopiData("input.txt");

Console.Write("Starting octopi map before step #1\n");
PrintOctopiMap(octopi);

int flash = 0;
int answer = 0;
int firstsync = 0;
bool foundFirstSync = false;

foreach (int i in Enumerable.Range(1, totalsteps))
{
    StepOctopi(octopi);
    ChainReaction(octopi, ref flash);
    if (i == flashstep)
        answer = flash;

    if (!foundFirstSync && HasOctopiSynchronized(octopi))
    {
        firstsync = i;
        foundFirstSync = true;
    }
}
Console.WriteLine($"Total flashes at step #{flashstep}: {answer}");
if (foundFirstSync)
    Console.WriteLine($"First synchronization point step #{firstsync}\n");

Console.WriteLine($"Final octopi map at step #{totalsteps}\n");
PrintOctopiMap(octopi);

watch.Stop();
Console.WriteLine($"This took {watch.ElapsedMilliseconds} ms to complete.");


bool HasOctopiSynchronized(int[,] octopi)
{
    for (int y = 0; y < octopi.GetLength(1); y++)
    {
        for (int x = 0; x < octopi.GetLength(0); x++)
        {
            if (octopi[x, y] != 0)
            {
                return false;
            }
        }
    }
    return true;
}

void ChainReaction(int[,] octopi, ref int flash)
{

    List<(int, int)> explosionStack = new();

    for (int y = 0; y < octopi.GetLength(1); y++)
    {
        for (int x = 0; x < octopi.GetLength(0); x++)
        {
            if (octopi[x, y] > 9)
                explosionStack.Add((x, y));
        }
    }

    // for each octopus that will explode, set to 0
    // check surrounding cells for anything not a 0 already, increment by 1
    while (explosionStack.Count > 0)
    {
        (int ox, int oy) octo = explosionStack[0];
        explosionStack.RemoveAt(0);
        if (octopi[octo.ox, octo.oy] > 9) // check
        {
            octopi[octo.ox, octo.oy] = 0;
            flash++;
            // increment the surrounding coordinates
            List<(int, int)> proximity = new List<(int, int)> { (0, -1), (-1, 0), (1, 0), (0, 1), (-1, 1), (1, 1), (-1, -1), (1, -1) };
            foreach ( (int ox, int oy) prox in proximity)
            {
                int checkx = octo.ox + prox.ox;
                int checky = octo.oy + prox.oy;

                if (checkx >= 0 && checky >= 0 && checkx < octopi.GetLength(0) && checky < octopi.GetLength(1))
                {
                    // after a 'step', there should not be a 0, except for one already exploded
                    if (octopi[checkx, checky] != 0)
                        octopi[checkx, checky]++;
                    // if it goes over, add to stack if the coordinates don't already exist
                    if (octopi[checkx, checky] > 9)
                        if (!explosionStack.Contains((checkx, checky)))
                            explosionStack.Add((checkx, checky));

                }
            }
        }
    }
}

void StepOctopi(int[,] octopi)
{
    for (int y = 0; y < octopi.GetLength(1); y++)
        for (int x = 0; x < octopi.GetLength(0); x++)
            octopi[x, y]++;
}

void PrintOctopiMap (int[,] octopi)
{
    for (int y = 0; y < octopi.GetLength(1); y++)
    {
        for (int x = 0; x < octopi.GetLength(0); x++)
            Console.Write($"{octopi[x, y]} ");
        Console.WriteLine();
    }

    Console.WriteLine();
}

/* 
 * Loads the file into a int matrix representing 100 octopi
 */
int[,] LoadOctopiData(string filename)
{
    List<string>? data = null;
    using (var sr = new StreamReader(@filename))
        data = sr.ReadToEnd().Split($"\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();

    int xwidth = data[0].Length;
    int ywidth = data.Count();
    int[,] tOctopi = new int[xwidth, ywidth];
    for(int y = 0; y < ywidth; y++)
    {
        int[] rowdata = new int[xwidth];
        rowdata = Array.ConvertAll(data[y].ToCharArray(), c => (int)Char.GetNumericValue(c));       
        for (int x = 0; x < xwidth; x++)
        {
            tOctopi[x, y] = rowdata[x];
        }
    }
    return tOctopi;
}