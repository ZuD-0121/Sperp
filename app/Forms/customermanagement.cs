using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sperp.Forms
{
    public partial class customermanagement : Form
    {
        MySqlConnection mySqlConnection = new MySqlConnection("datasource=localhost;port=3306;user=root;password=root;charset=utf8;");
        public customermanagement()
        {

            InitializeComponent();

        }
        private void customermanagement_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'serpdataset.customer' 資料表。您可以視需要進行移動或移除。
            this.customerTableAdapter.Fill(this.serpdataset.customer);

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string indata = "insert into sperp.customer(name,company,phone,address,email,gender,remarks) " +
                           "VALUES('" + textBox2.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + richTextBox1.Text + "','" + richTextBox2.Text + "','"
                           + textBox3.Text + "','" + richTextBox3.Text + "');";
            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand(indata, mySqlConnection);
            try
            {
                cmd.ExecuteNonQuery();
                this.customerTableAdapter.Fill(this.serpdataset.customer);
            }
            catch (Exception ex)
            {
                MessageBox.Show("請填入資料，或填寫資料有誤!" + Environment.NewLine + ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            mySqlConnection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.RowCount != 0)
            {
                DialogResult a = MessageBox.Show("確定是否刪除", "message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (a == System.Windows.Forms.DialogResult.OK)
                {
                    customerBindingSource.RemoveCurrent();
                    customerBindingSource.EndEdit();

                    this.customerTableAdapter.Update(this.serpdataset.customer);

                }
            }

        }
    }
}
