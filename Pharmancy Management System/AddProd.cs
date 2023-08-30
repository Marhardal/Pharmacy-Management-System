using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using AForge.Video;
using ZXing;
using Dapper;
using System.Configuration;

namespace Pharmancy_Management_System
{
    public partial class AddProd : Form
    {
        public AddProd()
        {
            InitializeComponent();
        }

        FilterInfoCollection filter;
        VideoCaptureDevice capture;
        SqlConnection db = new SqlConnection();
        SqlCommand com = new SqlCommand();
        
        private void AddProd_Load(object sender, EventArgs e)
        {
            prod();
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

        internal void prod()
        {
            try
            {
                db.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ToString();
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string que = "select Categories from Category;";
                    com = new SqlCommand(que, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            qttxt.Items.Add(sdr[0].ToString());
                        }
                    }
                    sdr.Close();
                    db.Close();
                    qttxt.selectedIndex = 0;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as combobox values were loading " + er);
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

        private void AddProd_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (capture != null)
            {
                if (capture.IsRunning)
                {
                    capture.Stop();
                }
            }
        }

        private void gunaControlBox1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void addpro_Click(object sender, EventArgs e)
        {
            try
            {
                db.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ToString();
                if (db.State == ConnectionState.Closed) 
                {
                    double ans;
                    if (vatxt.selectedValue == "Taxable")
                    {
                        double price = Convert.ToDouble(pptxt.Text);
                        double vat = 16.5;
                        ans = price * vat / 100;
                    }
                    else
                    {
                        ans = 0;
                    }
                    string que = "INSERT INTO Medicine VALUES(@na, @ct, @pr, @vp, @ol, @qt, @uqt, @rl, @vat, @bc, @md, @ed)";
                    db.Open();
                    com = new SqlCommand(que, db);
                    com.Parameters.Add("@na", SqlDbType.VarChar).Value = natxt.Text;
                    com.Parameters.Add("@qt", SqlDbType.VarChar).Value = cattxt.Text;
                    com.Parameters.Add("@ct", SqlDbType.VarChar).Value = qttxt.selectedValue;
                    com.Parameters.Add("@pr", SqlDbType.VarChar).Value = pptxt.Text;
                    com.Parameters.Add("@uqt", SqlDbType.VarChar).Value = uqtxt.Text;
                    com.Parameters.Add("@rl", SqlDbType.VarChar).Value = rltxt.Text;
                    com.Parameters.Add("@ol", SqlDbType.VarChar).Value = cattxt.Text;
                    com.Parameters.Add("@vat", SqlDbType.VarChar).Value = vatxt.selectedValue;
                    com.Parameters.Add("@bc", SqlDbType.VarChar).Value = bctxt.Text;
                    com.Parameters.Add("@vp", SqlDbType.VarChar).Value = ans.ToString();
                    com.Parameters.Add("@md", SqlDbType.Date).Value = mfdate.Value;
                    com.Parameters.Add("@ed", SqlDbType.Date).Value = expdate.Value;
                    com.ExecuteNonQuery();
                    db.Close();
                    insact();
                    Hide(); 
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured while inserting a Product " + er);
            }
            finally
            {
                db.Close();
            }
        }
        internal string Fname;
        internal string Sname;
        internal string Role;
        void insact()
        {
            db.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ToString();
            db.Open();
            string ins = "insert into Activity values(@fn, @sn, @rl, @dtl, @dt, @tm);";
            string dtl = "Added a sale.";
            string dt = DateTime.Now.ToShortDateString();
            string tm = DateTime.Now.ToShortTimeString();
            string fn = Fname;
            string sn = Sname;
            string rl = Role;
            var exe = db.Execute(ins, new { fn, sn, rl, dtl, dt, tm });
            db.Close();
        }

        private void uppro_Click(object sender, EventArgs e)
        {
            try
            {
                db.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ToString();
                if (db.State == ConnectionState.Closed) 
                {
                    double ans;
                    if (vatxt.selectedValue == "Taxable")
                    {
                        double price = Convert.ToDouble(pptxt.Text);
                        double vat = 16.5;
                        ans = price * vat / 100;
                    }
                    else
                    {
                        ans = 0;
                    }
                    string que = "UPDATE Medicine SET Name=@na, Category=@ct, Price=@pr, Quantity=@qt, [VAT Price]=@vp, "
                                + " [Unit Quantity] = @uqt, [Reorder Level] = @rl, VAT = @vat, [Bar Code] = @bc, "
                                + " [Manufacturing Date] = @md, [Expire Date] = @ed WHERE [Medicine ID] = @id ";
                    db.Open();
                    com = new SqlCommand(que, db);
                    com.Parameters.Add("@na", SqlDbType.VarChar).Value = natxt.Text;
                    com.Parameters.Add("@pr", SqlDbType.VarChar).Value = pptxt.Text;
                    com.Parameters.Add("@qt", SqlDbType.VarChar).Value = cattxt.Text;
                    com.Parameters.Add("@ct", SqlDbType.VarChar).Value = qttxt.selectedValue;
                    com.Parameters.Add("@uqt", SqlDbType.VarChar).Value = uqtxt.Text;
                    com.Parameters.Add("@rl", SqlDbType.VarChar).Value = rltxt.Text;
                    com.Parameters.Add("@ol", SqlDbType.VarChar).Value = cattxt.Text;
                    com.Parameters.Add("@vat", SqlDbType.VarChar).Value = vatxt.selectedValue;
                    com.Parameters.Add("@bc", SqlDbType.VarChar).Value = bctxt.Text;
                    com.Parameters.Add("@vp", SqlDbType.VarChar).Value = ans.ToString();
                    com.Parameters.Add("@md", SqlDbType.Date).Value = mfdate.Value;
                    com.Parameters.Add("@ed", SqlDbType.Date).Value = expdate.Value;
                    com.Parameters.Add("@id", SqlDbType.VarChar).Value = label1.Text;
                    com.ExecuteNonQuery();
                    db.Close();
                    upAct();
                    Hide();
                    MessageBox.Show("Product Updated."); 
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as the Product was about to be updated " + er);
            }
        }
        void upAct()
        {
            db.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ToString();
            db.Open();
            string ins = "insert into Activity values(@fn, @sn, @rl, @dtl, @dt, @tm);";
            string dtl = "Updated a product.";
            string dt = DateTime.Now.ToShortDateString();
            string tm = DateTime.Now.ToShortTimeString();
            string fn = Fname;
            string sn = Sname;
            string rl = Role;
            var exe = db.Execute(ins, new { fn , sn , rl , dtl, dt, tm });
            db.Close();
        }
        private void bctxt_TextChanged(object sender, EventArgs e)
        {
            BarcodeWriter bcw = new BarcodeWriter();
            bcw.Format = BarcodeFormat.CODE_128;
            scan.Image = bcw.Write(Convert.ToString(scan.Text));
        }
        
        private void vatxt_onItemSelected(object sender, EventArgs e)
        {
            if (vatxt.selectedValue == "Taxable") 
            {
                double price = Convert.ToDouble(pptxt.Text);
                double vat = 16.5;
                double ans = price * vat / 100;
            }
        }
    }
}
