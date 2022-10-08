using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace Library
{
    public partial class frmmain : Form
    {
        public frmmain()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            frmuser fuser = new frmuser();
            fuser.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmdepository fd = new frmdepository();
            fd.ShowDialog();
        }
        bool date(string date1, string date2)
        {
            string[] d1 = date1.Split('/');
            string[] d2 = date2.Split('/');
            if (int.Parse(d1[1]) > int.Parse(d2[1]))
                return true;
            else if(int.Parse(d1[2]) >= int.Parse(d2[2]))
                return true;
            else
                return false;
        }
        DataBase db = new DataBase();
        
        private void frmmain_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("id", "ID");
            dataGridView1.Columns.Add("name", "Name");
            DataTable dt = new DataTable();
            dt = db.Select("select tbl_depository.idbook ,tbl_book.name, tbl_depository.maxdate  from tbl_book inner join tbl_depository on tbl_depository.idbook=tbl_book.id");
            string s = "";
            PersianCalendar pc = new PersianCalendar();
            string now = pc.GetYear(DateTime.Now).ToString() +"/"+ pc.GetMonth(DateTime.Now).ToString() +"/"+ pc.GetDayOfMonth(DateTime.Now).ToString();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                s = dt.Rows[i].ItemArray[2].ToString();
                if (date(now,s))
                {
                    dataGridView1.Rows.Add(dt.Rows[i].ItemArray[0].ToString(), dt.Rows[i].ItemArray[1].ToString());
                }
            }

        }



        private void button3_Click(object sender, EventArgs e)
        {
            frm frmset = new frm();
            frmset.ShowDialog();
        }

      

        private void button6_Click(object sender, EventArgs e)
        {
            frmnote fnote = new frmnote();
            fnote.ShowDialog();
        }



        private void button10_Click(object sender, EventArgs e)
        {
            frmbook fbook = new frmbook();
            fbook.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = db.Select("select * from tbl_book");
            lblcb.Text = dt.Rows.Count.ToString()+ " books";
            dt.Reset();
            dt = db.Select("select * from tbl_user");
            lblcu.Text =  dt.Rows.Count.ToString() +" members";
            lbltime.Text = DateTime.Now.ToShortTimeString();
            PersianCalendar pc = new PersianCalendar();
            string now = pc.GetYear(DateTime.Now).ToString() +"/"+ pc.GetMonth(DateTime.Now).ToString() +"/" +pc.GetDayOfMonth(DateTime.Now).ToString();
            lbldate.Text = now;
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
        }
    }
}
