using NeoMix.BLL;
using NeoMix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using Terradue.ServiceModel.Syndication;

namespace NeoMix.Util
{
    public class RSSFeed
    {
        private NewsBLL _newsBLL = new NewsBLL();

        public string FeedRSS()
        {
            string result = "";

            result += "Mais E-Sports: " + ConsumeRSS("Mais E-Sports", "League of Legends", "http://www.lolnews.com.br/feed/") + "; \r\n";

            result += "MyCNB: " + ConsumeRSS("MyCNB", "E-Sports", "http://mycnb.uol.com.br/noticias?format=feed&type=rss") + "; \r\n";

            //result += "Tecmundo: " + ConsumeRSS("Tecmundo", "E-Sports", "http://feeds.feedburner.com/baixakijogos") + "; \r\n";

            result += "Blizz Hearth: " + ConsumeRSS("Blizzard", "Hearthstone", "http://us.battle.net/hearthstone/pt/feed/news") + "; \r\n";

            result += "Coja HS: " + ConsumeRSS("Coja", "Hearthstone", "http://cojagamer.com.br/DesktopModules/Blog/API/RSS/Get?tabid=90&moduleid=419&blog=1&termid=20&body=true") + "; \r\n";
            result += "COJA HOTS: " + ConsumeRSS("Coja", "Heroes of the Storm", "http://cojagamer.com.br/DesktopModules/Blog/API/RSS/Get?tabid=90&moduleid=419&blog=1&termid=26&body=true") + "; \r\n";
            result += "COJA OW: " + ConsumeRSS("Coja", "Overwatch", "http://cojagamer.com.br/DesktopModules/Blog/API/RSS/Get?tabid=90&moduleid=419&blog=1&termid=22&body=true") + "; \r\n";

            result += "WOWGIRL: " + ConsumeRSS("WoWGirl", "Blizzard", "http://feeds.feedburner.com/wowgirl") + "; \r\n";

            result += "TeamPlay: " + ConsumeRSS("TeamPlay", "E-Sports", "http://feeds.feedburner.com/Teamplay-ElectronicSports") + "; \r\n";

            result += "5Pots: " + Consume5Pots() + "; \r\n";

            result += "IGN: " + ConsumeRSS("IGN", "E-Sports", "http://br.ign.com/feed.xml") + "; \r\n";

            return result;
        }

        private int Consume5Pots()
        {
            string url = "5Pots";
            string type = "League of Legends";
            string link = "http://5pots.com/";
            HttpDownloader httpDownloader;
            List<News> RssNews = new List<News>();

            News _new = new News();

            httpDownloader = new HttpDownloader(link, "", "");

            string html = httpDownloader.GetPage();
            html = Regex.Replace(html, @"\t", "");
            html = Regex.Replace(html, @"\n", "");

            string[] aux = html.Split(new string[] { "full" }, StringSplitOptions.None);


            for (int i = 3; i < aux.Length; i++)
            {
                string[] subAux = aux[i].Split('>');

                _new.Url = url;
                _new.Source = url;
                _new.NewsType = type;
                _new.Link = subAux[18].Split('"')[1];

                _new.Title = subAux[6].Split('<')[0];
                _new.Img = subAux[19].Split('"')[5];
                _new.Date = DateTime.Parse(subAux[11].Split(' ')[2]);
                if (subAux[24] != "<strong")
                {
                    _new.ShortDesc = subAux[24].Split('<')[0];
                    _new.Text = "<p>Fala meu povo Mixturados, mais uma notícia adicionada para vocês, retirada do RSS do site " + url + "</p><p>" + subAux[26].Split('<')[0] + "</p><p>Para continuar lendo esta super matéria, basta clicar no link: <a href='" + _new.Link + "'>" + _new.Link + "</a></p>";
                }
                else
                {
                    _new.ShortDesc = subAux[29].Split('<')[0];
                    _new.Text = "<p>Fala meu povo Mixturados, mais uma notícia adicionada para vocês, retirada do RSS do site " + url + "</p><p>" + subAux[32].Split('<')[0] + "</p><p>Para continuar lendo esta super matéria, basta clicar no link: <a href='" + _new.Link + "'>" + _new.Link + "</a></p>";
                }
                
                _new.Author = "MixBot";
                _new.IsPublished = true;

                RssNews.Add(_new);
                _new = new News();
            }

            _new = new News();

            return CreateNews(RssNews);
        }

