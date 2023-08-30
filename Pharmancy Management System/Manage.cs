using CrystalDecisions.CrystalReports.Engine;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
    public partial class Manage : Form
    {
        public Manage()
        {
            InitializeComponent();
        }
        
        SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
        SqlCommand com = new SqlCommand();
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();

        internal static string Fname;
        internal static string Sname;
        internal static string Role;
        internal static string Usnm;
        private void Manage_Load(object sender, EventArgs e)
        {
            Fname = Home.Fname;
            Sname = Home.Sname;
            Role = Home.Role;
            Usnm = Home.usnm;
        }

        private void Catbtn_Click(object sender, EventArgs e)
        {
            Page.SetPage("Category");
            category();
        }

        internal void category()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string que = "select count(pro.Category) as [Category Count], ca.Categories from Category ca inner join Medicine pro on ca.Categories = pro.Category group by ca.Categories, Pro.Category; ";
                    com = new SqlCommand(que, db);
                    sda = new SqlDataAdapter(com);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        catdgv.DataSource = dt;
                    }
                    dt.Dispose();
                    sda.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as category was loaded " + er);
            }
        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            Page.SetPage("Quatation");
            Quat();
        }

        void Quat()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string que = "select * from Quantation order by ID desc";
                    com = new SqlCommand(que, db);
                    sda = new SqlDataAdapter(com);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        quadgv.DataSource = dt;
                    }
                    dt.Dispose();
                    sda.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured while loading Quatation " + er);
            }
        }

        private void Usersbtn_Click(object sender, EventArgs e)
        {
            Page.SetPage("Users");
            users();
        }

        internal void users()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string que = "Select * from Users";
                    sda = new SqlDataAdapter(que, db);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        userdgv.DataSource = dt;
                    }
                    dt.Dispose();
                    sda.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as Users were about to load " + er);
            }
        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            Page.SetPage("Activity");
            Activity_Log();
        }

        void Activity_Log()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "Select * from Activity order by ID Desc";
                    sda = new SqlDataAdapter(sele, db);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        actdgv.DataSource = dt;
                    }
                    dt.Dispose();
                    sda.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to load Activity log " + er);
            }
        }

        private void userseatxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select * from Users where Username like '" + userseatxt.Text + "%';";
                    com = new SqlCommand(sele, db);
                    sda = new SqlDataAdapter(com);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        userdgv.DataSource = dt;
                    }
                    dt.Dispose();
                    sda.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as you were searching for a user " + er);
            }
        }

        private void insusr_Click(object sender, EventArgs e)
        {
            Registration reg = new Registration();
            reg.fnlbl.Text = Fname;
            reg.snlbl.Text = Sname;
            reg.rllbl.Text = Role;
            reg.addpro.Show();
            reg.Show();
        }

        private void upusr_Click(object sender, EventArgs e)
        {
            Registration reg = new Registration();
            reg.fntxt.Text = userdgv.CurrentRow.Cells[1].Value.ToString();
            reg.sntxt.Text = userdgv.CurrentRow.Cells[2].Value.ToString();
            reg.usntxt.Text = userdgv.CurrentRow.Cells[3].Value.ToString();
            reg.emltxt.Text = userdgv.CurrentRow.Cells[4].Value.ToString();
            reg.pwtxt.Text = userdgv.CurrentRow.Cells[6].Value.ToString();
            reg.cattxt.SelectedItem(userdgv.CurrentRow.Cells[5].Value.ToString());
            reg.label1.Text = userdgv.CurrentRow.Cells[0].Value.ToString();
            reg.fnlbl.Text = Fname;
            reg.snlbl.Text = Sname;
            reg.rllbl.Text = Role;
            reg.upusrbtn.Show();
            reg.Show();
        }

        void act_Del()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string ins = "insert into Activity values(@fn, @sn, @rl, @dtl, @dt, @tm);";
                    string dtl = "Deleted a user.";
                    string dt = DateTime.Now.ToString("dd mm yyyy");
                    string tm = DateTime.Now.ToString("HH:mm:ss");
                    string fn = Fname;
                    string sn = Sname;
                    string rl = Role;
                    db.Execute(ins, new { fn, sn, rl, dtl, dt, tm });
                    db.Close();
                }
            }
            finally
            {

            }
        }
        private void Delusr_Click(object sender, EventArgs e)
        {
            try
            {
                
                string que = "Delete from Users where [User ID]=@id ;";
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    com = new SqlCommand(que, db);
                    com.Prepare();
                    com.Parameters.Add(new SqlParameter("@id", userdgv.CurrentRow.Cells[0].Value.ToString()));
                    DialogResult res = MessageBox.Show("Are you sure you want to delete a user.", "Delete", MessageBoxButtons.YesNo);
                    if (DialogResult.Yes == res)
                    {
                        com.ExecuteNonQuery();
                    }

                    db.Close();
                    users();
                    act_Del();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to delete a user " + er);
            }
        }

        private void quaseatxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select * from Quantation where [Quatation No] like '" + quaseatx.Text + "%';";
                    sda = new SqlDataAdapter(sele, db);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        quadgv.DataSource = dt;
                    }
                    dt.Dispose();
                    sda.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as you were searching for a Quotation " + er);
            }
        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            Quatation qua = new Quatation();
            qua.Fname = Fname;
            qua.Sname = Sname;
            qua.Role = Role;
            qua.Usnm = Usnm;
            qua.addsalbtn.Show();
            qua.Show();
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            Quatation qua = new Quatation();
            qua.fntxt.Text = quadgv.CurrentRow.Cells[1].Value.ToString();
            qua.sntxt.Text = quadgv.CurrentRow.Cells[2].Value.ToString();
            qua.pnotxt.Text = quadgv.CurrentRow.Cells[3].Value.ToString();
            //qua.cctxt.Text = quadgv.CurrentRow.Cells[10].Value.ToString();
            qua.Totallbl.Text = quadgv.CurrentRow.Cells[9].Value.ToString();
            qua.ID = quadgv.CurrentRow.Cells[0].Value.ToString();
            qua.Fname = Fname;
            qua.Sname = Sname;
            qua.Role = Role;
            qua.bunifuFlatButton1.Show();
            qua.Show();
        }

        private void catcrebtn_Click(object sender, EventArgs e)
        {
            Category cat = new Category();
            cat.Show();
        }

        private void Homebtn_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void catdgv_SelectionChanged(object sender, EventArgs e)
        {
            if (catdgv.SelectedRows.Count != 0)
            {
                    ctnmlbl.Text = catdgv.CurrentRow.Cells[1].Value.ToString();
                    qtlbl.Text = catdgv.CurrentRow.Cells[0].Value.ToString();
                    name();
            }
        }

        void name()
        {
            
            if (db.State == ConnectionState.Closed)
            {
                try
                {
                    db.Open();
                    if (catdgv.SelectedRows.Count != 0) 
                    {
                        string sele = "select Name from Medicine where Category='" + catdgv.SelectedRows[0].Cells[1].Value + "'";
                        sda = new SqlDataAdapter(sele, db);
                        dt = new DataTable();
                        sda.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            nmlist.DataSource = dt;
                        }
                        dt.Dispose();
                        sda.Dispose();
                        db.Close(); 
                    }
                }
                finally
                {

                }
            }
        }

        private void quaseatx_Enter(object sender, EventArgs e)
        {
            if (quaseatx.Text.Trim() == "Search for a Quatation Number.")
            {
                gunaAdvenceButton11.Enabled = true;
                quaseatx.Text = "";
                quaseatx.ForeColor = Color.Black;
                try
                {
                    
                    if (db.State == ConnectionState.Closed)
                    {
                        db.Open();
                        string sele = "select [Quatation No] from Quantation";
                        com = new SqlCommand(sele, db);
                        SqlDataAdapter sda = new SqlDataAdapter(com);
                        AutoCompleteStringCollection comp = new AutoCompleteStringCollection();
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            comp.Add(dt.Rows[i][0].ToString());
                        }
                        quaseatx.AutoCompleteMode = AutoCompleteMode.Append;
                        quaseatx.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        quaseatx.AutoCompleteCustomSource = comp;
                        sda.Dispose();
                        db.Close();
                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show("An error occured as products were loading into this textbox " + er);
                }
            }
            else
            {
                Quat();
            }
        }

        private void quaseatx_Leave(object sender, EventArgs e)
        {
            if (quaseatx.Text.Trim().Equals("Search for a Quatation Number.") || quaseatx.Text == "")
            {
                quaseatx.Text = "Search for a Quatation Number. ";
                quaseatx.ForeColor = Color.FromArgb(64, 64, 64);
                gunaAdvenceButton11.Enabled = false;
                Quat();
            }
        }

        private void gunaAdvenceButton11_Click(object sender, EventArgs e)
        {
            Page.SetPage("Quat");
            selequat();
            othdet();
        }

        void othdet()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string qua = "select [First Name], Surname, Date, [Quatation No], Role, SUM([Total Price]) as [Total Price], SUM([Total VAT]) as VAT, SUM(Price) as Price"
                            + " ,Until from Quantation where [Quatation No] = @qt group by [First Name], Surname, Date, [Quatation No], Role, Quantity, Until ;";
                    com = new SqlCommand(qua, db);
                    com.Parameters.Add(new SqlParameter("@qt", quaseatx.Text));
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        quatlbl.Text = sdr[3].ToString();
                        dtllbl.Text = sdr[2].ToString();
                        cntxt.Text = sdr[0].ToString() + "  " + sdr[1].ToString();
                        rllbl.Text = sdr[4].ToString();
                        qttltxt.Text = sdr[5].ToString();
                        qvatlbl.Text = sdr[6].ToString();
                        bunifuLabel14.Text = sdr[7].ToString();
                        bunifuLabel13.Text = "This Quotation is valid until " + sdr[8].ToString();
                    }
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to load quatation onto print " + er);
            }
        }

        void selequat()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string qua = "select Name, Quantity, Price, [Total VAT], [Total Price] from Quantation where [Quatation No]=@qt;";
                    com = new SqlCommand(qua, db);
                    com.Parameters.Add(new SqlParameter("@qt", quaseatx.Text));
                    sda = new SqlDataAdapter(com);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        selquadgv.DataSource = dt;
                    }
                    dt.Dispose();
                    sda.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to load quatation onto print " + er);
            }
        }

        private void gunaAdvenceButton12_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt.Columns.Add("Name");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("Price");
                dt.Columns.Add("Total VAT");
                dt.Columns.Add("Total Price");
                foreach (DataGridViewRow dr in selquadgv.Rows)
                {
                    dt.Rows.Add(dr.Cells[0].Value, dr.Cells[1].Value, dr.Cells[2].Value, dr.Cells[3].Value, dr.Cells[4].Value);
                }
                ds.Tables.Add(dt);
                ds.WriteXmlSchema("Quontation.xml");
                var rr = new Quatview();
                var re = new Quat();
                re.SetDataSource(ds);
                TextObject ntxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["ntxt"];
                ntxt.Text = File.ReadLines("info.txt").First();
                TextObject pobtxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["pobtxt"];
                pobtxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(1);
                TextObject ctytxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["ctytxt"];
                ctytxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(2);
                TextObject cytxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["cytxt"];
                cytxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(3);
                TextObject rntxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["rntxt"];
                rntxt.Text = quatlbl.Text;
                TextObject cn = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["cntxt"];
                cn.Text = cntxt.Text + " , we are pleased to submit our quatation as follows.";
                TextObject rl = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["rllbl"];
                rl.Text = rllbl.Text;
                TextObject va = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["va"];
                va.Text = bunifuLabel13.Text;
                TextObject dtl = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["dtllbl"];
                dtl.Text = dtllbl.Text;
                TextObject ttltax = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["ttltax"];
                ttltax.Text = qvatlbl.Text;
                TextObject ttl = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["ttl"];
                ttl.Text = qttltxt.Text;
                TextObject fntxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["fnm"];
                fntxt.Text = Fname;
                TextObject sntxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["snm"];
                fntxt.Text = Sname;
                rr.crystalReportViewer1.ReportSource = re;
                rr.crystalReportViewer1.Refresh();
                dt.Dispose();
                ds.Dispose();
                rr.Show();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void userseatxt_Enter(object sender, EventArgs e)
        {
            if (userseatxt.Text.Trim() == "Search for a User.")
            {
                userseatxt.Text = "";
                userseatxt.ForeColor = Color.Black;
            }
            else
            {
                users();
            }
        }

        private void userseatxt_Leave(object sender, EventArgs e)
        {
            if (userseatxt.Text.Trim().Equals("Search for a User.") || userseatxt.Text == "")
            {
                userseatxt.Text = "Search for a User.";
                userseatxt.ForeColor = Color.FromArgb(64, 64, 64);
                users();
            }
        }

        private void Manage_LocationChanged(object sender, EventArgs e)
        {

        }

        private void prescriptbtn_Click(object sender, EventArgs e)
        {
            Page.SetPage("Prescription");
            priscript();
        }

        private void gunaTransfarantPictureBox1_Click(object sender, EventArgs e)
        {
            Prescript pre = new Prescript();
            pre.Fname = Fname;
            pre.Sname = Sname;
            pre.Role = Role;
            pre.addpro.Show();
            pre.Show();
        }
        void priscript()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string que = "Select * from Prescription ";
                    com = new SqlCommand(que, db);
                    sda = new SqlDataAdapter(com);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Prescrptdgv.DataSource = dt;
                    }
                    dt.Dispose();
                    sda.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void preseatxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = " Select * from Prescription where Category like '" + preseatxt.Text + "%' or Medicine like '" + preseatxt.Text + "%';";
                    sda = new SqlDataAdapter(sele, db);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Prescrptdgv.DataSource = dt;
                    }
                    dt.Dispose();
                    sda.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as you were searching for a Quotation " + er);
            }
        }

        private void preseatxt_Leave(object sender, EventArgs e)
        {
            if (preseatxt.Text.Trim() == "Search for a Category or Medicine." || preseatxt.Text == "")
            {
                preseatxt.Text = "Search for a Category or Medicine.";
                preseatxt.ForeColor = Color.FromArgb(64, 64, 64);
                priscript();
            }
            else
            {
                priscript();
            }
        }

        private void preseatxt_Enter(object sender, EventArgs e)
        {
            if (preseatxt.Text.Trim() == "Search for a Category or Medicine.")
            {
                preseatxt.Text = "";
                preseatxt.ForeColor = Color.Black;
            }
            else
            {
                priscript();
            }
        }

        private void gunaTransfarantPictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (db.State == ConnectionState.Closed) 
                {
                    db.Open();
                    string del = "delete from Prescription where ID='" + Prescrptdgv.CurrentRow.Cells[0].Value.ToString() + "';";
                    com = new SqlCommand(del, db);
                    DialogResult dr = MessageBox.Show("Are you sure you want to delete a Prescription.", "Delete", MessageBoxButtons.YesNo);
                    if (DialogResult.Yes == dr) 
                    {
                        com.ExecuteNonQuery();
                    }
                    db.Close();
                    priscript();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
    }
}

