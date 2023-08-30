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
using AForge.Video.DirectShow;
using AForge.Video;
using ZXing;
using Dapper;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using Microsoft.VisualBasic;
using System.Configuration;

namespace Pharmancy_Management_System
{
    public partial class Sale : Form
    {
        public Sale()
        {
            InitializeComponent();
        }
        FilterInfoCollection filter;
        VideoCaptureDevice capture;
        SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
        SqlCommand com = new SqlCommand();

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
                dt.Rows.Add(dr.Cells[0].Value, Convert.ToSingle(dr.Cells[4].Value), dr.Cells[1].Value, Convert.ToSingle(dr.Cells[3].Value));
            }
            //ds.Tables.Add(dt);
            //ds.WriteXmlSchema("applicant.xml");
            //Receiptread rr = new Receiptread();
            //var re = new Receipt();
            //re.SetDataSource(ds);
            //float wt = Convert.ToSingle(totallbl.Text) - Convert.ToSingle(ttlvatlbl.Text);
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
            //vin.Text = "VIN: " + sev + "/" + eig + "/" + five + "/" + four;
            //TextObject ttltax = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["ttltax"];
            //ttltax.Text = ttlvatlbl.Text;
            //TextObject ttl = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["ttl"];
            //ttl.Text = totallbl.Text;
            //TextObject ctxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["ctxt"];
            //ctxt.Text = prtxt.Text;
            //TextObject chtxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["chtxt"];
            //chtxt.Text = chg;
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
            }
        }
        string zno;
        string show;
        void num()
        {
            if (db.State==ConnectionState.Closed)
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
                    string two = USNM.Substring(0, 2).ToUpper();
                    //show = two + DateTime.Now.ToString("dd") + DateTime.Now.ToString("MM") + left + "00" + right + DateTime.Now.Year.ToString();
                    show = "00" + left + "/00" + right + "0" + DateTime.Now.Month + "0" + DateTime.Now.ToString("yyyy");
                    zno = "00" + sdr[2].ToString();
                    sdr.Close();
                }
                db.Close(); 
            }
        }
        float cou;
        private void addsalbtn_Click(object sender, EventArgs e)
        {
            if (prtxt.Text != "")
            {
                try
                {
                    rece();
                    num();
                    if (db.State == ConnectionState.Closed)
                    {
                        if (Convert.ToSingle(prtxt.Text) >= Convert.ToSingle(totallbl.Text))
                        {
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
                                var que = db.Execute(ins, new { rc, na, qt, dt, tm, fn, sn, tt, vat, tp });
                                db.Close();
                                upproduct();
                            }
                            insact();
                            Crystal();
                            viewprod.Rows.Clear();
                            totallbl.Text = "";
                            ActiveControl = bctxt;
                            bctxt.Text = "";
                            bctxt.HintText = "";
                            prtxt.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Customer offered less price than the total price required.", "Info");
                        }
                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show("An error occured as Products was about to be sold " + er);
                }
            }
            else
            {
                MessageBox.Show("Please fill in fields.");
            }

        }
        void upsale()
        {
            ;
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
                string tp = totallbl.Text;
                var exe = db.Execute(up, new { ch, tp, id });
            }
            db.Close();
        }
        

        void upproduct()
        {
            ;
            db.Open();
            float diff;
            string sele = "select Quantity from Medicine where Name='" + na + "';";
            com = new SqlCommand(sele, db);
            SqlDataReader sdr = com.ExecuteReader();
            if (sdr.HasRows)
            {
                sdr.Read();
                float qua = Convert.ToSingle(sdr[0].ToString());
                diff = qua - Convert.ToSingle(qt);
                sdr.Close();
                string que = "update Medicine set Quantity='" + diff + "' where Name='" + na + "';";
                com = new SqlCommand(que, db);
                com.ExecuteNonQuery();
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

        private void pntxt_Enter(object sender, EventArgs e)
        {
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "Select Name from Medicine";
                    com = new SqlCommand(sele, db);
                    SqlDataAdapter sda = new SqlDataAdapter(com);
                    AutoCompleteStringCollection comp = new AutoCompleteStringCollection();
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        comp.Add(dt.Rows[i][0].ToString());
                    }
                    pntxt.AutoCompleteMode = AutoCompleteMode.Append;
                    pntxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    pntxt.AutoCompleteCustomSource = comp;
                    sda.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as products were loading into this textbox " + er);
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
                MessageBox.Show("An error occured as products was auto filling." + er);
            }
            pntxt.Text = "";
            bctxt.Text = "";
        }

        string chg;

        private void prtxt_Leave(object sender, EventArgs e)
        {
            try
            {
                var fig = Convert.ToSingle(totallbl.Text);
                var cus = Convert.ToSingle(prtxt.Text);
                var change = cus - fig;
                chg = Convert.ToString(change);
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to get the change " + er);
            }
        }
        

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (db.State == ConnectionState.Closed) 
                {
                    db.Open();
                    string up = " update Sales set Name=@nm, Quantity=@qt, Date=@dt, Time=@tm, Price=@pr, VAT=@vat, [Total Price]=@tp where [Receipt No]=@rn;";
                    com = new SqlCommand(up, db);
                    com.Parameters.Add("@na", SqlDbType.VarChar).Value = pntxt.Text;
                    com.Parameters.Add("@cs", SqlDbType.VarChar).Value = prtxt.Text;
                    //com.Parameters.Add("@qt", SqlDbType.VarChar).Value = qtttxt.Text;
                    com.Parameters.Add("@dt", SqlDbType.Date).Value = DateTime.Now.ToString("dd MM yyyy");
                    com.Parameters.Add("@tm", SqlDbType.VarChar).Value = DateTime.Now.ToString("HH:mm:ss");
                    //com.Parameters.Add("@fn", SqlDbType.VarChar).Value = fame;
                    //com.Parameters.Add("@sn", SqlDbType.VarChar).Value = same;
                    //com.Parameters.Add("@tt", SqlDbType.VarChar).Value = ptxt.Text;
                    //com.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    com.ExecuteNonQuery();
                    db.Close();
                    upgraph();
                    Hide();
                    MessageBox.Show("Thank you for doing business with us.");
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as you were trying to update Sale" + er);
            }
        }

        void upgraph()
        {
            //db.Open();
            //string update = "update Graph set Amount='" + ptxt.Text + "' where ID='" + id + "';";
            //com = new SqlCommand(update, db);
            //com.ExecuteNonQuery();
            //db.Close();
        }

        internal void select()
        {
            try
            {
                //////
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "Select [First Name], Surname, Role, [Phone Number] from Users where Username = @usnm;";
                    com = new SqlCommand(sele, db);
                    com.Parameters.Add(new SqlParameter("@usnm", USNM));
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
        internal static string Fname;
        internal static string Sname;
        internal static string Role;
        internal static string USNM;
        private void Sale_Load(object sender, EventArgs e)
        {
            addsalbtn.Show();
            USNM = Login.usnm;
            select();
            filter = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filt in filter)
            {
                cam.Items.Add(filt.Name);
                cam.selectedIndex = 0;
            }
            capture = new VideoCaptureDevice(filter[cam.selectedIndex].MonikerString);
            capture.NewFrame += Capture_NewFrame;
            capture.Start();
            gunaAdvenceButton6.BringToFront();
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

        private void Sale_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (capture != null)
            {
                if (capture.IsRunning)
                {
                    capture.Stop();
                }
            }
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
                MessageBox.Show("An error occured as products was auto filling." + er);
            }
            pntxt.Text = "";
            bctxt.Text = "";
        }

        private void gunaControlBox1_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            Hide();
        }

        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {
            if (viewprod.Rows.Count > 0 )
            {
                string mes = Interaction.InputBox("Enter password to verify account", "Verify", "Enter Password", 500, 300);
                if (mes != "")
                {
                    try
                    {
                        if (db.State == ConnectionState.Closed)
                        {
                            db.Open();
                            string que = "Select * from Users where Password = '" + mes + "' and Username='" + USNM + "'";
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
                                float sub = 0;
                                for (int i = 0; i < viewprod.Rows.Count; i++)
                                {
                                    sum += Convert.ToSingle(viewprod.Rows[i].Cells[4].Value.ToString());
                                    vat += Convert.ToSingle(viewprod.Rows[i].Cells[3].Value.ToString());
                                    sub += Convert.ToSingle(viewprod.Rows[i].Cells[2].Value.ToString());
                                }
                                ttlvatlbl.Text = vat.ToString();
                                totallbl.Text = sum.ToString();
                                sbttllbl.Text = sub.ToString();
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
            else
            {
                MessageBox.Show("Please select a product before you proceed.");
            }
        }

        private void viewprod_SelectionChanged(object sender, EventArgs e)
        {
            //viewprod.Rows[viewprod.Rows.Count - 1].Selected = true;
        }

        private void viewprod_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //viewprod.Rows[e.RowIndex].Selected = true;
        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            string mes = Interaction.InputBox("Enter password to verify account", "Verify", "Enter Password", 500, 300);
            if (mes != "")
            {
                try
                {
                    if (viewprod.Rows.Count > 0)
                    {
                        if (db.State == ConnectionState.Closed)
                        {
                            db.Open();
                            string que = "Select * from Users where Password = '" + mes + "' and Username='" + USNM + "'";
                            com = new SqlCommand(que, db);
                            SqlDataReader sdr = com.ExecuteReader();
                            if (sdr.HasRows)
                            {
                                sdr.Read();
                                viewprod.Rows.Clear();
                                ttlvatlbl.Text = "0.00";
                                totallbl.Text = "0.00";
                                sbttllbl.Text = "0.00";
                            }
                            else
                            {
                                MessageBox.Show("Password didn't match any details.");
                            }
                            sdr.Close();
                            db.Close();
                }
                    }
                    else
                    {
                        MessageBox.Show("Please make sure the grid contains data.");
                    }
                    }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message);
                }
            }
        }

        private void gunaAdvenceButton6_Click(object sender, EventArgs e)
        {
            Page.SetPage("Create");
            gunaAdvenceButton7.BringToFront();
        }

        private void gunaAdvenceButton7_Click(object sender, EventArgs e)
        {
            Page.SetPage("View");
            gunaAdvenceButton6.BringToFront();
            Sales();
            coucus();
        }
        internal void coucus()
        {
            try
            {
                
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "Select count([Sales ID]) as Today from Sales where DAY(Date)=DAY(CURRENT_TIMESTAMP)";
                    com = new SqlCommand(sele, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        Counttxt.Text = "Sales made today " + sdr[0].ToString();
                    }
                    sdr.Close();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as was count todays sales " + er);
            }
        }

        internal void Sales()
        {
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "Select * from Sales order by [Sales ID] desc;";
                    com = new SqlCommand(sele, db);
                    var sda = new SqlDataAdapter(com);
                    var dt = new DataTable();
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
                MessageBox.Show("Failed to load Sales " + er);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
          
        }

        private void bctxt_Enter(object sender, EventArgs e)
        {
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "Select [Bar Code] from Medicine";
                    com = new SqlCommand(sele, db);
                    SqlDataAdapter sda = new SqlDataAdapter(com);
                    AutoCompleteStringCollection comp = new AutoCompleteStringCollection();
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        comp.Add(dt.Rows[i][0].ToString());
                    }
                    bctxt.AutoCompleteMode = AutoCompleteMode.Append;
                    bctxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    bctxt.AutoCompleteCustomSource = comp;
                    sda.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as products were loading into this textbox " + er);
            }
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
                        float sub = 0;
                        for (int i = 0; i < viewprod.Rows.Count; i++)
                        {
                            sum += Convert.ToSingle(viewprod.Rows[i].Cells[4].Value.ToString());
                            va += Convert.ToSingle(viewprod.Rows[i].Cells[3].Value.ToString());
                            sub += Convert.ToSingle(viewprod.Rows[i].Cells[2].Value.ToString());
                        }
                        totallbl.Text = sum.ToString();
                        ttlvatlbl.Text = va.ToString();
                        sbttllbl.Text = sub.ToString();
                        sdr.Close();
                        db.Close();
                    }
                }
            }
            finally
            {

            }
        }

        private void viewprod_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            pntxt.Text = "";
            bctxt.Text = "";
        }

        private void proseatxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select * from Sales where [Receipt No] like '" + proseatxt.Text + "%';";
                    var sda = new SqlDataAdapter(sele, db);
                    var dt = new DataTable();
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
                MessageBox.Show("An error occured as you were searching for a Quotation " + er);
            }
        }

        private void proseatxt_Leave(object sender, EventArgs e)
        {
            if (proseatxt.Text.Trim().Equals("Search for a Receipt No.") || proseatxt.Text == "")
            {
                proseatxt.Text = "Search for a Receipt No.";
                proseatxt.ForeColor = Color.FromArgb(64, 64, 64);
                Sales();
            }
            else
            {
                Sales();
            }
        }

        private void proseatxt_Enter(object sender, EventArgs e)
        {
            if (proseatxt.Text.Trim() == "Search for a Receipt No.")
            {
                proseatxt.Text = "";
                proseatxt.ForeColor = Color.Black;
                try
                {
                    if (db.State == ConnectionState.Closed)
                    {
                        db.Open();
                        string sele = "select [Receipt No] from Sales";
                        com = new SqlCommand(sele, db);
                        SqlDataAdapter sda = new SqlDataAdapter(com);
                        AutoCompleteStringCollection comp = new AutoCompleteStringCollection();
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            comp.Add(dt.Rows[i][0].ToString());
                        }
                        proseatxt.AutoCompleteMode = AutoCompleteMode.Append;
                        proseatxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        proseatxt.AutoCompleteCustomSource = comp;
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
                Sales();
            }
        }

        void price()
        {
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string que = " select Name, Quantity, Price, VAT, [Total Price] from Sales where [Receipt No]='" + proseatxt.Text + "';";
                    com = new SqlCommand(que, db);
                    SqlDataAdapter sda = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        //upviewprod.DataSource = dt;
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

        void ttl()
        {
            try
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = " select SUM([Total Price]), SUM(VAT) from Sales where [Receipt No]='" + proseatxt.Text + "'";
                    com = new SqlCommand(sele, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        //bunifuLabel7.Text = sdr[0].ToString();
                        bunifuLabel8.Text = sdr[1].ToString();
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

        private void pntxt_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void gunaControlBox11_Click(object sender, EventArgs e)
        {
            Home ho = new Home();
            ho.Show();
            Close();
        }

        private void gunaControlBox1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void totallbl_Click(object sender, EventArgs e)
        {

        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            string mes = Interaction.InputBox("Enter password to verify account", "Verify", "Enter Password", 500, 300);
            if (mes != "")
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                    {
                        db.Open();
                        string que = "Select * from Users where Password = '" + mes + "' and Username='" + USNM + "'";
                        com = new SqlCommand(que, db);
                        SqlDataReader sdr = com.ExecuteReader();
                        if (sdr.HasRows)
                        {
                            sdr.Read();
                            sdr.Close();
                            Page.SetPage("Update");
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

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            string mes = Interaction.InputBox("Enter password to verify account", "Verify", "Enter Password", 500, 300);
            if (mes != "")
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                    {
                        db.Open();
                        string que = "Select * from Users where Password = '" + mes + "' and Username='" + USNM + "'";
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
                            float sub = 0;
                            for (int i = 0; i < viewprod.Rows.Count; i++)
                            {
                                sum += Convert.ToSingle(viewprod.Rows[i].Cells[4].Value.ToString());
                                vat += Convert.ToSingle(viewprod.Rows[i].Cells[3].Value.ToString());
                                sub += Convert.ToSingle(viewprod.Rows[i].Cells[2].Value.ToString());
                            }
                            ttlvatlbl.Text = vat.ToString();
                            totallbl.Text = sum.ToString();
                            sbttllbl.Text = sub.ToString();
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

        private void bunifuMaterialTextbox1_Enter(object sender, EventArgs e)
        {
            string search = "select [Receipt No] from Sales";
            if (db.State == ConnectionState.Closed)
            {
                db.Open();
                com = new SqlCommand(search, db);
                AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
                SqlDataAdapter sda = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    auto.Add(dt.Rows[i][0].ToString());
                }

                bunifuMaterialTextbox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                bunifuMaterialTextbox1.AutoCompleteMode = AutoCompleteMode.Append;
                bunifuMaterialTextbox1.AutoCompleteCustomSource = auto;
                sda.Dispose();
                dt.Dispose();
                db.Close();
            }
        }

        internal void load()
        {
            string loa = "select SUM(Price) as Price, SUM(VAT) as VAT, SUM([Total Price]) as Total from Sales where [Receipt No]='0026/001240402022';";
            if (db.State == ConnectionState.Closed) 
            {
                db.Open();
                com = new SqlCommand(loa, db);
                SqlDataReader sdr = com.ExecuteReader();
                if (sdr.HasRows)
                {
                    sdr.Read();
                    bunifuLabel6.Text = sdr[0].ToString();
                    bunifuLabel13.Text = sdr[1].ToString();
                    bunifuLabel16.Text = sdr[2].ToString();
                }
                sdr.Close();
                db.Close();
            }
        }

        private void bunifuMaterialTextbox1_Leave(object sender, EventArgs e)
        {
            string sale = "Select Name, Quantity, Price, VAT, [Total Price] from Sales where [Receipt No]=@rno";
            if (db.State == ConnectionState.Closed)
            {
                db.Open();
                com = new SqlCommand(sale, db);
                com.Parameters.Add(new SqlParameter("@rno", bunifuMaterialTextbox1.Text));
                SqlDataAdapter sda = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Columns.Count > 0)
                {
                    upsaledgv.DataSource = dt;
                }
                sda.Dispose();
                db.Close();
                load();
            }
        }

        private void bunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {
        }
    }
}
