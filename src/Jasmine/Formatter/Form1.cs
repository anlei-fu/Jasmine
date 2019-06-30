using Jasmine.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formatter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            btn_change_input.Click += (x, y) =>
              {
                  var odl = new OpenFileDialog();

                  odl.Title = "选择日志文件";
                  odl.Filter = "日志(*.txt)|*.txt";

                  if (odl.ShowDialog() == DialogResult.OK)
                  {
                      tb_input.Text = odl.FileName;

                      tb_output.Text = odl.FileName.Replace(".txt", ".cvs");
                  }
              };

            btn_change_putout.Click += (x, y) =>
            {
                var odl = new SaveFileDialog();
                odl.DefaultExt = ".cvs";
                odl.Title = "选择要保存的csv文件";
                odl.FileName = $"{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}__{DateTime.Now.Hour}_{DateTime.Now.Minute}";

                if (odl.ShowDialog() == DialogResult.OK)
                {
                    tb_output.Text = odl.FileName;

                }
            };

            btn_formmat.Click += (x, y) =>
              {
                  if (!File.Exists(tb_input.Text.Trim()) || tb_output.Text.Trim() == string.Empty)
                  {
                      MessageBox.Show("检查输入文件是否存在", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);

                      return;
                  }

                  formatt();
              };

        }


        private void formatt()
        {
            var reader = new StreamReader(new FileStream(tb_input.Text.Trim(), FileMode.Open, FileAccess.Read));

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



                if (g.Count != 0)
                {
                    if (g.Count > 0)
                        item.RSSI = g[0];
                    if (g.Count > 1)
                        item.Snr = g[1];
                    item.Success = true;
                }
                else
                {
                    item.Success = false;
                }

                ls.Add(item);

            }


            var temp = new List<Item>();

            last = null;

            foreach (var item in ls)
            {
                if (last != null)
                {
                    while (item.Time - last.Time > TimeSpan.FromSeconds(2))
                    {
                        var _new = new Item
                        {
                            Success = false,
                            Time = TimeSpan.FromMilliseconds(last.Time.TotalMilliseconds + 2000)
                        };




                        temp.Add(_new);

                        last = _new;
                    }

                }

                last = item;

                temp.Add(item);
            }


            ls.Clear();

            for (int i = 0; i < temp.Count; i++)
            {



                if (i + 1 < temp.Count)
                {
                    var c = temp[i].Time.Seconds % 2 == 0 ? temp[i].Time.Seconds : temp[i].Time.Seconds - 1;
                    var d = temp[i + 1].Time.Seconds % 2 == 0 ? temp[i + 1].Time.Seconds : temp[i + 1].Time.Seconds - 1;

                    if (c == d)
                        continue;
                }


                ls.Add(temp[i]);
            }


            var builder = new StringBuilder();

            foreach (var a in ls)
            {
                builder.Append(a.ToString());
            }
            try
            {
                File.WriteAllText(tb_output.Text, builder.ToString(), Encoding.UTF8);

                lb_info.Text = $"保存文件到{tb_output.Text}成功！";
            }
            catch
            {
                MessageBox.Show("检查当前cvs文件是否被占用", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

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
