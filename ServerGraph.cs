using System.Xml.Linq;

public class ServerGraph
{
    // 3 marks
    private class WebServer
    {
        public string Name;
        public List<WebPage> P;
        //tofinish
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
                if (V[i].Equals(name))
                    return i;
            }
            return -1;
        }
    // 3 marks
    // Double the capacity of the server graph with the respect to web servers
    private void DoubleCapacity()
        {
            WebServer[] tempV = new WebServer[V.Length * 2];
            bool [,] tempE = new bool[V.Length*2 , V.Length*2];
    }
    // 3 marks
    // Add a server with the given name and connect it to the other server
    // Return true if successful; otherwise return false
    public bool AddServer(string name, string other)
    {
        if(NumServers == V.Length)
        {
            DoubleCapacity();
        }   
        if (FindServer(name) == -1)
        {
            V[NumServers] = new WebServer;
            for (int i = 0; i <= NumServers; i++)
            {
                E[i, NumServers] = false;
                E[NumServers, i] = false;
            }
            NumServers++;
            return true;
        }
        return false;
    }
    // 3 marks
    // Add a webpage to the server with the given name
    // Return true if successful; otherwise return false
    public bool AddWebPage(WebPage w, string name)
        {

        }
    // 4 marks
    // Remove the server with the given name by assigning its connections
    // and webpages to the other server
    // Return true if successful; otherwise return false
    public bool RemoveServer(string name, string other)
        {

        }
    // 3 marks (Bonus)
    // Remove the webpage from the server with the given name
    // Return true if successful; otherwise return false
    public bool RemoveWebPage(string webpage, string name)
        {

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

        }
    // 6 marks
    // Return the shortest path from one server to another
    // Hint: Use a variation of the breadth-first search
    public int ShortestPath(string from, string to)
        {

        }
    // 4 marks
    // Print the name and connections of each server as well as
    // the names of the webpages it hosts
    public void PrintGraph()
    {
        //print all the servers
        int i;
        for (i = 0; i < NumServers; i++)
            Console.WriteLine(V[i]);    //@TODO: write all the associated web pages as well

        //print all the connections
        int i, j;
        for (i = 0; i < NumServers; i++)
            for (j = 0; j < NumServers; j++)
                if (E[i, j] == true)
                    Console.WriteLine("(" + V[i] + "," + V[j] + "," + E[i, j] + ")");
    }
    
}


