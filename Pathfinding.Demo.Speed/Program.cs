using Pathfinding.Core;
using Pathfinding.Finders;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Pathfinding.Demo.Speed
{
    /// <summary>
    /// Speed tests
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Random class
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            List<Graph> graphs = new List<Graph>();
            //graphs.Add(EmptyGraph1());
            //graphs.Add(EmptyGraph2());
            //graphs.Add(EmptyGraph3());
            //graphs.Add(EmptyGraph4());
            //graphs.Add(EmptyGraph5());
            //graphs.Add(EmptyGraph6());
            graphs.Add(Graph1());
            graphs.Add(Graph2());
            graphs.Add(Graph3());
            graphs.Add(Graph4());
            graphs.Add(Graph5());
            graphs.Add(Graph6());

            foreach (Graph graph in graphs)
            {
                Console.WriteLine(graph);

                Dijkstra dijkstra = new Dijkstra(graph);
                Astar astarManhattan = new Astar(graph);
                Astar astarEuclidean = new Astar(graph, HeuristicType.Euclidean);
                Astar astarChebyshev = new Astar(graph, HeuristicType.Chebyshev);

                Stopwatch stopwatch1 = Stopwatch.StartNew();
                SearchResult r1 = dijkstra.Search();
                stopwatch1.Stop();

                Stopwatch stopwatch2 = Stopwatch.StartNew();
                SearchResult r2 = astarManhattan.Search();
                stopwatch2.Stop();

                Stopwatch stopwatch3 = Stopwatch.StartNew();
                SearchResult r3 = astarEuclidean.Search();
                stopwatch3.Stop();

                Stopwatch stopwatch4 = Stopwatch.StartNew();
                SearchResult r4 = astarChebyshev.Search();
                stopwatch4.Stop();

                Console.WriteLine("Dijkstra: {0}ms {1}", stopwatch1.ElapsedMilliseconds, r1.FoundPath);
                Console.WriteLine("A* (Manhattan): {0}ms {1}", stopwatch2.ElapsedMilliseconds, r2.FoundPath);
                Console.WriteLine("A* (Euclidean): {0}ms {1}", stopwatch3.ElapsedMilliseconds, r3.FoundPath);
                Console.WriteLine("A* (Chebyshev): {0}ms {1}", stopwatch4.ElapsedMilliseconds, r4.FoundPath);
            }

            Console.ReadLine();
        }

        private static Graph EmptyGraph1()
        {
            Graph graph = new Graph(25, 25);
            graph.Start = graph.Map[0, 0];
            graph.Start.Type = NodeType.Start;
            graph.End = graph.Map[24, 24];
            graph.End.Type = NodeType.End;

            return graph;
        }

        private static Graph EmptyGraph2()
        {
            Graph graph = new Graph(50, 50);
            graph.Start = graph.Map[0, 0];
            graph.Start.Type = NodeType.Start;
            graph.End = graph.Map[49, 49];
            graph.End.Type = NodeType.End;

            return graph;
        }

        private static Graph EmptyGraph3()
        {
            Graph graph = new Graph(100, 100);
            graph.Start = graph.Map[0, 0];
            graph.Start.Type = NodeType.Start;
            graph.End = graph.Map[99, 99];
            graph.End.Type = NodeType.End;

            return graph;
        }

        private static Graph EmptyGraph4()
        {
            Graph graph = new Graph(150, 150);
            graph.Start = graph.Map[0, 0];
            graph.Start.Type = NodeType.Start;
            graph.End = graph.Map[149, 149];
            graph.End.Type = NodeType.End;

            return graph;
        }

        private static Graph EmptyGraph5()
        {
            Graph graph = new Graph(200, 200);
            graph.Start = graph.Map[0, 0];
            graph.Start.Type = NodeType.Start;
            graph.End = graph.Map[199, 199];
            graph.End.Type = NodeType.End;

            return graph;
        }

        private static Graph EmptyGraph6()
        {
            Graph graph = new Graph(250, 250);
            graph.Start = graph.Map[0, 0];
            graph.Start.Type = NodeType.Start;
            graph.End = graph.Map[249, 249];
            graph.End.Type = NodeType.End;

            return graph;
        }

        private static Graph Graph1()
        {
            Graph graph = new Graph(25, 25);
            graph.Start = graph.Map[0, 0];
            graph.Start.Type = NodeType.Start;
            graph.End = graph.Map[24, 24];
            graph.End.Type = NodeType.End;

            int r = 200;
            for (int i = 0; i < r; i++)
            {
                int x = random.Next(25);
                int y = random.Next(25);
                if (graph.Map[x, y].Type == NodeType.Empty)
                {
                    graph.Map[x, y].Type = NodeType.Wall;
                }
            }

            return graph;
        }

        private static Graph Graph2()
        {
            Graph graph = new Graph(50, 50);
            graph.Start = graph.Map[0, 0];
            graph.Start.Type = NodeType.Start;
            graph.End = graph.Map[49, 49];
            graph.End.Type = NodeType.End;

            int r = 800;
            for (int i = 0; i < r; i++)
            {
                int x = random.Next(50);
                int y = random.Next(50);
                if (graph.Map[x, y].Type == NodeType.Empty)
                {
                    graph.Map[x, y].Type = NodeType.Wall;
                }
            }

            return graph;
        }

        private static Graph Graph3()
        {
            Graph graph = new Graph(100, 100);
            graph.Start = graph.Map[0, 0];
            graph.Start.Type = NodeType.Start;
            graph.End = graph.Map[99, 99];
            graph.End.Type = NodeType.End;

            int r = 3000;
            for (int i = 0; i < r; i++)
            {
                int x = random.Next(100);
                int y = random.Next(100);
                if (graph.Map[x, y].Type == NodeType.Empty)
                {
                    graph.Map[x, y].Type = NodeType.Wall;
                }
            }

            return graph;
        }

        private static Graph Graph4()
        {
            Graph graph = new Graph(150, 150);
            graph.Start = graph.Map[0, 0];
            graph.Start.Type = NodeType.Start;
            graph.End = graph.Map[149, 149];
            graph.End.Type = NodeType.End;

            int r = 7000;
            for (int i = 0; i < r; i++)
            {
                int x = random.Next(150);
                int y = random.Next(150);
                if (graph.Map[x, y].Type == NodeType.Empty)
                {
                    graph.Map[x, y].Type = NodeType.Wall;
                }
            }

            return graph;
        }

        private static Graph Graph5()
        {
            Graph graph = new Graph(200, 200);
            graph.Start = graph.Map[0, 0];
            graph.Start.Type = NodeType.Start;
            graph.End = graph.Map[199, 199];
            graph.End.Type = NodeType.End;

            int r = 13000;
            for (int i = 0; i < r; i++)
            {
                int x = random.Next(200);
                int y = random.Next(200);
                if (graph.Map[x, y].Type == NodeType.Empty)
                {
                    graph.Map[x, y].Type = NodeType.Wall;
                }
            }

            return graph;
        }

        private static Graph Graph6()
        {
            Graph graph = new Graph(250, 250);
            graph.Start = graph.Map[0, 0];
            graph.Start.Type = NodeType.Start;
            graph.End = graph.Map[249, 249];
            graph.End.Type = NodeType.End;

            int r = 20000;
            for (int i = 0; i < r; i++)
            {
                int x = random.Next(250);
                int y = random.Next(250);
                if (graph.Map[x, y].Type == NodeType.Empty)
                {
                    graph.Map[x, y].Type = NodeType.Wall;
                }
            }

            return graph;
        }
    }
}
