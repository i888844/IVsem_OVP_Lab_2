using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Lab._2.Classes;

namespace Lab._2
{
    public partial class MedicineForm : Form
    {
        public MedicineForm()
        {
            InitializeComponent();
        }

        internal Medicine Medicine { get; private set; }

        private void button1_Click(object sender, EventArgs e)
        {
            Medicine = new Medicine
            {
                Name = textBox1.Text,
                ExpiryDate = dateTimePicker1.Value,
                Price = double.Parse(textBox3.Text),
                ApplicationType = comboBox1.SelectedItem.ToString(),
                ReleaseForm = comboBox2.SelectedItem.ToString()
            };
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}