using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Average_Line_Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            //gather data
            int numberLineGraphs = (int)getDouble("Number of total line graphs");
            Function[] functions = new Function[numberLineGraphs];
            for (int lineGraphIndex = 0; lineGraphIndex < numberLineGraphs; lineGraphIndex++)
            {
                int pointsPerLineGraph = (int)getDouble("Points per line segment");
                Point[] points = new Point[pointsPerLineGraph];
                for (int pointIndex = 0; pointIndex < pointsPerLineGraph; pointIndex++)
                {
                    points[pointIndex] = getPoint();
                }
                functions[lineGraphIndex] = new LineGraph(points);
            }

            //get params.
            Double step = getDouble("Step size");
            Double startingPoint = getDouble("Start");
            Double endPoint = getDouble("End");

            //calculate resulting value
            Point[] averagePoints = new Point[(int)((endPoint - startingPoint) / step)];
            int averagePointIndex = 0;
            for (Double x = startingPoint; x < endPoint; x += step)
            {
                Double cumulativeY = 0;
                foreach (Function function in functions)
                {
                    cumulativeY += function.f(x);
                }
                Double averageY = cumulativeY / functions.Length;
                averagePoints[averagePointIndex] = new Point(x, averageY);
                averagePointIndex++;
            }

            //spitting it back out
            Console.WriteLine("Output:");
            foreach (Point averagePoint in averagePoints)
            {
                Console.WriteLine(averagePoint.x + ", " + averagePoint.y);
            }
            Console.Read();
        }

        static Point getPoint()
        {
            String[] points = Console.ReadLine().Split(new char[2] { ',', ' ' }, 2);
            Double x = 0;
            Double y = 0;
            Double.TryParse(points[0], out x);
            Double.TryParse(points[1], out y);
            return new Point(x, y);
        }

        static Double getDouble(String prompt)
        {
            Console.Write(prompt + ":");
            Double returnDouble;
            Double.TryParse(Console.ReadLine(), out returnDouble);
            return returnDouble;
        }

        struct Point
        {
            public Double x;
            public Double y;

            public Point(Double x, Double y)
            {
                this.x = x;
                this.y = y;
            }
        }

        interface Function
        {
            Double f(Double x);
        }

        class LineGraph : Function
        {
            Point[] points;

            public LineGraph(Point[] points)
            {
                this.points = points;
            }

            public Double f(Double x)
            {
                for(int index = 1; index < points.Length; index++) {
                    if ((points[index - 1].x <= x) && (x < points[index].x))
                    {
                        return ((points[index - 1].y - points[index].y) / 
                                (points[index - 1].x - points[index].x)) 
                                * (x - points[index].x) + points[index].y;
                    }
                }
                return 0.00;
            }
        }
    }
}
