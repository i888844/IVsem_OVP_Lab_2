using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab._2
{
    public class Classes
    {
        public class Medicine
        {
            public string Name { get; set; }
            public DateTime ExpiryDate { get; set; }
            public double Price { get; set; }
            public string ApplicationType { get; set; }
            public string ReleaseForm { get; set; }
        }

        public class MedicineView
        {
            public string Name { get; set; }
            public string ExpiryDate { get; set; }
            public string Cost { get; set; }
            public string Usage { get; set; }
            public string Form { get; set; }
        }

        public class MedicineViewModel
        {
            private Dictionary<string, Medicine> medicines;

            public MedicineViewModel()
            {
                medicines = new Dictionary<string, Medicine>();
            }

            public void AddMedicine(Medicine medicine)
            {
                if (medicines.ContainsKey(medicine.Name))
                {
                    medicines[medicine.Name] = medicine;
                }
                else
                {
                    medicines.Add(medicine.Name, medicine);
                }
            }

            public List<Medicine> GetMedicinesByReleaseFormAndPrice(string releaseForm, double maxPrice)
            {
                return medicines.Values
                    .Where(m => m.ReleaseForm == releaseForm && m.Price <= maxPrice)
                    .OrderBy(m => m.ExpiryDate)
                    .ToList();
            }

            public List<Medicine> GetMedicines()
            {
                return medicines.Values.ToList();
            }

            public List<Medicine> GetExpiredMedicines()
            {
                var expiredMedicines = medicines.Values
                    .Where(m => m.ExpiryDate < DateTime.Now)
                    .ToList();

                foreach (var medicine in expiredMedicines)
                {
                    medicines.Remove(medicine.Name);
                }

                return expiredMedicines;
            }

            public void LoadFromFile(string filename)
            {
                var lines = File.ReadAllLines(filename);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    var medicine = new Medicine
                    {
                        Name = parts[0].Split(':')[1],
                        ExpiryDate = DateTime.Parse(parts[1].Split(':')[1]),
                        Price = double.Parse(parts[2].Split(':')[1]),
                        ApplicationType = parts[3].Split(':')[1],
                        ReleaseForm = parts[4].Split(':')[1]
                    };
                    AddMedicine(medicine);
                }
            }

            public void SaveToFile(string filename)
            {
                var lines = medicines.Values.Select(m =>
                    $"Название:{m.Name},СрокГодности:{m.ExpiryDate:yyyy-MM-dd},Цена:{m.Price},ВидПрименения:{m.ApplicationType},ФормаВыпуска:{m.ReleaseForm}");
                File.WriteAllLines(filename, lines);
            }

            public MedicineView ToView(Medicine medicine)
            {
                return new MedicineView
                {
                    Name = medicine.Name,
                    ExpiryDate = medicine.ExpiryDate.ToString("yyyy-MM-dd"),
                    Cost = medicine.Price.ToString(),
                    Usage = medicine.ApplicationType,
                    Form = medicine.ReleaseForm
                };
            }
        }
    }
}
