using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using System.Configuration;

namespace Pharmancy_Management_System
{
    public partial class Prescript : Form
    {
        public Prescript()
        {
            InitializeComponent();
        }

        SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());

        private void gunaControlBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void addpro_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (db.State == ConnectionState.Closed) 
                {
                    string que = "insert into Prescription values(@af, @at, @ds, @ct, @med);";
                    string af = aftxt.Text;
                    string at = attxt.Text;
                    string ct = cattxt.selectedValue;
                    string ds = dstxt.Text;
                    string med = medtxt.selectedValue;
                    var exe = db.Execute(que, new { af, at, ds, ct, med });
                    if (exe > 0)
                    {
                        MessageBox.Show("Priscription Added");
                        Hide();
                    }
                    db.Close();
                    insact();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        

        void insact()
        {
            
            db.Open();
            string ins = "insert into Activity values(@fn, @sn, @rl, @dtl, @dt, @tm);";
            string dtl = "Made a Prescription.";
            string dt = DateTime.Now.ToShortDateString();
            string tm = DateTime.Now.ToShortTimeString();
            var exe = db.Execute(ins, new { fn = Fname, sn = Sname, rl = Role, dtl, dt, tm });
            db.Close();
        }

        private void upusrbtn_Click(object sender, EventArgs e)
        {

        }

        internal string Fname;
        internal string Sname;
        internal string Role;
        //internal string Phone;
        private void Prescript_Load(object sender, EventArgs e)
        {
            cat();
        }

        internal void prod()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string que = " select Name from Medicine where Category='" + cattxt.selectedValue + "';";
                    var com = new SqlCommand(que, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            medtxt.Items.Add(sdr[0].ToString());
                        }
                    }
                    medtxt.selectedIndex = 0;
                    sdr.Close();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as combobox values were loading " + er);
            }
        }

        internal void cat()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string que = "select Categories from Category;";
                    var com = new SqlCommand(que, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            cattxt.Items.Add(sdr[0].ToString());
                        }
                    }
                    cattxt.selectedIndex = 0;
                    sdr.Close();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as combobox values were loading " + er);
            }
        }

        private void cattxt_onItemSelected(object sender, EventArgs e)
        {
            prod();
        }
    }
}