        private int ConsumeSiteHard(string url, string type, string site)
        {
            HttpDownloader httpDownloader;
            List<News> RssNews = new List<News>();

            News _new = new News();

            WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent", "MyRSSReader/1.0");

            XmlReader reader = XmlReader.Create(webClient.OpenRead(site));
            var xmlDocument = new XmlDocument();
            //xmlDocument.Load(reader);

            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            foreach (SyndicationItem item in feed.Items)
            {
                String subject = item.Title.Text;
                String summary = item.Summary.Text;

                string link = item.Links[0].Uri.AbsoluteUri;

                httpDownloader = new HttpDownloader(link, "", "");

                string html = httpDownloader.GetPage();

                //var html = new System.Net.WebClient().DownloadString(link);
                _new.Img = FetchLinksFromSource(html, url);

                _new.Url = url;
                _new.Link = item.Links[0].Uri.AbsoluteUri;
                _new.Text = FormatText(item.Summary.Text, url, _new.Link);
                _new.ShortDesc = item.Title.Text;
                _new.NewsType = type;
                _new.Date = DateTime.Parse(item.PublishDate.ToString());
                _new.Author = "MixBot";
                _new.Title = item.Title.Text;
                _new.Source = url;
                _new.IsPublished = true;

                RssNews.Add(_new);
                _new = new News();
            }

            return CreateNews(RssNews);
        }

        private int ConsumeSite(string url, string type, string site)
        {
            HttpDownloader httpDownloader;
            List<News> RssNews = new List<News>();

            News _new = new News();

            XmlReader reader = XmlReader.Create(site);
            var xmlDocument = new XmlDocument();

            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            foreach (SyndicationItem item in feed.Items)
            {
                _new.Date = DateTime.Parse(item.PublishDate.ToString());

                if (_new.Date >= new DateTime(2016, 05, 16))
                {
                    string link;
                    if (url != "NerfPlz")
                        link = item.Links[0].Uri.AbsoluteUri;
                    else
                        link = item.Links[4].Uri.AbsoluteUri;

                    httpDownloader = new HttpDownloader(link, "", "");

                    string html = httpDownloader.GetPage();

                    //var html = new System.Net.WebClient().DownloadString(link);
                    _new.Img = FetchLinksFromSource(html, url);

                    _new.Url = url;
                    _new.Text = GenerateText(html, url, item);
                    _new.ShortDesc = item.Title.Text;
                    _new.NewsType = type;
                    _new.Date = DateTime.Parse(item.PublishDate.ToString());
                    _new.Author = "MixBot";
                    _new.Title = item.Title.Text;
                    _new.Source = url;
                    _new.Link = link;
                    _new.IsPublished = false;

                    RssNews.Add(_new);
                }

                _new = new News();
            }

            return CreateNews(RssNews);
        }

        private int ConsumeCoja(string url, string type, string site)
        {
            HttpDownloader httpDownloader;
            List<News> RssNews = new List<News>();
            News _news = new News();

            var webClient = new WebClient();

            httpDownloader = new HttpDownloader(site, "", "");

            string rss = httpDownloader.GetPage();

            XDocument document = XDocument.Parse(rss);

            XNamespace nsContent = "http://purl.org/rss/1.0/modules/content/";
            XNamespace dcM = "http://search.yahoo.com/mrss/";

            foreach (var descendant in document.Descendants("item"))
            {
                _news.Text = descendant.Element(nsContent + "encoded").Value;
                _news.ShortDesc = descendant.Element("description").Value;
                _news.Date = DateTime.Parse(descendant.Element("pubDate").Value);
                _news.Title = descendant.Element("title").Value;
                _news.Img = descendant.Element(dcM + "thumbnail").Attribute("url").Value;
                _news.Author = "MixBot";
                _news.Source = url;
                _news.Url = url;
                _news.NewsType = type;
                _news.Link = descendant.Element("link").Value;
                _news.IsPublished = false;

                RssNews.Add(_news);

                _news = new News();
            }


            return CreateNews(RssNews);
        }

