using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MovieSpider
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private MovieFinder _fider = new MovieFinder();
        private string _key;
        private void Button1_Click(object sender, EventArgs e)
        {
            if(keywords.Text.Trim()!=string.Empty)
            {
                _set.Clear();
                _key = new Guid().ToString();
                _fider.Find(keywords.Text.Trim(), _key, addMovie);
                resultView.Items.Clear();
                statu.Text = $"正在搜索{keywords.Text.Trim()}......";
            }
        }
        protected override CreateParams CreateParams => base.CreateParams;

        private HashSet<string> _set = new HashSet<string>();
        private void addMovie(string key, Movie movie)
        {
            if (movie == null || key != _key)
                return;

            while (!IsHandleCreated) ;

            Action act = () =>
              {
                  if (!_set.Contains(movie.Link))
                  {
                      var listItem = new ListViewItem(movie.Name);
                      listItem.SubItems.Add(movie.Site);
                      listItem.SubItems.Add(movie.Link);
                      

                      resultView.Items.Add(listItem);
                      _set.Add(movie.Link);
                  }
              };

            Invoke(act);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            resultView.Columns.Add("电影", 200);
            resultView.Columns.Add("网站", 200);
            resultView.Columns.Add("下载地址", 700);
            
            
        }
    }
}
