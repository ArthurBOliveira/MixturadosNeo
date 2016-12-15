using NeoMix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace NeoMix.Util
{
    public class HtmlMinerStream
    {
        #region Twitch
        public List<Stream> MineTwitch()
        {
            List<Stream> result = new List<Stream>();
            Stream s = new Stream();
            int position;

            WebClient webClient = new WebClient();
            string html = webClient.DownloadString("http://streams.twitch.tv/kraken/streams?limit=60&offset=20&broadcaster_language=pt&on_site=1");

            string[] streams = html.Split(new string[] { "\"_id\":" }, StringSplitOptions.None);

            for (int i = 1; i < streams.Length; i++)
            {
                if (i % 2 != 0)
                {
                    streams[i] += streams[i + 1];
                    string[] aux = streams[i].Split('"');

                    s.Source = "Twitch";

                    position = Array.IndexOf(aux, "game");
                    s.Game = aux[position + 2];

                    position = Array.IndexOf(aux, "viewers");
                    s.Views = int.Parse(aux[position + 1].Substring(1).Replace(",", ""));

                    position = Array.IndexOf(aux, "self");
                    s.Link = aux[position + 2];

                    position = Array.IndexOf(aux, "status");
                    s.Title = aux[position + 2];
                    s.Title = s.Title.Length > 50 ? s.Title.Substring(0, 40) : s.Title;

                    position = Array.IndexOf(aux, "display_name");
                    s.Name = aux[position + 2];

                    position = Array.IndexOf(aux, "logo");
                    s.Logo = aux[position + 2] != "banner" ? aux[position + 2] : "http://mixturadosneo.com/Images/twitch.png";

                    result.Add(s);

                    s = new Stream();
                }
            }

            return result;
        }
        #endregion

        public List<Stream> MineHome(int views)
        {
            List<Stream> result = new List<Stream>();
            Stream s = new Stream();
            int position;

            WebClient webClient = new WebClient();
            string html = webClient.DownloadString("http://streams.twitch.tv/kraken/streams?limit=60&offset=20&broadcaster_language=pt&on_site=1");

            string[] streams = html.Split(new string[] { "\"_id\":" }, StringSplitOptions.None);

            for (int i = 1; i <= views*2; i++)
            {
                if (i % 2 != 0)
                {
                    streams[i] += streams[i + 1];
                    string[] aux = streams[i].Split('"');

                    s.Source = "Twitch";

                    position = Array.IndexOf(aux, "game");
                    s.Game = aux[position + 2];

                    position = Array.IndexOf(aux, "viewers");
                    s.Views = int.Parse(aux[position + 1].Substring(1).Replace(",", ""));

                    position = Array.IndexOf(aux, "self");
                    s.Link = aux[position + 2];

                    position = Array.IndexOf(aux, "status");
                    s.Title = aux[position + 2];
                    s.Title = s.Title.Length > 50 ? s.Title.Substring(0, 40) : s.Title;

                    position = Array.IndexOf(aux, "display_name");
                    s.Name = aux[position + 2];

                    position = Array.IndexOf(aux, "logo");
                    s.Logo = aux[position + 2] != "banner" ? aux[position + 2] : "http://mixturadosneo.com/Images/twitch.png";

                    result.Add(s);

                    s = new Stream();
                }
            }

            return result;
        }
    }
}