        private int ConsumeRSS(string url, string type, string site)
        {
            HttpDownloader httpDownloader;
            List<News> RssNews = new List<News>();
            News _news = new News();

            var webClient = new WebClient();

            httpDownloader = new HttpDownloader(site, "", "");
            string rss = httpDownloader.GetPage();

            XDocument document = XDocument.Parse(rss);

            XNamespace nsContent = "http://purl.org/rss/1.0/modules/content/";
            XNamespace dcM = "http://search.yahoo.com/mrss/";

            foreach (var descendant in document.Descendants("item"))
            {
                if (descendant.Element(nsContent + "encoded") != null)
                    _news.Text = ConvertEncoding(descendant.Element(nsContent + "encoded").Value);
                else
                    _news.Text = ConvertEncoding(descendant.Element("description").Value);
                _news.Text = FormatText(_news.Text, url, descendant.Element("link").Value);
                _news.ShortDesc = StripTagsRegex(descendant.Element("description").Value);
                _news.Date = DateTime.Parse(descendant.Element("pubDate").Value);
                _news.Title = descendant.Element("title").Value;
                if (descendant.Element(dcM + "thumbnail") == null)
                {
                    if (descendant.Element("enclosure") == null)
                    {
                        httpDownloader = new HttpDownloader(descendant.Element("link").Value, "", "");

                        string html = httpDownloader.GetPage();
                        _news.Img = FetchLinksFromSource(html, url);
                    }
                    else
                        _news.Img = descendant.Element("enclosure").Attribute("url").Value;
                }
                else
                    _news.Img = descendant.Element(dcM + "thumbnail").Attribute("url").Value;
                _news.Author = "MixBot";
                _news.Source = url;
                _news.Url = url;
                _news.NewsType = DefineType(_news.Text, _news.Title);
                _news.Link = descendant.Element("link").Value;
                _news.IsPublished = true;

                RssNews.Add(_news);

                _news = new News();
            }

            foreach (var descendant in document.Descendants("entry"))
            {
                if (descendant.Element(nsContent + "encoded") != null)
                    _news.Text = descendant.Element(nsContent + "encoded").Value;
                else
                    _news.Text = descendant.Element("description").Value;
                _news.Text = FormatText(_news.Text, url, descendant.Element("link").Value);
                _news.ShortDesc = StripTagsRegex(descendant.Element("description").Value);
                _news.Date = DateTime.Parse(descendant.Element("pubDate").Value);
                _news.Title = descendant.Element("title").Value;
                if (descendant.Element(dcM + "thumbnail") == null)
                {
                    if (descendant.Element("enclosure") == null)
                    {
                        httpDownloader = new HttpDownloader(descendant.Element("link").Value, "", "");

                        string html = httpDownloader.GetPage();
                        _news.Img = FetchLinksFromSource(html, url);
                    }
                    else
                        _news.Img = descendant.Element("enclosure").Attribute("url").Value;
                }
                else
                    _news.Img = descendant.Element(dcM + "thumbnail").Attribute("url").Value;
                _news.Author = "MixBot";
                _news.Source = url;
                _news.Url = url;
                _news.NewsType = DefineType(_news.Text, _news.Title);
                _news.Link = descendant.Element("link").Value;
                _news.IsPublished = true;

                RssNews.Add(_news);

                _news = new News();
            }

            return CreateNews(RssNews);
        }

