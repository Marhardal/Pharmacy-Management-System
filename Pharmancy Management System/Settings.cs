using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pharmancy_Management_System
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage("Address");
        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage("Name");
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            string info = "./info.txt";
            StreamWriter sw = new StreamWriter(info);
            sw.Write(fsttxt.Text);
            sw.Close();
            MessageBox.Show("Please restart or logout for the changes to show.");
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            string info = "./info.txt";
            if (File.Exists(info))
            {
                using (StreamReader sr = File.OpenText(info)) 
                {
                    //var s = "";
                    //while ((s = sr.ReadLine()) != null)
                    //{
                    //    fsttxt.Text = s;
                    //}
                    fsttxt.Text = sr.ReadToEnd();
                }
            }
            else
            {
                using (FileStream sw = File.Create(info)) 
                {
                    Byte[] title = new UTF8Encoding(true).GetBytes("New Text FIle");
                    sw.Write(title, 0, title.Length);
                    byte[] author = new UTF8Encoding(true).GetBytes("Martin Harawa");
                    sw.Write(author, 0, author.Length);
                }
            }
        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.Filter = "Backup File(*.bak |*.bak)";
            saveFileDialog1.ShowDialog();
            Backtxt.Text = saveFileDialog1.FileName;
        }
        private void BBrowsebtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog()==DialogResult.OK)
            {
                Backtxt.Text = fbd.SelectedPath;
                backupbtn.Enabled = true;
            }
        }
        SqlConnection db = new SqlConnection(@"Data Source=DESKTOP-3TSVGJC;Initial Catalog='Point Of Sale';Integrated Security=True");
        SqlCommand com = new SqlCommand();
        private void backupbtn_Click(object sender, EventArgs e)
        {
            string database = db.Database.ToString();
            if (Backtxt.Text == string.Empty) 
            {
                MessageBox.Show("Please click browse and select a Location.");
            }
            else
            {
                if (db.State == ConnectionState.Closed) 
                {
                    string cmd = "BACKUP DATABASE [" + database + "] TO DISK='" + Backtxt.Text + "\\" + database + "-" + DateTime.Now.ToString("yyyy MM dd HH mm ss") + ".bak';";
                    db.Open();
                    com = new SqlCommand(cmd, db);
                    com.ExecuteNonQuery();
                    MessageBox.Show("Database backup successfully.");
                    db.Close();
                    backupbtn.Enabled = false;
                }
            }
        }

        private void brestorebtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofp = new OpenFileDialog();
            ofp.Filter = "SQL SERVER Backup Files|*.bak";
            ofp.Title = "Database Restore";
            if (ofp.ShowDialog()==DialogResult.OK)
            {
                restoretxt.Text = ofp.FileName;
                Restorebtn.Enabled = true;
            }
        }

        private void Restorebtn_Click(object sender, EventArgs e)
        {
            string database = db.Database.ToString();
            try
            {
                if (db.State == ConnectionState.Closed) 
                {
                    db.Open();
                    string que = string.Format("ALTER DATABASE [" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                    com = new SqlCommand(que, db);
                    com.ExecuteNonQuery();
                    string que1 = "USE MASTER RESTORE DATABASE [" + database + "] FROM DISK='" + restoretxt.Text + "' WITH REPLACE";
                    com = new SqlCommand(que1, db);
                    com.ExecuteNonQuery();
                    string que2 = "ALTER DATABASE [" + database + "] SET MULTI_USER";
                    com = new SqlCommand(que2, db);
                    com.ExecuteNonQuery();
                    MessageBox.Show("Restore completed.");
                    db.Close(); 
                }
;            }
            catch (Exception fa)
            {
                MessageBox.Show("Failed to load database " + fa);
            }
        }
    }
}
