using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace GeneratorCutHtml
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding encoding = Encoding.GetEncoding("windows-1251");
            string htmlAll = File.ReadAllText("history.html", encoding);
            var htmlDocAll = new HtmlDocument();
            htmlDocAll.LoadHtml(htmlAll);
            var platDivs = htmlDocAll.DocumentNode.SelectNodes("//div[@class='printArea printView fixedTable']");

            string htmlTemplate = File.ReadAllText("Template.html",encoding);
            var htmlDocTemplate = new HtmlDocument();
            htmlDocTemplate.LoadHtml(htmlTemplate);
            var htmlBody = htmlDocTemplate.DocumentNode.SelectSingleNode("//body");

            foreach (var oneDiv in platDivs)
            {
                var check = oneDiv.InnerHtml;
                //var check = "<td style=\"border - width: 0; height: 18mm; padding: 0; \" colspan=\"5\">< strong > Госпошлина по делам, рассматриваемым в судах общей юрисдикции, мировыми судьями Договор № k6oa98 от 19.05.2017 </ strong >< br />< br /></ td > <td style=\"border - width: 0; height: 18mm; padding: 0; \" colspan=\"5\">< strong > Госпошлина по делам, рассматриваемым в судах общей юрисдикции, мировыми судьями Договор № k6oa98 от 19.05.2017 </ strong >< br />< br /></ td > ";
                var dogovor = Regex.Match(check, @"Договор\s+№\s+\w*\s");
                var numDogovor = (dogovor.Value.Replace("Договор №", "")).Replace(" ", string.Empty);
                htmlBody.AppendChild(oneDiv);

                htmlDocTemplate.Save("plategs\\" + numDogovor + ".html", encoding);

                htmlBody.RemoveChild(oneDiv);
            }
        }
    }
}
