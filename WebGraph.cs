
public class WebGraph
{
    private List<WebPage> P;
...
    // 2 marks
    // Create an empty WebGraph
    public WebGraph() ...
    // 2 marks
    // Return the index of the webpage with the given name; otherwise return -1
    private int FindPage(string name) ...
    // 4 marks
    // Add a webpage with the given name and store it on the host server
    // Return true if successful; otherwise return false
    public bool AddPage(string name, string host, ServerGraph S) ...
    // 8 marks
    // Remove the webpage with the given name, including the hyperlinks
    // from and to the webpage
    // Return true if successful; otherwise return false
    public bool RemovePage(string name, ServerGraph S) ...
    // 3 marks
    // Add a hyperlink from one webpage to another
    // Return true if successful; otherwise return false
    public bool AddLink(string from, string to) ...
    // 3 marks
    // Remove a hyperlink from one webpage to another
    // Return true if successful; otherwise return false
    public bool RemoveLink(string from, string to) ...
    // 6 marks
    // Return the average length of the shortest paths from the webpage with
    // given name to each of its hyperlinks
    // Hint: Use the method ShortestPath in the class ServerGraph
    public float AvgShortestPaths(string name, ServerGraph S) ...
    // 3 marks
    // Print the name and hyperlinks of each webpage
    public void PrintGraph() ...
...
}