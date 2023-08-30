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
    public partial class Bars : Form
    {
        public Bars()
        {
            InitializeComponent();
        }

        SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
        SqlCommand com = new SqlCommand();
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        internal void active()
        {
            try
            {
                
                if (db.State==ConnectionState.Closed)
                {
                    db.Open();
                    string sele = " select * from Medicine Where Quantity > [Reorder Level] and Quantity!=[Order Level];";
                    sda = new SqlDataAdapter(sele, db);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Activedgv.DataSource = dt;
                    }
                    dt.Dispose();
                    sda.Dispose();
                    db.Close(); 
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to load Active contents." + er);
            }
        }
        internal void critical()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select * from Medicine where Quantity < [Reorder Level];";
                    sda = new SqlDataAdapter(sele, db);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Inactivedgv.DataSource = dt;
                    }
                    dt.Dispose();
                    sda.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to load Inactive Content " + er);
            }
        }

        internal void inactive()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = " select * from Medicine Where Quantity = [Order Level]";
                    sda = new SqlDataAdapter(sele, db);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Criticaldgv.DataSource = dt;
                    }
                    dt.Dispose();
                    sda.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to load critical Content " + er);
            }
        }

        private void gunaControlBox1_Click(object sender, EventArgs e)
        {
            Hide();
        }
        string Fname;
        string Sname;
        private void Bars_Load(object sender, EventArgs e)
        {
            Fname = Home.Fname;
            Sname = Home.Sname;
        }
        internal void expire()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string que = "select * from Medicine where DATEDIFF(DD, GETDATE(), [Expire Date]) between 0 and 60;";
                    sda = new SqlDataAdapter(que, db);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        expiredgv.DataSource = dt;
                    }
                    dt.Dispose();
                    sda.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to show products about to expire " + er);
            }
        }

        void rece()
        {
            
            db.Open();
            string que = "Select * from Rece";
            SqlCommand com = new SqlCommand(que, db);
            SqlDataReader sdr = com.ExecuteReader();
            if (sdr.HasRows)
            {
                sdr.Read();
                if (sdr[1].ToString().Length == 9 || sdr[2].ToString().Length == 5)
                {
                    sdr.Close();
                    string upd = "Update Rece set [right]=1 where ID = 1";
                    com = new SqlCommand(upd, db);
                    com.ExecuteNonQuery();
                    db.Close();
                }
                else if (sdr[2].ToString().Length == 5)
                {
                    sdr.Close();
                    string upd = "Update Rece set [ZNO]=(DAY(CURRENT_TIMESTAMP)+1) where ID = 1";
                    com = new SqlCommand(upd, db);
                    com.ExecuteNonQuery();
                    db.Close();
                }

                else
                {
                    int one = Convert.ToInt32(sdr[0].ToString()) + 1;
                    int two = Convert.ToInt32(sdr[1].ToString()) + 1;
                    int three = Convert.ToInt32(sdr[2].ToString()) + 1;
                    sdr.Close();
                    string up = "update Rece set [Left] = DAY(CURRENT_TIMESTAMP), [Right] =" + two + ", [ZNO]=" + three + " where ID = 1 ;";
                    com = new SqlCommand(up, db);
                    com.ExecuteNonQuery();
                    db.Close();
                }
            }
        }
        string zno;
        string show;
        void num()
        {
            
            db.Open();
            string sele = "Select * from Rece";
            com = new SqlCommand(sele, db);
            SqlDataReader sdr = com.ExecuteReader();
            if (sdr.HasRows)
            {
                sdr.Read();
                int left = Convert.ToInt32(sdr[0].ToString());
                int right = Convert.ToInt32(sdr[1].ToString());
                show = "00" + left + "/00" + right;
                zno = "00" + sdr[2].ToString();
                sdr.Close();
            }
            db.Close();
        }

        void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            rece();
            num();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Category");
            dt.Columns.Add("Price");
            dt.Columns.Add("VAT Price");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Reorder Level");
            dt.Columns.Add("Manufacturing Date");
            dt.Columns.Add("Expirely Date");
            foreach (DataGridViewRow dr in Criticaldgv.Rows)
            {
                dt.Rows.Add(dr.Cells[1].Value, dr.Cells[2].Value, dr.Cells[3].Value, dr.Cells[4].Value, dr.Cells[5].Value, dr.Cells[7].Value, dr.Cells[10].Value, dr.Cells[11].Value);
            }
            ds.Tables.Add(dt);
            ds.WriteXmlSchema("Product.xml");
            Proview rr = new Proview();
            var re = new Products();
            re.SetDataSource(ds);
            TextObject ntxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["ntxt"];
            ntxt.Text = File.ReadLines("info.txt").First();
            TextObject rntxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["rntxt"];
            rntxt.Text = show;
            TextObject pobtxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["pobtxt"];
            pobtxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(1);
            TextObject ctytxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["ctytxt"];
            ctytxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(2);
            TextObject ctttxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["ctttxt"];
            ctttxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(3);
            TextObject ttletxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["ttletxt"];
            ttletxt.Text = "Inactive Products.";
            TextObject fntxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["fntxt"];
            fntxt.Text = Fname;
            TextObject sntxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["sntxt"];
            sntxt.Text = Sname;
            rr.crystalReportViewer1.ReportSource = re;
            rr.crystalReportViewer1.Refresh();
            dt.Dispose();
            ds.Dispose();
            rr.Show();
            act_Pri();
        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            rece();
            num();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Category");
            dt.Columns.Add("Price");
            dt.Columns.Add("VAT Price");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Reorder Level");
            dt.Columns.Add("Manufacturing Date");
            dt.Columns.Add("Expirely Date");
            foreach (DataGridViewRow dr in Criticaldgv.Rows)
            {
                dt.Rows.Add(dr.Cells[1].Value, dr.Cells[2].Value, dr.Cells[3].Value, dr.Cells[4].Value, dr.Cells[5].Value, dr.Cells[7].Value, dr.Cells[10].Value, dr.Cells[11].Value);
            }
            ds.Tables.Add(dt);
            ds.WriteXmlSchema("Product.xml");
            Proview rr = new Proview();
            var re = new Products();
            re.SetDataSource(ds);
            TextObject ntxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["ntxt"];
            ntxt.Text = File.ReadLines("info.txt").First();
            TextObject pobtxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["pobtxt"];
            pobtxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(1);
            TextObject rntxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["rntxt"];
            rntxt.Text = show;
            TextObject ctytxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["ctytxt"];
            ctytxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(2);
            TextObject ctttxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["ctttxt"];
            ctttxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(3);
            TextObject ttletxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["ttletxt"];
            ttletxt.Text = "Critical Products.";
            TextObject fntxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["fntxt"];
            fntxt.Text = Fname;
            TextObject sntxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["sntxt"];
            sntxt.Text = Sname;
            rr.crystalReportViewer1.ReportSource = re;
            rr.crystalReportViewer1.Refresh();
            dt.Dispose();
            ds.Dispose();
            rr.Show();
            act_Pri();
        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            rece();
            num();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Category");
            dt.Columns.Add("Price");
            dt.Columns.Add("VAT Price");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Reorder Level");
            dt.Columns.Add("Manufacturing Date");
            dt.Columns.Add("Expire Date");
            foreach (DataGridViewRow dr in Activedgv.Rows)
            {
                dt.Rows.Add(dr.Cells[1].Value, dr.Cells[2].Value, dr.Cells[3].Value, dr.Cells[4].Value, dr.Cells[5].Value, dr.Cells[7].Value, dr.Cells[10].Value, dr.Cells[11].Value);
            }
            ds.Tables.Add(dt);
            ds.WriteXmlSchema("Product.xml");
            Proview rr = new Proview();
            var re = new Products();
            re.SetDataSource(ds);
            TextObject ntxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["ntxt"];
            ntxt.Text = File.ReadLines("info.txt").First();
            TextObject pobtxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["pobtxt"];
            pobtxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(1);
            TextObject ctytxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["ctytxt"];
            ctytxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(2);
            TextObject ctttxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["ctttxt"];
            ctttxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(3);
            TextObject rntxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["rntxt"];
            rntxt.Text = show;
            TextObject ttletxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["ttletxt"];
            ttletxt.Text = "Active Products.";
            TextObject fntxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["fntxt"];
            fntxt.Text = Fname;
            TextObject sntxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["sntxt"];
            sntxt.Text = Sname;
            rr.crystalReportViewer1.ReportSource = re;
            rr.crystalReportViewer1.Refresh();
            dt.Dispose();
            ds.Dispose();
            rr.Show();
            act_Pri();
        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        internal string Role;

        void act_Pri()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string ins = "insert into Activity values(@fn, @sn, @rl, @dtl, @dt, @tm);";
                    string nm = headtxt.Text.Substring(0);
                    string dtl = "Printed " + nm;
                    string dt = DateTime.Now.ToString("dd mm yyyy");
                    string tm = DateTime.Now.ToString("HH:mm:ss");
                    string fn = Fname;
                    string sn = Sname;
                    Role = Home.Role;
                    string rl = Role;
                    db.Execute(ins, new { fn, sn, rl, dtl, dt, tm });
                    db.Close();
                }
            }
            finally
            {

            }
        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Category");
            dt.Columns.Add("Price");
            dt.Columns.Add("VAT Price");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Reorder Level");
            dt.Columns.Add("Manufacturing Date");
            dt.Columns.Add("Expire Date");
            foreach (DataGridViewRow dr in Activedgv.Rows)
            {
                dt.Rows.Add(dr.Cells[1].Value, dr.Cells[2].Value, dr.Cells[3].Value, dr.Cells[4].Value, dr.Cells[5].Value, dr.Cells[7].Value, dr.Cells[10].Value, dr.Cells[11].Value);
            }
            ds.Tables.Add(dt);
            ds.WriteXmlSchema("Product.xml");
            Proview rr = new Proview();
            var re = new Products();
            re.SetDataSource(ds);
            TextObject ntxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["ntxt"];
            ntxt.Text = File.ReadLines("info.txt").First();
            TextObject pobtxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["pobtxt"];
            pobtxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(1);
            TextObject ctytxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["ctytxt"];
            ctytxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(2);
            TextObject ctttxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["ctttxt"];
            ctttxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(3);
            TextObject rntxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["rntxt"];
            rntxt.Text = show;
            TextObject ttletxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["ttletxt"];
            ttletxt.Text = "Products which will expire in 60 days.";
            TextObject fntxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["fntxt"];
            fntxt.Text = Fname;
            TextObject sntxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["sntxt"];
            sntxt.Text = Sname;
            rr.crystalReportViewer1.ReportSource = re;
            rr.crystalReportViewer1.Refresh();
            dt.Dispose();
            ds.Dispose();
            rr.Show();
            act_Pri();
        }
    }
}
