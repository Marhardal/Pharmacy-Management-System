using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pharmancy_Management_System
{
    public partial class Category : Form
    {
        public Category()
        {
            InitializeComponent();
        }

        SqlConnection db = new SqlConnection();

        private void addpro_Click(object sender, EventArgs e)
        {
            try
            {
                db.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ToString();
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string ins = "Insert into Category values(@ct)";
                    SqlCommand com = new SqlCommand(ins, db);
                    com.Parameters.Add("@ct", SqlDbType.VarChar).Value = natxt.Text;
                    com.ExecuteNonQuery();
                    
                    db.Close();
                    Hide();
                    insact();

                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as a new category was about to be inserted " + er);
            }
        }
        void insact()
        {
            db.Open();
            string ins = "insert into Activity values(@fn, @sn, @rl, @dtl, @dt, @tm);";
            string dtl = "Made a Category.";
            string dt = DateTime.Now.ToShortDateString();
            string tm = DateTime.Now.ToShortTimeString();
            var exe = db.Execute(ins, new { fn = Fname, sn = Sname, rl = Role, dtl, dt, tm });
            db.Close();
        }

        internal static string Fname;
        internal static string Sname;
        internal static string Role;
        private void Category_Load(object sender, EventArgs e)
        {
            Fname = Home.Fname;
            Sname = Home.Sname;
            Role = Home.Role;
        }
    }
}
