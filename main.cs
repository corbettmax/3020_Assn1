using System;
namespace _3020_assn1
{
    public class main
    {
        static void Main(string[] args)
        {
            // 1.Instantiate a server graph and a web graph.
            WebGraph wgraph = new WebGraph();
            ServerGraph sgraph = new ServerGraph();

            // 2.Add a number of servers.
            for (int i = 1; i <= 5; i++)
            {
                sgraph.AddServer($"Server {i}", "Server " + i);
            }
            //sgraph.PrintGraph();

            // 3.Add additional connections between servers.
            sgraph.AddConnection("Server 1", "Server 4");
            sgraph.AddConnection("Server 2", "Server 5");
            sgraph.AddConnection("Server 5", "Server 3");
            sgraph.AddConnection("Server 1", "Server 3");
            sgraph.AddConnection("Server 4", "Server 5");
            sgraph.AddConnection("Server 3", "Server 2");
            sgraph.PrintGraph();

            // 4.Add a number of webpages to various servers.
            for (int i = 1; i <= 10; i++)
            {
                wgraph.AddPage($"Web Page {i}", "Server " + i,sgraph);
            }
            wgraph.PrintGraph();

            // 5.Add and remove hyperlinks between the webpages.
            wgraph.AddLink("Web Page 1", "Web Page 4");
            wgraph.AddLink("Web Page 2", "Web Page 5");
            wgraph.AddLink("Web Page 6", "Web Page 1");
            wgraph.AddLink("Web Page 7", "Web Page 8");
            wgraph.AddLink("Web Page 10", "Web Page 3");
            wgraph.AddLink("Web Page 8", "Web Page 5");
            wgraph.AddLink("Web Page 6", "Web Page 2");
            wgraph.AddLink("Web Page 1", "Web Page 2");
            wgraph.PrintGraph();

            wgraph.RemoveLink("Web Page 1", "Web Page 4");
            wgraph.RemoveLink("Web Page 2", "Web Page 5");
            wgraph.RemoveLink("Web Page 6", "Web Page 1");
            wgraph.RemoveLink("Web Page 7", "Web Page 8");
            wgraph.RemoveLink("Web Page 10", "Web Page 3");
            wgraph.RemoveLink("Web Page 8", "Web Page 5");
            wgraph.RemoveLink("Web Page 6", "Web Page 2");
            wgraph.PrintGraph();
            

            // 6.Remove both webpages and servers.
            for (int i = 1; i <= 10; i++)
            {
                wgraph.RemovePage($"Web Page {i}", sgraph);
            }
            for (int i = 1; i <= 5; i++)
            {
                sgraph.RemoveServer($"Server {i}", "Other");
            }
            wgraph.PrintGraph();
            sgraph.PrintGraph();

            // 7.Determine the critical servers of the remaining Internet.
            string[] criticalServers = sgraph.CriticalServers();
            for (int i = 0; i < criticalServers.Length; i++)
                if (criticalServers[i] != null)
                    Console.WriteLine(criticalServers[i]);

            // 8.Calculate the average shortest distance to the hyperlinks of a given webpage.
            Console.WriteLine("Average Shortest Path: " + wgraph.AvgShortestPaths("Web Page 1", sgraph));
            Console.WriteLine("Done");
        }
    }
}