        private string GenerateText(string html, string url, SyndicationItem item)
        {
            string text = "<p>Fala meu povo Mixturados, mais uma notícia adicionada para vocês!</p>";
            string[] aux;

            switch (url)
            {
                case "LolNews":
                    aux = html.Split(new string[] { "<div class=\"entry-content\">" }, StringSplitOptions.None);
                    aux = aux[1].Split(new string[] { "<div style=\"    width: 100%;" }, StringSplitOptions.None);
                    text += aux[0];
                    break;

                case "Gamelogia":
                    aux = html.Split(new string[] { "<div class=\"post-meta\">" }, StringSplitOptions.None);
                    aux = aux[1].Split(new string[] { "<div class=\"sharedaddy sd-sharing-enabled\">" }, StringSplitOptions.None);
                    text += aux[0];
                    break;

                case "Hearth PWN":
                    text += item.Summary.Text;
                    break;

                case "NerfPlz":
                    TextSyndicationContent tsc = (TextSyndicationContent)item.Content;
                    text += tsc.Text;
                    break;

                case "Heroes Nexus":
                    //img = links[3];
                    break;

                case "MyCNB":
                    aux = html.Split(new string[] { "<div class=\"intro-artigo\">" }, StringSplitOptions.None);
                    aux = aux[1].Split(new string[] { "<a target='_blank' href='index.php?option=com_banners&task=click&id=27' >" }, StringSplitOptions.None);
                    text += aux[0];

                    aux = text.Split(new string[] { "<img src=\"/" }, StringSplitOptions.None);
                    if (aux.Length > 1)
                        text = aux[0] + "<img src=\"http://mycnb.uol.com.br/" + aux[1];
                    break;

                case "Tecmundo":
                    aux = html.Split(new string[] { "<div class=\"article-text highlight\" itemprop=articleBody><div class=\"uk-hidden-small nzn-ads-rectangle\" id=ads-square><script type=text/javascript>googletag.cmd.push(function(){googletag.display('ads-square');});</script></div>" }, StringSplitOptions.None);
                    aux = aux[1].Split(new string[] { "<p class=nzn-rexposta-intervention>" }, StringSplitOptions.None);
                    text += aux[0];
                    break;
            }

            return text;
        }

        private string FormatText(string text, string url, string link)
        {
            string finalText = "<p>Fala meu povo Mixturados, mais uma notícia adicionada para vocês, retirada do RSS do site " + url + "</p>";
            string[] aux;

            switch (url)
            {
                case "Mais E-Sports":
                    aux = text.Split(new string[] { "</p>" }, StringSplitOptions.None);
                    finalText += aux[0] + "</p>" + aux[1] + "</p>" + aux[2] + "</p>";
                    break;

                default:
                    finalText += text;
                    break;
            }

            finalText += "<p>Para continuar lendo esta super matéria, basta clicar no link: <a href='" + link + "'>" + link + "</a></p>";

            return finalText;
        }

        private int CreateNews(List<News> rssNews)
        {
            int result = 0;
            DateTime expireDate = new DateTime(2016, 05, 24);

            List<News> allNews = _newsBLL.NewsList();

            foreach (News n in rssNews)
            {
                n.Text = RemakeNew(n.Text);

                if (allNews.Find(x => x.Title == n.Title) == null && n.Date > expireDate)
                {
                    if (_newsBLL.NewsCreate(n))
                        result++;
                }
            }
            return result;
        }

        private string RemakeNew(string text)
        {
            text = text.Replace("&nbsp;", "");

            text = text.Replace("&", "");

            return text;
        }

        private string FetchLinksFromSource(string htmlSource, string source)
        {
            string img = "";
            string[] imgs = htmlSource.Split(new string[] { "property=\"og:image\" content=\"" }, StringSplitOptions.None);

            switch (source)
            {
                case "Mais E-Sports":
                    try
                    {
                        img = imgs[1].Split('"')[0];
                    }
                    catch
                    {
                        imgs = htmlSource.Split(new string[] { "<div class=\"image\">" }, StringSplitOptions.None);
                        img = imgs[1].Split('"')[1];
                    }
                    break;

                case "Gamelogia":
                    img = imgs[1].Split('"')[0];
                    break;

                case "Hearth PWN":
                    //img = links[0];
                    break;

                case "Heroes Nexus":
                    //img = links[3];
                    break;

                case "NerfPlz":
                    string[] aux = htmlSource.Split(new string[] { "<div class=\"topimage\">" }, StringSplitOptions.None);
                    img = aux[1].Split('"')[3];
                    break;

                case "MyCNB":
                    img = imgs[1].Split('"')[0];
                    break;

                case "TeamPlay":                    
                    img = imgs[1].Split('"')[0];
                    break;

            }

            return img;
        }

