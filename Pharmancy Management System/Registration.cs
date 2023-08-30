using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pharmancy_Management_System
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }
        

        private void gunaControlBox1_Click(object sender, EventArgs e)
        {
            Hide();
        }


        SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());

        Boolean empty()
        {
            if (fntxt.Text == "" | sntxt.Text == "" | usntxt.Text == "" | emltxt.Text == "" | pwtxt.Text == "") 
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        private void addpro_Click(object sender, EventArgs e)
        {
            if (pwtxt.Text == cpwtxt.Text) 
            {
                if (fntxt.Text == "" | sntxt.Text == "" | usntxt.Text == "" | emltxt.Text == "" | pwtxt.Text == "" | pntxt.Text == "" | pntxt.Text.Length <= 9) 
                {
                    MessageBox.Show("Please fill in all fields or Make sure that your phone number is 10 numbers.");
                }
                else
                {
                    try
                    {
                        db.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ToString();
                        if (db.State == ConnectionState.Closed)
                        {
                            db.Open();
                            string sele = "SELECT * FROM Users WHERE Username=@usn or Password=@pw or [Phone Number]=@pn ;";
                            SqlCommand com = new SqlCommand(sele, db);
                            com.Parameters.Add("@usn", SqlDbType.VarChar).Value = usntxt.Text;
                            com.Parameters.Add("@pw", SqlDbType.VarChar).Value = pwtxt.Text;
                            com.Parameters.Add("@pn", SqlDbType.VarChar).Value = pntxt.Text;
                            SqlDataReader rn = com.ExecuteReader();
                            if (rn.HasRows)
                            {
                                MessageBox.Show("A user with the same Username or Password already exist change to continue.");
                            }
                            else
                            {
                                rn.Close();
                                string ins = "INSERT INTO Users  VALUES(@fn, @sn, @usn, @em, @pn, @rl, @st, @pw);";
                                string fn = fntxt.Text;
                                string sn = sntxt.Text;
                                string em = emltxt.Text;
                                string usn = usntxt.Text;
                                string pw = pwtxt.Text;
                                string rl = cattxt.selectedValue;
                                string pn = pntxt.Text;
                                string st = bunifuDropdown1.selectedValue;
                                var run = db.Execute(ins, new { fn, sn, usn, em, pn, rl, st, pw });
                                if (run > 0)
                                {
                                    MessageBox.Show("New user Inserted.");
                                    Hide();
                                }
                                db.Close();
                                insact();
                            }
                        }


                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("An error occured as new user was about to be inserted. " + er);
                    }
                }
            }
            else
            {
                MessageBox.Show("Make sure that the text in the Password field and the confirm password field are the same.");
            }
        }
        void insact()
        {
            
            db.Open();
            string ins = "insert into Activity values(@fn, @sn, @rl, @dtl, @dt, @tm);";
            string dtl = "Inserted a new User.";
            string dt = DateTime.Now.ToShortDateString();
            string tm = DateTime.Now.ToShortTimeString();
            string fn = fnlbl.Text;
            string sn = snlbl.Text;
            string rl = rllbl.Text;
            var exe = db.Execute(ins, new { fn, sn, rl, dtl, dt, tm });
            db.Close();
        }
        private void upusrbtn_Click(object sender, EventArgs e)
        {
            try
            {

                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string que = "update Users set [First Name] =@fn, Surname=@sn, Username=@usn, Email=@eml, Role=@rl, Status=@st , Password=@pw where [User ID]=@id;";
                    string usn = usntxt.Text;
                    string pw = pwtxt.Text;
                    string fn = fntxt.Text;
                    string sn = sntxt.Text;
                    string eml = emltxt.Text;
                    string rl = cattxt.selectedValue;
                    string id = label1.Text;
                    string st = bunifuDropdown1.selectedValue;
                    var run = db.Execute(que, new { fn, sn, usn, eml, rl, st, pw, id });
                    if (run > 0)
                    {
                        MessageBox.Show("User Update.");
                        Application.Restart();
                    }
                    db.Close();
                    upact();
                }
                else
                {
                    MessageBox.Show("Connection is already open.");
                }

            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as user was trying to Update " + er);
            }
        }

        void upact()
        {
            
            db.Open();
            string ins = "insert into Activity values(@fn, @sn, @rl, @dtl, @dt, @tm);";
            string dtl = "Updated a User.";
            string dt = DateTime.Now.ToShortDateString();
            string tm = DateTime.Now.ToShortTimeString();
            string fn = fnlbl.Text;
            string sn = snlbl.Text;
            string rl = rllbl.Text;
            var exe = db.Execute(ins, new { fn , sn , rl , dtl, dt, tm });
            db.Close();
        }
        private void Registration_Load(object sender, EventArgs e)
        {

        }
    }
}
