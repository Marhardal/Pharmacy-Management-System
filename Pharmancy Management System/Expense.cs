using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;

namespace Pharmancy_Management_System
{
    public partial class Expense : Form
    {
        public Expense()
        {
            InitializeComponent();
        }

        SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());

        internal string Fname;
        internal string Sname;
        internal string Role;
        internal string phn;

        private void addpro_Click(object sender, EventArgs e)
        {
            if (supnmtxt.Text != "" || amtxt.Text != "" || pmtpcom.selectedValue != "Select a Payment Type." || balatxt.Text != "")
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    try
                    {
                        db.Open();
                        var id = invdttxt.Value;
                        var sn = supnmtxt.Text;
                        var am = amtxt.Text;
                        var pt = pmtpcom.selectedValue;
                        var bl = balatxt.Text;
                        var dd = ddtxt.Value;
                        string quer = "insert into Payable values('0', @id, @sn, @am, @pt, @bl, @dd);";
                        var exe = db.Execute(quer, new { id, sn, am, pt, bl, dd });
                        if (exe > 0)
                        {
                            MessageBox.Show("Inserted a Payable..");
                        }
                        db.Close();
                        ins();
                        INV();
                        Hide();
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("Failed to insert new expense " + er);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill in all fields to countinue.");
            }
        }

        void INV()
        {
            db.Open();
            string que = "select MAX(ID) from Payable;";
            SqlCommand com = new SqlCommand(que, db);
            SqlDataReader sdr = com.ExecuteReader();
            if (sdr.HasRows)
            {
                sdr.Read();
                string id = sdr[0].ToString();
                string fr = supnmtxt.Text.Substring(0, 3).ToUpper();
                sdr.Close();
                string up = "update Payable set [Invoice ID]=@iid where ID=@id";
                var iid = fr + "INV" + id;
                var exe = db.Execute(up, new { iid, id });
            }
            sdr.Close();
            db.Close();
        }
        
        private void Expense_Load(object sender, EventArgs e)
        {

        }

        private void recadd_Click(object sender, EventArgs e)
        {
            if (recamotxt.Text != "" || advrectxt.Text != "" || baltxt.Text != "")
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    try
                    {
                        db.Open();
                        string fn = Fname;
                        string sn = Sname;
                        var bc = baltxt.Text;
                        var dd = duedttxt.Value;
                        var rc = recamotxt.Text;
                        var ar = advrectxt.Text;
                        var pn = phn;
                        string quer = "insert into Receivables values('0', @rc, @ar, @bc, @fn, @sn, @pn, @dd);";
                        var exe = db.Execute(quer, new { rc, ar, bc, fn, sn, pn, dd });
                        if (exe > 0)
                        {
                            MessageBox.Show("Inserted a Receivables..");
                        }
                        db.Close();
                        ins();
                        Rec();
                        Hide();
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("Failed to insert new Receivable " + er);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill in all fields to countinue.");
            }
        }
        void Rec()
        {
            
            db.Open();
            string que = "select MAX(ID) from Receivables;";
            SqlCommand com = new SqlCommand(que, db);
            SqlDataReader sdr = com.ExecuteReader();
            if (sdr.HasRows)
            {
                sdr.Read();
                string id = sdr[0].ToString();
                sdr.Close();
                string up = "update Receivables set [Receive ID]=@iid where ID=@id";
                var iid = "REC" + id;
                var exe = db.Execute(up, new { iid, id });
            }
            db.Close();
        }

        void ins()
        {
            
            db.Open();
            string ins = "insert into Activity values(@fn, @sn, @rl, @dtl, @dt, @tm);";
            string dt = DateTime.Now.ToShortDateString();
            string tm = DateTime.Now.ToShortTimeString();
            string nm = bunifuLabel1.Text.Substring(7);
            string dtl = "Inserted a " + nm;
            string fn = Fname;
            string sn = Sname;
            string rl = Role;
            var exe = db.Execute(ins, new { fn, sn, rl, dtl, dt, tm });
            db.Close();
        }

