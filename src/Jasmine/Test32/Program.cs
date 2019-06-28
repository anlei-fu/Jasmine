using Jasmine.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test32
{
    class Program
    {
        static void Main(string[] args)
        {
            var reader = new StreamReader(new FileStream("test.txt", FileMode.Open, FileAccess.Read));

            var ls = new List<Item>();

            Item last = null;

            while (true)
            {
                var line = reader.ReadLine();

                if (line == null)
                    break;

                if (line.Trim() == string.Empty)
                    continue;

                var item = new Item();

                var s = line.Select1("[", "]")[0];

                item.Time = TimeSpan.Parse(s);

                var g = line.Select1("=", "!");

                bool fail = false;

                if (g.Count != 0)
                {
                    item.RSSI = g[0];
                    item.Snr = g[1];
                    item.Success = true;
                }
                else
                {
                    item.Success = false;
                }

                if (last == null)
                {
                    item.Success = true;
                }


                var iterval = last == null ? TimeSpan.FromSeconds(1) : (item.Time - last.Time);


                if (iterval >= TimeSpan.FromSeconds(4))
                {
                    var now = item.Time;

                    var t = 1;

                    while (true)
                    {
                        var _newTime = TimeSpan.FromMilliseconds(last.Time.TotalMilliseconds + 2 * t * 1000);

                        if (_newTime >= TimeSpan.FromMilliseconds(now.TotalMilliseconds - 2000))
                            break;



                        var _new = new Item()
                        {
                            Time = _newTime,
                            Success = false
                        };

                        ls.Add(_new);
                        t++;
                    }
                }



                last = item;



                ls.Add(item);

            }


            var temp = new List<Item>();

            last = null;

            foreach (var item in ls)
            {
                if (last != null && item.Time.Seconds == last.Time.Seconds)
                {
                    if (item.Success)
                    {
                        last.Time = TimeSpan.FromMilliseconds(last.Time.TotalMilliseconds - 2 * 1000);
                    }


                }

                last = item;

                temp.Add(item);
            }


            var builder = new StringBuilder();

            foreach (var a in temp)
            {
                builder.Append(a.ToString());
            }

            File.WriteAllText("output.csv", builder.ToString(), Encoding.UTF8);

        }


        class Item
        {
            public string RSSI;
            public string Snr;
            public TimeSpan Time;
            public bool Success;
            public override string ToString()
            {
                var result = Success ? "正确" : "错误";

                var second = Time.Seconds % 2 == 0 ? Time.Seconds : Time.Seconds - 1;
                return $"{result},{Time.Hours}:{Time.Minutes}:{second},{RSSI},{Snr}\r\n";
            }
        }
    }
}
