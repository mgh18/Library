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
    public partial class frmouser : Form
    {
        public frmouser()
        {
            InitializeComponent();
        }
        bool temp = false;
        private void frmouser_Load(object sender, EventArgs e)
        {
            strfilename = txtname.Text + txtlname.Text + txttel.Text;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select....";
            op.Filter = "JPG (*.jpg)|*.jpg";
            if (op.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = op.FileName;
                temp = true;
            }
        }
        DataBase db = new DataBase();
        string strfilename = "";
        private void button1_Click(object sender, EventArgs e)
        {
            string filename = txtname.Text + txtlname.Text + txttel.Text;
            string path = "E:\\pics\\" + filename + ".jpg";
            if (Text == "Add")
            {
                if (temp)
                {
                    pictureBox1.Image.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                else
                {
                    path = "E:\\pics\\default-pic.jpg";
                }
                if (db.Exe("insert into tbl_user(fname,lname,tel,pic) values (N'" + txtname.Text + "',N'" + txtlname.Text + "',N'" + txttel.Text + "',N'" + path + "')") == true)
                {
                    txtname.Text = txtlname.Text = txttel.Text = "";
                    pictureBox1.ImageLocation = "E:\\pics\\default-pic.jpg";
                    temp = false;
                }
            }
            else if (Text == "Edit")
            {
                System.IO.File.Delete("E:\\pics\\" + strfilename + ".jpg");
                pictureBox1.Image.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                if (db.Exe("update tbl_user set fname= N'" + txtname.Text + "',lname= N'" + txtlname.Text + "',tel= N'" + txttel.Text + "' ,pic= N'" + path + "' where id = " + int.Parse(txtid.Text)) == true)
                {
                    txtname.Text = txtlname.Text = txttel.Text = "";
                    pictureBox1.ImageLocation = "E:\\pics\\default-pic.jpg";
                }
            }
        }
        WinFormCharpWebCam.WebCam wb = new WinFormCharpWebCam.WebCam();
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkLabel2.Text == "Camera")
            {
                wb.InitializeWebCam(ref pictureBox1);
                try
                {
                    wb.Start();
                }
                catch
                {

                }
                finally
                {
                    wb.Continue();
                    linkLabel2.Text = "Take picture";
                }
            }
            else if(linkLabel2.Text== "Take picture")
            {
                temp = true;
                wb.Stop();
                linkLabel2.Text = "Camera";
            }
        }
    }
}
