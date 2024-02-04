
// 5 marks
public class WebPage
{
    public string Name { get; set; }
    public string Server { get; set; }
    public List<WebPage> E { get; set; }

public WebPage(string name, string host)
    {
        Name = name;
        Server = host;
        E = new List<WebPage>();
    }
public int FindLink(string name)
    {
        for (int i = 0; i < E.Count; i++)
        {
            if (E[i].Name == name)
                return i;
        }
        return -1;
    }

}
public class WebGraph
{
    private List<WebPage> P;

    // 2 marks
    // Create an empty WebGraph
    public WebGraph()
    {
        P = new List<WebPage>();

    }
    // 2 marks
    // Return the index of the webpage with the given name; otherwise return -1
    private int FindPage(string name)
    {
        for (int i = 0; i < P.Count; i++)
        {
            if (P[i].Name.Equals(name))
                return i;
        }
        return -1;
    }
    // 4 marks
    // Add a webpage with the given name and store it on the host server
    // Return true if successful; otherwise return false
    public bool AddPage(string name, string host, ServerGraph S)
    {
        if (FindPage(name) == -1)
        {
            WebPage p = new WebPage(name, host);
            P.Add(p);
            S.AddWebPage(p, name);
            return true;
        }
        return false;
    }
    // 8 marks
    // Remove the webpage with the given name, including the hyperlinks
    // from and to the webpage
    // Return true if successful; otherwise return false
    public bool RemovePage(string name, ServerGraph S)
    {
        int i;
        if ((i = FindPage(name)) > -1)
        {
            for (int j = 0; j < P.Count; j++)
            {
                for (int k = 0; k < P[j].E.Count; k++)
                {
                    if (P[j].E[k].Name.Equals(name))
                    {
                        P[j].E.RemoveAt(k);
                        break;
                    }
                }
            }
            P.RemoveAt(i);
            S.RemoveWebPage(name, V[S.FindPage(name)].Name);
            return true;
        }
        return false;
    }
    // 3 marks
    // Add a hyperlink from one webpage to another
    // Return true if successful; otherwise return false
    public bool AddLink(string from, string to)
    {
        int i, j;
        WebPage e;
        if ((i = FindPage(from)) > -1 &&  (j = FindPage(to)) > -1)
        {
            if (P[i].FindLink(to) == -1)
            {
                e = new WebPage(P[j].Name, P[j].Server);
                P[i].E.Add(e);
                return true;
            }
        }
        return false;
    }
    // 3 marks
    // Remove a hyperlink from one webpage to another
    // Return true if successful; otherwise return false
    public bool RemoveLink(string from, string to)
    {
        int i, j;
        if ((i = FindPage(from)) > -1 && (j = P[i].FindLink(to)) > -1)
        {
            P[i].E.RemoveAt(j);
            return true;
        }
        return false;
    }
    // 6 marks
    // Return the average length of the shortest paths from the webpage with
    // given name to each of its hyperlinks
    // Hint: Use the method ShortestPath in the class ServerGraph
    public float AvgShortestPaths(string name, ServerGraph S)
    {
        int sum = 0;
        for (int i = 0; i < P.Count; i++)
        {
            string link = P[FindPage(name)].E[i].Name;
            sum += S.ShortestPath(name, link);
        }
        return sum = sum / P.Count;
    }
    // 3 marks
    // Print the name and hyperlinks of each webpage
    public void PrintGraph()
    {
        for (int i = 0; i < P.Count; i++)
        {
            Console.WriteLine(P[i].Name);
            for (int j = 0; j < P[i].E.Count; j++)
                Console.WriteLine("(" + P[i].Name + "," + P[i].E[j].Name + ")");
        }
        Console.ReadLine();
    }

}

