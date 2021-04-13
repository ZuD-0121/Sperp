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
    public partial class productmanagement : Form
    {
        private bool tc = true;
        private string pid = "";
        MySqlConnection mySqlConnection = new MySqlConnection("datasource=localhost;port=3306;user=root;password=root;charset=utf8;");
        public productmanagement()
        {
            NativeTabControl1 = new NativeTabControl();
            InitializeComponent();
            NativeTabControl1.AssignHandle(this.tabControl1.Handle);
        }

        private void productmanagement_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'serpdataset.stock' 資料表。您可以視需要進行移動或移除。
            this.matTableAdapter.Fill(this.serpdataset.stock);

            // TODO: 這行程式碼會將資料載入 'serpdataset.productclass' 資料表。您可以視需要進行移動或移除。
            this.productclassTableAdapter.Fill(this.serpdataset.productclass);

            // TODO: 這行程式碼會將資料載入 'serpdataset.prodcompsncache' 資料表。您可以視需要進行移動或移除。
            this.prodcompsncacheTableAdapter.Fill(this.serpdataset.prodcompsncache);

            // TODO: 這行程式碼會將資料載入 'serpdataset.productcache' 資料表。您可以視需要進行移動或移除。
            this.productcacheTableAdapter.Fill(this.serpdataset.productcache);

            string indata = "use sperp; truncate table productcache; ALTER TABLE `productcache` AUTO_INCREMENT = 1";
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

            indata = "insert into sperp.productcache(id,class1,product,selling,salesvolume,grossprofit,netincome,prodevalvol,manhours,remarks) " +
                "select product.id,productclass.class,product.product,product.sellingprice,product.salesvolume,product.grossprofit,product.netincome," +
                "product.prodevalvol,product.manhours,product.remarks " +
                "from sperp.product,sperp.productclass where productclass.id=product.class;";
            cmd.CommandText = indata;
            try
            {
                cmd.ExecuteNonQuery();
                this.productcacheTableAdapter.Fill(this.serpdataset.productcache);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            mySqlConnection.Close();



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
            dataGridView2.Sort(dataGridView2.Columns[0], ListSortDirection.Ascending);

            comboBox1.DataSource = serpdataset.productclass;
            comboBox1.DisplayMember = "class";

            tabControl1.SelectedTab = tabPage2;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bool check = true;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Cells[1].Value.ToString() == textBox2.Text)
                    check = false;
            }

            if (textBox2.Text != "" && check)
            {
                string indata = "insert into sperp.productclass(class) values('"+textBox2.Text+"');";
                mySqlConnection.Open();
                MySqlCommand cmd = new MySqlCommand(indata, mySqlConnection);
                try
                {
                    cmd.ExecuteNonQuery();
                    this.productclassTableAdapter.Fill(this.serpdataset.productclass);
                    textBox2.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                mySqlConnection.Close();
            }
            else
            {
                MessageBox.Show("已有相同類別", "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string indata = "DELETE FROM sperp.productclass WHERE (class = '"+comboBox1.Text+"');";
            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand(indata, mySqlConnection);
            try
            {
                cmd.ExecuteNonQuery();
                this.productclassTableAdapter.Fill(this.serpdataset.productclass);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            mySqlConnection.Close();
        }

        private void updatalable2(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView2.Rows.Count!=0)
            {
                label2.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            }
           
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string indata = "UPDATE sperp.productclass SET class = '"+textBox3.Text+"' WHERE (`class` = '"+label2.Text+"');";
            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand(indata, mySqlConnection);
            bool check = true;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Cells[1].Value.ToString() == textBox3.Text)
                    check = false;
            }
            if (check)
            {
                try
                {
                    cmd.ExecuteNonQuery();
                    this.productclassTableAdapter.Fill(this.serpdataset.productclass);
                    label2.Text = "請選擇欲修改類別";
                    textBox3.Text = "";

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("已有相同類別", "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            mySqlConnection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tc = true;

            comboBox2.DataSource = serpdataset.productclass;
            comboBox2.DisplayMember = "class";

            button6.Text = "新增";
            textBox8.Text = ""; textBox8.Enabled = true;
            textBox9.Text = ""; textBox9.Enabled = true;
            textBox10.Text = "";textBox10.Enabled = true;
            textBox11.Text = ""; textBox12.Text = ""; textBox13.Text = ""; textBox15.Text = "";
            textBox16.Text = ""; textBox4.Text = "";  textBox5.Text = "";  textBox6.Text = "";
            textBox7.Text = ""; richTextBox1.Text = "";
            label19.Text = ""; label20.Text = ""; label21.Text = ""; label22.Text = ""; label23.Text = "";
            label24.Text = ""; label33.Text = ""; label34.Text = ""; label35.Text = ""; label36.Text = "";

            string indata = "use sperp;truncate table prodcompsncache;ALTER TABLE `prodcompsncache` AUTO_INCREMENT=1;";
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
            this.prodcompsncacheTableAdapter.Fill(this.serpdataset.prodcompsncache);

            tabControl1.SelectedTab = tabPage3;
        }


        private void button13_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count!=0)
            {
                textBox9.Enabled = false;
                textBox8.Enabled = false;
                textBox10.Enabled = false;
                button6.Text = "修改";

                tc = false;
                pid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string indata = "use sperp;truncate table prodcompsncache;ALTER TABLE `prodcompsncache` AUTO_INCREMENT=1;";
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
                indata = "INSERT INTO sperp.prodcompsncache (`material`, `volume`, `unit`, `unitcost`,`remarks`) select productcompsn.material," +
                    "productcompsn.volume,productcompsn.unit,productcompsn.unitcost,productcompsn.remarks" +
                    " from sperp.productcompsn where productid='" + dataGridView1.CurrentRow.Cells[0].Value + "';";
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
                this.prodcompsncacheTableAdapter.Fill(this.serpdataset.prodcompsncache);




                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "包裝")
                    {
                        textBox8.Text = dataGridView3.Rows[i].Cells[3].Value.ToString();
                        textBox9.Text = dataGridView3.Rows[i].Cells[2].Value.ToString();
                        textBox10.Text = dataGridView3.Rows[i].Cells[4].Value.ToString();
                    }

                }



                comboBox2.DataSource = serpdataset.productclass;
                comboBox2.DisplayMember = "class";
                comboBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox6.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                textBox7.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                richTextBox1.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                label24.Text = textBox8.Text;

                int n;
                if (int.TryParse(textBox5.Text, out n) && int.TryParse(textBox6.Text, out n) && int.TryParse(textBox7.Text, out n))
                {
                    label19.Text = textBox6.Text;
                    label22.Text = textBox5.Text;
                    label23.Text = textBox7.Text;

                    if (dataGridView3.Rows.Count != 0)
                    {
                        int value = 0;
                        for (int i = 0; i < dataGridView3.Rows.Count; i++)
                        {
                            value += Convert.ToInt32(dataGridView3.Rows[i].Cells[4].Value);
                        }
                        label20.Text = value.ToString();

                        if (Convert.ToInt32(label19.Text) != 0 && Convert.ToInt32(label20.Text) != 0)
                        {
                            label21.Text = (Convert.ToInt32(label20.Text) / Convert.ToInt32(label19.Text)).ToString();
                        }
                        label33.Text = ((Convert.ToInt32(label22.Text) * Convert.ToInt32(label19.Text)) - Convert.ToInt32(label21.Text)).ToString();
                        label34.Text = (Convert.ToInt32(label33.Text) * 100 / ((Convert.ToInt32(label22.Text) * Convert.ToInt32(label19.Text)))).ToString();
                        label35.Text = ((Convert.ToInt32(label22.Text) * Convert.ToInt32(label19.Text)) - Convert.ToInt32(label21.Text) - (210 * Convert.ToInt32(label23.Text))).ToString();
                        label36.Text = (Convert.ToInt32(label35.Text) * 100 / ((Convert.ToInt32(label22.Text) * Convert.ToInt32(label19.Text)))).ToString();

                    }
                }

                tabControl1.SelectedTab = tabPage3;
            }
            else
            {
                MessageBox.Show("沒有產品，請新增一個產品!", "message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button15_Click(object sender, EventArgs e)
        {
            int n;
            if (int.TryParse(textBox5.Text,out n)&&int.TryParse(textBox6.Text,out n)&&int.TryParse(textBox7.Text,out n))
            {
                label19.Text = textBox6.Text;
                label22.Text = textBox5.Text;
                label23.Text = textBox7.Text;

                if (dataGridView3.Rows.Count != 0)
                {
                    int value = 0;
                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        value += Convert.ToInt32(dataGridView3.Rows[i].Cells[4].Value);
                    }
                    label20.Text = value.ToString();

                    if (Convert.ToInt32(label19.Text) != 0 && Convert.ToInt32(label20.Text) != 0)
                    {
                        label21.Text = (Convert.ToInt32(label20.Text) / Convert.ToInt32(label19.Text)).ToString();
                    }
                    label33.Text = ((Convert.ToInt32(label22.Text) * Convert.ToInt32(label19.Text)) - Convert.ToInt32(label21.Text)).ToString();
                    label34.Text = (Convert.ToInt32(label33.Text) * 100 / ((Convert.ToInt32(label22.Text) * Convert.ToInt32(label19.Text)))).ToString();
                    label35.Text = ((Convert.ToInt32(label22.Text) * Convert.ToInt32(label19.Text)) - Convert.ToInt32(label21.Text) - (210 * Convert.ToInt32(label23.Text))).ToString();
                    label36.Text = (Convert.ToInt32(label35.Text) * 100 / ((Convert.ToInt32(label22.Text) * Convert.ToInt32(label19.Text)))).ToString();

                }
            }
            else
            {
                MessageBox.Show("相關選項有誤，請輸入正確值!");
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool check = true;
            for(int i=0;i<dataGridView3.Rows.Count;i++)
            {
                if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "包裝")
                    check = false;
            }
            if (textBox8.Enabled == false)
            {
                textBox10.Enabled = true;
                textBox9.Enabled = true;
                textBox8.Enabled = true;
            }
            else if(!check&&textBox9.Text!=""&&textBox8.Text!=""&&textBox10.Text!="")
            {
                
                string indata = "UPDATE sperp.prodcompsncache set  `volume`='" + textBox9.Text + "', `unit`='" + textBox8.Text + "', " +
                    "`unitcost`='" + textBox10.Text + "' where(`material`='包裝');";
                mySqlConnection.Open();
                MySqlCommand cmd = new MySqlCommand(indata, mySqlConnection);
                try
                {
                    cmd.ExecuteNonQuery();
                    this.prodcompsncacheTableAdapter.Fill(this.serpdataset.prodcompsncache);
                    textBox9.Enabled = false;
                    textBox8.Enabled = false;
                    textBox10.Enabled = false;
                    button6.Text = "修改";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                mySqlConnection.Close();
            }
            else if(check && textBox9.Text != "" && textBox8.Text != "" && textBox10.Text != "")
            {
                string indata = "INSERT INTO sperp.prodcompsncache (`material`, `volume`, `unit`, `unitcost`) VALUES('包裝', " +
                            "'" + textBox9.Text + "', '" + textBox8.Text + "', '" + textBox10.Text + "');";
                mySqlConnection.Open();
                MySqlCommand cmd = new MySqlCommand(indata, mySqlConnection);
                try
                {
                    cmd.ExecuteNonQuery();
                    this.prodcompsncacheTableAdapter.Fill(this.serpdataset.prodcompsncache);
                    textBox9.Enabled = false;
                    textBox8.Enabled = false;
                    textBox10.Enabled = false;
                    button6.Text = "修改";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                mySqlConnection.Close();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            
            if(tc)
            {
                string indata = "insert into sperp.product(class,product,sellingprice,grossprofit,netincome,prodevalvol,manhours,remarks) values('"
                        + serpdataset.productclass.Rows[comboBox2.FindString(comboBox2.Text)].ItemArray[0].ToString()
                        + "','" + textBox4.Text + "','" + textBox5.Text + "','" + label34.Text + "','"
                        + label36.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + richTextBox1.Text + "')";

                mySqlConnection.Open();
                MySqlCommand cmd = new MySqlCommand(indata, mySqlConnection);

                cmd.CommandText = indata;
                try
                {
                    cmd.ExecuteNonQuery();
                    string prodid = "";
                    indata = "select product.id FROM sperp.product where product='" + textBox4.Text + "' and class='"
                        + serpdataset.productclass.Rows[comboBox2.FindString(comboBox2.Text)].ItemArray[0].ToString() + "';";
                    cmd.CommandText = indata;
                    try
                    {
                        MySqlDataReader reader = cmd.ExecuteReader();
                        reader.Read();
                        prodid = reader[0].ToString();
                        reader.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        //******
                        indata = "insert into sperp.productcompsn (productid,material,volume,unit,unitcost,remarks) values('" +
                            prodid+"','" + dataGridView3.Rows[i].Cells[1].Value + "','" + dataGridView3.Rows[i].Cells[2].Value + "','"
                            + dataGridView3.Rows[i].Cells[3].Value + "','" + dataGridView3.Rows[i].Cells[4].Value + "','" + dataGridView3.Rows[i].Cells[5].Value + "')";
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                indata = "use sperp; truncate table productcache; ALTER TABLE `productcache` AUTO_INCREMENT = 1";
                cmd.CommandText = indata;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                indata = "insert into sperp.productcache(id,class1,product,selling,salesvolume,grossprofit,netincome,prodevalvol,manhours,remarks) " +
                    "select product.id,productclass.class,product.product,product.sellingprice,product.salesvolume,product.grossprofit,product.netincome," +
                    "product.prodevalvol,product.manhours,product.remarks " +
                    "from sperp.product,sperp.productclass where productclass.id=product.class";
                cmd.CommandText = indata;
                try
                {
                    cmd.ExecuteNonQuery();
                    this.productcacheTableAdapter.Fill(this.serpdataset.productcache);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                mySqlConnection.Close();
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
                tabControl1.SelectedTab = tabPage1;

            }
            else
            {
                string indata = "use sperp;delete from sperp.productcompsn where productid=" + pid + ";";
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

                indata = "UPDATE sperp.product SET class = '" + serpdataset.productclass.Rows[comboBox2.FindString(comboBox2.Text)].ItemArray[0].ToString()
                    + "',product='" + textBox4.Text + "', sellingprice = '" + textBox5.Text + "' " +
                    ",grossprofit='" + label34.Text + "',netincome='" + label36.Text + "',prodevalvol='" +
                    textBox6.Text + "',manhours='" + textBox7.Text + "',remarks='" + richTextBox1.Text + "' WHERE(id = '" + pid + "')";
                cmd.CommandText = indata;
                try
                {
                    cmd.ExecuteNonQuery();
                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        indata = "insert into sperp.productcompsn (productid,material,volume,unit,unitcost,remarks) values('" +
                            pid + "','" + dataGridView3.Rows[i].Cells[1].Value + "','" + dataGridView3.Rows[i].Cells[2].Value + "','"
                            + dataGridView3.Rows[i].Cells[3].Value + "','" + dataGridView3.Rows[i].Cells[4].Value + "','" + dataGridView3.Rows[i].Cells[5].Value + "')";
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                indata = "use sperp; truncate table productcache; ALTER TABLE `productcache` AUTO_INCREMENT = 1";
                cmd.CommandText = indata;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                indata = "insert into sperp.productcache(id,class1,product,selling,salesvolume,grossprofit,netincome,prodevalvol,manhours,remarks) " +
                    "select product.id,productclass.class,product.product,product.sellingprice,product.salesvolume,product.grossprofit,product.netincome," +
                    "product.prodevalvol,product.manhours,product.remarks " +
                    "from sperp.product,sperp.productclass where productclass.id=product.class";
                cmd.CommandText = indata;
                try
                {
                    cmd.ExecuteNonQuery();
                    this.productcacheTableAdapter.Fill(this.serpdataset.productcache);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                mySqlConnection.Close();
                dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
                tabControl1.SelectedTab = tabPage1;
            }
            
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string indata = "insert into sperp.prodcompsncache(`material`, `volume`, `unit`, `unitcost`,`remarks`) values('" 
                +textBox13.Text + "','"+textBox12.Text+ "','" + textBox11.Text + "','" + textBox16.Text + "','" + textBox15.Text + "');";
            mySqlConnection.Open();
            MySqlCommand cmd = new MySqlCommand(indata, mySqlConnection);
            try
            {
                cmd.ExecuteNonQuery();
                this.prodcompsncacheTableAdapter.Fill(this.serpdataset.prodcompsncache);
            }
            catch (Exception ex)
            {
                MessageBox.Show("材料、數量、單位、單位成本不能空白!"+Environment.NewLine+ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            mySqlConnection.Close();
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            prodcompsncacheBindingSource.RemoveCurrent();
            prodcompsncacheBindingSource.EndEdit();

            prodcompsncacheTableAdapter.Update(this.serpdataset.prodcompsncache);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                string indata = "use sperp;delete from sperp.productcompsn where productid='" + dataGridView1.CurrentRow.Cells[0].Value + "'";
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

                indata = "use sperp;delete from sperp.product where id='" + dataGridView1.CurrentRow.Cells[0].Value + "'";
                cmd.CommandText = indata;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                indata = "use sperp; truncate table productcache; ALTER TABLE `productcache` AUTO_INCREMENT = 1";
                cmd.CommandText = indata;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                indata = "insert into sperp.productcache(id,class1,product,selling,salesvolume,grossprofit,netincome,prodevalvol,manhours,remarks) " +
                    "select product.id,productclass.class,product.product,product.sellingprice,product.salesvolume,product.grossprofit,product.netincome," +
                    "product.prodevalvol,product.manhours,product.remarks " +
                    "from sperp.product,sperp.productclass where productclass.id=product.class";
                cmd.CommandText = indata;
                try
                {
                    cmd.ExecuteNonQuery();
                    this.productcacheTableAdapter.Fill(this.serpdataset.productcache);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                mySqlConnection.Close();
            }
            else
            {
                MessageBox.Show("沒有可刪除的產品!", "message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                this.productcacheTableAdapter.Fill(this.serpdataset.productcache);
                productcacheBindingSource.DataSource = this.serpdataset.productcache;
            }
            else
            {
                this.productcacheTableAdapter.serpro(this.serpdataset.productcache,textBox1.Text);
                productcacheBindingSource.DataSource = this.serpdataset.productcache;

            }
        }

        private void matclick(object sender, EventArgs e)
        {
            Form form2 = new Form();
            Button button1 = new Button();
            Button button2 = new Button();
            Panel panel = new System.Windows.Forms.Panel();
            form2.ClientSize = new System.Drawing.Size(800, 500);
            DataGridView matview = new DataGridView();
            DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGridViewTextBoxColumn idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn classDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn itemsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn qtyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn unitDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn unitcostDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn supplierDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn buydataDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn remarkDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();

            panel.BackColor = System.Drawing.Color.OldLace;
            panel.Dock = System.Windows.Forms.DockStyle.Top;
            panel.Location = new System.Drawing.Point(0, 30);
            panel.Margin = new System.Windows.Forms.Padding(0);
            panel.Name = "panellist";
            panel.Size = new System.Drawing.Size(1076, 30);
            panel.TabIndex = 1;

            button1.Text = "OK";
            button2.Text = "Cancel";
            button2.Location = new Point(button1.Left + (2 * button1.Width), button1.Location.Y);

            button1.DialogResult = DialogResult.OK;
            button2.DialogResult = DialogResult.Cancel;

            form2.Text = "choese";

            form2.FormBorderStyle = FormBorderStyle.FixedDialog;
            form2.AcceptButton = button1;
            form2.CancelButton = button2;
            form2.StartPosition = FormStartPosition.CenterScreen;

            matview.AllowUserToAddRows = false;
            matview.AllowUserToDeleteRows = false;
            matview.AllowUserToResizeColumns = false;
            matview.AllowUserToResizeRows = false;
            matview.AutoGenerateColumns = false;
            matview.BackgroundColor = System.Drawing.Color.OldLace;
            matview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            matview.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Wheat;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            matview.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            matview.ColumnHeadersHeight = 30;
            matview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            matview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            idDataGridViewTextBoxColumn,
            classDataGridViewTextBoxColumn,
            itemsDataGridViewTextBoxColumn,
            qtyDataGridViewTextBoxColumn,
            unitDataGridViewTextBoxColumn,
            unitcostDataGridViewTextBoxColumn,
            supplierDataGridViewTextBoxColumn,
            buydataDataGridViewTextBoxColumn,
            remarkDataGridViewTextBoxColumn});
            matview.DataSource = matBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.OldLace;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            matview.DefaultCellStyle = dataGridViewCellStyle2;
            matview.Dock = System.Windows.Forms.DockStyle.Fill;
            matview.EnableHeadersVisualStyles = false;
            matview.GridColor = System.Drawing.Color.WhiteSmoke;
            matview.Location = new System.Drawing.Point(235, 0);
            matview.Margin = new System.Windows.Forms.Padding(0);
            matview.Name = "matview";
            matview.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            matview.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            matview.RowHeadersVisible = false;
            matview.RowHeadersWidth = 30;
            matview.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            matview.RowTemplate.Height = 30;
            matview.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            matview.Size = new System.Drawing.Size(713, 450);
            matview.TabIndex = 2;

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
            // classDataGridViewTextBoxColumn
            // 
            classDataGridViewTextBoxColumn.DataPropertyName = "class1";
            classDataGridViewTextBoxColumn.HeaderText = "類別";
            classDataGridViewTextBoxColumn.MinimumWidth = 80;
            classDataGridViewTextBoxColumn.Name = "classDataGridViewTextBoxColumn";
            classDataGridViewTextBoxColumn.ReadOnly = true;
            classDataGridViewTextBoxColumn.Width = 80;
            // 
            // itemsDataGridViewTextBoxColumn
            // 
            itemsDataGridViewTextBoxColumn.DataPropertyName = "items";
            itemsDataGridViewTextBoxColumn.FillWeight = 80F;
            itemsDataGridViewTextBoxColumn.HeaderText = "品項";
            itemsDataGridViewTextBoxColumn.MinimumWidth = 80;
            itemsDataGridViewTextBoxColumn.Name = "itemsDataGridViewTextBoxColumn";
            itemsDataGridViewTextBoxColumn.ReadOnly = true;
            itemsDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            itemsDataGridViewTextBoxColumn.Width = 80;
            // 
            // qtyDataGridViewTextBoxColumn
            // 
            qtyDataGridViewTextBoxColumn.DataPropertyName = "qty";
            qtyDataGridViewTextBoxColumn.HeaderText = "庫存數量";
            qtyDataGridViewTextBoxColumn.MinimumWidth = 100;
            qtyDataGridViewTextBoxColumn.Name = "qtyDataGridViewTextBoxColumn";
            qtyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // unitDataGridViewTextBoxColumn
            // 
            unitDataGridViewTextBoxColumn.DataPropertyName = "unit";
            unitDataGridViewTextBoxColumn.HeaderText = "單位";
            unitDataGridViewTextBoxColumn.MinimumWidth = 60;
            unitDataGridViewTextBoxColumn.Name = "unitDataGridViewTextBoxColumn";
            unitDataGridViewTextBoxColumn.ReadOnly = true;
            unitDataGridViewTextBoxColumn.Width = 60;
            // 
            // unitcostDataGridViewTextBoxColumn
            // 
            unitcostDataGridViewTextBoxColumn.DataPropertyName = "unitcost";
            unitcostDataGridViewTextBoxColumn.HeaderText = "單價";
            unitcostDataGridViewTextBoxColumn.MinimumWidth = 80;
            unitcostDataGridViewTextBoxColumn.Name = "unitcostDataGridViewTextBoxColumn";
            unitcostDataGridViewTextBoxColumn.ReadOnly = true;
            unitcostDataGridViewTextBoxColumn.Width = 80;
            // 
            // supplierDataGridViewTextBoxColumn
            // 
            supplierDataGridViewTextBoxColumn.DataPropertyName = "supplier";
            supplierDataGridViewTextBoxColumn.HeaderText = "供應商";
            supplierDataGridViewTextBoxColumn.MinimumWidth = 100;
            supplierDataGridViewTextBoxColumn.Name = "supplierDataGridViewTextBoxColumn";
            supplierDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // buydataDataGridViewTextBoxColumn
            // 
            buydataDataGridViewTextBoxColumn.DataPropertyName = "buydata";
            buydataDataGridViewTextBoxColumn.HeaderText = "上次進貨日";
            buydataDataGridViewTextBoxColumn.MinimumWidth = 120;
            buydataDataGridViewTextBoxColumn.Name = "buydataDataGridViewTextBoxColumn";
            buydataDataGridViewTextBoxColumn.ReadOnly = true;
            buydataDataGridViewTextBoxColumn.Width = 120;
            // 
            // remarkDataGridViewTextBoxColumn
            // 
            remarkDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            remarkDataGridViewTextBoxColumn.DataPropertyName = "remark";
            remarkDataGridViewTextBoxColumn.HeaderText = "備註";
            remarkDataGridViewTextBoxColumn.MinimumWidth = 100;
            remarkDataGridViewTextBoxColumn.Name = "remarkDataGridViewTextBoxColumn";
            remarkDataGridViewTextBoxColumn.ReadOnly = true;
            //
            panel.Controls.Add(button1);
            panel.Controls.Add(button2);
            form2.Controls.Add(panel);
            form2.Controls.Add(matview);
            matview.BringToFront();

            this.matTableAdapter.serch(this.serpdataset.stock);
            matview.DataSource = matBindingSource;

            form2.ShowDialog();
            if (form2.DialogResult == DialogResult.OK)
            {
                textBox13.Text = matview.CurrentRow.Cells[2].Value.ToString();
                textBox11.Text = matview.CurrentRow.Cells[4].Value.ToString();
                textBox16.Text = matview.CurrentRow.Cells[5].Value.ToString();
                form2.Dispose();
            }
            else
            {
                form2.Dispose();
            }
        }

        private void packgeser(object sender, EventArgs e)
        {
            Form form2 = new Form();
            Button button1 = new Button();
            Button button2 = new Button();
            Panel panel = new System.Windows.Forms.Panel();
            form2.ClientSize = new System.Drawing.Size(800, 500);
            DataGridView matview = new DataGridView();
            DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            DataGridViewTextBoxColumn idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn classDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn itemsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn qtyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn unitDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn unitcostDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn supplierDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn buydataDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn remarkDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();

            panel.BackColor = System.Drawing.Color.OldLace;
            panel.Dock = System.Windows.Forms.DockStyle.Top;
            panel.Location = new System.Drawing.Point(0, 30);
            panel.Margin = new System.Windows.Forms.Padding(0);
            panel.Name = "panellist";
            panel.Size = new System.Drawing.Size(1076, 30);
            panel.TabIndex = 1;

            button1.Text = "OK";
            button2.Text = "Cancel";
            button2.Location = new Point(button1.Left + (2 * button1.Width), button1.Location.Y);

            button1.DialogResult = DialogResult.OK;
            button2.DialogResult = DialogResult.Cancel;

            form2.Text = "choese";

            form2.FormBorderStyle = FormBorderStyle.FixedDialog;
            form2.AcceptButton = button1;
            form2.CancelButton = button2;
            form2.StartPosition = FormStartPosition.CenterScreen;

            matview.AllowUserToAddRows = false;
            matview.AllowUserToDeleteRows = false;
            matview.AllowUserToResizeColumns = false;
            matview.AllowUserToResizeRows = false;
            matview.AutoGenerateColumns = false;
            matview.BackgroundColor = System.Drawing.Color.OldLace;
            matview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            matview.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Wheat;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            matview.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            matview.ColumnHeadersHeight = 30;
            matview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            matview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            idDataGridViewTextBoxColumn,
            classDataGridViewTextBoxColumn,
            itemsDataGridViewTextBoxColumn,
            qtyDataGridViewTextBoxColumn,
            unitDataGridViewTextBoxColumn,
            unitcostDataGridViewTextBoxColumn,
            supplierDataGridViewTextBoxColumn,
            buydataDataGridViewTextBoxColumn,
            remarkDataGridViewTextBoxColumn});
            matview.DataSource = matBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.OldLace;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("標楷體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            matview.DefaultCellStyle = dataGridViewCellStyle2;
            matview.Dock = System.Windows.Forms.DockStyle.Fill;
            matview.EnableHeadersVisualStyles = false;
            matview.GridColor = System.Drawing.Color.WhiteSmoke;
            matview.Location = new System.Drawing.Point(235, 0);
            matview.Margin = new System.Windows.Forms.Padding(0);
            matview.Name = "matview";
            matview.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            matview.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            matview.RowHeadersVisible = false;
            matview.RowHeadersWidth = 30;
            matview.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            matview.RowTemplate.Height = 30;
            matview.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            matview.Size = new System.Drawing.Size(713, 450);
            matview.TabIndex = 2;

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
            // classDataGridViewTextBoxColumn
            // 
            classDataGridViewTextBoxColumn.DataPropertyName = "class1";
            classDataGridViewTextBoxColumn.HeaderText = "類別";
            classDataGridViewTextBoxColumn.MinimumWidth = 80;
            classDataGridViewTextBoxColumn.Name = "classDataGridViewTextBoxColumn";
            classDataGridViewTextBoxColumn.ReadOnly = true;
            classDataGridViewTextBoxColumn.Width = 80;
            // 
            // itemsDataGridViewTextBoxColumn
            // 
            itemsDataGridViewTextBoxColumn.DataPropertyName = "items";
            itemsDataGridViewTextBoxColumn.FillWeight = 80F;
            itemsDataGridViewTextBoxColumn.HeaderText = "品項";
            itemsDataGridViewTextBoxColumn.MinimumWidth = 80;
            itemsDataGridViewTextBoxColumn.Name = "itemsDataGridViewTextBoxColumn";
            itemsDataGridViewTextBoxColumn.ReadOnly = true;
            itemsDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            itemsDataGridViewTextBoxColumn.Width = 80;
            // 
            // qtyDataGridViewTextBoxColumn
            // 
            qtyDataGridViewTextBoxColumn.DataPropertyName = "qty";
            qtyDataGridViewTextBoxColumn.HeaderText = "庫存數量";
            qtyDataGridViewTextBoxColumn.MinimumWidth = 100;
            qtyDataGridViewTextBoxColumn.Name = "qtyDataGridViewTextBoxColumn";
            qtyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // unitDataGridViewTextBoxColumn
            // 
            unitDataGridViewTextBoxColumn.DataPropertyName = "unit";
            unitDataGridViewTextBoxColumn.HeaderText = "單位";
            unitDataGridViewTextBoxColumn.MinimumWidth = 60;
            unitDataGridViewTextBoxColumn.Name = "unitDataGridViewTextBoxColumn";
            unitDataGridViewTextBoxColumn.ReadOnly = true;
            unitDataGridViewTextBoxColumn.Width = 60;
            // 
            // unitcostDataGridViewTextBoxColumn
            // 
            unitcostDataGridViewTextBoxColumn.DataPropertyName = "unitcost";
            unitcostDataGridViewTextBoxColumn.HeaderText = "單價";
            unitcostDataGridViewTextBoxColumn.MinimumWidth = 80;
            unitcostDataGridViewTextBoxColumn.Name = "unitcostDataGridViewTextBoxColumn";
            unitcostDataGridViewTextBoxColumn.ReadOnly = true;
            unitcostDataGridViewTextBoxColumn.Width = 80;
            // 
            // supplierDataGridViewTextBoxColumn
            // 
            supplierDataGridViewTextBoxColumn.DataPropertyName = "supplier";
            supplierDataGridViewTextBoxColumn.HeaderText = "供應商";
            supplierDataGridViewTextBoxColumn.MinimumWidth = 100;
            supplierDataGridViewTextBoxColumn.Name = "supplierDataGridViewTextBoxColumn";
            supplierDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // buydataDataGridViewTextBoxColumn
            // 
            buydataDataGridViewTextBoxColumn.DataPropertyName = "buydata";
            buydataDataGridViewTextBoxColumn.HeaderText = "上次進貨日";
            buydataDataGridViewTextBoxColumn.MinimumWidth = 120;
            buydataDataGridViewTextBoxColumn.Name = "buydataDataGridViewTextBoxColumn";
            buydataDataGridViewTextBoxColumn.ReadOnly = true;
            buydataDataGridViewTextBoxColumn.Width = 120;
            // 
            // remarkDataGridViewTextBoxColumn
            // 
            remarkDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            remarkDataGridViewTextBoxColumn.DataPropertyName = "remark";
            remarkDataGridViewTextBoxColumn.HeaderText = "備註";
            remarkDataGridViewTextBoxColumn.MinimumWidth = 100;
            remarkDataGridViewTextBoxColumn.Name = "remarkDataGridViewTextBoxColumn";
            remarkDataGridViewTextBoxColumn.ReadOnly = true;
            //
            panel.Controls.Add(button1);
            panel.Controls.Add(button2);
            form2.Controls.Add(panel);
            form2.Controls.Add(matview);
            matview.BringToFront();

            this.matTableAdapter.packser(this.serpdataset.stock);
            matview.DataSource = matBindingSource;

            form2.ShowDialog();
            if (form2.DialogResult == DialogResult.OK)
            {
                textBox8.Text = matview.CurrentRow.Cells[4].Value.ToString();
                textBox10.Text = matview.CurrentRow.Cells[5].Value.ToString();
                form2.Dispose();
            }
            else
            {
                form2.Dispose();
            }
        }
    }
}
