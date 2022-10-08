using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Library
{
    public partial class frmbook : Form
    {
        public frmbook()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmtype frmtype = new frmtype();
            frmtype.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmobook fobook = new frmobook();
            fobook.Text = "Add";
            fobook.ShowDialog();
        }
        DataBase db = new DataBase();
        private void frmbook_Load(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            dt = db.Select("select tbl_book.id,tbl_book.name,tbl_book.writer,tbl_type.type from tbl_book inner join tbl_type on tbl_book.type = tbl_type.id");
            dataGridView1.DataSource = dt;
           

            dt2 = db.Select("select * from tbl_type");
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                treeView1.Nodes.Add(dt2.Rows[i].ItemArray[1].ToString());
            }
            dt2.Reset();
            dt2 = db.Select("select * from tbl_book");
            int id = 0;
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                id = (int)dt2.Rows[i].ItemArray[3];
                treeView1.Nodes[id-1].Nodes.Add(dt2.Rows[i].ItemArray[1].ToString());
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt=new DataTable();
            int id;
            id=int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            dt=db.Select("select * from tbl_depository where idbook="+id);

            if (dt.Rows.Count == 0)
            {
                if (db.Exe("delete from tbl_book where id = " + id) == true)
                    frmbook_Load(sender, e);
            }
            else
                MessageBox.Show("The book is on loan", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmobook fobook = new frmobook();
            fobook.Text = "Edit";
            fobook.textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            fobook.textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            fobook.textBox3.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            fobook.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmbook_Load(sender, e);
        }

        private void cTextBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string strf="";
            if (radioButton1.Checked)
                strf = "name";
            else if (radioButton2.Checked)
                strf = "writer";
            dt = db.Select("select tbl_book.id,tbl_book.name,tbl_book.writer,tbl_type.type from tbl_book inner join tbl_type on tbl_book.type = tbl_type.id and " + strf + " like '%' + N'" + txtsearch.Text + "' + '%'");
            dataGridView1.DataSource = dt;

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
