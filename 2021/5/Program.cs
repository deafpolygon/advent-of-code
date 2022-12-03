/* 
 * --- Day 5: Hydrothermal Venture ---
 * Solved
 */
using System;
using System.IO;

List<string>? data = null;

try
{
    using (var sr = new StreamReader(@"input.txt"))
    {
        data = sr.ReadToEnd().Split($"\n", StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}
catch (Exception ex)
{
    Console.WriteLine("Error in importing input file:");
    Console.WriteLine(ex.Message);
}

int maxXY = 0;
List<Line> lines = new();

foreach (var d in data)
{
    List<int> pts = new();

    var points = d.Split(" -> ", StringSplitOptions.RemoveEmptyEntries);
    var p1 = points[0].Split(",").Select(int.Parse).ToArray();
    var p2 = points[1].Split(",").Select(int.Parse).ToArray();

    pts.AddRange(p1);
    pts.AddRange(p2);
    pts.Add(maxXY);
    maxXY = pts.Max();

    Point pt1 = new Point(p1[0], p1[1]);
    Point pt2 = new Point(p2[0], p2[1]);

    lines.Add(new Line(pt1, pt2));
}


OceanFloorMapper mapper = new(maxXY);

foreach (Line line in lines)
{
    var plotpoints = line.PlotLine();

    foreach (var pt in plotpoints)
    {
        mapper.AddPoint(pt);
    }
}

int intersects = mapper.GetIntersectionCount();
Console.WriteLine($"There are {intersects} intersections");




class Point
{
    public double X;
    public double Y;

    public Point(int p1, int p2)
    {
        this.X = p1;
        this.Y = p2;
    }
}

class Line
{

    public Point Point1 { get; set; }
    public Point Point2 { get; set; }

    public Line(Point p1, Point p2)
    {
        Point1 = p1;
        Point2 = p2;
    }

    public float Slope()
    {
        float m = (float)( (Point2.Y - Point1.Y) / (Point2.X - Point1.X) );
        return m;
    }

    // returns a list of all integer points between the two points, if any
    public List<(int, int)> PlotLine()
    {
        List<(int, int)> plot = new();

        if(float.IsInfinity(this.Slope())) {
            // plot the lines y1 to y2 with x = static value
            int starty = 0;
            int endy = 0;

            if (this.Point1.Y < this.Point2.Y)
            {
                starty = (int)this.Point1.Y;
                endy = (int)this.Point2.Y;
            }
            else
            {
                starty = (int)this.Point2.Y;
                endy = (int)this.Point1.Y;
            }
                

            for (int y = starty; y <= endy; y++)
            {
                plot.Add(((int)this.Point1.X, y));
            }

        }
        else if (Math.Abs(this.Slope()) == 0)
        {
            int startx = 0;
            int endx = 0;

            if (this.Point1.X < this.Point2.X)
            {
                startx = (int)this.Point1.X;
                endx = (int)this.Point2.X;
            }
            else
            {
                startx = (int)this.Point2.X;
                endx = (int)this.Point1.X;
            }

            for (int x = startx; x <= endx; x++)
            {
                plot.Add((x, (int)this.Point1.Y));
            }
        }
        else if (Math.Abs(this.Slope()) == 1)
        {
            int startx, starty, endx, endy = 0;
            // plot the lines x1,y1 to x2,y2 by adding or subtracting just 1
            if (this.Point1.X < this.Point2.X)
            {
                startx = (int)this.Point1.X;
                starty = (int)this.Point1.Y;
                endx = (int)this.Point2.X;
                endy = (int)this.Point2.Y;
            }
            else
            {
                startx = (int)this.Point2.X;
                starty = (int)this.Point2.Y;
                endx = (int)this.Point1.X;
                endy = (int)this.Point1.Y;
            }

            int iterx = startx;
            int itery = starty;

            //plot.Add((startx, starty));
            while (iterx <= endx)
            {
                plot.Add((iterx, itery));
                iterx = (int)(iterx + 1);
                itery = (int)(itery + this.Slope());

            }
        }
        else
        {
            // handle other cases here
        }

        return plot;

    }

    public override string ToString()
    {
        if (float.IsInfinity(this.Slope())) {
            return $"x = {this.Point1.X}";
        }
        else if (this.Slope() == 0)
        {
            return $"y = {this.Point1.Y}";
        }
        else if (this.Slope() == -1)
        {
            return $"y = -x + {this.YIntercept()}";
        }
        else if (this.Slope() == 1)
        {
            return $"y = x + {this.YIntercept()}";
        }

        return $"y = {this.Slope()}x + {this.YIntercept()}";

    }

    public float YIntercept()
    {
        float slope = this.Slope();
        float intercept = (float)(Point1.Y - (slope * Point1.X));
        return intercept;
    }
    public bool IsVertOrHoriz()
    {
        if (Point1.X == Point2.X) { return true; }
        else if (Point1.Y == Point2.Y) { return true; }
        else
        {
            return false;
        }
    }
}

public class OceanFloorMapper
{
    int[,] map;

    public OceanFloorMapper(int maxXY)
    {
        Console.WriteLine($"Creating a map grid with size {maxXY}x{maxXY}");
        map = new int[maxXY+1, maxXY+1]; //set size

    }

    public void AddPoint( (int x, int y) point)
    {
        map[point.x, point.y]++;
    }

    public void PrintMap()
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                Console.Write(map[j, i]);
            }
            Console.Write("\n");
        }
    }

    public int GetIntersectionCount()
    {
        int counter = 0;

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[j, i] >= 2)
                {
                    counter++;
                }
            }
        }
        return counter;
    }
}
       

