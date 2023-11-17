using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        List<string> names = new List<string>();
        List<double[]> data = new List<double[]>();
        public Form1()
        {
            InitializeComponent();
            names.Add("Item");
            // Three numbers of cat data
            data.Add(new double[]
	    {
		1,
		2,
		3
	    });

            // Another example column
            names.Add("Quantity");
            // Add three numbers of dog data
            data.Add(new double[]
	    {
		3,
		5,
		7
	    });
            names.Add("Quantity2");
            // Add three numbers of dog data
            data.Add(new double[]
        {
        3,
        5,
        7
        });
            // Render the DataGridView.
            dataGridView1.DataSource = GetResultsTable();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public DataTable GetResultsTable()
        {
            // Create the output table.
            DataTable d = new DataTable();

            // Loop through all process names.
            for (int i = 0; i < this.data.Count; i++)
            {
                // The current process name.
                string name = this.names[i];

                // Add the program name to our columns.
                d.Columns.Add(name);

                // Add all of the memory numbers to an object list.
                List<object> objectNumbers = new List<object>();

                // Put every column's numbers in this List.
                foreach (double number in this.data[i])
                {
                    objectNumbers.Add((object)number);
                }

                // Keep adding rows until we have enough.
                while (d.Rows.Count < objectNumbers.Count)
                {
                    d.Rows.Add();
                }

                // Add each item to the cells in the column.
                for (int a = 0; a < objectNumbers.Count; a++)
                {
                    d.Rows[a][i] = objectNumbers[a];
                }
            }
            return d;
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Word Documents (*.doc)|*.doc";

            sfd.FileName = "export.doc";

            if (sfd.ShowDialog() == DialogResult.OK)
            {

                //ToCsV(dataGridView1, @"c:\export.xls");

                ToCsV(dataGridView1, sfd.FileName); // Here dvwACH is your grid view name

            }

        }
        private void ToCsV(DataGridView dGV, string filename)
        {

            string stOutput = "";

            // Export titles:

            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)

                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";

            stOutput += sHeaders + "\r\n";

            // Export data.

            for (int i = 0; i < dGV.RowCount - 1; i++)
            {

                string stLine = "";

                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)

                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";

                stOutput += stLine + "\r\n";

            }

            Encoding utf16 = Encoding.GetEncoding(1254);

            byte[] output = utf16.GetBytes(stOutput);

            FileStream fs = new FileStream(filename, FileMode.Create);

            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(output, 0, output.Length); //write the encoded file

            bw.Flush();

            bw.Close();

            fs.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Excel Documents (*.xls)|*.xls";

            sfd.FileName = "export.xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {

                //ToCsV(dataGridView1, @"c:\export.xls");

                ToCsV(dataGridView1, sfd.FileName); // Here dvwACH is your grid view name

            }
        }

    }
}