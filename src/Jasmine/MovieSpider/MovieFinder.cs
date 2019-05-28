using Jamine.Parser.Html;
using Jasmine.Extensions;
using Spider.Downloader;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieSpider
{
    public class MovieFinder
    {
       public void Find(string name,string key,Action<string ,Movie>onFind)
        {

            run($"{name}lol电影天堂迅雷下载", key, onFind);
            run($"{name}电影天堂迅雷下载", key, onFind);
            run($"{name}飘花电影网迅雷下载", key, onFind);
            run($"{name}迅雷下载", key, onFind);
            run($"{name}bt种子迅雷下载", key, onFind);


        }

        private void run(string name, string key, Action<string, Movie> onFind)
        {

            Task.Run(()=>
            {
                var parameter = new DownloadParamers($"https://www.baidu.com/s?wd={name}");


                var baiduPage = new Downloader().DownLoad(parameter, Encoding.UTF8);




                foreach (var item in getBaiduLinks(baiduPage))
                {
                    findMovie(item, key, onFind);
                }
            });
        }

        private List<string> getBaiduLinks(string page)
        {
            var parser = new HtmlParser();

            var root = parser.Parse(page);


            var ls = new List<string>();

            foreach (var item in root.GetAllChildren(x=>x.Name=="h3"))
            {
                if(item.Attributes.Contains("class")&&item.Attributes["class"]=="t")
                {
                    var a = item.GetAllChildren(x1=>x1.Name=="a");

                    if (a.Count != 0)
                    {
                        if (a[0].Attributes.Contains("href"))
                        {
                          ls.Add(  a[0].Attributes["href"]);
                        }
                    }
                }

            }

            return ls;
       }


        private void findMovie(string link,string key,Action<string, Movie> onFind)
        {
            Task.Run(() =>
            {

                var parameter = new DownloadParamers(link);

                var page = new Downloader().DownLoad(parameter,Encoding.UTF8);

                if(page!=null)
                {
                    var movies = ExtractMovie(link,page);

                    if (movies != null)
                    {

                        foreach (var item in movies)
                        {
                            onFind(key, item);
                        }
                    }
                }
            });
        }

        private Movie[] ExtractMovie(string link,string page)
        {
            if (page.Contains("/css/dytt8.css")||page.Contains("/css/dygod.css")) 
            {
                var result = extractMovieParidise(link);

                if(result!=null)
                {
                    return new Movie[] { result };
                }
                else
                {
                    return Array.Empty<Movie>();
                }
            }
            else if(page.Contains("飘花电影网"))
            {
                return extractPiaoHua(page);
            }
            else if(page.Contains("/skin/lol/js/jquery.js"))
            {
                return extractLol(link);
            }
            else if(page.Contains("/template/993dy_new/css/home.css"))
            {
                return extract993Dy(page);
            }
            else if(page.Contains("http://apps.bdimg.com/libs/bootstrap/3.2.0/css/bootstrap.min.css"))
            {
                return extractSzhongzi(page);
            }
            else if(page.Contains("https://s.jizy.cn/share/css?b=7.84"))
            {
                return extractFoxizy(page);
            }
            else if(page.Contains("http://www.tl95.vip/wp-content/themes/begin/img/favicon.ico"))
            {
                return extractTl95(page);
            }
            else if(page.Contains("http://www.xunleige.com"))
            {
                return extractXunleiGe(link);
            }
            else if(page.Contains("//www.lbldy.com/favicon.ico"))
            {
                return extractLongBuluo(page);
            }
            else
            {
              return  Array.Empty<Movie>();
            }






        }

        private Movie[] extractLongBuluo(string page)
        {
            var ls = new List<Movie>();

            var parse = new HtmlParser();

            var root = parse.Parse(page);

            var titles = root.GetAllChildrenByElementType(Jasmine.Parsers.Html.ElementType.Title);

            var title = string.Empty;

            if (titles.Count != 0)
                title = titles[0].InnerText.Replace("_BT种子_迅雷种子下载_种子搜索神器网页版", "");


            var result = StringHelper.Select(page, "ed2k:", "\"");

            if (result.Count != 0)
            {
                foreach (var item in result)
                {
                    var data = item.Replace("\"", "");

                    ls.Add(new Movie()
                    {
                        Link = data,
                        Site = "http://www.lbldy.com",
                        Name = title
                    });
                }

            }

             result = StringHelper.Select(page, "magnet:", "\"");

            if (result.Count != 0)
            {
                foreach (var item in result)
                {
                    var data = item.Replace("\"", "");

                    ls.Add(new Movie()
                    {
                        Link = data,
                        Site = "http://www.lbldy.com",
                        Name = title
                    });
                }

            }


            return ls.ToArray();
        }

        private Movie[] extractFoxizy(string page)
        {
            var ls = new List<Movie>();

            var parse = new HtmlParser();

            var root = parse.Parse(page);

            var titles = root.GetAllChildrenByElementType(Jasmine.Parsers.Html.ElementType.Title);

            var title = string.Empty;

            if (titles.Count != 0)
                title = titles[0].InnerText.Replace("_BT种子_迅雷种子下载_种子搜索神器网页版", "");


            var result = StringHelper.Select(page, "data-link=\"ed2k:", "\"");

            if (result.Count != 0)
            {
                foreach (var item in result)
                {
                    var data = item.Replace("data-link=\"", "").Replace("\"", "");

                    ls.Add(new Movie()
                    {
                        Link = data,
                        Site = "http://www.szhongzi.com",
                        Name = title
                    });
                }
               
            }

             result = StringHelper.Select(page, "value=\"ed2k:", "\"");

            if (result.Count != 0)
            {
                foreach (var item in result)
                {
                    var data = item.Replace("value=\"", "").Replace("\"", "");

                    ls.Add(new Movie()
                    {
                        Link = data,
                        Site = "http://www.szhongzi.com",
                        Name = title
                    });
                }
              
            }

            return ls.ToArray();
        }

        private Movie[] extractXunleiGe(string link)
        {
            var page = new Downloader().DownLoad(new DownloadParamers(link), Encoding.GetEncoding("gb2312"));

            var ls = new List<Movie>();

            var parse = new HtmlParser();

            var root = parse.Parse(page);

            var titles = root.GetAllChildrenByElementType(Jasmine.Parsers.Html.ElementType.Title);

            var title = string.Empty;

            if (titles.Count != 0)
                title = titles[0].InnerText.Replace("高清迅雷下载_百度云 - TL95", "");


            var result = StringHelper.Select(page, "value=\"thunder", "\"");

            if (result.Count != 0)
            {
                var data = result[0].Replace("value=\"", "").Replace("\"","");

                ls.Add(new Movie()
                {
                    Link = data,
                    Site = "www.xunleige520.com",
                    Name = title
                });
            }

             result = StringHelper.Select(page, "thunderhref=\"", "\"");

            if (result.Count != 0)
            {
                var data = result[0].Replace("thunderhref=\"", "").Replace("\"", "");

                ls.Add(new Movie()
                {
                    Link = data,
                    Site = "www.xunleige520.com",
                    Name = title
                });
            }

            return ls.ToArray();
        }

        private Movie[] extractTl95(string page)
        {
            var ls = new List<Movie>();

            var parse = new HtmlParser();

            var root = parse.Parse(page);

            var titles = root.GetAllChildrenByElementType(Jasmine.Parsers.Html.ElementType.Title);

            var title = string.Empty;

            if (titles.Count != 0)
                title = titles[0].InnerText.Replace("高清迅雷下载_百度云 - TL95", "");


            var result = StringHelper.Select(page, "href=\"magnet", " \"");

            if (result.Count != 0)
            {
                var data = result[0].Replace("href=\"", "").Replace("\"","\"");

                ls.Add(new Movie()
                {
                    Link = data,
                    Site = "www.tl95.vip",
                    Name = title
                });
            }

            return ls.ToArray();
        }

        private Movie[] extractSzhongzi(string page)
        {
            var ls = new List<Movie>();

            var parse = new HtmlParser();

            var root = parse.Parse(page);

            var titles = root.GetAllChildrenByElementType(Jasmine.Parsers.Html.ElementType.Title);

            var title = string.Empty;

            if (titles.Count != 0)
                title = titles[0].InnerText.Replace("_BT种子_迅雷种子下载_种子搜索神器网页版", "");


            var result = StringHelper.Select(page, "ed2k://", "</");

            if (result.Count != 0)
            {
                var data = result[0].Replace("</", "");

                ls.Add(new Movie()
                {
                    Link = data,
                    Site = "http://www.szhongzi.com",
                    Name = title
                });
            }

            return ls.ToArray();
        }

        private Movie[] extract993Dy(string page)
        {
            var ls = new List<Movie>();

            var parse = new HtmlParser();

            var root = parse.Parse(page);

            var titles = root.GetAllChildrenByElementType(Jasmine.Parsers.Html.ElementType.Title);

            var title = string.Empty;

            if (titles.Count != 0)
                title = titles[0].InnerText.Replace("迅雷下载_BT种子磁力下载 - LOL电影天堂", "");


            var result = StringHelper.Select(page, "var downurls=\"", "\";");

            if(result.Count!=0)
            {
                var data = result[0].Replace("var downurls=\"", "").Replace("\";", "");

                foreach (var item in StringHelper.Splite1(data,"#"))
                {
                    var pair = StringHelper.Splite1(item, "$");

                    if(pair.Count==2)
                    {
                        ls.Add(new Movie() { Site = "www.993dy.com", Link = pair[1], Name = pair[0] });
                    }
                }
            }

            return ls.ToArray();
        }
        private Movie[] extractLol(string link)
        {
            var parameter = new DownloadParamers(link);

            var page = new Downloader().DownLoad(parameter, Encoding.GetEncoding("gb2312"));


            var ls = new List<Movie>();

            if (page == null)
            {
                return null;
            }

            var parser = new HtmlParser();

            var root = parser.Parse(page);

            var titles = root.GetAllChildrenByElementType(Jasmine.Parsers.Html.ElementType.Title);

            var title = string.Empty;

            if (titles.Count != 0)
                title = titles[0].InnerText.Replace("迅雷下载-LOL电影天堂", "");

            foreach (var ul in root.GetAllChildren(x=>x.Name=="ul"&&x.Attributes.Contains("class")&&x.Attributes["class"]=="downurl"))
            {
                foreach (var item in ul.GetAllChildren(x=>x.Name=="a"&&x.Attributes.Contains("href")))
                {
                    ls.Add(new Movie() {Name=title,Link=item.Attributes["href"],Site= "www.loldyttw.com" });
                }
            }

            return ls.ToArray();
        }

        private Movie[] extractPiaoHua(string page)
        {
            var ls = new List<Movie>();

            var parse = new HtmlParser();

            var root = parse.Parse(page);

            var titles = root.GetAllChildrenByElementType(Jasmine.Parsers.Html.ElementType.Title);

            var title = string.Empty;

            if (titles.Count != 0)
                title = titles[0].InnerText.Replace("下载_迅雷下载_免费下载_飘花电影网", "");

            foreach (var div in root.GetAllChildren(x=>x.Name=="tbody"))
            {
               
                    foreach (var item in div.GetAllChildren(x=>x.Name=="a"&&x.Attributes.Contains("href")))
                    {
                        ls.Add(new Movie() { Name = title, Site = "www.piaohua.com", Link = item.Attributes["href"]});
                    }
                
            }

            return ls.ToArray();

        }

        private Movie extractMovieParidise(string link)
        {
           
            var parameter = new DownloadParamers(link);

            var page = new Downloader().DownLoad(parameter,Encoding.GetEncoding("gb2312"));




            if(page==null)
            {
                return null;
            }

            var parser = new HtmlParser();

            var root = parser.Parse(page);

            var titles = root.GetAllChildrenByElementType(Jasmine.Parsers.Html.ElementType.Title);

            var title = string.Empty;

            if (titles.Count != 0)
                title = titles[0].InnerText.Replace("迅雷下载_电影天堂","");

            foreach (var item in root.GetAllChildren(x=>x.Name=="tbody"))
            {
                foreach (var a in item.GetAllChildren(x=>x.Name=="a"))
                {
                    if (a.Attributes.Contains("href") /*item.Attributes["href"]=="#"&&*//*item.Attributes.Contains("target")*//*&&item.Attributes["target"]=="self"*/)
                    {
                        var moive = new Movie()
                        {
                            Name = title,
                            Link = a.InnerText,
                            Site = "www.dytt8.net"
                        };

                        return moive;
                    }
                
                }
            }

            return null;

        }
    }

   


    public class Movie
    {
        public string Name { get; set; }
        public string Link { get; set; }

        public string Site { get; set; }
    }
}
