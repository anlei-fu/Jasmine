using Jasmine.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeFormatter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var source = textBox1.Text.Trim();

            var items = StringExtensions.Splite1WithCount(source, "\r\n");

            var temp = new string[items.Count];

            items.CopyTo(temp);


            foreach (var item in  temp)
            {
                var parts = StringExtensions.Splite(item.Trim(), " ");

                if (item.Contains("Query"))
                {


                    

                    var parts2 = new string[parts.Count];

                    parts.CopyTo(parts2);

                    parts2[0] = "ICursor";

                    parts2[1] += "Cursor";

                    var _new = string.Empty;

                    foreach (var item1 in parts2)
                    {
                        _new += item1+" ";
                    }

                    items.Add(_new);

                    parts = parts2.ToList();
                }


            


                parts[0] = $"Task<" + parts[0] + "> ";

                if(parts[1].Contains("<T>"))
                {
                    parts[1] = parts[1].Replace("<T>", "Async<T>");
                }
                else
                {
                    parts[1] = parts[1].Replace("(", "(Async");
                }

                var newItem = "";

                foreach (var item1 in parts)
                {
                    newItem += item1;
                }

                items.Add(newItem);


              


              

            }


            items.Sort();

            source = string.Empty;

            foreach (var item in items)
            {
                source += item + "\r\n";
            }

            textBox1.Text = source;
        }
    }
}
