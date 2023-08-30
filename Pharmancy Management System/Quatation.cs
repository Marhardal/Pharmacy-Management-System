using AForge.Video;
using AForge.Video.DirectShow;
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
using ZXing;

namespace Pharmancy_Management_System
{
    public partial class Quatation : Form
    {
        public Quatation()
        {
            InitializeComponent();
        }

        SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
        SqlCommand com = new SqlCommand();
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
       
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
            db.Close();
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
                string fnf = fntxt.Text.Substring(0, 1);
                string snm = sntxt.Text.Substring(0, 1);
                show = fnf + snm + "00" + left + "00" + right + DateTime.Now.ToString("yyyy") + "QT";
                zno = "00" + sdr[2].ToString();
                sdr.Close();
            }
            db.Close();
        }

        internal string Fname;
        internal string Sname;
        internal string Role;
        internal string Usnm;
        
        private void addsalbtn_Click(object sender, EventArgs e)
        {
            if (pnotxt.Text == "" || sntxt.Text == "" || fntxt.Text == "" ) 
            {
                MessageBox.Show("Please fill in all fields to countinue.");
            }
            else
            {
                
                if (db.State == ConnectionState.Closed) 
                {
                    try
                    {
                        rece();
                        num();
                        db.Open();
                        for (int i = 0; i < viewprod.Rows.Count; i++)
                        {
                            string ins = "insert into Quantation values(@rno, @cfn, @csn, @cpn, @pn, @pq, @dt, @tm, @tt, @vat, @tp, @rl, @usn, @unt);";
                            string rno = show;
                            string cfn = fntxt.Text;
                            string csn = sntxt.Text;
                            string cpn = pnotxt.Text;
                            string pn = viewprod.Rows[i].Cells[0].Value.ToString();
                            string pq = viewprod.Rows[i].Cells[1].Value.ToString();
                            string dt = DateTime.Now.ToString("yyyy-MM-dd");
                            string tm = DateTime.Now.ToString("HH:mm:ss");
                            string tt = viewprod.Rows[i].Cells[2].Value.ToString();
                            string vat = viewprod.Rows[i].Cells[3].Value.ToString();
                            string tp = viewprod.Rows[i].Cells[4].Value.ToString();
                            string rl = Role;
                            string usn = Usnm;
                            string unt = DateTime.Now.AddDays(14).ToString("yyyy-MM-dd");
                            var ex = db.Execute(ins, new { rno, cfn, csn, cpn, pn, pq, dt, tm, tt, vat, tp, rl, usn, unt });
                            cou = viewprod.RowCount;
                            db.Close();
                            Hide();
                        }
                        db.Close();
                        insact();
                        viewprod.Rows.Clear();
                        Totallbl.Text = "";
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("An error occured as quotation was added " + er);
                    }
                }
            }
        }

        void insact()
        {
            
            db.Open();
            string ins = "insert into Activity values(@fn, @sn, @rl, @dtl, @dt, @tm);";
            string dtl = "Made a Quantation.";
            string dt = DateTime.Now.ToShortDateString();
            string tm = DateTime.Now.ToShortTimeString();
            string fn = Fname;
            string sn = Sname;
            string rl = Role;
            var exe = db.Execute(ins, new { fn, sn, rl, dtl, dt, tm });
            db.Close();
        }

        FilterInfoCollection filter;
        VideoCaptureDevice capture;

        private void Quatation_Load(object sender, EventArgs e)
        {
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

        //float price;
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
                    bctxt.Text = "";
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as products was auto filling." + er);
            }
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
                    pntxt.Text = "";
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as products was auto filling." + er);
            }
        }
        

        private void Quatation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (capture != null)
            {
                if (capture.IsRunning)
                {
                    capture.Stop();
                }
            }
        }

        private void pnotxt_Leave(object sender, EventArgs e)
        {
           
        }

        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {
            if (viewprod.SelectedRows.Count > 0)
            {
                viewprod.Rows.RemoveAt(viewprod.SelectedRows[0].Index);
            }
            else
            {   
                MessageBox.Show("Please select a row to continue.");
            }
            float sum = 0;
            //Totallbl.Text = viewprod.Rows.Cast<DataGridViewRow>().AsEnumerable().Sum(x => int.Parse(x.Cells[3].Value.ToString())).ToString();
            for (int i = 0; i < viewprod.Rows.Count; i++)
            {
                sum += Convert.ToSingle(viewprod.Rows[i].Cells[3].Value.ToString());
            }
            Totallbl.Text = sum.ToString();
        }
        internal string ID;
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            //if (db.State == ConnectionState.Closed)
            //{
            //    try
            //    {
            //        db.Open(); string ins = "update Quantation set Cash=@ca, Status='Paid' where No=@id;";
            //        string ca = cctxt.Text;
            //        string id = ID;
            //        var ex = db.Execute(ins, new { ca, id });
            //        if (ex > 0)
            //        {
            //            MessageBox.Show("Updated ");
            //            Hide();
            //        }
            //        else
            //        {
            //            MessageBox.Show("Failed to insert quatation.");
            //        }

            //    }
            //    catch (Exception er)
            //    {
            //        MessageBox.Show("An error occured as quotation was added " + er);
            //    }
            //}
        }

        private void gunaControlBox1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void viewprod_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
                if (db.State==ConnectionState.Closed)
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
                        viewprod.CurrentRow.Cells[2].Value = (ans-vat).ToString();
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

        private void viewprod_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pntxt_OnValueChanged(object sender, EventArgs e)
        {

        }
    }
}
