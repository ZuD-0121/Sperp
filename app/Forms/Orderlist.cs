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
using MySql.Data.MySqlClient;

namespace Sperp.Forms
{

    public partial class Orderlist : Form
    {
        private bool tc = true;
        private string ord = "";
        MySqlConnection mySqlConnection = new MySqlConnection("datasource=localhost;port=3306;user=root;password=root;charset=utf8;");

        public Orderlist()
        {
            NativeTabControl1 = new NativeTabControl();

            InitializeComponent();

            NativeTabControl1.AssignHandle(this.tabControl1.Handle);
            
        }

        private void Orderlist_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'serpdataset.customer' 資料表。您可以視需要進行移動或移除。
            this.customerTableAdapter.Fill(this.serpdataset.customer);
            // TODO: 這行程式碼會將資料載入 'serpdataset.discount' 資料表。您可以視需要進行移動或移除。
            this.discountTableAdapter.Fill(this.serpdataset.discount);
            comboBox4.DataSource = serpdataset.discount;
            comboBox4.DisplayMember = "discount";

            // TODO: 這行程式碼會將資料載入 'serpdataset1.productitemscache' 資料表。您可以視需要進行移動或移除。
            this.productitemscacheTableAdapter.Fill(this.serpdataset.productitemscache);

            // TODO: 這行程式碼會將資料載入 'serpdataset.subordercache' 資料表。您可以視需要進行移動或移除。
            this.subordercacheTableAdapter.Fill(this.serpdataset.subordercache);

            // TODO: 這行程式碼會將資料載入 'serpdataset.productclass' 資料表。您可以視需要進行移動或移除。
            this.productclassTableAdapter.Fill(this.serpdataset.productclass);
            comboBox1.DataSource = serpdataset.productclass;
            comboBox1.DisplayMember = "class";

            // TODO: 這行程式碼會將資料載入 'serpdataset.order' 資料表。您可以視需要進行移動或移除。
            this.orderTableAdapter.Fill(this.serpdataset.order);

            orderBindingSource.DataSource = this.serpdataset.order;

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

