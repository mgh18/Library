using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Library
{
    public partial class frm : Form
    {
        public frm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string lpass = Properties.Settings.Default.Pass;
            if (txtlp.Text == lpass)
            {
                Properties.Settings.Default.Pass = txtnp.Text;
                Properties.Settings.Default.Save();
                txtlp.Text = txtnp.Text = "";
                MessageBox.Show("Password changed successfully!");
            }
        }
        DataBase db = new DataBase();
        public bool SendMail(string strmail, string path)
        {
            try
            {
                MailMessage mm;
                MailAddress from = new MailAddress("m.ghazi.asad.168@gmail.com", "back up file", Encoding.UTF8);
                MailAddress to = new MailAddress(strmail);
                mm = new MailMessage(from, to);
                mm.Subject = "Backup From DataBase";
                mm.Body = DateTime.Now.ToShortDateString() + Environment.NewLine + DateTime.Now.ToShortTimeString();
                Attachment a = new Attachment(path, MediaTypeNames.Application.Octet);
                mm.Attachments.Add(a);
                SmtpClient smtpc = new SmtpClient();
                smtpc.Port = 587;
                smtpc.EnableSsl = true;
                smtpc.Host = "smtp.gmail.com";

                smtpc.Credentials = new NetworkCredential("m.ghazi.asad.168@gmail.com","");//google app password
                smtpc.Send(mm);
                return true;
            }
            catch(Exception ex)
            {
                string s = ex.Message;
                return false;
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save...";
            sfd.Filter = "Back File (*.bak)|*.bak";
            sfd.OverwritePrompt = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (db.Exe("backup database crudTest to disk ='" + sfd.FileName + "'"))
                {
                    MessageBox.Show("Backup completed successfully", "True", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Properties.Settings.Default.lastbackup = DateTime.Now.ToShortDateString();
                    Properties.Settings.Default.Save();
                    if (MessageBox.Show("Do you want the backup file to be sent to your e-mail?", "True", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (SendMail(Properties.Settings.Default.mail, sfd.FileName) == true)
                            MessageBox.Show("Backup file sent successfully!", "True", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Failed to send the backup file", "False", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("Back-up failed.", "False", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frm_Load(object sender, EventArgs e)
        {
            txtlb.Text = Properties.Settings.Default.lastbackup;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Open...";
            op.Filter = "Buckup File (*.bak)|*.bak";
            if (op.ShowDialog() == DialogResult.OK)
            {
                if (db.Exe("alter database crudTest set single_user with rollback immediate use master restore database crudTest from disk = '" + op.FileName + "' with replace"))
                    MessageBox.Show("Successfully reverted to the last backup", "True", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Failed to roll back to last backup.", "False", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.mail = txtmail.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("Email address changed successfully.");
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