        private string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        private string ConvertEncoding(string sString)
        {

            if ((!string.IsNullOrEmpty(sString)))
            {
                sString = sString.Replace("&aacute;", "á");
                sString = sString.Replace("&acirc;", "â");
                sString = sString.Replace("&agrave;", "à");
                sString = sString.Replace("&atilde;", "ã");

                sString = sString.Replace("&ccedil;", "ç");

                sString = sString.Replace("&eacute;", "é");
                sString = sString.Replace("&ecirc;", "ê");

                sString = sString.Replace("&iacute;", "í");

                sString = sString.Replace("&oacute;", "ó");
                sString = sString.Replace("&ocirc;", "ô");
                sString = sString.Replace("&otilde;", "õ");

                sString = sString.Replace("&uacute;", "ú");
                sString = sString.Replace("&uuml;", "ü");

                sString = sString.Replace("&Aacute;", "Á");
                sString = sString.Replace("&Acirc;", "Â");
                sString = sString.Replace("&Agrave;", "À");
                sString = sString.Replace("&Atilde;", "Ã");

                sString = sString.Replace("&Ccedil;", "Ç");

                sString = sString.Replace("&Eacute;", "É");
                sString = sString.Replace("&Ecirc;", "Ê");

                sString = sString.Replace("&Iacute;", "Í");

                sString = sString.Replace("&Oacute;", "Ó");
                sString = sString.Replace("&Ocirc;", "Ô");
                sString = sString.Replace("&Otilde;", "Õ");

                sString = sString.Replace("&Uacute;", "Ú");
                sString = sString.Replace("&Uuml;", "Ü");

                sString = sString.Replace("&quot;", "\"");
                //"
                sString = sString.Replace("&lt;", "<");
                //<
                sString = sString.Replace("&gt;", ">");
                //>
                sString = sString.Replace("&ordf;", "ª");
                sString = sString.Replace("&ordm;", "º");

                sString = sString.Replace("#8220;", "\"");
                sString = sString.Replace("#8221;", "\"");

                sString = sString.Replace("#231;", "ç");
                sString = sString.Replace("#227;", "ã");
                sString = sString.Replace("#233;", "é");
                sString = sString.Replace("#234;", "ê");
                sString = sString.Replace("#225;", "á");
                sString = sString.Replace("#250;", "ú");
                sString = sString.Replace("#243;", "ó");

            }

            return sString;
        }

        public string DefineType(string text, string title)
        {
            string result = "E-Sports";

            if (text.Contains("League of Legends") || text.Contains("LOL") || text.Contains("Riot Games") || text.Contains("Riot") || text.Contains("PBE") || text.Contains("Tier List") || title.Contains("League of Legends") || title.Contains("LOL") || title.Contains("Riot Games") || title.Contains("Riot") || title.Contains("PBE") || title.Contains("Tier List"))
                result = "League of Legends";

            if (text.Contains("Counter Strike") || text.Contains("CS:GO") || text.Contains("ELEAGUE") || text.Contains("Counter-Strike Global Offensive") || title.Contains("Counter Strike") || title.Contains("CS:GO") || title.Contains("ELEAGUE") || title.Contains("Counter-Strike Global Offensive"))
                result = "Counter Strike";

            if (text.Contains("CBLOL") || text.Contains("Desafiante") || title.Contains("CBLOL") || title.Contains("Desafiante") || text.Contains("CBLoL") || title.Contains("CBLoL") || text.Contains("Desafiante") || title.Contains("Desafiante"))
                result = "CBLOL";

            if (text.Contains("DOTA") || text.Contains("Dota") || title.Contains("DOTA") || title.Contains("Dota"))
                result = "DOTA";

            if (text.Contains("SMITE") || text.Contains("Smite") || title.Contains("SMITE") || title.Contains("Smite"))
                result = "SMITE";

            if (text.Contains("Point Blank") || title.Contains("Point Blank"))
                result = "Point Blank";

            if (text.Contains("HearthStone") || title.Contains("HearthStone"))
                result = "HearthStone";

            if (text.Contains("Overwatch") || title.Contains("Overwatch"))
                result = "Overwatch";

            if (text.Contains("CrossFire") || title.Contains("CrossFire"))
                result = "CrossFire";

            return result;
        }

        public int FindPerWords(string text1, string text2)
        {
            int result = 0;

            var s = text1.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            var res1 = (from string part in text2
                        select new
                        {
                            list = part,
                            count = part.Split(new char[] { ' ' }).Sum(p => s.Contains(p) ? 1 : 0)

                        }).OrderByDescending(p => p.count).First();

            result = res1.count;

            return result;
        }
    }
}
