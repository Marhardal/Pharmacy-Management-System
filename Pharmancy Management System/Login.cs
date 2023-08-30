using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using System.Configuration;

namespace Pharmancy_Management_System
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        internal static string usnm;
        //SQLiteConnection db = new SQLiteConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
        SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
        private void addpro_Click(object sender, EventArgs e)
        {

            if (usntxt.Text != "" || pwtxt.Text != "")
            {
                try
                {
                    //Data Source = DESKTOP - 8R88JGH\SQLEXPRESS; Initial Catalog = Pharmance Management System; Integrated Security = True"
                    //connectionString = "Data Source=.\Pharmacy Management System.db; Version=3;"
                    /*connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Pharmance Management System.mdf;Integrated Security=True;"*/
                    if (db.State == ConnectionState.Closed)
                    {
                        db.Open();
                        string sele = "SELECT * FROM Users WHERE Username = '" + usntxt.Text + "' AND Password = '" + pwtxt.Text + "' AND Status = 'Enable' ;";
                        //var com = new SQLiteCommand(sele, db);
                        var com = new SqlCommand(sele, db);
                        //com.Parameters.Add(new SQLiteParameter("@usnm", usntxt.Text));
                        //com.Parameters.Add(new SQLiteParameter("@pw", pwtxt.Text));
                        //SQLiteDataReader sdr = com.ExecuteReader();
                        SqlDataReader sdr = com.ExecuteReader();
                        if (sdr.HasRows)
                        {
                            sdr.Read();
                            usnm = usntxt.Text;
                            string fn = sdr[1].ToString();
                            string sn = sdr[2].ToString();
                            string rle = sdr[6].ToString();
                            Hide();
                            sdr.Close();
                            string ins = "insert into Activity values(@fn, @sn, @rl, @dtl, @dt, @tm);";
                            string dtl = "Logged in into the system.";
                            string dt = DateTime.Now.ToShortDateString();
                            string tm = DateTime.Now.ToShortTimeString();
                            var exe = db.Execute(ins, new { fn, sn, rl = rle, dtl, dt, tm });
                            if (rle == "Admin")
                            {
                                Sale da = new Sale();
                                Hide();
                                da.Show();
                                Hide();
                            }
                            else if (rle == "Pharmacist")
                            {
                                Staff st = new Staff();
                                st.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Failed to Log you in, details did'nt match any.");
                        }
                        sdr.Close();
                        db.Close();
                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show("An error occured as you were trying to login " + er);
                }
            }
            else
            {
                MessageBox.Show("Please fill in all fields.");
            }
                
        }

        private void Login_Load(object sender, EventArgs e)
        {
            bunifuLabel2.Text = File.ReadLines("info.txt").First();
        }

        private void pwtxt_Leave(object sender, EventArgs e)
        {
            ActiveControl = bunifuLabel2;
        }
    }
}
