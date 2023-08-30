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
    public partial class Progview : Form
    {
        public Progview()
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
        private void Progview_Load(object sender, EventArgs e)
        {
            Fname = Home.Fname;
            Sname = Home.Sname;
            Role = Home.Role;
            month();
            year();
            datelbl.Text = DateTime.Now.ToString("01 MMMM yyyy") + " - Now";
            Day();
            m_per_pro();
            sales();
            expense();
            Total_inco();
            Total_expe();
            Total_Vat();
            Open_inc();
            Open_exp();
            float opbal = opinc - opexp;
            opballbl.Text = "K" + opbal.ToString();
            float balance = ttlinc - (ttlexp);
            ttlbl.Text = "K" + balance.ToString();
            try
            {
                if (yearcom.selectedIndex != 0)
                {
                    string date = "01-0" + index + "-" + yearcom.selectedValue;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
        internal void Open_inc()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string date = yearcom.selectedValue + "-0" + index + "-01";
                    string que = "select SUM([Total Price]) from Sales where DATEPART(MONTH, Date)=DATEPART(MONTH, DATEADD(MONTH, -1, GETDATE()))"
                        + " and DATEPART(yyyy, Date)= DATEPART(YYYY, DATEADD(month, -1, '" + date + "')); ";
                    com = new SqlCommand(que, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        if (sdr[0].ToString() == "NULL")
                        {
                            opinc = 0;
                        }
                        else
                        {
                            opinc = Convert.ToSingle(sdr[0].ToString());
                        }
                    }
                    sdr.Close();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to load Total Expense " + er);
            }
        }

        internal void Open_exp()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string date = yearcom.selectedValue + "-0" + index + "-01";
                    string que = "select SUM(Amount) from Expense where DATEPART(MONTH, Date)=DATEPART(MONTH, DATEADD(MONTH, -1, GETDATE()))"
                        + " and DATEPART(yyyy, Date)= DATEPART(YYYY, DATEADD(month, -1, '" + date + "')); ";
                    com = new SqlCommand(que, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        opexp = Convert.ToSingle(sdr[0].ToString());
                    }
                    sdr.Close();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to load Total Expense " + er);
            }
        }

        float opinc;
        float opexp;

        void month()
        {
            
            if (db.State == ConnectionState.Closed) 
            {
                db.Open();
                string Que = "select distinct DATENAME(MONTH, Date) from Sales";
                com = new SqlCommand(Que, db);
                SqlDataReader sdr = com.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        moncom.Items.Add(sdr[0].ToString());
                        moncom.SelectedItem(DateTime.Now.ToString("MMMM"));
                    }
                }
                sdr.Close();
                db.Close(); 
            }
        }

        void year()
        {
            
            if (db.State == ConnectionState.Closed)
            {
                db.Open();
                string Que = "select distinct DATENAME(YYYY, Date) from Sales";
                com = new SqlCommand(Que, db);
                SqlDataReader sdr = com.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        yearcom.Items.Add(sdr[0].ToString());
                        yearcom.SelectedItem(DateTime.Now.ToString("yyyy"));
                    }
                }
                sdr.Close();
                db.Close();
            }
        }

        float ttlvat;
        internal void Total_Vat()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string que = "select SUM(VAT) from Sales where MONTH(Date)=" + index + " and YEAR(Date)=" + yearcom.selectedValue + ";";
                    com = new SqlCommand(que, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        ttlvatlbl.Text = "K" + sdr[0].ToString();
                        ttlvat = Convert.ToSingle(sdr[0].ToString());
                    }
                    sdr.Close();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to load Total Expense " + er);
            }
        }

        float ttlexp;
        internal void Total_expe()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string que = "select SUM(Amount) from Expense where MONTH(Date)="+index+" AND YEAR(Date)="+yearcom.selectedValue+";";
                    com = new SqlCommand(que, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        ttlexplbl.Text = "K"+sdr[0].ToString();
                        ttlexp = Convert.ToSingle(sdr[0].ToString());
                    }
                    sdr.Close();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to load Total Expense " + er);
            }
        }

        float ttlinc;
        internal void Total_inco()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed) 
                {
                    db.Open();
                    string que = "select SUM([Total Price]) from Sales where MONTH(Date)=" + index + " AND YEAR(Date)=" + yearcom.selectedValue + ";";
                    com = new SqlCommand(que, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        ttlinclbl.Text = "K" + sdr[0].ToString();
                        ttlinc = Convert.ToSingle(sdr[0].ToString());
                    }
                    sdr.Close();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to load total income " + er);
            }
        }

        internal void expense()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string que = "select top 5 DATENAME(WEEKDAY, Date) As [Day], SUM(Amount) as Amount, [First Name], Surname from Expense"
                           + " where MONTH(Date) = " + index + " AND YEAR(Date)=" + yearcom.selectedValue + " group by Date, [First Name], Surname order by Date asc";
                    sda = new SqlDataAdapter(que, db);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        expensedgv.DataSource = dt;
                    }
                    dt.Dispose();
                    sda.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to load expenses " + er);
            }
        }

        internal void sales()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string que = "select top 5 DATENAME(WEEKDAY, Date) As [Day], SUM([Total Price]) as Amount, [First Name], Surname from Sales"
                            + " where MONTH(Date) = " + index + " AND YEAR(Date)=" + yearcom.selectedValue + " group by Date, [First Name], Surname";
                    sda = new SqlDataAdapter(que, db);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        salesdgv.DataSource = dt;
                    }
                    dt.Dispose();
                    sda.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to load sales " + er);
            }
        }
        internal void Day()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string que = 
                                  " select 'Income' as Type, DATENAME(WEEKDAY, sa.Date) as [Day], SUM(sa.[Total Price]) as Total from Sales sa where "
                                + " MONTH(Date) = '" + index + "' and YEAR(Date) = '" + yearcom.selectedValue + "' group by DATENAME(WEEKDAY, sa.Date) union "
                                + " select 'Expense' as Type, DATENAME(WEEKDAY, Date) as [Day], SUM(Amount) as Total from Expense where "
                                + " MONTH(Date) = '" + index + "' and YEAR(Date) = '" + yearcom.selectedValue + "' group by DATENAME(WEEKDAY, Date)";
                    sda = new SqlDataAdapter(que, db);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Daydgv.DataSource = dt;
                    }
                    dt.Dispose();
                    sda.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as Days Performance were Loading " + er);
            }
        }
        internal void m_per_pro()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sel = "select top 10 pro.Name, sum(sa.Quantity) as Quantity, SUM(sa.[Total Price]) as [Total Income]  from Sales sa, Medicine pro where pro.Name=sa.Name and MONTH(sa.Date) = '" + index + "' and YEAR(sa.Date)='" + yearcom.selectedValue + "' group by pro.Name order by Quantity desc";
                    sda = new SqlDataAdapter(sel, db);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        mostdgv.DataSource = dt;
                    }
                    dt.Dispose();
                    sda.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to load Most Performance Medicine " + er);
            }
        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            Close();
        }
        void rece()
        {
            
            if (db.State == ConnectionState.Closed) 
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
                sdr.Close();
                db.Close(); 
            }
        }
        string zno;
        string show;
        void num()
        {
            
            if (db.State == ConnectionState.Closed) 
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
        }

        void act_Del()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string ins = "insert into Activity values(@fn, @sn, @rl, @dtl, @dt, @tm);";
                    string dtl = "Printed a Monthly Report.";
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
        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            try
            {
                rece();
                num();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt.Columns.Add("Name");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("Total");
                foreach (DataGridViewRow dr in mostdgv.Rows)
                {
                    dt.Rows.Add(dr.Cells[0].Value, dr.Cells[1].Value, dr.Cells[2].Value);
                }
                ds.Tables.Add(dt);
                ds.WriteXmlSchema("Month.xml");
                var rr = new Monthview();
                var re = new Month();
                re.SetDataSource(ds);
                TextObject ntxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["ntxt"];
                ntxt.Text = File.ReadLines("info.txt").First();
                TextObject pobtxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["pobtxt"];
                pobtxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(1);
                TextObject ctytxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["ctytxt"];
                ctytxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(2);
                TextObject ctttxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["ctttxt"];
                ctttxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(3);
                //TextObject rntxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["rntxt"];
                //rntxt.Text = show;
                TextObject monthtxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["monthtxt"];
                monthtxt.Text = DateTime.Now.ToString("MMMM");
                TextObject opbaltxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["opbaltxt"];
                opballbl.Text = opballbl.Text;
                //TextObject ttlinctxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["ttlinctxt"];
                //ttlinctxt.Text = ttlinclbl.Text;
                TextObject tlexptxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["tlexptxt"];
                tlexptxt.Text = ttlexplbl.Text;
                TextObject vattxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["vattxt"];
                vattxt.Text = ttlvatlbl.Text;
                TextObject baltxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["baltxt"];
                baltxt.Text = ttlbl.Text;
                TextObject ttletxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["ttletxt"];
                ttletxt.Text = "Month Report.";
                TextObject fntxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["fntxt"];
                fntxt.Text = Fname;
                TextObject sntxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["sntxt"];
                sntxt.Text = Sname;
                rr.crystalReportViewer1.ReportSource = re;
                rr.crystalReportViewer1.Refresh();
                dt.Dispose();
                re.Dispose();
                ds.Dispose();
                rr.Show();
                act_Del();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
        void Inco()
        {
            
        }

        int index;
        private void moncom_onItemSelected(object sender, EventArgs e)
        {
            if (moncom.selectedValue == "January")
            {
                index = 1;
            }
            else if(moncom.selectedValue== "February")
            {
                index = 2;
            }
            else if (moncom.selectedValue=="March")
            {
                index = 3;
            }
            else if (moncom.selectedValue == "April")
            {
                index = 4;
            }
            else if (moncom.selectedValue == "May")
            {
                index = 5;
            }
            else if (moncom.selectedValue == "June")
            {
                index = 6;
            }
            else if (moncom.selectedValue == "July")
            {
                index = 7;
            }
            else if (moncom.selectedValue == "August")
            {
                index = 8;
            }
            else if (moncom.selectedValue == "September")
            {
                index = 9;
            }
            else if (moncom.selectedValue == "October")
            {
                index = 10;
            }
            else if (moncom.selectedValue == "November")
            {
                index = 11;
            }
            else if (moncom.selectedValue == "December")
            {
                index = 12;
            }
        }

        private void moncom_Leave(object sender, EventArgs e)
        {
            Total_Vat();
        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            Total_Vat();
            Total_expe();
            Total_inco();
            expense();
            sales();
            Day();
            m_per_pro();
            Open_inc();
            Open_exp();
            float opbal = opinc - opexp;
            opballbl.Text = "K" + opbal.ToString();
            float balance = ttlinc - (ttlexp + ttlvat);
            ttlbl.Text = "K" + balance.ToString();
        }
    }
}
