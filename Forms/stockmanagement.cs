using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sperp.Forms
{
    public partial class stockmanagement : Form
    {
        MySqlConnection mySqlConnection = new MySqlConnection("datasource=localhost;port=3306;user=root;password=root;charset=utf8;");
        public stockmanagement()
        {
            NativeTabControl1 = new NativeTabControl();
            InitializeComponent();
            NativeTabControl1.AssignHandle(this.tabControl1.Handle);
        }

        private void stockmanagement_Load(object sender, EventArgs e)
        {

            // TODO: 這行程式碼會將資料載入 'serpdataset.stockdt' 資料表。您可以視需要進行移動或移除。
            this.stockdtTableAdapter.Fill(this.serpdataset.stockdt);
            // TODO: 這行程式碼會將資料載入 'serpdataset.stock' 資料表。您可以視需要進行移動或移除。
            this.stockTableAdapter.Fill(this.serpdataset.stock);

        }

        private NativeTabControl NativeTabControl1;
        private class NativeTabControl : NativeWindow
        {

            protected override void WndProc(ref Message m)
            {
                if ((m.Msg == TCM_ADJUSTRECT))
                {
                    RECT rc = (RECT)m.GetLParam(typeof(RECT));
                    //Adjust these values to suit, dependant upon Appearance
                    rc.Left -= 3;
                    rc.Right += 3;
                    rc.Top -= 5;
                    rc.Bottom += 3;
                    Marshal.StructureToPtr(rc, m.LParam, true);
                }
                base.WndProc(ref m);
            }

            private const Int32 TCM_FIRST = 0x1300;
            private const Int32 TCM_ADJUSTRECT = (TCM_FIRST + 40);
            private struct RECT
            {
                public Int32 Left;
                public Int32 Top;
                public Int32 Right;
                public Int32 Bottom;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            dateTimePicker1.Value = DateTime.Today;
            tabControl1.SelectedTab = tabPage2;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string indata = "insert into sperp.stockdt(class,items,cost,qty,unit,unitcost,supplier,buydata,remark) " +
                            "VALUES('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','"
                            + textBox7.Text + "','" + textBox8.Text+ "','"+dateTimePicker1.Value.ToString("d")+"','"+textBox9.Text+"');";
            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand(indata, mySqlConnection);
            try
            {
                cmd.ExecuteNonQuery();
                this.stockdtTableAdapter.Fill(this.serpdataset.stockdt);
                indata = "insert into sperp.stock(class,items,qty,unit,unitcost,supplier,buydata,remark) " +
                    "value('" + textBox2.Text + "','" + textBox3.Text + "',qty+" + textBox5.Text + ",'" + textBox6.Text + "','"
                            + textBox7.Text + "','" + textBox8.Text + "','" + dateTimePicker1.Value.ToString("d") + "','" + textBox9.Text + "')" +
                            "on duplicate key update `qty`=qty+"+textBox5.Text+ ",`unit`='" +textBox6.Text+ "',`unitcost`='" + textBox7.Text + "';";
                cmd.CommandText = indata;
                try
                {
                    cmd.ExecuteNonQuery();
                    this.stockTableAdapter.Fill(this.serpdataset.stock);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("請填入資料!"+Environment.NewLine+ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            mySqlConnection.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void costchange(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBox4.Text)&&!string.IsNullOrEmpty(textBox5.Text))
            {
                textBox7.Text = Convert.ToInt32((Convert.ToDouble(textBox4.Text) / Convert.ToDouble(textBox5.Text))).ToString();
            }
            else
            {
                textBox7.Text = "0";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            DialogResult a=MessageBox.Show("確定是否刪除", "message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (a== System.Windows.Forms.DialogResult.OK)
            {
                string indata = "insert into sperp.stock(class,items,qty,unit,unitcost,supplier,buydata,remark) " +
                                "value('" + dataGridView2.CurrentRow.Cells[1].Value + "','" + dataGridView2.CurrentRow.Cells[2].Value + "',qty,'null',unitcost,supplier,'1999/9/9',remark)" +
                                "on duplicate key update `qty`=qty-" + dataGridView2.CurrentRow.Cells[4].Value + ";";
                mySqlConnection.Open();
                MySqlCommand cmd = new MySqlCommand(indata, mySqlConnection);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                mySqlConnection.Close();

                stockdtBindingSource.RemoveCurrent();
                stockdtBindingSource.EndEdit();

                this.stockdtTableAdapter.Update(this.serpdataset.stockdt);
                this.stockTableAdapter.Fill(this.serpdataset.stock);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                this.stockTableAdapter.Fill(this.serpdataset.stock);
                stockBindingSource.DataSource = this.serpdataset.stock;
            }
            else
            {
                this.stockTableAdapter.serstock(this.serpdataset.stock,textBox1.Text);
                stockBindingSource.DataSource = this.serpdataset.stock;

            }
        }
    }
}
