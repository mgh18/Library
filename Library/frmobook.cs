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
    public partial class frmobook : Form
    {
        public frmobook()
        {
            InitializeComponent();
        }
        DataBase db = new DataBase();
        private void Form4_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = db.Select("select * from tbl_type");
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id=0;
            if (this.Text == "Add")
            {
                
                id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                if (db.Exe("insert into tbl_book(name,writer,type) values (N'" + textBox1.Text + "',N'" + textBox2.Text + "','" + id + "')") == true)
                {
                    textBox1.Text = textBox2.Text = "";
                }
            }
            else
            {
                id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                if (db.Exe("update tbl_book set name = N'" + textBox1.Text + "',writer = N'" + textBox2.Text + "' , type = '" + id + "'where id ='" + int.Parse(textBox3.Text) + "'") == true)
                {
                    textBox1.Text = textBox2.Text = textBox3.Text = "";
                }
            }
        }
    }
}
