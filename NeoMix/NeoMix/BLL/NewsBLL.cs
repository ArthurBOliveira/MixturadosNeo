using NeoMix.DAL;
using NeoMix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace NeoMix.BLL
{
    public class NewsBLL
    {
        private NewsDAL _newsDAL = new NewsDAL();

        public List<News> NewsList()
        {
            return _newsDAL.NewsList();
        }

        public bool NewsCreate(News n)
        {
            return _newsDAL.NewsCreate(n);
        }

        public News NewSelect(int id_news)
        {
            return _newsDAL.NewsSelect(id_news);
        }

        public List<News> NewsListHome(int views)
        {
            return _newsDAL.NewsSelectHome(views);
        }

        public bool NewUpdate(News n)
        {
            return _newsDAL.NewsUpdate(n);
        }

        public string NewsListByGamePreview()
        {
            string html = "";

            List<News> news = _newsDAL.NewsSelectHome(6);
            int i = 0;

            foreach (News n in news)
            {
                if (i < 3)
                {

                    html += "<li>";
                    html += "<div class='twelve columns'>";
                    html += "    <div class='plain box twelve'>";
                    html += "        <section>";
                    html += "            <header style='background-color: #292929'>";
                    html += "                <h3>";
                    html += "                    <a style='color: #c11b50' href='/news?id_news=" + n.Id + "'>" + n.Title + "</a>";
                    html += "                </h3>";
                    html += "            </header>";
                    html += "            <figure>";
                    html += "                <a href='/news?id_news=" + n.Id + "'>";
                    html += "                    <img style='border-radius: 15px; border: solid #e51c5e;' src='" + n.Img + "' alt='@n.Img'>";
                    html += "                </a>";
                    html += "            </figure>";
                    html += "        </section>";
                    html += "    </div>";
                    html += "</div>";
                    html += "</li>";

                    i++;
                }
            }

            return html;
        }

        public string NewsListByGamePreview(string newsType)
        {
            string html = "";

            List<News> news = _newsDAL.NewsListByNewsType(newsType);
            int i = 0;

            foreach (News n in news)
            {
                if (i < 3)
                {

                    html += "<li>";
                    html += "<div class='twelve columns'>";
                    html += "    <div class='plain box twelve'>";
                    html += "        <section>";
                    html += "            <header style='background-color: #292929'>";
                    html += "                <h3>";
                    html += "                    <a style='color: #c11b50' href='/news?id_news=" + n.Id +"'>" + n.Title + "</a>";
                    html += "                </h3>";
                    html += "            </header>";
                    html += "            <figure>";
                    html += "                <a href='/news?id_news=" + n.Id + "'>";
                    html += "                    <img style='border-radius: 15px; border: solid #e51c5e;' src='" + n.Img + "' alt='@n.Img'>";
                    html += "                </a>";
                    html += "            </figure>";
                    html += "        </section>";
                    html += "    </div>";
                    html += "</div>";
                    html += "</li>";

                    i++;
                }
            }

            return html;
        }

        public List<News> NewsBaseList()
        {
            return _newsDAL.NewsBaseList();
        }

        public string NewsFeedApp()
        {
            string result = "{";
            int i = 0;

            List<News> news = _newsDAL.NewsList();

            foreach(News n in news)
            {
                
                if (i < 14)
                {
                    result += "\"" + n.Id.ToString() + "\"" + ": {";

                    string text = n.Text.Replace('"', '\'');
                    text = text.Replace("\n", "");
                    text = text.Replace("\r", "");

                    string desc = n.ShortDesc.Replace('"', '\'');

                    string title = n.Title.Replace('"', '\'');

                    result += "\"id\" : " + "\"" + n.Id.ToString() + "\"" + ",";
                    result += "\"img\" : " + "\"" + n.Img + "\"" + ",";
                    result += "\"source\" : " + "\"" + n.Source + "\"" + ",";
                    result += "\"text\" : " + "\"" + text + "\"" + ",";
                    result += "\"author\" : " + "\"" + n.Author + "\"" + ",";
                    result += "\"newstype\" : " + "\"" + n.NewsType + "\"" + ",";
                    result += "\"title\" : " + "\"" + title + "\"" + ",";
                    result += "\"ispremium\" : " + "\"" + n.IsPremium + "\"" + ",";
                    result += "\"shortdesc\" : " + "\"" + desc + "\"";

                    result += "},";
                    i++;
                }
            }

            result = result.Substring(0, result.Length - 1);

            result += "}";

            //result = StripTagsRegex(result);

            return result;
        }

        public List<News> NewsListBySubTitle(string subTitle)
        {
            return _newsDAL.NewsListBySubTitle(subTitle);
        }

        private static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        public bool NewsDelete(int id_new)
        {
            return _newsDAL.NewsDelete(id_new);
        }

        public List<News> NewsListBot()
        {
            return _newsDAL.NewsListBot();
        }

        public News NewsSelectBot(int id_news)
        {
            return _newsDAL.NewsSelectBot(id_news);
        }
    }
}