using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Lab._2.Classes;

namespace Lab._2
{
    public partial class FilterForm : Form
    {
        private MedicineViewModel viewModel;

        internal FilterForm(MedicineViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel; // сохраняем viewModel для дальнейшего использования
        }

        public string ReleaseForm { get; private set; }
        public double MaxPrice { get; private set; }

        private void button1_Click(object sender, EventArgs e)
        {
            ReleaseForm = comboBox1.SelectedItem.ToString();
            MaxPrice = double.Parse(textBox1.Text);
            var filteredMedicines = viewModel.GetMedicinesByReleaseFormAndPrice(ReleaseForm, MaxPrice);
            using (StreamWriter sw = new StreamWriter("Фильтр.txt"))
            {
                foreach (var medicine in filteredMedicines)
                {
                    sw.WriteLine($"Название:{medicine.Name},СрокГодности:{medicine.ExpiryDate:yyyy-MM-dd},Цена:{medicine.Price},ВидПрименения:{medicine.ApplicationType},ФормаВыпуска:{medicine.ReleaseForm}");
                }
                sw.Flush();
            }
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}