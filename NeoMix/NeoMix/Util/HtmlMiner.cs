using NeoMix.BLL;
using NeoMix.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace NeoMix.Util
{
    public class HtmlMiner
    {
        private ChampionshipBLL _champBLL = new ChampionshipBLL();

        #region MineLOL

        public bool MineLol(string html)
        {
            bool result = false;

            List<Championship> ch = _champBLL.ChampionshipList();

            string[] evs = html.Split(new string[] { "<div class='event-list-item" }, StringSplitOptions.None);

            List<Championship> champs = new List<Championship>();

            for (int i = 1; i < evs.Length; i++)
                champs.Add(MineEventsLoL(evs[i]));

            foreach (Championship c in champs)
                result = _champBLL.ChampionshipCreate(c, ch);

            return result;
        }

        private Championship MineEventsLoL(string html)
        {
            Championship c = new Championship();

            string[] aux = html.Split('>');

            string[] tst = aux[16].Split('/');

            c.Game = "LOL";
            c.Name = aux[4].Split(new string[] { "</a" }, StringSplitOptions.None)[0];
            c.Img = "/Images/l_logo.jpg";
            c.Link = "http://events.br.leagueoflegends.com" + aux[1].Split('\'')[3];
            c.Date = new DateTime(DateTime.Now.Year, int.Parse(aux[16].Split('/')[1].Substring(0, 2)), int.Parse(aux[16].Split('/')[0]));
            c.Details = aux[10].Split('<')[0];
            c.Owner = "";
            c.Place = "";
            c.Prize = "";
            c.IsLocal = false;
            c.Source = "Riot Games";
            c.Stream = "";

            return c;

            //_champBLL.ChampionshipCreate(c);
        }

        public bool MineLolJson(string json)
        {
            bool result = false;
            Championship c = new Championship();

            string[] dates = json.Split(new string[] { "{'day':" }, StringSplitOptions.None);

            for (int i = 0; i >= dates.Length; i++)
            {
                DateTime date = DateTime.Parse(dates[0].Substring(1));

                string[] champs = dates[i].Split('\'');

                for (int j = 0; j >= champs.Length; j++)
                {

                }
            }

            return result;
        }
        #endregion

        #region MineXFire

        public bool MineXFire(string html)
        {
            bool result = false;

            List<Championship> Champs = _champBLL.ChampionshipList();

            string[] evs = html.Split(new string[] { "<li class='anim-01" }, StringSplitOptions.None);

            List<Championship> champs = new List<Championship>();

            for (int i = 1; i < evs.Length; i++)
                champs.Add(MineChampsXFire(evs[i]));

            foreach (Championship c in champs)
                result = _champBLL.ChampionshipCreate(c, Champs);

            return result;
        }

        private Championship MineChampsXFire(string html)
        {
            Championship c = new Championship();

            string[] aux = html.Split('<');

            string Name = aux[7].Split('>')[1];
            string Link = aux[7].Split('\'')[1];
            string Date = aux[20].Split(new string[] { "Starts" }, StringSplitOptions.None)[1];
            string Game = GamesXFire(aux[12].Split('>')[1]);
            string Details = aux[16].Split('>')[1];
            string Img = aux[4].Split('\'')[3];

            c.Game = Game;
            c.Name = Name;
            c.Img = Img;
            c.Link = "http://xfire.com" + aux[1].Split('\'')[3];
            c.Date = new DateTime(DateTime.Now.Year, Months(Date.Split(',')[0].Split(' ')[1]), int.Parse(Date.Split(',')[1].Substring(1, 2)));
            c.Details = Details;
            c.Owner = "";
            c.Place = "";
            c.Prize = "";
            c.IsLocal = false;
            c.Source = "XFire";
            c.Stream = "";

            return c;
        }

        private string GamesXFire(string game)
        {
            string result = "";

            switch (game)
            {
                case "League of Legends":
                    result = "LOL";
                    break;

                case "Battlefield 4":
                    result = "BF4";
                    break;

                case "Counter Strike Go":
                    result = "CSGO";
                    break;

                case "Dota 2":
                    result = "DOTA";
                    break;

                case "Smite":
                    result = "SMITE";
                    break;

                case "Hearthstone":
                    result = "HS";
                    break;

                case "Heroes Of The Storm":
                    result = "HOTS";
                    break;

                default:
                    result = game;
                    break;
            }

            return result;
        }

        public bool MineXFireJson(string url)
        {
            bool result = false;
            Championship c = new Championship();

            List<Championship> Champs = _champBLL.ChampionshipList();

            WebClient webClient = new WebClient();
            string html = webClient.DownloadString(url);

            string[] champs = html.Split(new string[] { "_isFuture" }, StringSplitOptions.None);

            for (int i = 1; i < champs.Length; i++)
            {
                string[] aux = champs[i].Split('"');

                c.Date = ConvertSecondsToDate(long.Parse(aux[93].Substring(1, 10)));

                if (c.Date >= DateTime.Now)
                {
                    c.Name = aux[80];
                    c.Game = GamesXFire(aux[76]);
                    c.Link = "http://xfire.com/tournament/" + aux[14];
                    c.Img = aux[36].Replace("\\", "");
                    c.Details = "";
                    c.Owner = "";
                    c.Place = "";
                    c.Prize = "";
                    c.IsLocal = false;
                    c.Source = "XFire";
                    c.Stream = "";

                    result = _champBLL.ChampionshipCreate(c, Champs);
                }

                c = new Championship();
            }

            return result;
        }
        #endregion

        #region Battlefy

        public bool MineBattlefy(string html)
        {
            bool result = false;

            List<Championship> Champs = _champBLL.ChampionshipList();

            string[] evs = html.Split(new string[] { "<tr ng-repeat='tournament" }, StringSplitOptions.None);

            List<Championship> champs = new List<Championship>();

            for (int i = 1; i < evs.Length; i++)
                champs.Add(MineChampsBattlefy(evs[i]));

            foreach (Championship c in champs)
                result = _champBLL.ChampionshipCreate(c, Champs);

            return result;
        }

        private Championship MineChampsBattlefy(string html)
        {
            Championship c = new Championship();

            string[] aux = html.Split('<');

            string Name = aux[7].Split('>')[1];
            string Link = aux[7].Split('\'')[5];
            string Date = aux[12].Split('>')[1];
            string Game = GamesBattlefy(aux[4].Split('>')[1]);
            string Details = aux[10].Split('>')[1];
            string Img = aux[2].Split('\'')[3];

            c.Game = Game;
            c.Name = Name;
            c.Img = ImgOfGame(Game, Img);
            c.Link = "https://battlefy.com/" + Link;
            c.Date = new DateTime(DateTime.Now.Year, Months(Date.Split(' ')[0]), int.Parse(Date.Split(' ')[1].Split(',')[0]));
            c.Details = Details;
            c.Owner = "";
            c.Place = "";
            c.Prize = "";
            c.IsLocal = false;
            c.Source = "Battlefy";
            c.Stream = "";

            c = MineDeeperChampsBattlefy(c);

            return c;
        }

        private Championship MineDeeperChampsBattlefy(Championship c)
        {
            WebClient webClient = new WebClient();
            string html = webClient.DownloadString(c.Link);

            string[] champs = html.Split(new string[] { "_id" }, StringSplitOptions.None);



            return c;
        }

        private string GamesBattlefy(string game)
        {
            string result = "";

            switch (game)
            {
                case "League of Legends":
                    result = "LOL";
                    break;

                case "Battlefield 4":
                    result = "BF4";
                    break;

                case "Counter Strike Go":
                    result = "CSGO";
                    break;

                case "Dota 2":
                    result = "DOTA";
                    break;

                case "Smite":
                    result = "SMITE";
                    break;

                case "Hearthstone: Heroes of Warcraft":
                    result = "HS";
                    break;

                case "Heroes of the Storm":
                    result = "HOTS";
                    break;

                default:
                    result = game;
                    break;
            }

            return result;
        }

        public bool MineBattlefyJson(string url, string owner)
        {
            bool result = false;
            Championship c = new Championship();

            List<Championship> Champs = _champBLL.ChampionshipList();

            WebClient webClient = new WebClient();
            string html = webClient.DownloadString(url);

            string[] champs = html.Split(new string[] { "_id" }, StringSplitOptions.None);

            for (int i = 1; i < champs.Length; i++)
            {
                if (i % 2 != 0)
                {
                    champs[i] += champs[i + 1];
                    string[] aux = champs[i].Split('"');

                    if (aux[4] != "type")
                    {
                        DateTime Date;

                        if (DateTime.TryParse(aux[12], out Date))
                        {
                            if (aux[5] == ":true," && Date >= DateTime.Now)
                            {
                                c.Name = aux[8];
                                c.Img = aux[48];
                                c.Game = GamesBattlefy(aux[52]);
                                c.Date = Date;
                                c.Link = "https://battlefy.com/" + owner + "/" + aux[24] + "/" + aux[2] + "/info";
                                c.Details = "";
                                c.Owner = owner;
                                c.Place = "";
                                c.Prize = "";
                                c.IsLocal = false;
                                c.Source = "Battlefy";
                                c.Stream = "";

                                result = _champBLL.ChampionshipCreate(c, Champs);

                                c = new Championship();
                            }
                        }
                    }
                    else
                    {
                        DateTime Date;

                        if (DateTime.TryParse(aux[16], out Date))
                        {
                            if (aux[9] == ":true," && Date >= DateTime.Now)
                            {
                                c.Name = aux[12];
                                c.Date = Date;
                                c.Link = "https://battlefy.com/" + owner + "/" + aux[26] + "/" + aux[2] + "/info";
                                c.Details = "";
                                c.Owner = owner;
                                c.Place = "";
                                c.Prize = "";
                                c.IsLocal = false;
                                c.Source = "Battlefy";
                                c.Stream = "";

                                int pos = Array.IndexOf(aux, "imageUrl");
                                c.Img = aux[pos + 2];

                                int pos1 = Array.LastIndexOf(aux, "name");
                                c.Game = GamesBattlefy(aux[pos1 + 2]);

                                result = _champBLL.ChampionshipCreate(c, Champs);

                                c = new Championship();
                            }
                        }
                    }
                }
            }


            return result;
        }
        #endregion

        #region GamersClub
        public bool MineGC(string url)
        {
            bool result = false;
            Championship c = new Championship();

            List<Championship> Champs = _champBLL.ChampionshipList();

            WebClient webClient = new WebClient();
            string html = webClient.DownloadString(url);

            string[] champs = html.Split(new string[] { "camp-box" }, StringSplitOptions.None);

            for (int i = 1; i < champs.Length; i++)
            {
                string[] aux = champs[i].Split('"');

                c.Name = aux[15].Split('<')[0].Substring(1);
                c.Link = "https://www.gamersclub.com.br" + aux[4];
                c.Img = "/Images/csgo-logo.png";
                c.Date = DateTime.Parse(aux[21].Split('>')[2].Substring(1, 10));
                c.Game = "CSGO";
                c.Owner = "GamersClub";
                c.Details = "";
                c.Owner = "";
                c.Place = "";
                c.Prize = "";
                c.IsLocal = false;
                c.Source = "GamersClub";
                c.Stream = "";

                result = _champBLL.ChampionshipCreate(c, Champs);

                c = new Championship();
            }

            return result;
        }
        #endregion

        #region ESL
        public bool MineESL()
        {
            bool result = false;
            Championship c = new Championship();

            List<Championship> Champs = _champBLL.ChampionshipList();

            WebClient webClient = new WebClient();
            string html = webClient.DownloadString("http://play.eslgaming.com/api/leagues?types=cup,premiership&states=inProgress,upcoming&skill_levels=pro_qualifier,open,major&limit.total=14&path=%2Fplay%2F&portals=&tags=brazil&limit.state.inProgress=12&limit.state.finished=0&limit.state.upcoming=0");

            string[] champs = html.Split(new string[] { "\"id\":" }, StringSplitOptions.None);

            for (int i = 1; i < champs.Length; i++)
            {
                string[] aux = champs[i].Split('"');

                c.Name = aux[9];
                c.Link = "http://play.eslgaming.com" + aux[21];
                c.Img = "/Images/ESL-LOGO-433x350.jpg";
                c.Game = GamesESL(aux[21].Split('/')[2]);
                c.Owner = "ESL";
                c.Details = "";
                c.Owner = "";
                c.Place = "";
                c.Prize = "";
                c.IsLocal = false;
                c.Source = "ESL";
                c.Stream = "";

                int pos = Array.IndexOf(aux, "inProgress");

                c.Date = DateTime.Parse(aux[pos + 4]);

                result = _champBLL.ChampionshipCreate(c, Champs);

                c = new Championship();
            }

            return result;
        }

        private string GamesESL(string game)
        {
            string result = "";

            switch (game)
            {
                case "LeagueofLegends":
                    result = "LOL";
                    break;

                case "Battlefield 4":
                    result = "BF4";
                    break;

                case "Counter Strike Go":
                    result = "CSGO";
                    break;

                case "Dota 2":
                    result = "DOTA";
                    break;

                case "Smite":
                    result = "SMITE";
                    break;

                case "Hearthstone: Heroes of Warcraft":
                    result = "HS";
                    break;

                case "Heroes of the Storm":
                    result = "HOTS";
                    break;

                default:
                    result = game;
                    break;
            }

            return result;
        }
        #endregion

        #region ESLPremier
        public bool MineESLPremier()
        {
            bool result = false;
            Championship c = new Championship();

            List<Championship> Champs = _champBLL.ChampionshipList();

            WebClient webClient = new WebClient();
            string html = webClient.DownloadString("http://pro.eslgaming.com/brasil/agenda/");

            string[] champs = html.Split(new string[] { "<td colspan=\"4\"" }, StringSplitOptions.None);

            for (int i = 1; i < 4; i++)
            {
                string[] aux = champs[i].Split(new string[] { "<tr>" }, StringSplitOptions.None);

                for (int j = 1; j < aux.Length; j++)
                {
                    string[] aux1 = aux[i].Split(new string[] { "<td " }, StringSplitOptions.None);

                    c.Name = aux1[9];
                    c.Link = "http://play.eslgaming.com" + aux1[21];
                    c.Img = "/Images/ESL_Premier.png";
                    c.Game = aux[0].Split('/')[2];
                    c.Owner = "ESL";
                    c.Details = "";
                    c.Owner = "";
                    c.Place = "";
                    c.Prize = "";
                    c.IsLocal = false;
                    c.Source = "ESL";
                    c.Stream = "";
                }

                int pos = Array.IndexOf(aux, "inProgress");

                c.Date = DateTime.Parse(aux[pos + 4]);

                result = _champBLL.ChampionshipCreate(c, Champs);

                c = new Championship();
            }

            return result;
        }
        #endregion

        #region eSportGG
        public bool MineESportGG()
        {
            bool result = false;
            Championship c = new Championship();

            List<Championship> Champs = _champBLL.ChampionshipList();

            WebClient webClient = new WebClient();
            string html = webClient.DownloadString("http://esportgg.com/campeonato");

            string[] champs = html.Split(new string[] { "\"<h5>\"" }, StringSplitOptions.None);

            for (int i = 1; i < champs.Length; i++)
            {
                string[] aux = champs[i].Split('"');

                c.Name = aux[9];
                c.Link = "http://play.eslgaming.com" + aux[21];
                c.Img = "/Images/ESL-LOGO-433x350.jpg";
                c.Game = aux[21].Split('/')[2];
                c.Owner = "ESL";
                c.Details = "";
                c.Owner = "";
                c.Place = "";
                c.Prize = "";
                c.IsLocal = false;
                c.Source = "ESL";
                c.Stream = "";

                int pos = Array.IndexOf(aux, "inProgress");

                c.Date = DateTime.Parse(aux[pos + 4]);

                result = _champBLL.ChampionshipCreate(c, Champs);

                c = new Championship();
            }

            return result;
        }
        #endregion

        #region RazerArena
        public bool MineRazerArena()
        {
            bool result = false;
            Championship c = new Championship();

            List<Championship> Champs = _champBLL.ChampionshipList();

            WebClient webClient = new WebClient();
            webClient.Headers.Add("Content-Type", "application/json; charset=utf-8");
            webClient.Headers.Add("Accept", "application/json; charset=utf-8");
            webClient.Headers.Add("gg-client-api-key", "C434EDE3-2E7E-4B9D-A070-58B2CF94846D");

            string html = webClient.DownloadString("https://client.arena.razerzone.com/api/Tournament/Search?ListType=Upcoming&SearchTerms=&IncludeGameInSearchTerm=false&GameShortCode=&Paging.SortByName=RegistrationEndDate&Paging.ReturnTopRowCount=300&_=1462986515731");

            string[] champs = html.Split(new string[] { "IsTournamentSpecificAdministrator" }, StringSplitOptions.None);

            for (int i = 1; i < champs.Length; i++)
            {
                string[] aux = champs[i].Split('"');

                string id = aux[69].Substring(1, 4);

                c.Name = aux[52];
                c.Link = "https://arena.razerzone.com/tournaments/details/" + id;
                c.Img = aux[16];
                c.Game = GamesRazerArena(aux[8]);
                c.Date = DateTime.Parse(aux[36]);
                c.Details = "";
                c.Owner = "";
                c.Place = "";
                c.Prize = "";
                c.IsLocal = false;
                c.Source = "RazerArena";
                c.Stream = "";

                result = _champBLL.ChampionshipCreate(c, Champs);

                c = new Championship();
            }

            return result;
        }

        private string GamesRazerArena(string game)
        {
            string result = "";

            switch (game)
            {
                case "League of Legends":
                    result = "LOL";
                    break;

                case "Battlefield 4":
                    result = "BF4";
                    break;

                case "Counter-Strike: Global Offensive":
                    result = "CSGO";
                    break;

                case "Dota 2":
                    result = "DOTA";
                    break;

                case "Smite":
                    result = "SMITE";
                    break;

                case "Hearthstone: Heroes of Warcraft":
                    result = "HS";
                    break;

                case "Heroes of the Storm":
                    result = "HOTS";
                    break;

                default:
                    result = game;
                    break;
            }

            return result;
        }
        #endregion

        #region PrivateMethods
        private int Months(string m)
        {
            int result = 1;

            switch (m)
            {
                case "January":
                    result = 1;
                    break;

                case "February":
                    result = 2;
                    break;

                case "March":
                    result = 3;
                    break;

                case "April":
                    result = 4;
                    break;

                case "May":
                    result = 5;
                    break;

                case "June":
                    result = 6;
                    break;

                case "July":
                    result = 7;
                    break;

                case "August":
                    result = 8;
                    break;

                case "September":
                    result = 9;
                    break;

                case "Octuber":
                    result = 10;
                    break;

                case "November":
                    result = 11;
                    break;

                case "December":
                    result = 12;
                    break;
            }

            return result;
        }

        private DateTime ConvertSecondsToDate(long s)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(s).ToLocalTime();
        }

        private string ImgOfGame(string game, string aux)
        {
            string img = "";

            switch (game)
            {
                case "LOL":
                    img = "/Images/l_logo.jpg";
                    break;
                case "rainbowsix":
                    img = "/Images/R6_logo.png";
                    break;
                case "HS":
                    img = "/Images/HS_Logo.png";
                    break;
                case "HOTS":
                    img = "/Images/HOTS_Logo.png";
                    break;
                case "CSGO":
                    img = "/Images/csgo-logo.png";
                    break;
                default:
                    img = aux;
                    break;
            }

            return img;
        }
        #endregion
    }
}