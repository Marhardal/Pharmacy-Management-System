using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using System.Data.SqlClient;
using ZXing;
using AForge.Video.DirectShow;
using AForge.Video;
using CrystalDecisions.CrystalReports.Engine;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.VisualBasic;
using System.Configuration;

namespace Pharmancy_Management_System
{
    public partial class Staff : Form
    {
        public Staff()
        {
            InitializeComponent();
        }

        private void gunaControlBox1_Click(object sender, EventArgs e)
        {
            
            if (db.State == ConnectionState.Closed)
            {
                db.Open();
                string ins = "insert into Activity values(@fn, @sn, @rl, @dtl, @dt, @tm);";
                string dtl = "Logged out of the system.";
                string dt = DateTime.Now.ToShortDateString();
                string tm = DateTime.Now.ToShortTimeString();
                var exe = db.Execute(ins, new { fn = Fname, sn = Sname, rl = Role, dtl, dt, tm });
                db.Close();
                Application.Exit();
            }
        }

        SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
        SqlCommand com = new SqlCommand();
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        FilterInfoCollection filter;
        VideoCaptureDevice capture;

        void view()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string que = "Select * from Sales where [First Name] = @fn and Surname = @sn order by [Sales ID] desc";
                    com = new SqlCommand(que, db);
                    com.Parameters.Add("@fn", SqlDbType.VarChar).Value = Fname;
                    com.Parameters.Add("@sn", SqlDbType.VarChar).Value = Sname;
                    sda = new SqlDataAdapter(com);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0) 
                    {
                        viewdgv.DataSource = dt;
                    }
                    dt.Dispose();
                    sda.Dispose();
                    //var list = db.Query<Saleview>(que, new { Fname, Sname }).ToList();
                    //if (list.Count > 0)
                    //{
                    //    viewdgv.DataSource = list;
                    //}
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to Load Sales" + er);
            }
        }

        void expeview()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string que = "Select * from Expense Where [First Name] = @fn and Surname = @sn order by ID desc;";
                    com = new SqlCommand(que, db);
                    com.Parameters.Add("@fn", SqlDbType.VarChar).Value = Fname;
                    com.Parameters.Add("@sn", SqlDbType.VarChar).Value = Sname;
                    sda = new SqlDataAdapter(com);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        expensedgv.DataSource = dt;
                    }
                    dt.Dispose();
                    sda.Dispose();
                    //var lis = db.Query<Expenses>(que, new { Fname, Sname }).ToList();
                    //if (lis.Count > 0)
                    //{
                    //    expensedgv.DataSource = lis;
                    //}
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to load Expense " + er);
            }
        }

        public static string usnm;
        internal static string Fname;
        internal static string Sname;
        internal static string Role;
        internal void select()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "Select [First Name], Surname, Role from Users where Username = @usnm;";
                    com = new SqlCommand(sele, db);
                    com.Parameters.Add("@usnm", SqlDbType.VarChar).Value = usnm;
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        Fname = sdr[0].ToString();
                        Sname = sdr[1].ToString();
                        Role = sdr[2].ToString();
                    }
                    sdr.Close();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to load First Name and Surname " + er);
            }
        }
        private void Staff_Load(object sender, EventArgs e)
        {
            usnm = Login.usnm;
            select();
            today();
            week();
            monthly();
            yearly();
            recsales();
            recexpe();
            quatdel();
            ActiveControl = headtxt;
            headtxt.Text = File.ReadLines("info.txt").First();
            panel1.Size = new Size(213, 46);
            abouttxt.Text = "@ " + DateTime.Now.Year + " Dot Click IT Solutions ";
            expensebtn.Location = new Point(13, 62);
            Homebtn.Location = new Point(13, 122);
            gunaAdvenceButton1.Location = new Point(13, 182);
            gunaAdvenceButton6.Location = new Point(13, 242);
            Prescriptbtn.Location = new Point(13, 302);
            ActiveControl = dashlbl;
            filter = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filt in filter)
            {
                cam.Items.Add(filt.Name);
                cam.selectedIndex = 0;
            }
            capture = new VideoCaptureDevice(filter[cam.selectedIndex].MonikerString);
            capture.NewFrame += Capture_NewFrame;
            capture.Start();
            
        }

        internal void recexpe()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    string que = "select top 8 Name, Amount, Date from Expense where [First Name]='" + Fname + "' and Surname='" + Sname + "' order by ID desc;";
                    db.Open();
                    com = new SqlCommand(que, db);
                    sda = new SqlDataAdapter(com);
                    dt = new DataTable();
                    sda.Fill(dt);
                    expdgv.DataSource = dt;
                    sda.Dispose();
                    dt.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to Load recent expenses." + er);
            }
        }

        internal void recsales()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    string que = "select top 8 Date, Name, [Total Price], Quantity from Sales where [Total Price] > 0 and [First Name]='" + Fname + "' and Surname='" + Sname + "' order by [Sales ID] desc;";
                    db.Open();
                    com = new SqlCommand(que, db);
                    sda = new SqlDataAdapter(com);
                    dt = new DataTable();
                    sda.Fill(dt);
                    saldgv.DataSource = dt;
                    sda.Dispose();
                    dt.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to Load recent Sales." + er);
            }
        }

        internal void yearly()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select SUM([Total Price]) from Sales where YEAR(Date)=YEAR(CURRENT_TIMESTAMP) and [First Name]=@fn and Surname=@sn;";
                    com = new SqlCommand(sele, db);
                    com.Parameters.Add("@fn", SqlDbType.VarChar).Value = Fname;
                    com.Parameters.Add("@sn", SqlDbType.VarChar).Value = Sname;
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        if (sdr[0].ToString() != "")
                        {
                            yttxt.Text = "Total Sales Made this Years K" + sdr[0].ToString();
                        }
                        else
                        {
                            yttxt.Text = "No Sales Made this Years ";
                        }
                    }
                    sdr.Close();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to count this Years income." + er);
            }
        }

        internal void monthly()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select SUM([Total Price]) from Sales where MONTH(Date)=MONTH(CURRENT_TIMESTAMP) and YEAR(Date)=YEAR(CURRENT_TIMESTAMP) and [First Name]=@fn and Surname=@sn;";
                    com = new SqlCommand(sele, db);
                    com.Parameters.Add("@fn", SqlDbType.VarChar).Value = Fname;
                    com.Parameters.Add("@sn", SqlDbType.VarChar).Value = Sname;
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        if (sdr[0].ToString()!="Null")
                        {
                            mttxt.Text = "Total Sales Made this Month K" + sdr[0].ToString();
                        }
                        else
                        {
                            mttxt.Text = "No Sales Made this Month. ";
                        }
                    }
                    sdr.Close();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to count income made this month." + er);
            }
        }

        internal void week()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select SUM([Total Price]) from Sales where DATEPART(WEEK, Date)=DATEPART(WEEK, GETDATE()) and MONTH(Date)=MONTH(CURRENT_TIMESTAMP) and YEAR(Date) = YEAR(CURRENT_TIMESTAMP) and [First Name]=@fn and Surname=@sn ;";
                    com = new SqlCommand(sele, db);
                    com.Parameters.Add("@fn", SqlDbType.VarChar).Value = Fname;
                    com.Parameters.Add("@sn", SqlDbType.VarChar).Value = Sname;
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        if (sdr[0].ToString() != "Null") 
                        {
                            wktxt.Text = "Total Sales Made this Week K" + sdr[0].ToString();
                        }
                        else
                        {
                            wktxt.Text = "No sales made this week.";
                        }
                    }
                    sdr.Close();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to calculate todays income." + er);
            }
        }

        internal void today()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select SUM([Total Price]) from Sales where DAY(Date)=DAY(CURRENT_TIMESTAMP) and MONTH(Date)=MONTH(CURRENT_TIMESTAMP) and YEAR(Date)=YEAR(CURRENT_TIMESTAMP) and [First Name]=@fn and Surname=@sn ;";
                    com = new SqlCommand(sele, db);
                    com.Parameters.Add("@fn", SqlDbType.VarChar).Value = Fname;
                    com.Parameters.Add("@sn", SqlDbType.VarChar).Value = Sname;
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        if (sdr[0].ToString() != "Null")
                        {
                            ttltxt.Text = "K" + sdr[0].ToString();
                        }
                        else
                        {
                            ttltxt.Text = "No sales made this week.";
                        }
                    }
                    sdr.Close();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to calculate todays income." + er);
            }
        }
        private void Capture_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap map = (Bitmap)eventArgs.Frame.Clone();
            BarcodeReader reader = new BarcodeReader();
            var result = reader.Decode(map);
            if (result != null)
            {
                bctxt.Invoke(new MethodInvoker(delegate ()
                {
                    bctxt.Text = result.ToString();
                }));
            }
            scan.Image = map;
        }

        private void Salesbtn_Click(object sender, EventArgs e)
        {
            panel1.Size = new Size(213, 154);
            panel1.BackColor = Color.Gainsboro;
            mansep.Show();
            expensebtn.Location = new Point(13, 160);
            Homebtn.Location = new Point(13, 220);
            gunaAdvenceButton1.Location = new Point(13, 280);
            gunaAdvenceButton6.Location = new Point(13, 340);
            Prescriptbtn.Location = new Point(13, 400);
        }

        private void gunaTransfarantPictureBox6_Click(object sender, EventArgs e)
        {
            panel1.Size = new Size(213, 46);
            panel1.BackColor = Color.Transparent;
            mansep.Hide();
            expensebtn.Location = new Point(13, 62);
            Homebtn.Location = new Point(13, 122);
            gunaAdvenceButton1.Location = new Point(13, 182);
            gunaAdvenceButton6.Location = new Point(13, 242);
            Prescriptbtn.Location = new Point(13, 302);
        }

        private void expensebtn_Click(object sender, EventArgs e)
        {
            Page.SetPage("Expense");
            expeview();
            dashlbl.Text = "Expenses";
            gunaTransfarantPictureBox2.Image = Image.FromFile(@"Icons\icons8_exclamation_mark_32.png");
            gunaTransfarantPictureBox2.Size = new Size(45, 45);
        }

        private void Catbtn_Click(object sender, EventArgs e)
        {
            Page.SetPage("View");
            view();
            viewdgv.Update();
            viewdgv.Refresh();
            dashlbl.Text = "View all Sales Done By You.";
            gunaTransfarantPictureBox2.Image = Image.FromFile(@"Icons\icons8_view_32.png");
            gunaTransfarantPictureBox2.Size = new Size(45, 45);
        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            Page.SetPage("Create");
            dashlbl.Text = "Create a Sale";
            gunaTransfarantPictureBox2.Image = Image.FromFile(@"Icons\icons8_create_32.png");
            gunaTransfarantPictureBox2.Size = new Size(45, 45);
        }

        private void Usersbtn_Click(object sender, EventArgs e)
        {
            Page.SetPage("Update");
            dashlbl.Text = "Update A sale";
        }

        private void expseatxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select * from Sales where Name like '" + saleseatxt.Text + "%' or Cash like '" + saleseatxt.Text + "%' or Quantity like '" + saleseatxt.Text + "%' or Date like '" + saleseatxt.Text + "%' or Time like '" + saleseatxt.Text + "%'"
                    + " or Price like '" + saleseatxt.Text + "%' or[Sales ID] like '" + saleseatxt.Text + "%'; ";
                    com = new SqlCommand(sele, db);
                    sda = new SqlDataAdapter(com);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        viewdgv.DataSource = dt;
                    }
                    dt.Dispose();
                    sda.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as you were searching for a sale " + er);
            }
        }
        private void pntxt_Enter(object sender, EventArgs e)
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select Name from Medicine;";
                    com = new SqlCommand(sele, db);
                    sda = new SqlDataAdapter(com);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        AutoCompleteStringCollection comp = new AutoCompleteStringCollection();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            comp.Add(dt.Rows[i][0].ToString());
                        }
                        pntxt.AutoCompleteMode = AutoCompleteMode.Append;
                        pntxt.AutoCompleteCustomSource = comp;
                        pntxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        sda.Dispose();
                        dt.Dispose();
                        db.Close();
                    }
                        
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as You were searching." + er);
            }
        }

        private void bctxt_Enter(object sender, EventArgs e)
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select [Bar Code] from Medicine;";
                    com = new SqlCommand(sele, db);
                    sda = new SqlDataAdapter(com);
                    dt = new DataTable();
                    sda.Fill(dt);
                    AutoCompleteStringCollection comp = new AutoCompleteStringCollection();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        comp.Add(dt.Rows[i][0].ToString());
                    }
                    bctxt.AutoCompleteMode = AutoCompleteMode.Append;
                    bctxt.AutoCompleteCustomSource = comp;
                    bctxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    sda.Dispose();
                    dt.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as You were searching." + er);
            }
        }

        private void Staff_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (capture != null)
            {
                if (capture.IsRunning)
                {
                    capture.Stop();
                }
            }
        }

        private void pntxt_Leave(object sender, EventArgs e)
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select * from Medicine where [Name]='" + pntxt.Text + "';";
                    com = new SqlCommand(sele, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        pntxt.Text = sdr[1].ToString();
                        float price = Convert.ToSingle(sdr[3].ToString()) - Convert.ToSingle(sdr[4].ToString());
                        viewprod.Rows.Add(sdr[1].ToString(), "0", price.ToString(), sdr[4].ToString(), sdr[3].ToString());
                    }
                    sdr.Close();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as Medicine was auto filling." + er);
            }
            pntxt.Text = "";
            bctxt.Text = "";
        }

        string qt;
        string na;

        void Crystal()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("Medicine Name");
            dt.Columns.Add("Total Price");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Total VAT");
            foreach (DataGridViewRow dr in viewprod.Rows)
            {
                dt.Rows.Add(dr.Cells[0].Value, Convert.ToSingle(dr.Cells[4].Value), dr.Cells[1].Value ,Convert.ToSingle(dr.Cells[3].Value));
            }
            //ds.Tables.Add(dt);
            //ds.WriteXmlSchema("applicant.xml");
            //Receiptread rr = new Receiptread();
            //var re = new Receipt();
            //re.SetDataSource(ds);
            //float wt = Convert.ToSingle(Totallbl.Text) - Convert.ToSingle(ttlvatlbl.Text);
            //TextObject wtxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["wtxt"];
            //wtxt.Text = wt.ToString();
            //TextObject ntxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["ntxt"];
            //ntxt.Text = File.ReadLines("info.txt").First();
            //TextObject pobtxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["pobtxt"];
            //pobtxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(1);
            //TextObject ctytxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["ctytxt"];
            //ctytxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(2);
            //TextObject ttletxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["ttletxt"];
            //ttletxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(3);
            //TextObject rntxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["rntxt"];
            //rntxt.Text = show;
            //TextObject znotxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["znotxt"];
            //znotxt.Text = zno;
            //TextObject txt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["txt"];
            //txt.Text = ttlvatlbl.Text;
            //TextObject tin = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["tin"];
            //string four = File.ReadLines("info.txt").ElementAtOrDefault(4);
            //tin.Text = four;
            //TextObject EJSN = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["ejsn"];
            //string five = File.ReadLines("info.txt").ElementAtOrDefault(5);
            //EJSN.Text = five;
            //TextObject EJACT = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["EJACT"];
            //string six = File.ReadLines("info.txt").ElementAtOrDefault(6);
            //EJACT.Text = six;
            //string sev = File.ReadLines("info.txt").ElementAtOrDefault(7);
            //string eig = File.ReadLines("info.txt").ElementAtOrDefault(6);
            //TextObject vin = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["vin"];
            //vin.Text = "VIN: "+ sev + "/" + eig + "/" + five + "/" + four;
            //TextObject ttltax = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["ttltax"];
            //ttltax.Text = ttlvatlbl.Text;
            //TextObject ttl = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["ttl"];
            //ttl.Text = Totallbl.Text;
            //TextObject ctxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["ctxt"];
            //ctxt.Text = prtxt.Text;
            //TextObject chtxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["chtxt"];
            //chtxt.Text = cctxt.Text;
            //TextObject fntxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["fntxt"];
            //fntxt.Text = Fname;
            //TextObject sntxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["sntxt"];
            //sntxt.Text = Sname;
            //rr.crystalReportViewer1.ReportSource = re;
            //rr.crystalReportViewer1.Refresh();
            //dt.Dispose();
            //ds.Dispose();
            //rr.Show();
        }
        float cou;
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
                show = "00" + left + "/00" + right + "0" + DateTime.Now.ToString("yyyy");
                zno = "00" + sdr[2].ToString();
                sdr.Close();
            }
            db.Close();
        }
        private void Add_Click(object sender, EventArgs e)
        {
            if (prtxt.Text != "" || cctxt.Text != "")
            {
                
                try
                {
                    if (db.State == ConnectionState.Closed)
                    {
                        rece();
                        num();
                        string rc = show;
                        for (int i = 0; i < viewprod.Rows.Count; i++)
                        {
                            string ins = "Insert into Sales Values(@rc, @na, @qt, @dt, @tm, @fn, @sn, @tt, @vat, @tp);";
                            db.Open();
                            na = viewprod.Rows[i].Cells[0].Value.ToString();
                            string pr = viewprod.Rows[i].Cells[1].Value.ToString();
                            cou = viewprod.RowCount;
                            qt = viewprod.Rows[i].Cells[1].Value.ToString();
                            string dt = DateTime.Now.ToString("yyyy-MM-dd");
                            string tm = DateTime.Now.ToString("HH:mm:ss");
                            string tt = viewprod.Rows[i].Cells[2].Value.ToString();
                            string vat = viewprod.Rows[i].Cells[3].Value.ToString();
                            string tp = viewprod.Rows[i].Cells[4].Value.ToString();
                            string fn = Fname;
                            string sn = Sname;
                            var que = db.Execute(ins, new {rc, na, qt, dt, tm, fn, sn, tt, vat, tp });
                            db.Close();
                            upMedicine ();
                        }
                        insact();
                        Crystal();
                        viewprod.Rows.Clear();
                        Totallbl.Text = "";
                        ActiveControl = bctxt;
                        bctxt.Text = "";
                        bctxt.HintText = "";
                        prtxt.Text = "";
                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show("An error occured as Medicine was about to be sold " + er);
                }
            }
            else
            {
                MessageBox.Show("Please fill in fields.");
            }
        }

        void upMedicine ()
        {
            
            //MessageBox.Show(qt);
            db.Open();
            float diff;
            string sele = "select Quantity from Medicine where Name='" + na + "';";
            com = new SqlCommand(sele, db);
            SqlDataReader sdr = com.ExecuteReader();
            if (sdr.HasRows)
            {
                sdr.Read();
                float qua = Convert.ToSingle(sdr[0].ToString());
                //MessageBox.Show(qua.ToString());
                diff = qua - Convert.ToSingle(qt);
                sdr.Close();
                string que = "update Medicine set Quantity='" + diff + "' where Name='" + na + "';";
                com = new SqlCommand(que, db);
                com.ExecuteNonQuery();
            }
            db.Close();
        }


        void upsale()
        {
            
            db.Open();
            string sele = "select MAX([Sales ID]) from Sales;";
            com = new SqlCommand(sele, db);
            SqlDataReader sdr = com.ExecuteReader();
            if (sdr.HasRows)
            {
                sdr.Read();
                string id = sdr[0].ToString();
                sdr.Close();
                string up = "update Sales set VAT=@ch, [Total Price]=@tp where [Sales ID]=@id;";
                string ch = ttlvatlbl.Text;
                string tp = Totallbl.Text;
                var exe = db.Execute(up, new { ch, tp, id });
            }
            db.Close();
        }

        void insact()
        {
            db.Open();
            string ins = "insert into Activity values(@fn, @sn, @rl, @dtl, @dt, @tm);";
            string dtl = "Made a Sale.";
            string dt = DateTime.Now.ToShortDateString();
            string tm = DateTime.Now.ToShortTimeString();
            var exe = db.Execute(ins, new { fn = Fname, sn = Sname, rl = Role, dtl, dt, tm });
            db.Close();
        }

        void insgra()
        {
            
            db.Open();
            string que = "insert into Graph values('Income', '" + Totallbl.Text + "', @dt)";
            com = new SqlCommand(que, db);
            com.Parameters.Add("@dt", SqlDbType.Date).Value = DateTime.Now.ToString("dd MM yyyy");
            com.ExecuteNonQuery();
            db.Close();
        }
        
        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {
            string mes = Interaction.InputBox("Enter password to verify account", "Verify", "Enter Password", 500, 300);
            if (mes != "")
            {
                try
                {
                    
                    if (db.State == ConnectionState.Closed)
                    {
                        db.Open();
                        string que = "Select * from Users where Password = '" + mes + "' and Username='" + usnm + "'";
                        com = new SqlCommand(que, db);
                        SqlDataReader sdr = com.ExecuteReader();
                        if (sdr.HasRows)
                        {
                            sdr.Read();
                            if (viewprod.SelectedRows.Count > 0)
                            {
                                viewprod.Rows.RemoveAt(viewprod.SelectedRows[0].Index);
                            }
                            else
                            {
                                MessageBox.Show("Please select a row to continue.");
                            }
                            float sum = 0;
                            float vat = 0;
                            for (int i = 0; i < viewprod.Rows.Count; i++)
                            {
                                sum += Convert.ToSingle(viewprod.Rows[i].Cells[4].Value.ToString());
                                vat += Convert.ToSingle(viewprod.Rows[i].Cells[3].Value.ToString());
                            }
                            ttlvatlbl.Text = vat.ToString();
                            Totallbl.Text = sum.ToString();
                        }
                        else
                        {
                            MessageBox.Show("Password didn't match any details.");
                        }
                        sdr.Close();
                        db.Close();
                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message);
                }
            }
        }

        private void ccatxt_Leave(object sender, EventArgs e)
        {
            try
            {
                float ttl = Convert.ToSingle(Totallbl.Text);
                float cca = Convert.ToSingle(prtxt.Text);
                float change = cca - ttl;
                if (cca < ttl)
                {
                    MessageBox.Show("Customer Paid Less than the required Price.");
                }
                else
                {
                    cctxt.Text = change.ToString();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Please enter a number " + er);
            }
        }

        private void bunifuDropdown1_onItemSelected(object sender, EventArgs e)
        {
            if (bunifuDropdown1.selectedValue == "Logout")
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to Logout.", "System Logging out", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    
                    if (db.State == ConnectionState.Closed)
                    {
                        db.Open();
                        string ins = "insert into Activity values(@fn, @sn, @rl, @dtl, @dt, @tm);";
                        string dtl = "Logged out of the system.";
                        string dt = DateTime.Now.ToShortDateString();
                        string tm = DateTime.Now.ToShortTimeString();
                        var exe = db.Execute(ins, new { fn = Fname, sn = Sname, rl = Role, dtl, dt, tm });
                        Hide();
                        db.Close();
                        Application.Restart();
                    }
                }
            }
            else if (bunifuDropdown1.selectedValue == "Profile") 
            {
                try
                {
                    
                    if (db.State == ConnectionState.Closed) 
                    {
                        db.Open();
                        string sele = "Select * from Users Where Username = @usn";
                        com = new SqlCommand(sele, db);
                        com.Parameters.Add("@usn", SqlDbType.VarChar).Value = usnm;
                        SqlDataReader sdr = com.ExecuteReader();
                        if (sdr.HasRows)
                        {
                            sdr.Read();
                            Registration reg = new Registration();
                            reg.fntxt.Text = sdr[1].ToString();
                            reg.sntxt.Text = sdr[2].ToString();
                            reg.usntxt.Text = sdr[3].ToString();
                            reg.emltxt.Text = sdr[4].ToString();
                            reg.pntxt.Text = sdr[5].ToString();
                            reg.cattxt.SelectedItem(sdr[6].ToString());
                            reg.bunifuDropdown1.SelectedItem(sdr[7].ToString());
                            reg.pwtxt.Text = sdr[8].ToString();
                            reg.cpwtxt.Text = sdr[8].ToString();
                            reg.cattxt.Enabled = false;
                            reg.label1.Text = sdr[0].ToString();
                            reg.bunifuDropdown1.Enabled = false;
                            reg.fnlbl.Text = Fname;
                            reg.snlbl.Text = Sname;
                            reg.rllbl.Text = Role;
                            reg.upusrbtn.Show();
                            reg.Show();
                            Hide();
                        }
                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show("An error happen as you want to open your profile details." + er);
                }
            }
        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            Page.SetPage("Home");
            today();
            week();
            monthly();
            yearly();
            recexpe();
            recsales();
            saldgv.Update();
            saldgv.Refresh();
            expedgv.Update();
            expedgv.Refresh();
            gunaTransfarantPictureBox2.Image = Image.FromFile(@"Icons\icons8_home_32.png");
            gunaTransfarantPictureBox2.Size = new Size(45, 45);
        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            Expense expe = new Expense();
            expe.payaddbtn.Show();
            expe.Fname = Fname;
            expe.Sname = Sname;
            expe.Role = Role;
            expe.Page.SetPage("Expense");
            expe.bunifuLabel1.Text = "Insert Expense";
            expe.Show();
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            Expense expe = new Expense();
            expe.expnmtxt.Text = expensedgv.CurrentRow.Cells[1].Value.ToString();
            expe.expamotxt.Text = expensedgv.CurrentRow.Cells[2].Value.ToString();
            expe.bunifuLabel4.Text = expensedgv.CurrentRow.Cells[0].Value.ToString();
            expe.payupbtn.Show();
            expe.Fname = Fname;
            expe.Sname = Sname;
            expe.Role = Role;
            expe.bunifuLabel1.Text = "Update Expense";
            expe.Show();
        }

        private void expedgv_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "Select * from Expense where ID like '" +expedgv.Text + "%' or Name like '" + expedgv.Text + "%' or Amount like '" + expedgv.Text + "%'";
                    com = new SqlCommand(sele, db);
                    sda = new SqlDataAdapter(com);
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
                MessageBox.Show("An error occured as you were searching for an expense " + er);
            }
        }

        private void pntxt_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void viewprod_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bctxt_Leave(object sender, EventArgs e)
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select * from Medicine where [Bar Code]='" + bctxt.Text + "';";
                    com = new SqlCommand(sele, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        pntxt.Text = sdr[1].ToString();
                        float price = Convert.ToSingle(sdr[3].ToString()) - Convert.ToSingle(sdr[4].ToString());
                        viewprod.Rows.Add(sdr[1].ToString(), "0", price.ToString(), sdr[4].ToString(), sdr[3].ToString());
                    }
                    sdr.Close();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as Medicine was auto filling." + er);
            }
            pntxt.Text = "";
            bctxt.Text = "";
        }

        private void addsalbtn_Click(object sender, EventArgs e)
        {

        }

        private void viewprod_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select * from Medicine where Name='" + viewprod.CurrentRow.Cells[0].Value.ToString() + "';";
                    com = new SqlCommand(sele, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        float pr = Convert.ToSingle(sdr[3].ToString());
                        float qt = Convert.ToSingle(viewprod.CurrentRow.Cells[1].Value.ToString());
                        float vt = Convert.ToSingle(sdr[4].ToString());
                        float ans = pr * qt;
                        float vat = vt * qt;
                        string add = ans.ToString();
                        viewprod.CurrentRow.Cells[3].Value = vat.ToString();
                        viewprod.CurrentRow.Cells[4].Value = ans.ToString();
                        viewprod.CurrentRow.Cells[2].Value = (ans - vat).ToString();
                        float sum = 0;
                        float va = 0;
                        for (int i = 0; i < viewprod.Rows.Count; i++)
                        {
                            sum += Convert.ToSingle(viewprod.Rows[i].Cells[4].Value.ToString());
                            va += Convert.ToSingle(viewprod.Rows[i].Cells[3].Value.ToString());
                        }
                        Totallbl.Text = sum.ToString();
                        ttlvatlbl.Text = va.ToString();
                        sdr.Close();
                        db.Close();
                    }
                }
            }
            finally
            {

            }
        }

        private void bctxt_Load(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton1_Click_1(object sender, EventArgs e)
        {
            
            Page.SetPage("Products");
            Medicine();
            dashlbl.Text = "Medicine";
            gunaTransfarantPictureBox2.Image = Image.FromFile(@"Icons\icons8_pill_32.png");
            gunaTransfarantPictureBox2.Size = new Size(42, 42);
        }

        internal void Medicine()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    string sele = "Select * from Medicine order by [Medicine ID] desc";
                    db.Open();
                    com = new SqlCommand(sele, db);
                    sda = new SqlDataAdapter(com);
                    dt = new DataTable();
                    sda.Fill(dt);
                    prodgv.DataSource = dt;
                    sda.Dispose();
                    dt.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as the Medicine were about to load " + er);
            }
        }

        private void gunaAdvenceButton6_Click(object sender, EventArgs e)
        {
            Page.SetPage("Quotation");
            Quat();
            dashlbl.Text = "Quotation";
            gunaTransfarantPictureBox2.Image = Image.FromFile(@"Icons\icons8_categorize_32.png");
            gunaTransfarantPictureBox2.Size = new Size(42, 42);

        }

        void Quat()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string que = "select * from Quantation where [Username]=@usn order by ID desc";
                    com = new SqlCommand(que, db);
                    com.Parameters.Add("@usn", SqlDbType.VarChar).Value = usnm;
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

        private void gunaAdvenceButton8_Click(object sender, EventArgs e)
        {
            var prod = new AddProd();
            prod.addpro.Show();
            prod.Fname = Fname;
            prod.Sname = Sname;
            prod.Role = Role;
            prod.mfdate.Value = DateTime.Now;
            prod.expdate.Value = DateTime.Now.AddMonths(2);
            prod.Show();
        }

        private void gunaAdvenceButton7_Click(object sender, EventArgs e)
        {
            AddProd pro = new AddProd();
            pro.prod();
            pro.natxt.Text = prodgv.CurrentRow.Cells[1].Value.ToString();
            pro.cattxt.Text = prodgv.CurrentRow.Cells[6].Value.ToString();
            pro.pptxt.Text = prodgv.CurrentRow.Cells[3].Value.ToString();
            //pro.qttxt.SelectedItem(prodgv.CurrentRow.Cells[2].Value.ToString());
            pro.uqtxt.Text = prodgv.CurrentRow.Cells[7].Value.ToString();
            pro.rltxt.Text = prodgv.CurrentRow.Cells[8].Value.ToString();
            pro.vatxt.SelectedItem(prodgv.CurrentRow.Cells[9].Value.ToString());
            pro.bctxt.Text = prodgv.CurrentRow.Cells[10].Value.ToString();
            pro.Fname = Fname;
            pro.Sname = Sname;
            pro.Role = Role;
            pro.mfdate.Value = Convert.ToDateTime(prodgv.CurrentRow.Cells[11].Value.ToString());
            pro.expdate.Value = Convert.ToDateTime(prodgv.CurrentRow.Cells[12].Value.ToString());
            pro.label1.Text = prodgv.CurrentRow.Cells[0].Value.ToString();
            if (db.State==ConnectionState.Closed)
            {
                db.Open();
                string que = "select Category from Medicine where [Medicine ID]='" + prodgv.CurrentRow.Cells[0].Value.ToString() + "';";
                com = new SqlCommand(que, db);
                SqlDataReader sdr = com.ExecuteReader();
                if (sdr.HasRows)
                {
                    sdr.Read();
                    pro.qttxt.SelectedItem(sdr[0].ToString());
                }
                sdr.Close();
                db.Close();
            }
            pro.uppro.Show();
            pro.Show();
        }

        private void proseatxt_Enter(object sender, EventArgs e)
        {
            if (proseatxt.Text.Trim() == "Search for a Medicine.") 
            {
                proseatxt.Text = "";
                proseatxt.ForeColor = Color.Black;
            }
            else
            {
                Medicine ();
            }
        }

        private void proseatxt_Leave(object sender, EventArgs e)
        {
            if (proseatxt.Text.Trim().Equals("Search for a Medicine.") || proseatxt.Text == "") 
            {
                proseatxt.Text = "Search for a Medicine. ";
                proseatxt.ForeColor = Color.FromArgb(64, 64, 64);
                Medicine ();
            }
            else
            {
                Medicine ();
            }
        }

        private void proseatxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                string sele = "SELECT * FROM Medicine WHERE Name like '" + proseatxt.Text + "%' or Category like '" + proseatxt.Text + "%' or "
                    + "Price like '" + proseatxt.Text + "%' or Quantity like '" + proseatxt.Text + "%' or [Unit Quantity] like '" + proseatxt.Text + "%' or "
                    + "[Reorder Level] like '" + proseatxt.Text + "%' or VAT like '" + proseatxt.Text + "%' or [Bar Code] like '" + proseatxt.Text + "%' or [Medicine  ID] like '" + proseatxt.Text + "%'; ";
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    dt = new DataTable();
                    com = new SqlCommand(sele, db);
                    sda = new SqlDataAdapter(com);
                    sda.Fill(dt);
                    prodgv.DataSource = dt;
                    dt.Dispose();
                    sda.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to serch for the Medicine ." + er);
            }
        }

        private void quaseatxt_Enter(object sender, EventArgs e)
        {
            if (quaseatxt.Text.Trim() == "Search for a Quatation Number.")
            {
                gunaAdvenceButton11.Enabled = true;
                quaseatxt.Text = "";
                quaseatxt.ForeColor = Color.Black;
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
                        quaseatxt.AutoCompleteMode = AutoCompleteMode.Append;
                        quaseatxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        quaseatxt.AutoCompleteCustomSource = comp;
                        sda.Dispose();
                        db.Close();
                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show("An error occured as Medicine were loading into this textbox " + er);
                }
            }
            else
            {
                Quat();
            }
        }

        private void quaseatxt_Leave(object sender, EventArgs e)
        {
            if (quaseatxt.Text.Trim().Equals("Search for a Quatation Number.") || quaseatxt.Text == "")
            {
                quaseatxt.Text = "Search for a Quatation Number. ";
                quaseatxt.ForeColor = Color.FromArgb(64, 64, 64);
                gunaAdvenceButton11.Enabled = false;
                Quat();
            }
            else
            {
                Quat();
            }
        }

        private void quaseatxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select * from Quantation where [Quatation No] like '" + quaseatxt.Text + "%';";
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

        private void gunaAdvenceButton9_Click(object sender, EventArgs e)
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

        private void gunaAdvenceButton10_Click(object sender, EventArgs e)
        {
            Quatation qua = new Quatation();
            qua.Fname = Fname;
            qua.Sname = Sname;
            qua.Role = Role;
            qua.Usnm = usnm;
            qua.addsalbtn.Show();
            qua.Show();
        }

        private void Home_Click(object sender, EventArgs e)
        {

        }

        private void saleseatxt_Enter(object sender, EventArgs e)
        {
            if (saleseatxt.Text.Trim() == "Search for a Sale.")
            {
                saleseatxt.Text = "";
                saleseatxt.ForeColor = Color.Black;
            }
            else
            {
                view();
            }
        }

        private void saleseatxt_Leave(object sender, EventArgs e)
        {
            if (saleseatxt.Text.Trim().Equals("Search for a Sale.") || saleseatxt.Text == "")
            {
                saleseatxt.Text = "Search for a Sale.";
                saleseatxt.ForeColor = Color.FromArgb(64, 64, 64);
                view();
            }
            else
            {
                view();
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void expedgv_Enter(object sender, EventArgs e)
        {
            if (expedgv.Text.Trim() == "Search for an Expense.") 
            {
                expedgv.Text = "";
                expedgv.ForeColor = Color.Black;
            }
            else
            {
                expeview();
            }
        }

        private void expedgv_Leave(object sender, EventArgs e)
        {
            if (expedgv.Text.Trim() == "Search for an Expense." || expedgv.Text == "") 
            {
                expedgv.Text = "Search for an Expense.";
                expedgv.ForeColor = Color.FromArgb(64, 64, 64);
                expeview();
            }
            else
            {
                expeview();
            }
        }

        private void gunaAdvenceButton11_Click(object sender, EventArgs e)
        {
            Page.SetPage("Quat");
            selequat();
            othdet();
        }

        void quatdel()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed) 
                {
                    db.Open();
                    string sele = "select Until from Quantation where DAY(Until)=DAY(CURRENT_TIMESTAMP) group by Until;";
                    com = new SqlCommand(sele, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        string until = sdr[0].ToString();
                        sdr.Close();
                        string del = "Delete From Quantation where DAY(Until)=DATEPART(DAY, '" + until + "');";
                        com = new SqlCommand(del, db);
                        com.ExecuteNonQuery();
                    }
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
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
                    com.Parameters.Add(new SqlParameter("@qt", quaseatxt.Text));
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        quatlbl.Text = sdr[3].ToString();
                        dtllbl.Text = sdr[2].ToString();
                        cntxt.Text = sdr[0].ToString() +"  " + sdr[1].ToString();
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
                
                if (db.State==ConnectionState.Closed)
                {
                    db.Open();
                    string qua = "select Name, Quantity, Price, [Total VAT], [Total Price] from Quantation where [Quatation No]=@qt;";
                    com = new SqlCommand(qua, db);
                    com.Parameters.Add(new SqlParameter("@qt", quaseatxt.Text));
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

        private void Prescriptbtn_Click(object sender, EventArgs e)
        {
            Page.SetPage("Prescription");
            dashlbl.Text = "Prescriptions";
            gunaTransfarantPictureBox2.Image = Image.FromFile(@"Icons\icons8_pharmacy_32.png");
            gunaTransfarantPictureBox2.Size = new Size(42, 42);
            priscript();
        }

        void priscript()
        {
            try
            {
                
                if (db.State==ConnectionState.Closed)
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
                    string sele = " Select * from Prescription where Category like '"+ preseatxt.Text +"%' or Medicine like '"+ preseatxt.Text +"%';";
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
                proseatxt.Text = "Search for a Category or Medicine.";
                proseatxt.ForeColor = Color.FromArgb(64, 64, 64);
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

        private void gunaTransfarantPictureBox3_Click(object sender, EventArgs e)
        {
            Prescript pre = new Prescript();
            pre.Fname = Fname;
            pre.Sname = Sname;
            pre.Role = Role;
            pre.addpro.Show();
            pre.Show();
        }
    }
}
