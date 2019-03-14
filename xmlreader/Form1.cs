using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace xmlreader
{

    public partial class Form1 : Form
    {
        string searchItem;
        string path;
        int rst = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet();
            CMBsearchcol.Items.Clear();     
            path = "";
            openFileDialog1.Filter = "Xml Files|*.xml";
            //openFileDialog1.ShowDialog();
            DialogResult dr = this.openFileDialog1.ShowDialog();
            try
            {
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    path = openFileDialog1.FileName;
                    dataSet.ReadXml(@"" + path);
                    dataGridView1.DataSource = dataSet.Tables[0];
                    dataGridView1.Refresh();
                    int counts = dataGridView1.Columns.Count;
                    CMBsearchcol.Items.Add("<----SELECT VALUE---->");
                    for (int i = 0; i <counts; i++)
                    {

                        CMBsearchcol.Items.Add(dataGridView1.Columns[i].Name.ToUpper());
                    }
                    
                    if (dataGridView1.Rows.Count == 0)
                    {
                        txtSearch.Enabled = false;
                        BtnSearch.Enabled = false;
                    }
                    else
                    {
                        txtSearch.Enabled = true;
                        BtnSearch.Enabled = true;
                        CMBsearchcol.Enabled = true;
                    }

                }
                else
                {
                    MessageBox.Show("File Not Selected", "XML READER");
                    txtSearch.Enabled = false;
                    BtnSearch.Enabled = false;
                    dataGridView1.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid FILE");
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {

            string search = txtSearch.Text;
            searchItem = CMBsearchcol.SelectedItem.ToString();
            
            
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            int counts = dataGridView1.Columns.Count;
            if (search == "")
            {
                MessageBox.Show("Enter Value To Search", "XML READER");
            }
            else
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {


                        if (row.Cells[searchItem].Value.ToString().Equals(search))
                        {
                            row.Selected = true;
                            row.Visible = true;

                        }
                        else
                        {
                            CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[dataGridView1.DataSource];
                            currencyManager1.SuspendBinding();
                            row.Selected = false;
                            row.Visible = false;
                            currencyManager1.ResumeBinding();
                        }

                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "XML READER");
                }
            }
        
        }

        private void CMBsearchcol_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            
            searchItem = CMBsearchcol.SelectedItem.ToString();
            string item=(CMBsearchcol.SelectedItem.ToString()).Trim();
            BtnReset.Enabled = true;
            if (CMBsearchcol.SelectedIndex==0 && rst==0)
            {
                MessageBox.Show("Invalid Column Name", "XML READER");
            }
            else if (rst == 1)
            {
                rst = 0;
            }
            else
            {
            }
            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (CMBsearchcol.SelectedValue ==null && searchItem==null)
            {
                MessageBox.Show("Select a Column Name", "XML READER");
                CMBsearchcol.Focus();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            rst = 1;
            CMBsearchcol.SelectedIndex = 0;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            Byte[] encodedBytes = System.Text.Encoding.UTF8.GetBytes("\uD83D\uDDD6");
            String decodedString = utf8.GetString(encodedBytes);
            BTnMax.Text = decodedString;
            BtnReset.Enabled = false;
            BtnSearch.Enabled = false;
            txtSearch.Enabled = false;
            CMBsearchcol.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTnMax_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void BTNABOUT_Click(object sender, EventArgs e)
        {
            Mozhi frm = new Mozhi();
            frm.ShowDialog();
        }

        
        
    }
}
