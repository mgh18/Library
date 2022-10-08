using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class frmuser : Form
    {
        public frmuser()
        {
            InitializeComponent();
        }
        DataBase db=new DataBase();
        private void button1_Click(object sender, EventArgs e)
        {
            frmouser fouser = new frmouser();
            fouser.Text = "Add";
            fouser.ShowDialog();
        }

        private void frmuser_Load(object sender, EventArgs e)
        {
            DataTable dt=new DataTable();
            dt=db.Select("select id,fname,lname,tel from tbl_user");
            dataGridView1.DataSource=dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            DataTable dt = new DataTable();
            dt = db.Select("select * from tbl_depository where idborrower=" + id);
            if (dt.Rows.Count == 0)
            {
                dt = db.Select("select * from tbl_user where id=" + id);
                string name;
                name = dt.Rows[0].ItemArray[1].ToString();
                name += dt.Rows[0].ItemArray[2].ToString();
                name += dt.Rows[0].ItemArray[3].ToString();
                if (db.Exe("delete from tbl_user where id=" + id) == true)
                {
                    frmuser_Load(sender, e);
                    pictureBox1.ImageLocation = "";
                    System.IO.File.Delete("E:\\" + name + ".jpg");
                }
            }
            else
                MessageBox.Show("This member has borrowed a book.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            DataTable dt = new DataTable();
            dt = db.Select("select pic from tbl_user where id = " + id);
            string filename = dt.Rows[0].ItemArray[0].ToString();
            pictureBox1.ImageLocation = filename;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmouser fouser = new frmouser();
            fouser.Text = "Edit";
            fouser.txtname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            fouser.txtlname.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            fouser.txttel .Text= dataGridView1.CurrentRow.Cells[3].Value.ToString();
            fouser.txtid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            fouser.pictureBox1.Image = pictureBox1.Image;
            fouser.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string strname="";
            if (radioButton1.Checked)
                strname = "fname";
            else if(radioButton2.Checked)
                strname = "lname";
            dt= db.Select("select id,fname,lname,tel from tbl_user where "+ strname +" like '%'+ N'" + txtsearch.Text + "' + '%'");
            dataGridView1.DataSource = dt;
        }


    }
}
