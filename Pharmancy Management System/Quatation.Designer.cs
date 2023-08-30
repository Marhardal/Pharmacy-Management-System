namespace Pharmancy_Management_System
{
    partial class Quatation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Quatation));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bctxt = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.bunifuLabel9 = new Bunifu.UI.WinForms.BunifuLabel();
            this.cam = new Bunifu.Framework.UI.BunifuDropdown();
            this.scan = new Guna.UI.WinForms.GunaPictureBox();
            this.chgtxt = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.fntxt = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.sntxt = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.bunifuLabel15 = new Bunifu.UI.WinForms.BunifuLabel();
            this.bunifuLabel14 = new Bunifu.UI.WinForms.BunifuLabel();
            this.bunifuLabel12 = new Bunifu.UI.WinForms.BunifuLabel();
            this.pntxt = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.bunifuLabel10 = new Bunifu.UI.WinForms.BunifuLabel();
            this.bunifuGradientPanel1 = new Bunifu.Framework.UI.BunifuGradientPanel();
            this.gunaControlBox1 = new Guna.UI.WinForms.GunaControlBox();
            this.bunifuLabel1 = new Bunifu.UI.WinForms.BunifuLabel();
            this.addsalbtn = new Bunifu.Framework.UI.BunifuFlatButton();
            this.pnotxt = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.bunifuLabel2 = new Bunifu.UI.WinForms.BunifuLabel();
            this.Totallbl = new Bunifu.UI.WinForms.BunifuLabel();
            this.bunifuLabel4 = new Bunifu.UI.WinForms.BunifuLabel();
            this.gunaAdvenceButton5 = new Guna.UI.WinForms.GunaAdvenceButton();
            this.bunifuFlatButton1 = new Bunifu.Framework.UI.BunifuFlatButton();
            this.ttlvatlbl = new Bunifu.UI.WinForms.BunifuLabel();
            this.viewprod = new Bunifu.Framework.UI.BunifuCustomDataGrid();
            this.pn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qtt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.scan)).BeginInit();
            this.bunifuGradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewprod)).BeginInit();
            this.SuspendLayout();
            // 
            // bctxt
            // 
            this.bctxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.bctxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.bctxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bctxt.characterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.bctxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.bctxt.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.bctxt.ForeColor = System.Drawing.Color.Black;
            this.bctxt.HintForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bctxt.HintText = "Scan the Barcode.";
            this.bctxt.isPassword = false;
            this.bctxt.LineFocusedColor = System.Drawing.Color.Blue;
            this.bctxt.LineIdleColor = System.Drawing.Color.Gray;
            this.bctxt.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.bctxt.LineThickness = 3;
            this.bctxt.Location = new System.Drawing.Point(105, 52);
            this.bctxt.Margin = new System.Windows.Forms.Padding(4);
            this.bctxt.MaxLength = 32767;
            this.bctxt.Name = "bctxt";
            this.bctxt.Size = new System.Drawing.Size(248, 30);
            this.bctxt.TabIndex = 87;
            this.bctxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.bctxt.Enter += new System.EventHandler(this.bctxt_Enter);
            this.bctxt.Leave += new System.EventHandler(this.bctxt_Leave);
            // 
            // bunifuLabel9
            // 
            this.bunifuLabel9.AutoEllipsis = false;
            this.bunifuLabel9.CursorType = null;
            this.bunifuLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.bunifuLabel9.Location = new System.Drawing.Point(23, 56);
            this.bunifuLabel9.Name = "bunifuLabel9";
            this.bunifuLabel9.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel9.Size = new System.Drawing.Size(59, 21);
            this.bunifuLabel9.TabIndex = 86;
            this.bunifuLabel9.Text = "Barcode";
            this.bunifuLabel9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuLabel9.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // cam
            // 
            this.cam.BackColor = System.Drawing.Color.Transparent;
            this.cam.BorderRadius = 3;
            this.cam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cam.DisabledColor = System.Drawing.Color.Gray;
            this.cam.ForeColor = System.Drawing.Color.Black;
            this.cam.items = new string[0];
            this.cam.Location = new System.Drawing.Point(139, 73);
            this.cam.Name = "cam";
            this.cam.NomalColor = System.Drawing.Color.Transparent;
            this.cam.onHoverColor = System.Drawing.Color.Transparent;
            this.cam.selectedIndex = -1;
            this.cam.Size = new System.Drawing.Size(248, 30);
            this.cam.TabIndex = 85;
            this.cam.Visible = false;
            // 
            // scan
            // 
            this.scan.BaseColor = System.Drawing.Color.White;
            this.scan.Location = new System.Drawing.Point(138, 59);
            this.scan.Name = "scan";
            this.scan.Size = new System.Drawing.Size(248, 60);
            this.scan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.scan.TabIndex = 84;
            this.scan.TabStop = false;
            this.scan.Visible = false;
            // 
            // chgtxt
            // 
            this.chgtxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.chgtxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.chgtxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chgtxt.characterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.chgtxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.chgtxt.Enabled = false;
            this.chgtxt.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.chgtxt.ForeColor = System.Drawing.Color.Black;
            this.chgtxt.HintForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chgtxt.HintText = "Customer Change";
            this.chgtxt.isPassword = false;
            this.chgtxt.LineFocusedColor = System.Drawing.Color.Blue;
            this.chgtxt.LineIdleColor = System.Drawing.Color.Gray;
            this.chgtxt.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.chgtxt.LineThickness = 3;
            this.chgtxt.Location = new System.Drawing.Point(138, 241);
            this.chgtxt.Margin = new System.Windows.Forms.Padding(4);
            this.chgtxt.MaxLength = 32767;
            this.chgtxt.Name = "chgtxt";
            this.chgtxt.Size = new System.Drawing.Size(248, 30);
            this.chgtxt.TabIndex = 82;
            this.chgtxt.Text = "0";
            this.chgtxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.chgtxt.Visible = false;
            // 
            // fntxt
            // 
            this.fntxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.fntxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.fntxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fntxt.characterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.fntxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fntxt.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.fntxt.ForeColor = System.Drawing.Color.Black;
            this.fntxt.HintForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.fntxt.HintText = "First Name";
            this.fntxt.isPassword = false;
            this.fntxt.LineFocusedColor = System.Drawing.Color.Blue;
            this.fntxt.LineIdleColor = System.Drawing.Color.Gray;
            this.fntxt.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.fntxt.LineThickness = 3;
            this.fntxt.Location = new System.Drawing.Point(138, 467);
            this.fntxt.Margin = new System.Windows.Forms.Padding(4);
            this.fntxt.MaxLength = 32767;
            this.fntxt.Name = "fntxt";
            this.fntxt.Size = new System.Drawing.Size(248, 30);
            this.fntxt.TabIndex = 81;
            this.fntxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // sntxt
            // 
            this.sntxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.sntxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.sntxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sntxt.characterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.sntxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.sntxt.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.sntxt.ForeColor = System.Drawing.Color.Black;
            this.sntxt.HintForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.sntxt.HintText = "Surname";
            this.sntxt.isPassword = false;
            this.sntxt.LineFocusedColor = System.Drawing.Color.Blue;
            this.sntxt.LineIdleColor = System.Drawing.Color.Gray;
            this.sntxt.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.sntxt.LineThickness = 3;
            this.sntxt.Location = new System.Drawing.Point(682, 472);
            this.sntxt.Margin = new System.Windows.Forms.Padding(4);
            this.sntxt.MaxLength = 32767;
            this.sntxt.Name = "sntxt";
            this.sntxt.Size = new System.Drawing.Size(248, 30);
            this.sntxt.TabIndex = 80;
            this.sntxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // bunifuLabel15
            // 
            this.bunifuLabel15.AutoEllipsis = false;
            this.bunifuLabel15.CursorType = null;
            this.bunifuLabel15.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.bunifuLabel15.Location = new System.Drawing.Point(5, 242);
            this.bunifuLabel15.Name = "bunifuLabel15";
            this.bunifuLabel15.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel15.Size = new System.Drawing.Size(124, 21);
            this.bunifuLabel15.TabIndex = 79;
            this.bunifuLabel15.Text = "Customer Change";
            this.bunifuLabel15.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuLabel15.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            this.bunifuLabel15.Visible = false;
            // 
            // bunifuLabel14
            // 
            this.bunifuLabel14.AutoEllipsis = false;
            this.bunifuLabel14.CursorType = null;
            this.bunifuLabel14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.bunifuLabel14.Location = new System.Drawing.Point(28, 472);
            this.bunifuLabel14.Name = "bunifuLabel14";
            this.bunifuLabel14.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel14.Size = new System.Drawing.Size(76, 21);
            this.bunifuLabel14.TabIndex = 78;
            this.bunifuLabel14.Text = "First Name";
            this.bunifuLabel14.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuLabel14.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // bunifuLabel12
            // 
            this.bunifuLabel12.AutoEllipsis = false;
            this.bunifuLabel12.CursorType = null;
            this.bunifuLabel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.bunifuLabel12.Location = new System.Drawing.Point(560, 475);
            this.bunifuLabel12.Name = "bunifuLabel12";
            this.bunifuLabel12.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel12.Size = new System.Drawing.Size(63, 21);
            this.bunifuLabel12.TabIndex = 77;
            this.bunifuLabel12.Text = "Surname";
            this.bunifuLabel12.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuLabel12.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // pntxt
            // 
            this.pntxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.pntxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.pntxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pntxt.characterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.pntxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.pntxt.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.pntxt.ForeColor = System.Drawing.Color.Black;
            this.pntxt.HintForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pntxt.HintText = "Search for Medicine Name.";
            this.pntxt.isPassword = false;
            this.pntxt.LineFocusedColor = System.Drawing.Color.Blue;
            this.pntxt.LineIdleColor = System.Drawing.Color.Gray;
            this.pntxt.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.pntxt.LineThickness = 3;
            this.pntxt.Location = new System.Drawing.Point(682, 52);
            this.pntxt.Margin = new System.Windows.Forms.Padding(4);
            this.pntxt.MaxLength = 32767;
            this.pntxt.Name = "pntxt";
            this.pntxt.Size = new System.Drawing.Size(248, 30);
            this.pntxt.TabIndex = 72;
            this.pntxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.pntxt.OnValueChanged += new System.EventHandler(this.pntxt_OnValueChanged);
            this.pntxt.Enter += new System.EventHandler(this.pntxt_Enter);
            this.pntxt.Leave += new System.EventHandler(this.pntxt_Leave);
            // 
            // bunifuLabel10
            // 
            this.bunifuLabel10.AutoEllipsis = false;
            this.bunifuLabel10.CursorType = null;
            this.bunifuLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.bunifuLabel10.Location = new System.Drawing.Point(560, 56);
            this.bunifuLabel10.Name = "bunifuLabel10";
            this.bunifuLabel10.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel10.Size = new System.Drawing.Size(99, 21);
            this.bunifuLabel10.TabIndex = 71;
            this.bunifuLabel10.Text = "Product Name";
            this.bunifuLabel10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuLabel10.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // bunifuGradientPanel1
            // 
            this.bunifuGradientPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuGradientPanel1.BackgroundImage")));
            this.bunifuGradientPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuGradientPanel1.Controls.Add(this.gunaControlBox1);
            this.bunifuGradientPanel1.Controls.Add(this.bunifuLabel1);
            this.bunifuGradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bunifuGradientPanel1.GradientBottomLeft = System.Drawing.Color.White;
            this.bunifuGradientPanel1.GradientBottomRight = System.Drawing.Color.White;
            this.bunifuGradientPanel1.GradientTopLeft = System.Drawing.Color.White;
            this.bunifuGradientPanel1.GradientTopRight = System.Drawing.Color.White;
            this.bunifuGradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.bunifuGradientPanel1.Name = "bunifuGradientPanel1";
            this.bunifuGradientPanel1.Quality = 10;
            this.bunifuGradientPanel1.Size = new System.Drawing.Size(943, 49);
            this.bunifuGradientPanel1.TabIndex = 88;
            // 
            // gunaControlBox1
            // 
            this.gunaControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gunaControlBox1.AnimationHoverSpeed = 0.07F;
            this.gunaControlBox1.AnimationSpeed = 0.03F;
            this.gunaControlBox1.IconColor = System.Drawing.Color.Black;
            this.gunaControlBox1.IconSize = 15F;
            this.gunaControlBox1.Location = new System.Drawing.Point(886, 16);
            this.gunaControlBox1.Name = "gunaControlBox1";
            this.gunaControlBox1.OnHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.gunaControlBox1.OnHoverIconColor = System.Drawing.Color.White;
            this.gunaControlBox1.OnPressedColor = System.Drawing.Color.Black;
            this.gunaControlBox1.Size = new System.Drawing.Size(45, 25);
            this.gunaControlBox1.TabIndex = 1;
            this.gunaControlBox1.Click += new System.EventHandler(this.gunaControlBox1_Click);
            // 
            // bunifuLabel1
            // 
            this.bunifuLabel1.AutoEllipsis = false;
            this.bunifuLabel1.AutoSize = false;
            this.bunifuLabel1.CursorType = null;
            this.bunifuLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.bunifuLabel1.Location = new System.Drawing.Point(22, 12);
            this.bunifuLabel1.Name = "bunifuLabel1";
            this.bunifuLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel1.Size = new System.Drawing.Size(99, 25);
            this.bunifuLabel1.TabIndex = 0;
            this.bunifuLabel1.Text = "New Quotation";
            this.bunifuLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuLabel1.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // addsalbtn
            // 
            this.addsalbtn.Active = false;
            this.addsalbtn.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.addsalbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.addsalbtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.addsalbtn.BorderRadius = 0;
            this.addsalbtn.ButtonText = "Create";
            this.addsalbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addsalbtn.DisabledColor = System.Drawing.Color.Gray;
            this.addsalbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addsalbtn.Iconcolor = System.Drawing.Color.Transparent;
            this.addsalbtn.Iconimage = null;
            this.addsalbtn.Iconimage_right = null;
            this.addsalbtn.Iconimage_right_Selected = null;
            this.addsalbtn.Iconimage_Selected = null;
            this.addsalbtn.IconMarginLeft = 0;
            this.addsalbtn.IconMarginRight = 0;
            this.addsalbtn.IconRightVisible = true;
            this.addsalbtn.IconRightZoom = 0D;
            this.addsalbtn.IconVisible = true;
            this.addsalbtn.IconZoom = 70D;
            this.addsalbtn.IsTab = false;
            this.addsalbtn.Location = new System.Drawing.Point(13, 542);
            this.addsalbtn.Margin = new System.Windows.Forms.Padding(4);
            this.addsalbtn.Name = "addsalbtn";
            this.addsalbtn.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.addsalbtn.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.addsalbtn.OnHoverTextColor = System.Drawing.Color.White;
            this.addsalbtn.selected = false;
            this.addsalbtn.Size = new System.Drawing.Size(77, 34);
            this.addsalbtn.TabIndex = 89;
            this.addsalbtn.Text = "Create";
            this.addsalbtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.addsalbtn.Textcolor = System.Drawing.Color.White;
            this.addsalbtn.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addsalbtn.Click += new System.EventHandler(this.addsalbtn_Click);
            // 
            // pnotxt
            // 
            this.pnotxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.pnotxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.pnotxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnotxt.characterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.pnotxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.pnotxt.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.pnotxt.ForeColor = System.Drawing.Color.Black;
            this.pnotxt.HintForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnotxt.HintText = "Phone Number";
            this.pnotxt.isPassword = false;
            this.pnotxt.LineFocusedColor = System.Drawing.Color.Blue;
            this.pnotxt.LineIdleColor = System.Drawing.Color.Gray;
            this.pnotxt.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.pnotxt.LineThickness = 3;
            this.pnotxt.Location = new System.Drawing.Point(139, 506);
            this.pnotxt.Margin = new System.Windows.Forms.Padding(4);
            this.pnotxt.MaxLength = 32767;
            this.pnotxt.Name = "pnotxt";
            this.pnotxt.Size = new System.Drawing.Size(248, 30);
            this.pnotxt.TabIndex = 92;
            this.pnotxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.pnotxt.Leave += new System.EventHandler(this.pnotxt_Leave);
            // 
            // bunifuLabel2
            // 
            this.bunifuLabel2.AutoEllipsis = false;
            this.bunifuLabel2.CursorType = null;
            this.bunifuLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.bunifuLabel2.Location = new System.Drawing.Point(28, 508);
            this.bunifuLabel2.Name = "bunifuLabel2";
            this.bunifuLabel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel2.Size = new System.Drawing.Size(103, 21);
            this.bunifuLabel2.TabIndex = 91;
            this.bunifuLabel2.Text = "Phone Number";
            this.bunifuLabel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuLabel2.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // Totallbl
            // 
            this.Totallbl.AutoEllipsis = false;
            this.Totallbl.AutoSize = false;
            this.Totallbl.CursorType = null;
            this.Totallbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.Totallbl.Location = new System.Drawing.Point(682, 508);
            this.Totallbl.Name = "Totallbl";
            this.Totallbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Totallbl.Size = new System.Drawing.Size(114, 25);
            this.Totallbl.TabIndex = 98;
            this.Totallbl.Text = null;
            this.Totallbl.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.Totallbl.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // bunifuLabel4
            // 
            this.bunifuLabel4.AutoEllipsis = false;
            this.bunifuLabel4.AutoSize = false;
            this.bunifuLabel4.CursorType = null;
            this.bunifuLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.bunifuLabel4.Location = new System.Drawing.Point(560, 508);
            this.bunifuLabel4.Name = "bunifuLabel4";
            this.bunifuLabel4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel4.Size = new System.Drawing.Size(98, 25);
            this.bunifuLabel4.TabIndex = 97;
            this.bunifuLabel4.Text = "Total Price";
            this.bunifuLabel4.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.bunifuLabel4.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // gunaAdvenceButton5
            // 
            this.gunaAdvenceButton5.AnimationHoverSpeed = 0.07F;
            this.gunaAdvenceButton5.AnimationSpeed = 0.03F;
            this.gunaAdvenceButton5.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.gunaAdvenceButton5.BorderColor = System.Drawing.Color.Black;
            this.gunaAdvenceButton5.CheckedBaseColor = System.Drawing.Color.Gray;
            this.gunaAdvenceButton5.CheckedBorderColor = System.Drawing.Color.Black;
            this.gunaAdvenceButton5.CheckedForeColor = System.Drawing.Color.White;
            this.gunaAdvenceButton5.CheckedImage = ((System.Drawing.Image)(resources.GetObject("gunaAdvenceButton5.CheckedImage")));
            this.gunaAdvenceButton5.CheckedLineColor = System.Drawing.Color.DimGray;
            this.gunaAdvenceButton5.DialogResult = System.Windows.Forms.DialogResult.None;
            this.gunaAdvenceButton5.FocusedColor = System.Drawing.Color.Empty;
            this.gunaAdvenceButton5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gunaAdvenceButton5.ForeColor = System.Drawing.Color.White;
            this.gunaAdvenceButton5.Image = null;
            this.gunaAdvenceButton5.ImageSize = new System.Drawing.Size(30, 30);
            this.gunaAdvenceButton5.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.gunaAdvenceButton5.Location = new System.Drawing.Point(814, 542);
            this.gunaAdvenceButton5.Name = "gunaAdvenceButton5";
            this.gunaAdvenceButton5.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.gunaAdvenceButton5.OnHoverBorderColor = System.Drawing.Color.Black;
            this.gunaAdvenceButton5.OnHoverForeColor = System.Drawing.Color.White;
            this.gunaAdvenceButton5.OnHoverImage = null;
            this.gunaAdvenceButton5.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.gunaAdvenceButton5.OnPressedColor = System.Drawing.Color.Black;
            this.gunaAdvenceButton5.Size = new System.Drawing.Size(116, 29);
            this.gunaAdvenceButton5.TabIndex = 102;
            this.gunaAdvenceButton5.Text = "Remove";
            this.gunaAdvenceButton5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.gunaAdvenceButton5.Click += new System.EventHandler(this.gunaAdvenceButton5_Click);
            // 
            // bunifuFlatButton1
            // 
            this.bunifuFlatButton1.Active = false;
            this.bunifuFlatButton1.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.bunifuFlatButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.bunifuFlatButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuFlatButton1.BorderRadius = 0;
            this.bunifuFlatButton1.ButtonText = "Update";
            this.bunifuFlatButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuFlatButton1.DisabledColor = System.Drawing.Color.Gray;
            this.bunifuFlatButton1.Enabled = false;
            this.bunifuFlatButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuFlatButton1.Iconcolor = System.Drawing.Color.Transparent;
            this.bunifuFlatButton1.Iconimage = null;
            this.bunifuFlatButton1.Iconimage_right = null;
            this.bunifuFlatButton1.Iconimage_right_Selected = null;
            this.bunifuFlatButton1.Iconimage_Selected = null;
            this.bunifuFlatButton1.IconMarginLeft = 0;
            this.bunifuFlatButton1.IconMarginRight = 0;
            this.bunifuFlatButton1.IconRightVisible = true;
            this.bunifuFlatButton1.IconRightZoom = 0D;
            this.bunifuFlatButton1.IconVisible = true;
            this.bunifuFlatButton1.IconZoom = 70D;
            this.bunifuFlatButton1.IsTab = false;
            this.bunifuFlatButton1.Location = new System.Drawing.Point(122, 542);
            this.bunifuFlatButton1.Margin = new System.Windows.Forms.Padding(4);
            this.bunifuFlatButton1.Name = "bunifuFlatButton1";
            this.bunifuFlatButton1.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.bunifuFlatButton1.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.bunifuFlatButton1.OnHoverTextColor = System.Drawing.Color.White;
            this.bunifuFlatButton1.selected = false;
            this.bunifuFlatButton1.Size = new System.Drawing.Size(77, 34);
            this.bunifuFlatButton1.TabIndex = 103;
            this.bunifuFlatButton1.Text = "Update";
            this.bunifuFlatButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuFlatButton1.Textcolor = System.Drawing.Color.White;
            this.bunifuFlatButton1.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuFlatButton1.Visible = false;
            this.bunifuFlatButton1.Click += new System.EventHandler(this.bunifuFlatButton1_Click);
            // 
            // ttlvatlbl
            // 
            this.ttlvatlbl.AutoEllipsis = false;
            this.ttlvatlbl.AutoSize = false;
            this.ttlvatlbl.CursorType = null;
            this.ttlvatlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.ttlvatlbl.Location = new System.Drawing.Point(667, 555);
            this.ttlvatlbl.Name = "ttlvatlbl";
            this.ttlvatlbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ttlvatlbl.Size = new System.Drawing.Size(114, 21);
            this.ttlvatlbl.TabIndex = 110;
            this.ttlvatlbl.Text = null;
            this.ttlvatlbl.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.ttlvatlbl.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            this.ttlvatlbl.Visible = false;
            // 
            // viewprod
            // 
            this.viewprod.AllowUserToAddRows = false;
            this.viewprod.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.viewprod.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.viewprod.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.viewprod.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.viewprod.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.viewprod.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.viewprod.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.viewprod.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.viewprod.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pn,
            this.qtt,
            this.Column2,
            this.Column1,
            this.tp});
            this.viewprod.DoubleBuffered = true;
            this.viewprod.EnableHeadersVisualStyles = false;
            this.viewprod.HeaderBgColor = System.Drawing.Color.Gainsboro;
            this.viewprod.HeaderForeColor = System.Drawing.Color.Black;
            this.viewprod.Location = new System.Drawing.Point(28, 86);
            this.viewprod.Name = "viewprod";
            this.viewprod.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.viewprod.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.viewprod.Size = new System.Drawing.Size(902, 374);
            this.viewprod.TabIndex = 111;
            this.viewprod.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.viewprod_CellEndEdit);
            this.viewprod.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.viewprod_CellLeave);
            // 
            // pn
            // 
            this.pn.HeaderText = "Medicine Name";
            this.pn.Name = "pn";
            this.pn.ReadOnly = true;
            // 
            // qtt
            // 
            this.qtt.HeaderText = "Quantity";
            this.qtt.Name = "qtt";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Price";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Total Vat";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // tp
            // 
            this.tp.HeaderText = "Total Price";
            this.tp.Name = "tp";
            this.tp.ReadOnly = true;
            // 
            // Quatation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 583);
            this.Controls.Add(this.viewprod);
            this.Controls.Add(this.ttlvatlbl);
            this.Controls.Add(this.bunifuFlatButton1);
            this.Controls.Add(this.gunaAdvenceButton5);
            this.Controls.Add(this.Totallbl);
            this.Controls.Add(this.bunifuLabel4);
            this.Controls.Add(this.pnotxt);
            this.Controls.Add(this.bunifuLabel2);
            this.Controls.Add(this.addsalbtn);
            this.Controls.Add(this.bunifuGradientPanel1);
            this.Controls.Add(this.bctxt);
            this.Controls.Add(this.bunifuLabel9);
            this.Controls.Add(this.cam);
            this.Controls.Add(this.scan);
            this.Controls.Add(this.chgtxt);
            this.Controls.Add(this.fntxt);
            this.Controls.Add(this.sntxt);
            this.Controls.Add(this.bunifuLabel15);
            this.Controls.Add(this.bunifuLabel14);
            this.Controls.Add(this.bunifuLabel12);
            this.Controls.Add(this.pntxt);
            this.Controls.Add(this.bunifuLabel10);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Quatation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quatation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Quatation_FormClosing);
            this.Load += new System.EventHandler(this.Quatation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.scan)).EndInit();
            this.bunifuGradientPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewprod)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal Bunifu.Framework.UI.BunifuMaterialTextbox bctxt;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel9;
        internal Bunifu.Framework.UI.BunifuDropdown cam;
        private Guna.UI.WinForms.GunaPictureBox scan;
        internal Bunifu.Framework.UI.BunifuMaterialTextbox chgtxt;
        internal Bunifu.Framework.UI.BunifuMaterialTextbox fntxt;
        internal Bunifu.Framework.UI.BunifuMaterialTextbox sntxt;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel15;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel14;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel12;
        internal Bunifu.Framework.UI.BunifuMaterialTextbox pntxt;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel10;
        private Bunifu.Framework.UI.BunifuGradientPanel bunifuGradientPanel1;
        private Guna.UI.WinForms.GunaControlBox gunaControlBox1;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel1;
        internal Bunifu.Framework.UI.BunifuFlatButton addsalbtn;
        internal Bunifu.Framework.UI.BunifuMaterialTextbox pnotxt;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel2;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel4;
        private Guna.UI.WinForms.GunaAdvenceButton gunaAdvenceButton5;
        internal Bunifu.UI.WinForms.BunifuLabel Totallbl;
        internal Bunifu.Framework.UI.BunifuFlatButton bunifuFlatButton1;
        private Bunifu.UI.WinForms.BunifuLabel ttlvatlbl;
        private Bunifu.Framework.UI.BunifuCustomDataGrid viewprod;
        private System.Windows.Forms.DataGridViewTextBoxColumn pn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qtt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn tp;
    }
}