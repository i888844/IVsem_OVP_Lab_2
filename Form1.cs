using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static Lab._2.Classes;

namespace Lab._2
{
    public partial class Form1 : Form
    {
        private MedicineViewModel viewModel;
        private BindingSource bindingSource = new BindingSource();

        public Form1()
        {
            InitializeComponent();
            viewModel = new MedicineViewModel();
            dataGridView1.DataSource = bindingSource;
            UpdateDataGridView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new MedicineForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var medicine = form.Medicine;
                viewModel.AddMedicine(medicine);
                UpdateDataGridView();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form = new FilterForm(viewModel);
            if (form.ShowDialog() == DialogResult.OK)
            {
                var releaseForm = form.ReleaseForm;
                var maxPrice = form.MaxPrice;
                var filteredMedicines = viewModel.GetMedicinesByReleaseFormAndPrice(releaseForm, maxPrice);
                UpdateDataGridView(filteredMedicines.Select(m => viewModel.ToView(m)).ToList());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var sortedMedicines = viewModel.GetMedicines().OrderBy(m => m.ExpiryDate).ToList();
            UpdateDataGridView(sortedMedicines.Select(m => viewModel.ToView(m)).ToList());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var expiredMedicines = viewModel.GetExpiredMedicines();
            using (StreamWriter sw = new StreamWriter("ИстёкСрокГодности.txt"))
            {
                foreach (var medicine in expiredMedicines)
                {
                    sw.WriteLine($"Название:{medicine.Name},СрокГодности:{medicine.ExpiryDate:yyyy-MM-dd},Цена:{medicine.Price},ВидПрименения:{medicine.ApplicationType},ФормаВыпуска:{medicine.ReleaseForm}");
                }
                sw.Flush();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = openFileDialog.FileName;
                viewModel.LoadFromFile(filename);
                UpdateDataGridView();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = saveFileDialog.FileName;
                viewModel.SaveToFile(filename);
            }
        }

        private void UpdateDataGridView(List<MedicineView> medicines = null)
        {
            if (medicines == null)
            {
                medicines = viewModel.GetMedicines().Select(m => viewModel.ToView(m)).ToList();
            }
            bindingSource.DataSource = medicines;
        }
    }
}
