namespace Sperp
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panelmenu = new System.Windows.Forms.Panel();
            this.Financial = new FontAwesome.Sharp.IconButton();
            this.customermanagement = new FontAwesome.Sharp.IconButton();
            this.stockmanagement = new FontAwesome.Sharp.IconButton();
            this.productmanagement = new FontAwesome.Sharp.IconButton();
            this.orderlist = new FontAwesome.Sharp.IconButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.homepage = new FontAwesome.Sharp.IconPictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.titlelogo = new FontAwesome.Sharp.IconPictureBox();
            this.titlelabel = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.paneldesk = new System.Windows.Forms.Panel();
            this.iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelmenu.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.homepage)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.titlelogo)).BeginInit();
            this.paneldesk.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelmenu
            // 
            this.panelmenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.panelmenu.Controls.Add(this.tableLayoutPanel1);
            this.panelmenu.Controls.Add(this.Financial);
            this.panelmenu.Controls.Add(this.customermanagement);
            this.panelmenu.Controls.Add(this.stockmanagement);
            this.panelmenu.Controls.Add(this.productmanagement);
            this.panelmenu.Controls.Add(this.orderlist);
            this.panelmenu.Controls.Add(this.panel2);
            this.panelmenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelmenu.Location = new System.Drawing.Point(0, 0);
            this.panelmenu.Name = "panelmenu";
            this.panelmenu.Size = new System.Drawing.Size(150, 562);
            this.panelmenu.TabIndex = 0;
            // 
            // Financial
            // 
            this.Financial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.Financial.Dock = System.Windows.Forms.DockStyle.Top;
            this.Financial.FlatAppearance.BorderSize = 0;
            this.Financial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Financial.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Financial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(225)))), ((int)(((byte)(249)))));
            this.Financial.IconChar = FontAwesome.Sharp.IconChar.ChartLine;
            this.Financial.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(225)))), ((int)(((byte)(249)))));
            this.Financial.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Financial.IconSize = 28;
            this.Financial.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Financial.Location = new System.Drawing.Point(0, 340);
            this.Financial.Name = "Financial";
            this.Financial.Size = new System.Drawing.Size(150, 60);
            this.Financial.TabIndex = 5;
            this.Financial.Text = " 財務分析";
            this.Financial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Financial.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Financial.UseVisualStyleBackColor = false;
            this.Financial.Click += new System.EventHandler(this.iconButton1_Click);
            // 
            // customermanagement
            // 
            this.customermanagement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.customermanagement.Dock = System.Windows.Forms.DockStyle.Top;
            this.customermanagement.FlatAppearance.BorderSize = 0;
            this.customermanagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customermanagement.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.customermanagement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(225)))), ((int)(((byte)(249)))));
            this.customermanagement.IconChar = FontAwesome.Sharp.IconChar.AddressBook;
            this.customermanagement.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(225)))), ((int)(((byte)(249)))));
            this.customermanagement.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.customermanagement.IconSize = 28;
            this.customermanagement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.customermanagement.Location = new System.Drawing.Point(0, 280);
            this.customermanagement.Name = "customermanagement";
            this.customermanagement.Size = new System.Drawing.Size(150, 60);
            this.customermanagement.TabIndex = 4;
            this.customermanagement.Text = " 客戶管理";
            this.customermanagement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.customermanagement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.customermanagement.UseVisualStyleBackColor = false;
            this.customermanagement.Click += new System.EventHandler(this.iconButton5_Click);
            // 
            // stockmanagement
            // 
            this.stockmanagement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.stockmanagement.Dock = System.Windows.Forms.DockStyle.Top;
            this.stockmanagement.FlatAppearance.BorderSize = 0;
            this.stockmanagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stockmanagement.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stockmanagement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(225)))), ((int)(((byte)(249)))));
            this.stockmanagement.IconChar = FontAwesome.Sharp.IconChar.StackOverflow;
            this.stockmanagement.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(225)))), ((int)(((byte)(249)))));
            this.stockmanagement.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.stockmanagement.IconSize = 28;
            this.stockmanagement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.stockmanagement.Location = new System.Drawing.Point(0, 220);
            this.stockmanagement.Name = "stockmanagement";
            this.stockmanagement.Size = new System.Drawing.Size(150, 60);
            this.stockmanagement.TabIndex = 3;
            this.stockmanagement.Text = " 庫存管理";
            this.stockmanagement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.stockmanagement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.stockmanagement.UseVisualStyleBackColor = false;
            this.stockmanagement.Click += new System.EventHandler(this.iconButton4_Click);
            // 
            // productmanagement
            // 
            this.productmanagement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.productmanagement.Dock = System.Windows.Forms.DockStyle.Top;
            this.productmanagement.FlatAppearance.BorderSize = 0;
            this.productmanagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.productmanagement.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.productmanagement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(225)))), ((int)(((byte)(249)))));
            this.productmanagement.IconChar = FontAwesome.Sharp.IconChar.CookieBite;
            this.productmanagement.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(225)))), ((int)(((byte)(249)))));
            this.productmanagement.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.productmanagement.IconSize = 28;
            this.productmanagement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.productmanagement.Location = new System.Drawing.Point(0, 160);
            this.productmanagement.Name = "productmanagement";
            this.productmanagement.Size = new System.Drawing.Size(150, 60);
            this.productmanagement.TabIndex = 2;
            this.productmanagement.Text = " 商品管理";
            this.productmanagement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.productmanagement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.productmanagement.UseVisualStyleBackColor = false;
            this.productmanagement.Click += new System.EventHandler(this.iconButton3_Click);
            // 
            // orderlist
            // 
            this.orderlist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.orderlist.Dock = System.Windows.Forms.DockStyle.Top;
            this.orderlist.FlatAppearance.BorderSize = 0;
            this.orderlist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.orderlist.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.orderlist.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(225)))), ((int)(((byte)(249)))));
            this.orderlist.IconChar = FontAwesome.Sharp.IconChar.ClipboardList;
            this.orderlist.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(225)))), ((int)(((byte)(249)))));
            this.orderlist.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.orderlist.IconSize = 28;
            this.orderlist.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.orderlist.Location = new System.Drawing.Point(0, 100);
            this.orderlist.Name = "orderlist";
            this.orderlist.Size = new System.Drawing.Size(150, 60);
            this.orderlist.TabIndex = 1;
            this.orderlist.Text = " 訂單列表";
            this.orderlist.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.orderlist.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.orderlist.UseVisualStyleBackColor = false;
            this.orderlist.Click += new System.EventHandler(this.iconButton2_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.panel2.Controls.Add(this.homepage);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.MaximumSize = new System.Drawing.Size(150, 100);
            this.panel2.MinimumSize = new System.Drawing.Size(150, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(150, 100);
            this.panel2.TabIndex = 0;
            // 
            // homepage
            // 
            this.homepage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.homepage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.homepage.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.homepage.IconChar = FontAwesome.Sharp.IconChar.Home;
            this.homepage.IconColor = System.Drawing.SystemColors.ButtonFace;
            this.homepage.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.homepage.IconSize = 67;
            this.homepage.Location = new System.Drawing.Point(12, 27);
            this.homepage.Name = "homepage";
            this.homepage.Size = new System.Drawing.Size(88, 67);
            this.homepage.TabIndex = 0;
            this.homepage.TabStop = false;
            this.homepage.Click += new System.EventHandler(this.iconPictureBox1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.PapayaWhip;
            this.panel1.Controls.Add(this.titlelogo);
            this.panel1.Controls.Add(this.titlelabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(150, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1034, 60);
            this.panel1.TabIndex = 1;
            // 
            // titlelogo
            // 
            this.titlelogo.BackColor = System.Drawing.Color.PapayaWhip;
            this.titlelogo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.titlelogo.IconChar = FontAwesome.Sharp.IconChar.Home;
            this.titlelogo.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.titlelogo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.titlelogo.Location = new System.Drawing.Point(21, 12);
            this.titlelogo.Name = "titlelogo";
            this.titlelogo.Size = new System.Drawing.Size(32, 32);
            this.titlelogo.TabIndex = 2;
            this.titlelogo.TabStop = false;
            // 
            // titlelabel
            // 
            this.titlelabel.AutoSize = true;
            this.titlelabel.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.titlelabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.titlelabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.titlelabel.Location = new System.Drawing.Point(44, 20);
            this.titlelabel.Name = "titlelabel";
            this.titlelabel.Size = new System.Drawing.Size(51, 16);
            this.titlelabel.TabIndex = 1;
            this.titlelabel.Text = " 首頁";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(150, 60);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1034, 5);
            this.panel3.TabIndex = 2;
            // 
            // paneldesk
            // 
            this.paneldesk.BackColor = System.Drawing.Color.OldLace;
            this.paneldesk.Controls.Add(this.iconPictureBox1);
            this.paneldesk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paneldesk.Location = new System.Drawing.Point(150, 65);
            this.paneldesk.Name = "paneldesk";
            this.paneldesk.Size = new System.Drawing.Size(1034, 497);
            this.paneldesk.TabIndex = 3;
            // 
            // iconPictureBox1
            // 
            this.iconPictureBox1.BackColor = System.Drawing.Color.OldLace;
            this.iconPictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.iconPictureBox1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.Home;
            this.iconPictureBox1.IconColor = System.Drawing.Color.CornflowerBlue;
            this.iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox1.IconSize = 203;
            this.iconPictureBox1.Location = new System.Drawing.Point(370, 132);
            this.iconPictureBox1.Name = "iconPictureBox1";
            this.iconPictureBox1.Size = new System.Drawing.Size(246, 203);
            this.iconPictureBox1.TabIndex = 1;
            this.iconPictureBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 532);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(150, 30);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(24, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 30);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ver:sperp_3 \r\ndata:20210115 Ed:Suyp";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1184, 562);
            this.Controls.Add(this.paneldesk);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelmenu);
            this.MaximumSize = new System.Drawing.Size(1500, 800);
            this.MinimumSize = new System.Drawing.Size(1200, 600);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelmenu.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.homepage)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.titlelogo)).EndInit();
            this.paneldesk.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelmenu;
        private FontAwesome.Sharp.IconButton Financial;
        private System.Windows.Forms.Panel panel2;
        private FontAwesome.Sharp.IconPictureBox homepage;
        private FontAwesome.Sharp.IconButton customermanagement;
        private FontAwesome.Sharp.IconButton stockmanagement;
        private FontAwesome.Sharp.IconButton productmanagement;
        private FontAwesome.Sharp.IconButton orderlist;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label titlelabel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel paneldesk;
        private FontAwesome.Sharp.IconPictureBox titlelogo;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
    }
}