        private void Button1_Click(object sender, EventArgs e)
        {

            button3.Text = "取消訂單";
            button2.Text = "確認訂單";
            listclear();
            tabControl1.SelectedTab = tabPage2;
            
            tc = true;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(tc)
            {
                bool check = true;
                bool buyercheck = true;
                if(textBox2.Text=="")
                {
                    buyercheck = false;
                }    
                for(int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString() == textBox1.Text)
                        check = false;
                }
                if (textBox1.Text != "" && check && buyercheck)
                {
                    string indata = "insert into sperp.order(ordernumber,buyer,orderdata,paydata,shipdata,payable,phone,remarks,shipped,paid) values('"
                        + textBox1.Text + "','" + textBox2.Text + "','" + dateTimePicker1.Value.ToString("d") + "','" + dateTimePicker2.Value.ToString("d") + "','"
                        + dateTimePicker3.Value.ToString("d") + "','" + label13.Text + "','" + textBox3.Text + "','" + richTextBox1.Text + "','0','0')";
                    mySqlConnection.Open();
                    MySqlCommand cmd = new MySqlCommand(indata, mySqlConnection);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        this.orderTableAdapter.Fill(this.serpdataset.order);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {

                        indata = "insert into sperp.suborder(suborderid,ordernumber,class,items,qty,discount,amount) values("
                            + "'" + dataGridView2.Rows[i].Cells[0].Value + "','" + textBox1.Text
                            + "','" + dataGridView2.Rows[i].Cells[1].Value + "','" + dataGridView2.Rows[i].Cells[2].Value
                            + "','" + dataGridView2.Rows[i].Cells[3].Value + "','" + dataGridView2.Rows[i].Cells[4].Value
                            + "','" + dataGridView2.Rows[i].Cells[5].Value + "')";
                        cmd.CommandText = indata;
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    mySqlConnection.Close();
                    orderBindingSource.MoveLast();
                    tabControl1.SelectedTab = tabPage1;
                }
                else
                {
                    if(check && buyercheck)
                    MessageBox.Show("請填寫訂單編號", "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if(buyercheck)
                    MessageBox.Show("訂單編號重複，請重新填寫", "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    MessageBox.Show("請填寫訂購人", "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



            }
            else
            {
                string indata="use sperp;delete from sperp.suborder where ordernumber=" + ord + ";";

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

                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {

                    indata = "insert into sperp.suborder(suborderid,ordernumber,class,items,qty,discount,amount) values("
                        + "'" + dataGridView2.Rows[i].Cells[0].Value + "','" + textBox1.Text
                        + "','" + dataGridView2.Rows[i].Cells[1].Value + "','" + dataGridView2.Rows[i].Cells[2].Value
                        + "','" + dataGridView2.Rows[i].Cells[3].Value + "','" + dataGridView2.Rows[i].Cells[4].Value
                        + "','" + dataGridView2.Rows[i].Cells[5].Value + "')";
                    cmd.CommandText = indata;
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                indata = "UPDATE sperp.order SET ordernumber = '"+textBox1.Text+"',buyer='"+textBox2.Text+"', phone = '"+textBox3.Text+"' " +
                    ",orderdata='" +dateTimePicker1.Value.ToString("d")+"',paydata='"+dateTimePicker2.Value.ToString("d")+"',shipdata='"+
                    dateTimePicker3.Value.ToString("d")+ "',payable='" + label13.Text + "',remarks='"+richTextBox1.Text+"' WHERE(ordernumber = "+ord+")";
                cmd.CommandText = indata;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                mySqlConnection.Close();

                this.orderTableAdapter.Fill(this.serpdataset.order);

                tabControl1.SelectedTab = tabPage1;
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(tc)
            {
                tabControl1.SelectedTab = tabPage1;
            }
            else 
            {
                listclear();

               // this.subordercacheTableAdapter.Fill(this.serpdataset.subordercache);

                tabControl1.SelectedTab = tabPage1;
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count!=0)
            {
                button3.Text = "取消修改";
                button2.Text = "確認修改";

                textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                dateTimePicker1.Value = (DateTime)dataGridView1.CurrentRow.Cells[3].Value;
                dateTimePicker2.Value = (DateTime)dataGridView1.CurrentRow.Cells[4].Value;
                dateTimePicker3.Value = (DateTime)dataGridView1.CurrentRow.Cells[5].Value;
                richTextBox1.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                label13.Text = textBox3.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

                ord = textBox1.Text;

                try
                {
                    string indata = "use sperp;truncate table subordercache;ALTER TABLE `subordercache` AUTO_INCREMENT=1;";
                    mySqlConnection.Open();
                    MySqlCommand cmd = new MySqlCommand(indata, mySqlConnection);
                    cmd.ExecuteNonQuery();

                    /*
                      INSERT INTO subordercache(class,items,qty,discount,amount)SELECT productclass.class,productitems.items,suborder.qty,suborder.discount
                      ,suborder.amount from suborder,productclass,productitems where suborder.ordernumber="+textBox1.Text + " and productclass.id=suborder.class
                      and productitems.id=suborder.items;
                    */
                    /*
                    indata = "use sperp;INSERT INTO subordercache(id,class,items,qty,discount,amount)SELECT " +
                        "suborder.suborderid,productclass.class,productitems.items,suborder.qty,suborder.discount,suborder.amount from suborder,productitems,productclass " +
                        "where suborder.ordernumber=" +textBox1.Text + " and productitems.id=suborder.items and productclass.id = suborder.class;";
                    */
                    indata = "use sperp;INSERT INTO subordercache(id,class,items,qty,discount,amount)SELECT " +
                        "suborder.suborderid,suborder.class,suborder.items,suborder.qty,suborder.discount,suborder.amount from suborder " +
                        "where suborder.ordernumber=" + textBox1.Text + ";";

                    cmd.CommandText = indata;
                    cmd.ExecuteNonQuery();
                    mySqlConnection.Close();

                    this.subordercacheTableAdapter.Fill(this.serpdataset.subordercache);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                tabControl1.SelectedTab = tabPage2;
                tc = false;
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(orderBindingSource.Count!=0)
            {
                string indata = "use sperp;delete from sperp.suborder where ordernumber=" + dataGridView1.CurrentRow.Cells[1].Value+";";
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


                orderBindingSource.RemoveCurrent();
                orderBindingSource.EndEdit();

                orderTableAdapter.Update(this.serpdataset.order);
            }

        }

        private void comboBox1_Change(object sender, EventArgs e)
        {

            try
            {
                string indata = "use sperp;truncate table productitemscache;ALTER TABLE `subordercache` AUTO_INCREMENT=1;";
                mySqlConnection.Open();
                MySqlCommand cmd = new MySqlCommand(indata, mySqlConnection);
                cmd.ExecuteNonQuery();

                string productclassid = serpdataset.productclass.Rows[comboBox1.FindString(comboBox1.Text)].ItemArray[0].ToString() ;

                indata = "insert into sperp.productitemscache(items,selling) select product.product,product.sellingprice from sperp.product where class = " + productclassid + ";";
                cmd.CommandText = indata;
                cmd.ExecuteNonQuery();

                mySqlConnection.Close();

                this.productitemscacheTableAdapter.Fill(this.serpdataset.productitemscache);
                comboBox2.DataSource = serpdataset.productitemscache;
                comboBox2.DisplayMember = "items";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != null && comboBox2.Text != null && numericUpDown1.Value != 0)
            {
                int amout = Convert.ToInt32(serpdataset.productitemscache.Rows[comboBox2.FindString(comboBox2.Text)].ItemArray[2].ToString());
                int discount = Convert.ToInt32(serpdataset.discount.Rows[comboBox4.FindString(comboBox4.Text)].ItemArray[2].ToString());

                string indata = "insert into sperp.subordercache(class,items,qty,discount,amount) values(" +
                    "'" + comboBox1.Text + "','" + comboBox2.Text + "','" + numericUpDown1.Value + "','"
                      + comboBox4.Text + "','" + (amout * numericUpDown1.Value*discount/100) + "')";
                mySqlConnection.Open();
                MySqlCommand cmd = new MySqlCommand(indata, mySqlConnection);
                try
                {
                    cmd.ExecuteNonQuery();
                    this.subordercacheTableAdapter.Fill(this.serpdataset.subordercache);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                mySqlConnection.Close();

                int ttamout = 0;
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    ttamout += Convert.ToInt32(dataGridView2.Rows[i].Cells[5].Value);
                }
                label13.Text = ttamout.ToString();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (subordercacheBindingSource.Count != 0)
            {
                subordercacheBindingSource.RemoveCurrent();
                subordercacheBindingSource.EndEdit();

                string indata = "use sperp;truncate table subordercache;ALTER TABLE `subordercache` AUTO_INCREMENT=1;";
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

                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    indata = "insert into sperp.subordercache(class,items,qty,discount,amount) values(" +
                        "'" + dataGridView2.Rows[i].Cells[1].Value + "','" + dataGridView2.Rows[i].Cells[2].Value +
                        "','" + dataGridView2.Rows[i].Cells[3].Value + "','" + dataGridView2.Rows[i].Cells[4].Value +
                        "','" + dataGridView2.Rows[i].Cells[5].Value + "')";
                    cmd.CommandText = indata;
                    try
                    {                       
                        cmd.ExecuteNonQuery();                      
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                mySqlConnection.Close();

                this.subordercacheTableAdapter.Fill(this.serpdataset.subordercache);

                int ttamout = 0;
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    ttamout += Convert.ToInt32(dataGridView2.Rows[i].Cells[5].Value);
                }
                label13.Text = ttamout.ToString();
            }
        }

        private void listclear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            label13.Text = "0";
            richTextBox1.Text = "";
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = new DateTime(1999, 9, 9, 0, 0, 0);
            dateTimePicker3.Value = DateTime.Today;
            numericUpDown1.Value = 0;
            //dateTimePicker1.Enabled = false;
            string indata = "use sperp;truncate table subordercache;ALTER TABLE `subordercache` AUTO_INCREMENT=1;";
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
            this.subordercacheTableAdapter.Fill(this.serpdataset.subordercache);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                if (dataGridView1.CurrentRow.Cells[8].Value.ToString() == "1")
                {
                    dataGridView1.CurrentRow.Cells[8].Value = "0";
                    string indata = "update sperp.order set shipped='0' where(ordernumber='" + dataGridView1.CurrentRow.Cells[1].Value + "');";
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
                    this.orderTableAdapter.Fill(this.serpdataset.order);
                }
                else
                {
                    dataGridView1.CurrentRow.Cells[8].Value = "1";
                    string indata = "update sperp.order set shipped='1' where(ordernumber='" + dataGridView1.CurrentRow.Cells[1].Value + "');";
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
                    this.orderTableAdapter.Fill(this.serpdataset.order);
                }
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                if (dataGridView1.CurrentRow.Cells[9].Value.ToString() == "1")
                {
                    dataGridView1.CurrentRow.Cells[9].Value = "0";
                    string indata = "update sperp.order set paydata='" + ("1999/9/9") + "',paid='0' where(ordernumber='" + dataGridView1.CurrentRow.Cells[1].Value + "');";
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
                    this.orderTableAdapter.Fill(this.serpdataset.order);
                }
                else
                {
                    dataGridView1.CurrentRow.Cells[9].Value = "1";
                    string indata = "update sperp.order set paydata='" + DateTime.Now.ToString("d") + "',paid='1' where(ordernumber='" + dataGridView1.CurrentRow.Cells[1].Value + "');";
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
                    this.orderTableAdapter.Fill(this.serpdataset.order);
                }
            }
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                this.orderTableAdapter.Fill(this.serpdataset.order);
                orderBindingSource.DataSource = this.serpdataset.order;
            }
            else
            {
                this.orderTableAdapter.serorder(this.serpdataset.order,textBox4.Text);
                orderBindingSource.DataSource = this.serpdataset.order;

            }
        }

        private void orderlist(object sender, EventArgs e)
        {
            Form form2 = new Form();
            Button button1 = new Button();
            Button button2 = new Button();
            Panel panel = new System.Windows.Forms.Panel();
            form2.ClientSize= new System.Drawing.Size(800, 500);
            DataGridView orderlistview = new DataGridView();
            DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGridViewTextBoxColumn idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn genderDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn companyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn phomeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn addressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn emailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn remarksDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();

            panel.BackColor = System.Drawing.Color.OldLace;
            panel.Dock = System.Windows.Forms.DockStyle.Top;
            panel.Location = new System.Drawing.Point(0, 30);
            panel.Margin = new System.Windows.Forms.Padding(0);
            panel.Name = "panellist";
            panel.Size = new System.Drawing.Size(1076, 30);
            panel.TabIndex = 1;

            button1.Text = "OK";
            button2.Text = "Cancel";
            button2.Location = new Point(button1.Left+(2*button1.Width), button1.Location.Y);

            button1.DialogResult = DialogResult.OK;
            button2.DialogResult = DialogResult.Cancel;

            form2.Text = "choese";

            form2.FormBorderStyle = FormBorderStyle.FixedDialog;
            form2.AcceptButton = button1;
            form2.CancelButton = button2;
            form2.StartPosition = FormStartPosition.CenterScreen;

            orderlistview.AllowUserToAddRows = false;
            orderlistview.AllowUserToDeleteRows = false;
            orderlistview.AllowUserToResizeColumns = false;
            orderlistview.AllowUserToResizeRows = false;
            orderlistview.AutoGenerateColumns = false;
            orderlistview.BackgroundColor = System.Drawing.Color.OldLace;
            orderlistview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            orderlistview.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Wheat;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            orderlistview.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            orderlistview.ColumnHeadersHeight = 30;
            orderlistview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            orderlistview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            idDataGridViewTextBoxColumn,
            nameDataGridViewTextBoxColumn,
            genderDataGridViewTextBoxColumn,
            companyDataGridViewTextBoxColumn,
            phomeDataGridViewTextBoxColumn,
            addressDataGridViewTextBoxColumn,
            emailDataGridViewTextBoxColumn,
            remarksDataGridViewTextBoxColumn});
            orderlistview.DataSource = customerBindingSource;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.OldLace;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            orderlistview.DefaultCellStyle = dataGridViewCellStyle5;
            orderlistview.Dock = System.Windows.Forms.DockStyle.Fill;
            orderlistview.EnableHeadersVisualStyles = false;
            orderlistview.GridColor = System.Drawing.Color.WhiteSmoke;
            orderlistview.Location = new System.Drawing.Point(235, 0);
            orderlistview.Margin = new System.Windows.Forms.Padding(0);
            orderlistview.Name = "orderlistview";
            orderlistview.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            orderlistview.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            orderlistview.RowHeadersVisible = false;
            orderlistview.RowHeadersWidth = 30;
            orderlistview.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            orderlistview.RowTemplate.Height = 30;
            orderlistview.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            orderlistview.Size = new System.Drawing.Size(713, 450);
            orderlistview.TabIndex = 2;

            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "id";
            idDataGridViewTextBoxColumn.HeaderText = "ID";
            idDataGridViewTextBoxColumn.MinimumWidth = 50;
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            idDataGridViewTextBoxColumn.ReadOnly = true;
            idDataGridViewTextBoxColumn.Width = 50;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            nameDataGridViewTextBoxColumn.DataPropertyName = "name";
            nameDataGridViewTextBoxColumn.HeaderText = "聯絡人";
            nameDataGridViewTextBoxColumn.MinimumWidth = 100;
            nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // genderDataGridViewTextBoxColumn
            // 
            genderDataGridViewTextBoxColumn.DataPropertyName = "gender";
            genderDataGridViewTextBoxColumn.HeaderText = "性別";
            genderDataGridViewTextBoxColumn.MinimumWidth = 50;
            genderDataGridViewTextBoxColumn.Name = "genderDataGridViewTextBoxColumn";
            genderDataGridViewTextBoxColumn.ReadOnly = true;
            genderDataGridViewTextBoxColumn.Width = 50;
            // 
            // companyDataGridViewTextBoxColumn
            // 
            companyDataGridViewTextBoxColumn.DataPropertyName = "company";
            companyDataGridViewTextBoxColumn.HeaderText = "公司行號";
            companyDataGridViewTextBoxColumn.MinimumWidth = 120;
            companyDataGridViewTextBoxColumn.Name = "companyDataGridViewTextBoxColumn";
            companyDataGridViewTextBoxColumn.ReadOnly = true;
            companyDataGridViewTextBoxColumn.Width = 120;
            // 
            // phomeDataGridViewTextBoxColumn
            // 
            phomeDataGridViewTextBoxColumn.DataPropertyName = "phone";
            phomeDataGridViewTextBoxColumn.HeaderText = "聯絡電話";
            phomeDataGridViewTextBoxColumn.MinimumWidth = 120;
            phomeDataGridViewTextBoxColumn.Name = "phomeDataGridViewTextBoxColumn";
            phomeDataGridViewTextBoxColumn.ReadOnly = true;
            phomeDataGridViewTextBoxColumn.Width = 120;
            // 
            // addressDataGridViewTextBoxColumn
            // 
            addressDataGridViewTextBoxColumn.DataPropertyName = "address";
            addressDataGridViewTextBoxColumn.HeaderText = "地址";
            addressDataGridViewTextBoxColumn.MinimumWidth = 250;
            addressDataGridViewTextBoxColumn.Name = "addressDataGridViewTextBoxColumn";
            addressDataGridViewTextBoxColumn.ReadOnly = true;
            addressDataGridViewTextBoxColumn.Width = 250;
            // 
            // emailDataGridViewTextBoxColumn
            // 
            emailDataGridViewTextBoxColumn.DataPropertyName = "email";
            emailDataGridViewTextBoxColumn.HeaderText = "E-mail";
            emailDataGridViewTextBoxColumn.MinimumWidth = 200;
            emailDataGridViewTextBoxColumn.Name = "emailDataGridViewTextBoxColumn";
            emailDataGridViewTextBoxColumn.ReadOnly = true;
            emailDataGridViewTextBoxColumn.Width = 200;
            // 
            // remarksDataGridViewTextBoxColumn
            // 
            remarksDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            remarksDataGridViewTextBoxColumn.DataPropertyName = "remarks";
            remarksDataGridViewTextBoxColumn.HeaderText = "備註";
            remarksDataGridViewTextBoxColumn.MinimumWidth = 80;
            remarksDataGridViewTextBoxColumn.Name = "remarksDataGridViewTextBoxColumn";
            remarksDataGridViewTextBoxColumn.ReadOnly = true;
            //
            panel.Controls.Add(button1);
            panel.Controls.Add(button2);
            form2.Controls.Add(panel);
            form2.Controls.Add(orderlistview);
            orderlistview.BringToFront();


            form2.ShowDialog();
            if(form2.DialogResult==DialogResult.OK)
            {
                textBox2.Text = orderlistview.CurrentRow.Cells[1].Value.ToString();
                textBox3.Text = orderlistview.CurrentRow.Cells[4].Value.ToString();
                form2.Dispose();
            }
            else
            {
                form2.Dispose();
            }
        }

    }
}