        private void payupbtn_Click(object sender, EventArgs e)
        {
            
            if (db.State == ConnectionState.Closed) 
            {
                try
                {
                    db.Open();
                    var sn = supnmtxt.Text;
                    var am = amtxt.Text;
                    var pt = pmtpcom.selectedValue;
                    var bl = balatxt.Text;
                    var dd = ddtxt.Value;
                    var id = bunifuLabel4.Text;
                    string quer = "update Payable set [Supplier Name]=@sn, Amount=@am, [Payment Type]=@pt, Balance=@bl, [Due Date]=@dd where ID=@id";
                    var exe = db.Execute(quer, new { sn, am, pt, bl, dd, id });
                    if (exe > 0)
                    {
                        MessageBox.Show("Inserted a Payable..");
                    }
                    db.Close();
                    upd();
                    Hide();
                }
                catch (Exception er)
                {
                    MessageBox.Show("Failed to insert new expense " + er);
                } 
            }
        }

        void upd()
        {
            
            db.Open();
            string ins = "insert into Activity values(@fn, @sn, @rl, @dtl, @dt, @tm);";
            string nm = bunifuLabel1.Text.Substring(7);
            string dtl = "Update a " + nm;
            string dt = DateTime.Now.ToShortDateString();
            string tm = DateTime.Now.ToShortTimeString();
            string fn = Fname;
            string sn = Sname;
            string rl = Role;
            var exe = db.Execute(ins, new { fn, sn, rl, dtl, dt, tm });
            db.Close();
        }

        private void receup_Click(object sender, EventArgs e)
        {
            
            if (db.State == ConnectionState.Closed)
            {
                try
                {
                    db.Open();
                    string fn = Fname;
                    string sn = Sname;
                    var bc = baltxt.Text;
                    var dd = duedttxt.Value;
                    var rc = recamotxt.Text;
                    var ar = advrectxt.Text;
                    var pn = phn;
                    var id = bunifuLabel4.Text;
                    string quer = "update Receivables set [Total Receivable]=@rc, [Advanced Receipt]=@ar, Balance=@bc, [Phone Number]=@pn, [Due Date]=@dd where ID=@id";
                    var exe = db.Execute(quer, new { rc, ar, bc, pn, dd, id });
                    if (exe > 0)
                    {
                        MessageBox.Show("Updated a Receivable..");
                    }
                    db.Close();
                    upd();
                    Rec();
                    Hide();
                }
                catch (Exception er)
                {
                    MessageBox.Show("Failed to insert new Receivable " + er);
                }
            }
        }

        private void advrectxt_Leave(object sender, EventArgs e)
        {
            try
            {
                float amo = Convert.ToSingle(recamotxt.Text);
                float adv = Convert.ToSingle(advrectxt.Text);
                float bal = amo - adv;
                baltxt.Text = bal.ToString();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void expadd_Click(object sender, EventArgs e)
        {
            if (expnmtxt.Text != "" || expamotxt.Text != "") 
            {
                try
                {
                    
                    if (db.State == ConnectionState.Closed) 
                    {
                        db.Open();
                        string que = "insert into Expense values (@nm, @amo, @dt, @fn, @sn);";
                        string nm = expnmtxt.Text;
                        string amo = expamotxt.Text;
                        string dt = DateTime.Now.ToString("yyyy-MM-dd");
                        string fn = Fname;
                        string sn = Sname;
                        var exe = db.Execute(que, new { nm, amo, dt, fn, sn });
                        if (exe > 0)
                        {
                            MessageBox.Show("Inserted an Expense");
                        }
                        db.Close();
                    }
                    ins();
                }
                catch (Exception er)
                {
                    MessageBox.Show("Failed to insert Expense " + er);
                }
            }
            else
            {
                MessageBox.Show("Please Fill in all Fields.");
            }
        }

        private void expeup_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string que = "update Expense set Name=@nm, Amount=@amo where ID=@id;";
                    string nm = expnmtxt.Text;
                    string amo = expamotxt.Text;
                    string id = bunifuLabel4.Text;
                    var exe = db.Execute(que, new { nm, amo, id });
                    if (exe > 0)
                    {
                        MessageBox.Show("Updated an Expense");
                    }
                    db.Close();
                }
                upd();
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to insert Expense " + er);
            }
        }

        private void supnmtxt_Enter(object sender, EventArgs e)
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select [Invoice ID] from Payable";
                    var com = new SqlCommand(sele, db);
                    SqlDataAdapter sda = new SqlDataAdapter(com);
                    AutoCompleteStringCollection comp = new AutoCompleteStringCollection();
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        comp.Add(dt.Rows[i][0].ToString());
                    }
                    supnmtxt.AutoCompleteMode = AutoCompleteMode.Append;
                    supnmtxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    supnmtxt.AutoCompleteCustomSource = comp;
                    sda.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
    }
}
