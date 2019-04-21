using System.IO;
using System.Text;
using WebParsers.Html;

namespace Jasmine.Spider.Worker.Excutor.Downloader
{
    public  class HttpPageDecoder
    {
        public static string GetPage(Stream stream)
        {
            var bt = new byte[1000];
            stream.Read(bt, 0, bt.Length);
            var header = Encoding.ASCII.GetString(bt);
            HtmlParser parser = new HtmlParser();
            var metas= parser.Parse(header).GetAllChildrenByElementType(WebParsers.ElementType.meta);
            var charset = string.Empty;
            foreach (var item in metas)
            {
                if(item.Attributes.Contains("charset"))
                {
                    charset = item.Attributes["charset"];
                    break;
                }
                if(item.Attributes.Contains("content"))
                {
                    var b = item.Attributes["content"].Split(new char[] { ';' });
                    if (b.Length != 2)
                        continue;
                    b = b[1].Split(new char[] { '=' });
                    if (b.Length != 2)
                        continue;
                    if(b[0].Trim()== "charset")
                    {
                        charset = b[1];
                        break;
                    }
                }
            }
           // stream.Seek(0, SeekOrigin.Begin);

            return GetPage(header, stream, charset == string.Empty ? Encoding.UTF8 : Encoding.GetEncoding(charset));  
        }
        public static string GetPage(string header,Stream stream,Encoding encoding=null)
        {
            encoding = encoding ?? Encoding.UTF8;
            StreamReader reader = new StreamReader(stream, encoding);
            var page= reader.ReadToEnd();
            reader.Dispose();
            stream.Dispose();
            return header+ page;
        }
    }
}
