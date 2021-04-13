using FontAwesome.Sharp;
using System;
using System.Drawing;
using System.Windows.Forms;
using Sperp.Forms;
using System.Data;
using MySql.Data.MySqlClient;

namespace Sperp
{
    public partial class Form1 : Form
    {
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentCForm;

        public Form1()
        {

            InitializeComponent();
           

            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(5,60);
            panelmenu.Controls.Add(leftBorderBtn);


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bool result = false;
            MySqlConnection connection = new MySqlConnection("server=127.0.0.1;port=3306;user id=root;password=root;");
            try
            {
                connection.Open();
                result = true;
                connection.Close();
            }
            catch
            {
                result = false;
            }
            if (result == false)
            {
                MessageBox.Show("MySQL未連接");
                Application.Exit();
            }
        }



        //
        private struct RGBColor
        {
            public static Color color = Color.FromArgb(217, 225, 249);
        }

        private void AtvBtn(object SBtn,Color color)
        {
            if(SBtn != null)
            {
                DisBtn();
                //
                currentBtn = (IconButton)SBtn;
                currentBtn.BackColor = Color.FromArgb(28, 46, 100);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
                //
                titlelogo.IconChar = currentBtn.IconChar;
                titlelogo.IconColor = Color.FromArgb(29, 40, 87);
                titlelabel.Text = currentBtn.Text;
                titlelabel.ForeColor = Color.FromArgb(29, 40, 87);
            }
        }

        private void DisBtn()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(29, 40, 87);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void OpenForm(Form childForm)
        {
            if (currentCForm != null)
            {
                currentCForm.Close();
            }

            currentCForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            paneldesk.Controls.Add(childForm);
            paneldesk.Tag = childForm;
            childForm.BringToFront();
            childForm.BackColor = paneldesk.BackColor;
            childForm.Show();

        }


        //
        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            if (currentCForm != null)
            {
                currentCForm.Close();
            }


            DisBtn();
            leftBorderBtn.Visible = false;
            titlelogo.IconChar = IconChar.Home;
            titlelogo.IconColor = Color.FromArgb(29, 40, 87);
            titlelabel.Text = " 首頁";

        }


        private void iconButton1_Click(object sender, EventArgs e)
        {

            AtvBtn(sender, RGBColor.color);
            OpenForm(new Financial());

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

            AtvBtn(sender, RGBColor.color);
            OpenForm(new Orderlist());
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {

            AtvBtn(sender, RGBColor.color);
            OpenForm(new productmanagement());
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {

            AtvBtn(sender, RGBColor.color);
            OpenForm(new stockmanagement());
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {

            AtvBtn(sender, RGBColor.color);
            OpenForm(new customermanagement());
        }
    }
}
