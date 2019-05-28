using System.Windows.Forms;

namespace MovieSpider
{
    public class NewListView:ListView
    {
        public NewListView()
        {
            //SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.  
            //SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲  

        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
    }
}
