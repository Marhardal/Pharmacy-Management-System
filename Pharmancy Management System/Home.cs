using CrystalDecisions.CrystalReports.Engine;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Pharmancy_Management_System
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        //SQLiteConnection db = new SQLiteConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
        //SqlCommand com = new SqlCommand();
        //SqlDataAdapter sda = new SqlDataAdapter();

        SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand com = new SqlCommand();
        DataTable dt = new DataTable();

        internal static string usnm;
        internal static string Fname;
        internal static string Sname;
        internal static string Role;
        internal static string Phone;

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
                    com.Parameters.Add(new SqlParameter("@usnm", usnm));
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        Fname = sdr[0].ToString();
                        Sname = sdr[1].ToString();
                        Role = sdr[2].ToString();
                        Phone = sdr[3].ToString();
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
        private void Home_Load(object sender, EventArgs e)
        {
            User user = new User();
            MessageBox.Show(user.usnm);
            headtxt.Text = File.ReadLines("info.txt").First();
            usnm = Sale.USNM;
            MessageBox.Show(usnm);
            select();
            abouttxt.Text = "@ " + DateTime.Now.Year + " Dot Click IT Solutions ";
            Proglbl.Text = "Progress Summary for " + DateTime.Now.Year;
            //progressbtn.Location = new Point(213, 46);
            recprod();
            recsales();
            couusr();
            couprod();
            count();
            month();
            monthly();
            more();
            less();
            inactive();
            timer1.Start();
            ////timer1.Enabled = true;
            cou();
            int countpro = Convert.ToInt32(couprodlbl.Text);
            activebar.Minimum = 0;
            activebar.Maximum = countpro;
            activebar.Value = Convert.ToInt32(activelbl.Text);
            criticalbar.Minimum = 0;
            criticalbar.Maximum = countpro;
            criticalbar.Value = Convert.ToInt32(criticallbl.Text);
            Inactivebar.Minimum = 0;
            Inactivebar.Maximum = countpro;
            Inactivebar.Value = Convert.ToInt32(inactivelbl.Text);
            Expirebar.Minimum = 0;
            Expirebar.Maximum = countpro;
            Expirebar.Value = Convert.ToInt32(bunifuLabel2.Text);
            quatdel();
        }

        int cont;
        int les;
        int bet;
        int expi;
        int Totalprod;

        void quatdel()
        {
            try
            {
                ////
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

        int GetTotalprod()
        {
            try
            {
                ////
                if (db.State == ConnectionState.Closed) 
                {
                    string sele = "SELECT count([Medicine ID]) as 'Medicine' FROM Medicine";
                    db.Open();
                    com = new SqlCommand(sele, db);
                    Totalprod = Convert.ToInt32(com.ExecuteScalar());
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
            return Totalprod;
        }

        void cou()
        {
            ////
            if (db.State == ConnectionState.Closed)
            {
                db.Open();
                string que = "select COUNT(*) as Expires from Medicine where DATEDIFF(DD, GETDATE(), [Expire Date]) between 1 and 60;";
                com = new SqlCommand(que, db);
                SqlDataReader sdr = com.ExecuteReader();
                if (sdr.HasRows)
                {
                    sdr.Read();
                    expi = Convert.ToInt32(sdr[0].ToString());
                    if (expi > 0)
                    {
                        bunifuLabel2.Text = sdr[0].ToString();
                        notifyIcon1.Icon = new Icon(Path.GetFullPath(@"Icons\icons8_high_priority.ico"));
                        notifyIcon1.Text = "Point of Sale Notification";
                        notifyIcon1.Visible = true;
                        notifyIcon1.BalloonTipTitle = "Warning";
                        notifyIcon1.BalloonTipText = "Some Medicine are about to expire, Please click here to see the Medicine about to expire.";
                        notifyIcon1.ShowBalloonTip(100);
                        gunaTransfarantPictureBox2.Show();
                    }
                }

                sdr.Close();
                db.Close();
            }
        }

        internal void inactive()
        {
            try
            {
                ////
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select COUNT(*) as Less from Medicine Where Quantity = [Order Level];";
                    com = new SqlCommand(sele, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        bet = Convert.ToInt32(sdr[0].ToString());
                        inactivelbl.Text = sdr[0].ToString();
                    }
                    sdr.Close();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to load inactive text " + er);
            }
        }
        internal void less()
        {
            try
            {
                ////
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select COUNT(*) as Less from Medicine Where Quantity < [Reorder Level] ;";
                    com = new SqlCommand(sele, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        les = Convert.ToInt32(sdr[0].ToString());
                        criticallbl.Text = sdr[0].ToString();
                        if (sdr[0].ToString() != "0")
                        {
                            notifyIcon1.Icon = new Icon(Path.GetFullPath(@"Icons\icons8_high_priority.ico"));
                            notifyIcon1.Text = "Point of Sale Notification";
                            notifyIcon1.Visible = true;
                            notifyIcon1.BalloonTipTitle = "Warning";
                            notifyIcon1.BalloonTipText = "The inventory level for some Medicine is low, Please click here to see the remaining quantity.";
                            notifyIcon1.ShowBalloonTip(100);
                            gunaTransfarantPictureBox2.Show();
                        }
                    }
                    sdr.Close();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to load less than text " + er);
            }
        }
        internal void more()
        {
            try
            {
                ////
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = " select COUNT(*) as More from Medicine Where Quantity > [Reorder Level] and Quantity != [Order Level]; ";
                    com = new SqlCommand(sele, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        cont = Convert.ToInt32(sdr[0].ToString());
                        activelbl.Text = sdr[0].ToString();
                    }
                    sdr.Close();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to load the more than text " + er);
            }
        }

        internal void month()
        {
            try
            {
                ////
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select ex.Expense, Inco.Sales from Month_Expe as ex, Month_Inco as Inco where ex.Month=Inco.Month and ex.Month=DATENAME(MONTH, CURRENT_TIMESTAMP) and Inco.Month=DATENAME(MONTH, CURRENT_TIMESTAMP)";
                    com = new SqlCommand(sele, db);
                    sda = new SqlDataAdapter(com);
                    //DataSet ds = new DataSet();
                    //sda.Fill(ds);
                    //chart1.DataSource = ds.Tables[0];
                    //chart1.Series[0].XValueMember = "Type";
                    ////chart1.Series[0].Points[1].Color = Color.Red;
                    ////chart1.Series[0].Palette;
                    //chart1.Series[0].YValueMembers = "Amount";
                    SqlDataReader sdr = com.ExecuteReader();
                    chart1.Series[0].Points.Clear();
                    while (sdr.Read())
                    {
                        chart1.Series[0].Points.AddXY("Expense", sdr[0]);
                        chart1.Series[0].Points[0].Color = Color.DarkRed;
                        chart1.Series[0].Points.AddXY("Income", sdr[1]);
                        chart1.Series[0].Points[1].Color = Color.SeaGreen;
                    }

                    chart1.Series[0].ChartType = SeriesChartType.Doughnut;
                    chart1.Titles.Add("Businness performance for this Month");
                    chart1.Series[0].IsValueShownAsLabel = true;
                    sdr.Close();
                    sda.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as Monthly graph was inprocess of loading" + er);
            }
        }

        internal void count()
        {
            try
            {
                ////
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "Select count(*) from Sales where [Total Price] > 0 and YEAR(Date) = YEAR(CURRENT_TIMESTAMP);";
                    com = new SqlCommand(sele, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        allbl.Text = sdr[0].ToString();
                    }
                    sdr.Close();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to count all Sales " + er);
            }
        }


        internal void recprod()
        {
            try
            {
                string que = "select distinct top 5 sa.Name, pro.Quantity from Sales sa, Medicine pro where sa.Name=pro.Name order by pro.Quantity asc;";

                ////
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    com = new SqlCommand(que, db);
                    dt = new DataTable();
                    sda = new SqlDataAdapter(com);
                    sda.Fill(dt);
                    proddgv.DataSource = dt;
                    dt.Dispose();
                    sda.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to load Recent Medicine" + er);
            }
        }

        internal void recsales()
        {
            try
            {
                ////
                if (db.State == ConnectionState.Closed)
                {
                    string que = "select top 5 Date, Name, [Total Price], Quantity from Sales where [Total Price] > 0 order by [Sales ID] desc;";
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

        private void Homebtn_Click(object sender, EventArgs e)
        {
            Page.SetPage("Home");
            dashlbl.Text = "Home";
            gunaTransfarantPictureBox12.Image = Image.FromFile(@"Icons\icons8_home_32.png");
            gunaTransfarantPictureBox12.Size = new Size(45, 45);
            Counttxt.Hide();
        }

        private void Productbtn_Click(object sender, EventArgs e)
        {
            Page.SetPage("Medicine");
            dashlbl.Text = "Medicine";
            gunaTransfarantPictureBox12.Image = Image.FromFile(@"Icons\icons8_pill_32.png");
            gunaTransfarantPictureBox12.Size = new Size(45, 45);
            Counttxt.Show();
            Product();
            couprod();
        }
        internal void couprod()
        {
            try
            {
                ////
                if (db.State == ConnectionState.Closed)
                {
                    string sele = "SELECT count([Medicine ID]) as 'Medicine' FROM Medicine";
                    db.Open();
                    com = new SqlCommand(sele, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        Counttxt.Text = "Manage all Medicine here. Entered Medicine equals " + sdr[0].ToString();
                        couprodlbl.Text = sdr[0].ToString();
                        //bunifuLabel2.Text = sdr[0].ToString();
                        pro = Convert.ToInt16(sdr[0].ToString());

                    }
                    sdr.Close();

                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as product were counted " + er);
            }
        }
        int pro;

        internal void Product()
        {
            try
            {
                ////
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

        private void Managebtn_Click(object sender, EventArgs e)
        {
            Counttxt.Hide();
            Manage man = new Manage();
            man.category();
            man.Show();
        }

        void coucatg()
        {
            try
            {
                ////
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select count(ID) from category";
                    com = new SqlCommand(sele, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        Counttxt.Text = "Sum of all Categories equals " + sdr[0].ToString();
                    }
                    sdr.Close();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to count all Category" + er);
            }

        }

        void couusr()
        {
            try
            {
                ////
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sel = "Select Count([User ID]) from Users;";
                    com = new SqlCommand(sel, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        Counttxt.Text = "Manage all your users here. Number of users is " + sdr[0].ToString();
                        ssuserlbl.Text = sdr[0].ToString();
                    }
                    sdr.Close();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as users were counted " + er);
            }
        }

        private void insusr_Click(object sender, EventArgs e)
        {
            Registration reg = new Registration();
            reg.addpro.Show();
            reg.Show();
        }
        
        void actcou()
        {
            try
            {
                ////
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select COUNT(*) as [Today] from Activity where DAY(Date)=DAY(CURRENT_TIMESTAMP) and MONTH(Date)=MONTH(CURRENT_TIMESTAMP) and YEAR(Date)=YEAR(CURRENT_TIMESTAMP);";
                    com = new SqlCommand(sele, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        Counttxt.Text = "Total Activities done today are " + sdr[0].ToString();
                    }
                    sdr.Close();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as system was counting Activities " + er);
            }
        }

        private void Salesbtn_Click(object sender, EventArgs e)
        {
            Sale sale = new Sale();
            sale.Show();
            Counttxt.Hide();
        }

        internal void coucus()
        {
            try
            {
                ////
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "Select count([Sales ID]) as Today from Sales where DAY(Date)=DAY(CURRENT_TIMESTAMP) and MONTH(Date)=MONTH(CURRENT_TIMESTAMP) and YEAR(Date)=YEAR(CURRENT_TIMESTAMP) and [Total Price]>0;";
                    com = new SqlCommand(sele, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        Counttxt.Text = "All Completed Sales for Today " + sdr[0].ToString();
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


        int yea;
        internal void monthly()
        {
            try
            {
                ////
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select COUNT([Sales ID]) from Sales where MONTH(Date)=MONTH(CURRENT_TIMESTAMP) and YEAR(Date)=YEAR(CURRENT_TIMESTAMP) and [Total Price]>0;";
                    com = new SqlCommand(sele, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        yea = Convert.ToInt32(sdr[0].ToString());
                        bunifuLabel19.Text = "Total Montly Sales " + sdr[0].ToString();
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

        private void Expensebtn_Click(object sender, EventArgs e)
        {
            panel1.Location = new Point(7, 274);
            panel1.Show();
            dashlbl.Text = "Ledger";
            gunaTransfarantPictureBox12.Image = Image.FromFile(@"Icons\icons8_ledger_32.png");
            gunaTransfarantPictureBox12.Size = new Size(45, 45);
            Counttxt.Hide();
            ledgerbtn.Hide();
        }

        private void progressbtn_Click(object sender, EventArgs e)
        {
            Page.SetPage("Progress");
            dashlbl.Text = "Progress"; ;
            dashlbl.Text = "Business Progress Report";
            gunaTransfarantPictureBox12.Image = Image.FromFile(@"Icons\icons8_play_pie_chart_report_32.png");
            gunaTransfarantPictureBox12.Size = new Size(45, 45);
            sum();
            Counttxt.Hide();
            expesum();
            monthbar.Minimum = 0;
            monthbar.Maximum = yea;
            monthbar.Value = yea;
            year();
        }

        void year()
        {
            try
            {
                ////
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string que = "select inc.Year, inc.Month, inc.Sales, ex.Expense, (inc.Sales-ex.Expense) as Balance from Month_Inco as inc"
                                + " inner join Month_Expe as ex on inc.Month = ex.Month and inc.Year = ex.Year ";
                    sda = new SqlDataAdapter(que, db);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        yeadgv.DataSource = dt;
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

        internal void sum()
        {
            try
            {
                ////
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "Select sum([Total Price]) from  Sales where [Total Price]>0";
                    com = new SqlCommand(sele, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        alsaleslbl.Text = "MK " + sdr[0].ToString() + "\n All Sales";
                    }
                    sdr.Close();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as sum sales was calsulating." + er);
            }
        }

        internal void expesum()
        {
            try
            {
                ////
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "Select sum(Amount) from Expense";
                    com = new SqlCommand(sele, db);
                    SqlDataReader sdr = com.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        Explbl.Text = "MK " + sdr[0].ToString() + " \n All Expenses";
                    }
                    sdr.Close();
                    db.Close();
                }
                else
                {
                    Explbl.Text = "Mk " + 0 + " \n All expenses";
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as sum of expense was about to calculate" + er);
            }
        }

        private void addpro_Click(object sender, EventArgs e)
        {
            AddProd pro = new AddProd();
            pro.addpro.Show();
            pro.Fname = Fname;
            pro.Sname = Sname;
            pro.Role = Role;
            pro.mfdate.Value = DateTime.Now;
            pro.expdate.Value = DateTime.Now.AddMonths(2);
            pro.Show();
        }

        private void uppro_Click(object sender, EventArgs e)
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
            if (db.State == ConnectionState.Closed)
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

        private void Delpro_Click(object sender, EventArgs e)
        {
            try
            {
                ////
                string dele = "Delete from Medicine where [Medicine ID]=@id";
                com = new SqlCommand(dele, db);
                db.Open();
                com.Parameters.Add(new SqlParameter("@id", prodgv.CurrentRow.Cells[0].Value.ToString()));
                DialogResult dr = MessageBox.Show("Are you sure that you want to delete the product.", "Delete.", MessageBoxButtons.YesNo);
                if (DialogResult.Yes == dr)
                {
                    com.ExecuteNonQuery();
                    MessageBox.Show("Product Deleted");
                }

                db.Close();
                Product();
                act_Del();
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as the Product was about to be delete " + er);
            }
        }

        void act_Del()
        {
            try
            {
                ////
                if (db.State == ConnectionState.Closed) 
                {
                    db.Open();
                    string ins = "insert into Activity values(@fn, @sn, @rl, @dtl, @dt, @tm);";
                    string nm = dashlbl.Text.Substring(0);
                    string dtl = "Deleted a Product.";
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

        private void proseatxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ////
                string sele = "SELECT * FROM Medicine WHERE Name like '" + proseatxt.Text + "%' or Category like '" + proseatxt.Text + "%' or "
                    + "Price like '" + proseatxt.Text + "%' or Quantity like '" + proseatxt.Text + "%' or [Unit Quantity] like '" + proseatxt.Text + "%' or "
                    + "[Reorder Level] like '" + proseatxt.Text + "%' or VAT like '" + proseatxt.Text + "%' or [Bar Code] like '" + proseatxt.Text + "%' or [Medicine ID] like '" + proseatxt.Text + "%'; ";
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
                MessageBox.Show("Failed to serch for the Product." + er);
            }
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            activebar.Value = Convert.ToInt32(activelbl.Text);
            criticalbar.Value = Convert.ToInt32(criticallbl.Text);
            Inactivebar.Value = Convert.ToInt32(inactivelbl.Text);
            Expirebar.Value = Convert.ToInt32(bunifuLabel2.Text);
            monthbar.Value = yea;
        }

        private void activelbl_Click(object sender, EventArgs e)
        {
            Bars bar = new Bars();
            bar.active();
            bar.Page.SetPage("Active");
            bar.headtxt.Text = "Active Medicine.";
            bar.Show();
        }

        private void criticallbl_Click(object sender, EventArgs e)
        {
            Bars bar = new Bars();
            bar.critical();
            bar.Page.SetPage("Critical");
            bar.headtxt.Text = "Critical Medicine.";
            bar.Show();
        }

        private void inactivelbl_Click(object sender, EventArgs e)
        {
            Bars bar = new Bars();
            bar.inactive();
            bar.Page.SetPage("Inactive");
            bar.headtxt.Text = "Inactive Medicine.";
            bar.Show();
        }

        private void bunifuLabel2_Click(object sender, EventArgs e)
        {
            Bars bar = new Bars();
            bar.expire();
            bar.Page.SetPage("Expire");
            bar.headtxt.Text = "These are Medicine about to expire.";
            bar.Show();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            Bars bar = new Bars();
            bar.Page.SetPage("Critical");
            bar.critical();
            bar.headtxt.Text = "These are Medicine which needs Reorder.";
            bar.Show();
        }

        private void notifyIcon2_MouseClick(object sender, MouseEventArgs e)
        {
            Bars bar = new Bars();
            bar.Page.SetPage("Expire");
            bar.expire();
            bar.headtxt.Text = "These are Medicine which are about to expire.";
            bar.Show();
        }

        private void proprint_Click(object sender, EventArgs e)
        {
            rece();
            num();
            Crystal();
            act_Pri();
        }
        void act_Pri()
        {
            try
            {
                ////
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string ins = "insert into Activity values(@fn, @sn, @rl, @dtl, @dt, @tm);";
                    string dtl = "Printed all Medicine.";
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

        void rece()
        {
            ////
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
        string zno;
        string show;
        void num()
        {
            ////
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
            sdr.Close();
            db.Close();
        }

        void Crystal()
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
            foreach (DataGridViewRow dr in prodgv.Rows)
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
            TextObject ttletxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["ttletxt"];
            ttletxt.Text = "All Medicine.";
            TextObject rntxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["rntxt"];
            rntxt.Text = show;
            //TextObject znotxt = (TextObject)re.ReportDefinition.Sections[1].ReportObjects["znotxt"];
            //znotxt.Text = zno;
            TextObject fntxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["fntxt"];
            fntxt.Text = Fname;
            TextObject sntxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["sntxt"];
            sntxt.Text = Sname;
            rr.crystalReportViewer1.ReportSource = re;
            rr.crystalReportViewer1.Refresh();
            //re.Dispose();
            dt.Dispose();
            ds.Dispose();
            //re.Dispose();
            rr.Show();
        }

        private void proview_Click(object sender, EventArgs e)
        {
            Page.SetPage("Medicine");
            dashlbl.Text = "Manage Medicine";
            Counttxt.Show();
            Product();
            couprod();
        }



        private void bunifuDropdown1_onItemSelected(object sender, EventArgs e)
        {
            if (bunifuDropdown1.selectedValue == "Logout")
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to Logout.", "System Logging out", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    ////
                    if (db.State == ConnectionState.Closed)
                    {
                        db.Open();
                        string ins = "insert into Activity values(@fn, @sn, @rl, @dtl, @dt, @tm);";
                        string dtl = "Logged out of the system.";
                        string dt = DateTime.Now.ToShortDateString();
                        string tm = DateTime.Now.ToShortTimeString();
                        var exe = db.Execute(ins, new { fn = Fname, sn = Sname, rl = Role, dtl, dt, tm });
                        if (exe > 0)
                        {
                            Hide();
                            Fname = "";
                            Sname = "";
                            Role = "";
                            usnm = "";
                            Application.Restart();
                        }
                        db.Close();
                    }
                }
            }
            else if (bunifuDropdown1.selectedValue == "Profile")
            {
                try
                {
                    ////
                    if (db.State == ConnectionState.Closed)
                    {
                        db.Open();
                        string sele = "Select * from Users Where Username = @usn";
                        com = new SqlCommand(sele, db);
                        com.Parameters.Add(new SqlParameter("@usn", usnm));
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
                            reg.label1.Text = sdr[0].ToString();
                            reg.fnlbl.Text = Fname;
                            reg.snlbl.Text = Sname;
                            reg.rllbl.Text = Role;
                            reg.upusrbtn.Show();
                            reg.Show();
                        }
                        sdr.Close();
                        db.Close();
                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show("An error happen as you want to open your profile details." + er);
                }
            }
            else if (bunifuDropdown1.selectedValue == "Settings")
            {
                Settings tin = new Settings();
                tin.Show();
            }
        }
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Progview view = new Progview();
            view.Show();
            
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            activebar.Value = e.ProgressPercentage;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void gunaControlBox1_Click(object sender, EventArgs e)
        {
            try
            {
                ////
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
            finally
            {

            }
        }

        private void expedgv_Enter(object sender, EventArgs e)
        {
            if (expseatxt.Text.Trim() == "Search for a Supplier Name.")
            {
                expseatxt.Text = "";
                expseatxt.ForeColor = Color.Black;
                gunaAdvenceButton5.Enabled = true;
                try
                {
                    ////
                    if (db.State == ConnectionState.Closed)
                    {
                        db.Open();
                        string sele = "select [Supplier Name] from Payable";
                        com = new SqlCommand(sele, db);
                        SqlDataAdapter sda = new SqlDataAdapter(com);
                        AutoCompleteStringCollection comp = new AutoCompleteStringCollection();
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            comp.Add(dt.Rows[i][0].ToString());
                        }
                        expseatxt.AutoCompleteMode = AutoCompleteMode.Append;
                        expseatxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        expseatxt.AutoCompleteCustomSource = comp;
                        sda.Dispose();
                        db.Close();
                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show("An error occured as Medicine were loading into this textbox " + er);
                }
            }
        }

        private void expedgv_Leave(object sender, EventArgs e)
        {
            if (expseatxt.Text.Trim() == "Search for a Supplier Name." || expseatxt.Text == "")
            {
                expseatxt.Text = "Search for a Supplier Name.";
                expseatxt.ForeColor = Color.FromArgb(64, 64, 64);

                gunaAdvenceButton5.Enabled = false;
                payable();
            }
        }

        private void proseatxt_Leave(object sender, EventArgs e)
        {
            if (proseatxt.Text.Trim().Equals("Search for a Medicine. ") || proseatxt.Text == "")
            {
                proseatxt.Text = "Search for a Medicine. ";
                proseatxt.ForeColor = Color.FromArgb(64, 64, 64);
                Product();
            }
        }

        private void proseatxt_Enter(object sender, EventArgs e)
        {
            if (proseatxt.Text.Trim() == "Search for a Medicine. ")
            {
                proseatxt.Text = "";
                proseatxt.ForeColor = Color.Black;
            }
            else
            {
                Product();
            }
        }

        private void proseatxt_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            rece();
            num();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("Year");
            dt.Columns.Add("Month");
            dt.Columns.Add("Income");
            dt.Columns.Add("Expense");
            dt.Columns.Add("Balance");
            foreach (DataGridViewRow dr in yeadgv.Rows)
            {
                dt.Rows.Add(dr.Cells[0].Value, dr.Cells[1].Value, dr.Cells[2].Value, dr.Cells[3].Value, dr.Cells[4].Value);
            }
            ds.Tables.Add(dt);
            ds.WriteXmlSchema("Year.xml");
            var rr = new Progyear();
            var re = new Year();
            re.SetDataSource(ds);
            TextObject ntxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["ntxt"];
            ntxt.Text = File.ReadLines("info.txt").First();
            TextObject pobtxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["pobtxt"];
            pobtxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(1);
            TextObject ctytxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["ctytxt"];
            ctytxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(2);
            TextObject ctttxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["ctttxt"];
            ctttxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(3);
            TextObject rntxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["rntxt"];
            rntxt.Text = show;
            TextObject ttletxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["ttletxt"];
            ttletxt.Text = "Progress Summary for ." + DateTime.Now.Year;
            TextObject fntxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["fntxt"];
            fntxt.Text = Fname;
            TextObject sntxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["sntxt"];
            sntxt.Text = Sname;
            rr.crystalReportViewer1.ReportSource = re;
            rr.crystalReportViewer1.Refresh();
            dt.Dispose();
            ds.Dispose();
            rr.Show();
            act_print();
        }
        void act_print()
        {
            try
            {
                ////
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string ins = "insert into Activity values(@fn, @sn, @rl, @dtl, @dt, @tm);";
                    string dtl = "Printed a " + dashlbl.Text.Substring(0);
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

        private void yeadgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void gunaTransfarantPictureBox7_Click(object sender, EventArgs e)
        {
            ledgerbtn.Show();
            panel1.Hide();
        }

        private void Catbtn_Click(object sender, EventArgs e)
        {
            Page.SetPage("Receivable");
            dashlbl.Text = "Receivable";
            gunaTransfarantPictureBox12.Image = Image.FromFile(@"Icons\icons8_receive_cash_32.png");
            gunaTransfarantPictureBox12.Size = new Size(45, 45);
            receivable();
        }

        void receivable()
        {
            try
            {
                ////
                if (db.State == ConnectionState.Closed) 
                {
                    db.Open();
                    string que = "select * from Receivables order by ID desc";
                    com = new SqlCommand(que, db);
                    sda = new SqlDataAdapter(com);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0) 
                    {
                        receivedgv.DataSource = dt;
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

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            Page.SetPage("Payable");
            dashlbl.Text = "Payable";
            gunaTransfarantPictureBox12.Image = Image.FromFile(@"Icons\icons8_pay_32.png");
            gunaTransfarantPictureBox12.Size = new Size(45, 45);
            payable();
        }

        internal void payable()
        {
            try
            {
                ////
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select * from Payable order by ID desc";
                    com = new SqlCommand(sele, db);
                    sda = new SqlDataAdapter(com);
                    dt = new DataTable();
                    sda.Fill(dt);
                    expensedgv.DataSource = dt;
                    sda.Dispose();
                    dt.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("A error occured as Expense was about to load " + er);
            }
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            Expense exp = new Expense();
            exp.phn = Phone;
            exp.Fname = Fname;
            exp.Sname = Sname;
            exp.Role = Role;
            exp.Page.SetPage("Payable");
            exp.bunifuLabel1.Text = "Insert Payable";
            exp.payaddbtn.Show();
            exp.Show();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            Expense expe = new Expense();
            expe.phn = Phone;
            expe.Fname = Fname;
            expe.Sname = Sname;
            expe.Role = Role;
            expe.invdttxt.Value = Convert.ToDateTime(expensedgv.CurrentRow.Cells[2].Value.ToString());
            expe.supnmtxt.Text = expensedgv.CurrentRow.Cells[3].Value.ToString();
            expe.amtxt.Text = expensedgv.CurrentRow.Cells[4].Value.ToString();
            expe.pmtpcom.SelectedItem(expensedgv.CurrentRow.Cells[5].Value.ToString());
            expe.balatxt.Text = expensedgv.CurrentRow.Cells[6].Value.ToString();
            expe.ddtxt.Value = Convert.ToDateTime(expensedgv.CurrentRow.Cells[7].Value.ToString());
            expe.bunifuLabel4.Text = expensedgv.CurrentRow.Cells[0].Value.ToString();
            expe.Page.SetPage("Payable");
            expe.bunifuLabel1.Text = "Update Payable";
            expe.payupbtn.Show();
            expe.Show();
        }

        private void gunaAdvenceButton4_Click(object sender, EventArgs e)
        {
            Expense exp = new Expense();
            exp.phn = Phone;
            exp.Fname = Fname;
            exp.Sname = Sname;
            exp.Role = Role;
            exp.Page.SetPage("Receivable");
            exp.bunifuLabel1.Text = "Insert Receivable";
            exp.recadd.Show();
            exp.Show();
        }

        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            Expense exp = new Expense();
            exp.phn = Phone;
            exp.Fname = Fname;
            exp.Sname = Sname;
            exp.Role = Role;
            exp.recamotxt.Text = receivedgv.CurrentRow.Cells[2].Value.ToString();
            exp.advrectxt.Text = receivedgv.CurrentRow.Cells[3].Value.ToString();
            exp.baltxt.Text = receivedgv.CurrentRow.Cells[4].Value.ToString();
            exp.duedttxt.Value = Convert.ToDateTime(receivedgv.CurrentRow.Cells[8].Value.ToString());
            exp.bunifuLabel4.Text= receivedgv.CurrentRow.Cells[0].Value.ToString();
            exp.Page.SetPage("Receivable");
            exp.bunifuLabel1.Text = "Update Receivable";
            exp.receup.Show();
            exp.Show();
        }

        private void ledgerbtn_Click(object sender, EventArgs e)
        {
            Page.SetPage("Expense");
            dashlbl.Text = "Expense";
            gunaTransfarantPictureBox12.Image = Image.FromFile(@"Icons\icons8_exclamation_mark_32.png");
            gunaTransfarantPictureBox12.Size = new Size(45, 45);
            Expense();
        }

        void Expense()
        {
            try
            {
                ////
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string que = "Select * from Expense order by ID desc;";
                    com = new SqlCommand(que, db);
                    sda = new SqlDataAdapter(com);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        ledgerdgv.DataSource = dt;
                    }
                    dt.Dispose();
                    sda.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Failed to load Expense " + er);
            }
        }

        private void gunaAdvenceButton7_Click(object sender, EventArgs e)
        {
            Expense exp = new Expense();
            exp.Page.SetPage("Expense");
            exp.bunifuLabel1.Text = "insert Expense";
            exp.phn = Phone;
            exp.Fname = Fname;
            exp.Sname = Sname;
            exp.Role = Role;
            exp.expadd.Show();
            exp.Show();
        }

        private void gunaAdvenceButton6_Click(object sender, EventArgs e)
        {
            Expense exp = new Expense();
            exp.Page.SetPage("Expense");
            exp.bunifuLabel1.Text = "Update Expense";
            exp.phn = Phone;
            exp.Fname = Fname;
            exp.Sname = Sname;
            exp.Role = Role;
            exp.expnmtxt.Text = expensedgv.CurrentRow.Cells[1].Value.ToString();
            exp.expamotxt.Text = expensedgv.CurrentRow.Cells[2].Value.ToString();
            exp.bunifuLabel4.Text = expensedgv.CurrentRow.Cells[0].Value.ToString();
            exp.expeup.Show();
            exp.Show();
        }

        private void exptxt_MouseLeave(object sender, EventArgs e)
        {

        }

        private void exptxt_Leave(object sender, EventArgs e)
        {
            if (exptxt.Text.Trim().Equals("Search for an Expense Name.") || exptxt.Text == "")
            {
                exptxt.Text = "Search for an Expense Name. ";
                exptxt.ForeColor = Color.FromArgb(64, 64, 64);
                Expense();
            }
        }

        private void exptxt_Enter(object sender, EventArgs e)
        {
            if (exptxt.Text.Trim() == "Search for an Expense Name.")
            {
                exptxt.Text = "";
                exptxt.ForeColor = Color.Black;
            }
            else
            {
                Expense();
            }
        }

        private void exptxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ////
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select * from Expense where [Name] like '" + exptxt.Text + "' order by ID";
                    com = new SqlCommand(sele, db);
                    sda = new SqlDataAdapter(com);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        ledgerdgv.DataSource = dt;
                    }
                    dt.Dispose();
                    sda.Dispose();
                    db.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as you were searching for an Expense " + er);
            }
        }

        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {
            details();
            Page.SetPage("View");
            paydetails();
        }

        void details()
        {
            try
            {
                ////
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select [Payment Type], Amount, Balance from Payable where [Supplier Name]= @sn order by [Invoice Date] desc";
                    com = new SqlCommand(sele, db);
                    com.Parameters.Add(new SqlParameter("@sn", expseatxt.Text));
                    sda = new SqlDataAdapter(com);
                    dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        paydgv.DataSource = dt;
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

        void paydetails()
        {
            ////
            db.Open();
            string que = "select MAX([Invoice ID]) as ID, MAX([Invoice Date]) as [Invoice Date], [Supplier Name], [Due Date]  from Payable where [Supplier Name]= '" + expseatxt.Text + "' group by [Supplier Name] , [Due Date] order by [Invoice Date] desc ;";
            com = new SqlCommand(que, db);
            SqlDataReader sdr = com.ExecuteReader();
            if (sdr.HasRows)
            {
                sdr.Read();
                idlbl.Text = sdr[0].ToString();
                iddlbl.Text = sdr[1].ToString();
                snlbl.Text = sdr[2].ToString();
                ddatlbl.Text = "This Payment is Due on " + sdr[3].ToString();
            }
            sdr.Close();
            db.Close();
        }

        private void expseatxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ////
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                    string sele = "select * from Payable where [Supplier Name] like '" + expseatxt.Text + "%'; ";
                    dt = new DataTable();
                    sda = new SqlDataAdapter(sele, db);
                    sda.Fill(dt);
                    expensedgv.DataSource = dt;
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

        private void gunaAdvenceButton8_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("Payment Type");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Balance");
            foreach (DataGridViewRow dr in paydgv.Rows)
            {
                dt.Rows.Add(dr.Cells[0].Value, dr.Cells[1].Value, dr.Cells[2].Value);
            }
            ds.Tables.Add(dt);
            ds.WriteXmlSchema("Payable.xml");
            var rr = new Payview();
            var re = new Pay();
            re.SetDataSource(ds);
            TextObject ntxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["ntxt"];
            ntxt.Text = File.ReadLines("info.txt").First();
            TextObject pobtxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["pobtxt"];
            pobtxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(1);
            TextObject ctytxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["ctytxt"];
            ctytxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(2);
            TextObject ctttxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["ctttxt"];
            ctttxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(3);
            TextObject rntxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["rntxt"];
            rntxt.Text = idlbl.Text;
            TextObject sn = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["sn"];
            sn.Text = snlbl.Text;
            TextObject invd = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["invd"];
            invd.Text = iddlbl.Text;
            //TextObject ttletxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["ttletxt"];
            //ttletxt.Text = "Invoice";
            TextObject fntxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["fntxt"];
            fntxt.Text = Fname;
            TextObject sntxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["sntxt"];
            sntxt.Text = Sname;
            rr.crystalReportViewer1.ReportSource = re;
            rr.crystalReportViewer1.Refresh();
            dt.Dispose();
            ds.Dispose();
            rr.Show();
            act_print();
        }

        private void gunaAdvenceButton10_Click(object sender, EventArgs e)
        {
            try
            {
                ////
                string dele = "Delete from Payable where [ID]=@id";
                com = new SqlCommand(dele, db);
                db.Open();
                com.Parameters.Add(new SqlParameter("@id", paydgv.CurrentRow.Cells[0].Value.ToString()));
                DialogResult dr = MessageBox.Show("Are you sure that you want to delete a Payable.", "Delete.", MessageBoxButtons.YesNo);
                if (DialogResult.Yes == dr)
                {
                    com.ExecuteNonQuery();
                    MessageBox.Show("Payable Deleted");
                }

                db.Close();
                payable();
                act_Del();
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as the Payable was about to be delete " + er);
            }
        }

        private void gunaAdvenceButton11_Click(object sender, EventArgs e)
        {
            try
            {
                ////
                string dele = "Delete from Receivables where [ID]=@id";
                com = new SqlCommand(dele, db);
                db.Open();
                com.Parameters.Add(new SqlParameter("@id", receivedgv.CurrentRow.Cells[0].Value.ToString()));
                DialogResult dr = MessageBox.Show("Are you sure that you want to delete a Receivable.", "Delete.", MessageBoxButtons.YesNo);
                if (DialogResult.Yes == dr)
                {
                    com.ExecuteNonQuery();
                    MessageBox.Show("Receivable Deleted");
                }

                db.Close();
                receivable();
                act_Del();
            }
            catch (Exception er)
            {
                MessageBox.Show("An error occured as the Recievable was about to be delete " + er);
            }
        }

        private void gunaAdvenceButton12_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("Receivable ID");
            dt.Columns.Add("Total Receivable");
            dt.Columns.Add("Advanced Receipt");
            dt.Columns.Add("Balance");
            dt.Columns.Add("First Name");
            dt.Columns.Add("Surname");
            dt.Columns.Add("Phone Number");
            dt.Columns.Add("Due Date");
            foreach (DataGridViewRow dr in receivedgv.Rows)
            {
                dt.Rows.Add(dr.Cells[0].Value, dr.Cells[1].Value, dr.Cells[2].Value, dr.Cells[3].Value, dr.Cells[4].Value, dr.Cells[5].Value, dr.Cells[6].Value, dr.Cells[7].Value);
            }
            ds.Tables.Add(dt);
            ds.WriteXmlSchema("Receive.xml");
            var rr = new Receiveview();
            var re = new Receivable();
            re.SetDataSource(ds);
            TextObject ntxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["ntxt"];
            ntxt.Text = File.ReadLines("info.txt").First();
            TextObject pobtxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["pobtxt"];
            pobtxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(1);
            TextObject ctytxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["ctytxt"];
            ctytxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(2);
            TextObject ctttxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["ctttxt"];
            ctttxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(3);
            //TextObject ttletxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["ttletxt"];
            //ttletxt.Text = "Invoice";
            TextObject fntxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["fntxt"];
            fntxt.Text = Fname;
            TextObject sntxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["sntxt"];
            sntxt.Text = Sname;
            rr.crystalReportViewer1.ReportSource = re;
            rr.crystalReportViewer1.Refresh();
            dt.Dispose();
            ds.Dispose();
            rr.Show();
            act_print();
        }

        private void gunaAdvenceButton9_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("Invoice ID");
            dt.Columns.Add("Invoice Date");
            dt.Columns.Add("Supplier Name");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Payment Type");
            dt.Columns.Add("Balance");
            dt.Columns.Add("Due Date");
            foreach (DataGridViewRow dr in expensedgv.Rows)
            {
                dt.Rows.Add(dr.Cells[0].Value, dr.Cells[1].Value, dr.Cells[2].Value, dr.Cells[3].Value, dr.Cells[4].Value, dr.Cells[5].Value, dr.Cells[6].Value);
            }
            ds.Tables.Add(dt);
            ds.WriteXmlSchema("Pay.xml");
            var rr = new Payableview();
            var re = new Payable();
            re.SetDataSource(ds);
            TextObject ntxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["ntxt"];
            ntxt.Text = File.ReadLines("info.txt").First();
            TextObject pobtxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["pobtxt"];
            pobtxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(1);
            TextObject ctytxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["ctytxt"];
            ctytxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(2);
            TextObject ctttxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["ctttxt"];
            ctttxt.Text = File.ReadLines("info.txt").ElementAtOrDefault(3);
            //TextObject ttletxt = (TextObject)re.ReportDefinition.Sections[2].ReportObjects["ttletxt"];
            //ttletxt.Text = "Invoice";
            TextObject fntxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["fntxt"];
            fntxt.Text = Fname;
            TextObject sntxt = (TextObject)re.ReportDefinition.Sections[4].ReportObjects["sntxt"];
            sntxt.Text = Sname;
            rr.crystalReportViewer1.ReportSource = re;
            rr.crystalReportViewer1.Refresh();
            dt.Dispose();
            ds.Dispose();
            rr.Show();
            act_print();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            Sale sal = new Sale();
            sal.Page.SetPage("Create");
            sal.Show();
        }
    }
}
