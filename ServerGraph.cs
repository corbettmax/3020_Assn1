using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace _3020_assn1
{
    public class ServerGraph
    {
        // 3 marks
        private class WebServer
        {
            public string Name;
            public List<WebPage> P;

            public WebServer(string name, List<WebPage> p)
            {
                Name = name;
                P = p;
            }
        }
        private WebServer[] V;
        private bool[,] E;
        private int NumServers;

        // 2 marks
        // Create an empty server graph
        public ServerGraph()
        {
            NumServers = 0;
            V = new WebServer[5];
            E = new bool[5, 5];
        }
        // 2 marks
        // Return the index of the server with the given name; otherwise return -1
        private int FindServer(string name)
        {
            for (int i = 0; i < NumServers; i++)
            {
                if (V[i].Name.Equals(name))
                    return i;
            }
            return -1;
        }
        // 3 marks
        // Double the capacity of the server graph with the respect to web servers
        private void DoubleCapacity()
        {
            WebServer[] tempV = new WebServer[V.Length * 2];
            bool[,] tempE = new bool[V.Length * 2, V.Length * 2];
        }
        // 3 marks
        // Add a server with the given name and connect it to the other server
        // Return true if successful; otherwise return false
        public bool AddServer(string name, string other)
        {
            if (NumServers == V.Length)
            {
                DoubleCapacity();
            }
            if (FindServer(name) == -1)
            {
                V[NumServers] = new WebServer(name, new List<WebPage>());
                for (int i = 0; i <= NumServers; i++)
                {
                    E[i, NumServers] = false;
                    E[NumServers, i] = false;
                }
                NumServers++;
                this.AddConnection(name, other);
                return true;
            }
            return false;
        }
        // 3 marks
        // Add a webpage to the server with the given name
        // Return true if successful; otherwise return false
        public bool AddWebPage(WebPage w, string name)
        {
            int serverIndex = FindServer(name);

            if (serverIndex > -1)
            {
                for (int i = 0; i < V[serverIndex].P.Count; i++)
                {
                    if (V[serverIndex].P[i] == w) return false;
                }

                V[serverIndex].P.Add(w);
                return true;
            }

            return false;

        }
        // 4 marks
        // Remove the server with the given name by assigning its connections
        // and webpages to the other server
        // Return true if successful; otherwise return false
        public bool RemoveServer(string name, string other)
        {
            int i, j;
            if ((i = FindServer(name)) > -1)
            {
                NumServers--;
                V[i] = V[NumServers];
                for (j = NumServers; j >= 0; j--)
                {
                    E[j, i] = E[j, NumServers];
                    E[i, j] = E[NumServers, j];
                }
                return true;
            }

            return false;
        }
        // 3 marks (Bonus)
        // Remove the webpage from the server with the given name
        // Return true if successful; otherwise return false
        public bool RemoveWebPage(string webpage, string name)
        {
            int serverIndex = FindServer(name);

            if (serverIndex > -1)
            {
                for (int i = 0; i < V[serverIndex].P.Count; i++)
                {
                    if (V[serverIndex].P[i].Name == webpage)
                    {
                        V[serverIndex].P.Remove(V[serverIndex].P[i]);
                        return true;
                    }
                }
                return false;
            }

            return false;
        }
        // 3 marks
        // Add a connection from one server to another
        // Return true if successful; otherwise return false
        // Note that each server is connected to at least one other server
        public bool AddConnection(string from, string to)
        {
            int i, j;
            if ((i = FindServer(from)) > -1 && (j = FindServer(to)) > -1)
            {
                if (E[i, j] == false)
                {
                    E[i, j] = true;
                    E[j, i] = true;
                    return true;
                }
            }
            return false;

        }
        // 10 marks
        // Return all servers that would disconnect the server graph into
        // two or more disjoint graphs if ever one of them would go down
        // Hint: Use a variation of the depth-first search
        public string[] CriticalServers()
        {
            string[] critServers = new string[NumServers];

            //loop over all servers and test the connectivity of the graph by checking how many servers are visited 
            for (int i = 0; i < NumServers; i++)
            {
                // tally the number of parts of the graph
                int components = 0;
                // to track the visited servers
                bool[] visited = new bool[NumServers + 1];
                visited[i] = true;

                // iterate over the broken up graph
                for (int j = 0; j < NumServers; j++)
                {
                    if (j != i)
                    {
                        // if server j is not visited form a new component.
                        if (visited[j] == false)
                        {
                            components++;

                            // call depthfirstsearch to visit all servers connected to the components
                            DepthFirstSearch(j, visited);
                        }
                    }
                }

                // If the number of components is more than 1
                // after removing the ith vertex, then vertex i
                // is an articulation point.  
                if (components > 1)
                {
                    critServers[i] = V[i].Name;
                }
            }

            return critServers;
        }



        private void DepthFirstSearch(int i, bool[] visited)
        {
            int j;

            visited[i] = true;    // Output vertex when marked as visited
            //Console.WriteLine(i);

            for (j = 0; j < NumServers; j++)    // Visit next unvisited adjacent vertex
                if (!visited[j] && E[i, j] == true)
                    DepthFirstSearch(j, visited);
        }

        // 6 marks
        // Return the shortest path from one server to another
        // Hint: Use a variation of the breadth-first search
        public int ShortestPath(string from, string to)
        {
            Queue<int> Q = new Queue<int>();
            bool[] visited = new bool[NumServers];
            int[] distance = new int[NumServers];
            int[] predecessor = new int[NumServers];
            int fromIndex = FindServer(from);
            int toIndex = FindServer(to);

            for (int i = 0; i < NumServers; i++)
            {
                visited[i] = false;
                distance[i] = int.MaxValue;
                predecessor[i] = -1;
            }

            visited[fromIndex] = true;
            distance[fromIndex] = 0;
            Q.Enqueue(fromIndex);

            while (Q.Count != 0)
            {
                int i = Q.Dequeue();

                for (int j = 0; j < NumServers; j++)    // Enqueue unvisited adjacent vertices
                    if (!visited[j] && E[i, j] == true)
                    {
                        // Mark vertex as visited
                        visited[j] = true;
                        //set the distance from the start to the current server
                        distance[j] = distance[i] + 1;
                        //set the predecessor to the last visited server
                        predecessor[j] = i;
                        //queue up the adjacent servers
                        Q.Enqueue(j);

                        //if reached the destination then the shortest path has been found
                        if (j == toIndex) return distance[j];
                    }
            }

            return -1;
        }
        // 4 marks
        // Print the name and connections of each server as well as
        // the names of the webpages it hosts
        public void PrintGraph()
        {
            //print all the servers
            int i;
            for (i = 0; i < NumServers; i++)
                Console.WriteLine(V[i].Name);    //@TODO: write all the associated web pages as well

            //print all the connections
            int j;
            for (i = 0; i < NumServers; i++)
                for (j = 0; j < NumServers; j++)
                    if (E[i, j] == true)
                        Console.WriteLine("(Connection: " + V[i].Name + "," + V[j].Name + ")");
        }

    }
}
