namespace Pharmancy_Management_System
{
    partial class Category
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Category));
            this.gunaControlBox1 = new Guna.UI.WinForms.GunaControlBox();
            this.bunifuGradientPanel1 = new Bunifu.Framework.UI.BunifuGradientPanel();
            this.gunaControlBox2 = new Guna.UI.WinForms.GunaControlBox();
            this.bunifuLabel1 = new Bunifu.UI.WinForms.BunifuLabel();
            this.natxt = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.bunifuLabel2 = new Bunifu.UI.WinForms.BunifuLabel();
            this.addpro = new Bunifu.Framework.UI.BunifuFlatButton();
            this.bunifuGradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gunaControlBox1
            // 
            this.gunaControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gunaControlBox1.AnimationHoverSpeed = 0.07F;
            this.gunaControlBox1.AnimationSpeed = 0.03F;
            this.gunaControlBox1.IconColor = System.Drawing.Color.Black;
            this.gunaControlBox1.IconSize = 15F;
            this.gunaControlBox1.Location = new System.Drawing.Point(353, 6);
            this.gunaControlBox1.Name = "gunaControlBox1";
            this.gunaControlBox1.OnHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.gunaControlBox1.OnHoverIconColor = System.Drawing.Color.White;
            this.gunaControlBox1.OnPressedColor = System.Drawing.Color.Black;
            this.gunaControlBox1.Size = new System.Drawing.Size(45, 25);
            this.gunaControlBox1.TabIndex = 1;
            // 
            // bunifuGradientPanel1
            // 
            this.bunifuGradientPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuGradientPanel1.BackgroundImage")));
            this.bunifuGradientPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuGradientPanel1.Controls.Add(this.gunaControlBox2);
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
            this.bunifuGradientPanel1.Size = new System.Drawing.Size(410, 39);
            this.bunifuGradientPanel1.TabIndex = 1;
            // 
            // gunaControlBox2
            // 
            this.gunaControlBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gunaControlBox2.AnimationHoverSpeed = 0.07F;
            this.gunaControlBox2.AnimationSpeed = 0.03F;
            this.gunaControlBox2.ControlBoxType = Guna.UI.WinForms.FormControlBoxType.MinimizeBox;
            this.gunaControlBox2.IconColor = System.Drawing.Color.Black;
            this.gunaControlBox2.IconSize = 15F;
            this.gunaControlBox2.Location = new System.Drawing.Point(302, 6);
            this.gunaControlBox2.Name = "gunaControlBox2";
            this.gunaControlBox2.OnHoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(58)))), ((int)(((byte)(183)))));
            this.gunaControlBox2.OnHoverIconColor = System.Drawing.Color.White;
            this.gunaControlBox2.OnPressedColor = System.Drawing.Color.Black;
            this.gunaControlBox2.Size = new System.Drawing.Size(45, 25);
            this.gunaControlBox2.TabIndex = 2;
            // 
            // bunifuLabel1
            // 
            this.bunifuLabel1.AutoEllipsis = false;
            this.bunifuLabel1.AutoSize = false;
            this.bunifuLabel1.CursorType = null;
            this.bunifuLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.bunifuLabel1.Location = new System.Drawing.Point(11, 8);
            this.bunifuLabel1.Name = "bunifuLabel1";
            this.bunifuLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel1.Size = new System.Drawing.Size(99, 25);
            this.bunifuLabel1.TabIndex = 0;
            this.bunifuLabel1.Text = "Category";
            this.bunifuLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuLabel1.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // natxt
            // 
            this.natxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.natxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.natxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.natxt.characterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.natxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.natxt.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.natxt.ForeColor = System.Drawing.Color.Black;
            this.natxt.HintForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.natxt.HintText = "Product Name";
            this.natxt.isPassword = false;
            this.natxt.LineFocusedColor = System.Drawing.Color.Blue;
            this.natxt.LineIdleColor = System.Drawing.Color.Gray;
            this.natxt.LineMouseHoverColor = System.Drawing.Color.Blue;
            this.natxt.LineThickness = 3;
            this.natxt.Location = new System.Drawing.Point(132, 43);
            this.natxt.Margin = new System.Windows.Forms.Padding(4);
            this.natxt.MaxLength = 32767;
            this.natxt.Name = "natxt";
            this.natxt.Size = new System.Drawing.Size(248, 30);
            this.natxt.TabIndex = 4;
            this.natxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // bunifuLabel2
            // 
            this.bunifuLabel2.AutoEllipsis = false;
            this.bunifuLabel2.CursorType = null;
            this.bunifuLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.bunifuLabel2.Location = new System.Drawing.Point(33, 50);
            this.bunifuLabel2.Name = "bunifuLabel2";
            this.bunifuLabel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel2.Size = new System.Drawing.Size(43, 21);
            this.bunifuLabel2.TabIndex = 3;
            this.bunifuLabel2.Text = "Name";
            this.bunifuLabel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuLabel2.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // addpro
            // 
            this.addpro.Active = false;
            this.addpro.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.addpro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.addpro.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.addpro.BorderRadius = 0;
            this.addpro.ButtonText = "Create";
            this.addpro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addpro.DisabledColor = System.Drawing.Color.Gray;
            this.addpro.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addpro.Iconcolor = System.Drawing.Color.Transparent;
            this.addpro.Iconimage = null;
            this.addpro.Iconimage_right = null;
            this.addpro.Iconimage_right_Selected = null;
            this.addpro.Iconimage_Selected = null;
            this.addpro.IconMarginLeft = 0;
            this.addpro.IconMarginRight = 0;
            this.addpro.IconRightVisible = true;
            this.addpro.IconRightZoom = 0D;
            this.addpro.IconVisible = true;
            this.addpro.IconZoom = 70D;
            this.addpro.IsTab = false;
            this.addpro.Location = new System.Drawing.Point(321, 78);
            this.addpro.Margin = new System.Windows.Forms.Padding(4);
            this.addpro.Name = "addpro";
            this.addpro.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.addpro.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.addpro.OnHoverTextColor = System.Drawing.Color.White;
            this.addpro.selected = false;
            this.addpro.Size = new System.Drawing.Size(77, 34);
            this.addpro.TabIndex = 18;
            this.addpro.Text = "Create";
            this.addpro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.addpro.Textcolor = System.Drawing.Color.White;
            this.addpro.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addpro.Click += new System.EventHandler(this.addpro_Click);
            // 
            // Category
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 116);
            this.Controls.Add(this.addpro);
            this.Controls.Add(this.natxt);
            this.Controls.Add(this.bunifuLabel2);
            this.Controls.Add(this.bunifuGradientPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Category";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Category";
            this.Load += new System.EventHandler(this.Category_Load);
            this.bunifuGradientPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI.WinForms.GunaControlBox gunaControlBox1;
        private Bunifu.Framework.UI.BunifuGradientPanel bunifuGradientPanel1;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel1;
        internal Bunifu.Framework.UI.BunifuMaterialTextbox natxt;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel2;
        internal Bunifu.Framework.UI.BunifuFlatButton addpro;
        private Guna.UI.WinForms.GunaControlBox gunaControlBox2;
    }
}