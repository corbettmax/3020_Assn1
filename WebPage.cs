﻿// 5 marks
public class WebPage
{
    public string Name { get; set; }
    public string Server { get; set; }
    public List<WebPage> E { get; set; }
...
public WebPage(string name, string host) ...
public int FindLink(string name) ...
...
}
