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

namespace Pharmancy_Management_System
{
    public partial class Progyear : Form
    {
        public Progyear()
        {
            InitializeComponent();
        }

        SqlConnection db = new SqlConnection(@"Data Source=DESKTOP-3TSVGJC;Initial Catalog='Point Of Sale';Integrated Security=True");
        SqlCommand com = new SqlCommand();
        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        private void Progyear_Load(object sender, EventArgs e)
        {
            
        }

        private void gunaControlBox1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
