using CsvHelper;
using HtmlAgilityPack;
using System.IO;
using System.Collections.Generic;
using System.Globalization;


namespace Webscraper;

class program
{
    public static StreamWriter writers { get; private set; }
    public static CsvWriter csv { get; private set; }

    static void Main(string[] args)
    {
        HtmlWeb web = new HtmlWeb();

        HtmlDocument doc = web.Load("https://en.wikipedia.org/wiki/Denmark");

        var HeaderNames = doc.DocumentNode.SelectNodes("//span[@class='toctext']");
        

        var titles = new List<Row>();

        foreach(var item in HeaderNames)
        {
            titles.Add(new Row { Title = item.InnerText});
        }
        
        using ( writers = new StreamWriter("..\\Wiki.csv"))

        using(csv = new CsvWriter(writers, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(titles);
        }
    }

    public class Row
    {
        public string Title { get; set; }
    }
}

