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
    public partial class frmtype : Form
    {
        public frmtype()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (db.Exe("insert tbl_type(type) values (N'" + txtname.Text + "')") == true)
            {
                txtname.Text = "";
                frmtype_Load(sender, e);
            }
    
        }

        private void frmtype_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt=db.Select("select * from tbl_type");
            dataGridView1.DataSource = dt;
        }
        DataBase db = new DataBase();
        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = db.Select("select * from tbl_book where type=" + txtdel.Text);
            if (dt.Rows.Count == 0)
            {
                if (db.Exe("delete from tbl_type where id =" + txtdel.Text))
                {
                    frmtype_Load(sender, e);
                    txtdel.Text = "";
                }
            }
            else
                MessageBox.Show(".دسته ای مورد نظر قابل حذف نیست", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            txtdel.Text=  dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }
    }
}
