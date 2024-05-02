using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace EmployeeDBFirst
{
    public partial class Form1 : Form
    {
        EmployeeDBFIRSTEntities1 db = new EmployeeDBFIRSTEntities1();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            var query = from x in db.Employees
                        join y in db.Departments
                        on x.DepartmentID equals y.DepartmentID
                        join z in db.Addresses
                        on x.AddressID equals z.AddressID
                        select new
                        {
                            x.EmployeeID,
                            x.FirstName,
                            x.SurName,
                            x.Salary,
                            x.Gender,
                            DepartmentName=y.Name,
                            DepartmentBudget=y.Budget,
                            z.Country,
                            z.City,
                            z.Street,
                            z.BuildingNumber
                        };
            dataGridView1.DataSource=query.ToList();
                       
                   
                       
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            Employees employee = new Employees();
            employee.FirstName = txtFirstName.Text;
            employee.SurName = txtSurName.Text;
            employee.Salary = Convert.ToInt32(txtSalary.Text);
            employee.Gender = txtGender.Text;
            employee.DepartmentID = Convert.ToInt16(txtDepartmentID.Text);
            employee.AddressID = Convert.ToInt16(txtAddressID.Text);
            db.Employees.Add(employee);
            db.SaveChanges();
            MessageBox.Show("Çalışanın kaydı eklendi!");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var log = db.Employees.Find(Convert.ToInt16(txtEmployeeID.Text));
            db.Employees.Remove(log);
            db.SaveChanges();
            MessageBox.Show("Çalışanın kaydı silindi!");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var log = db.Employees.Find(Convert.ToInt16(txtEmployeeID.Text));
            log.FirstName = txtFirstName.Text;
            log.SurName = txtSurName.Text;
            log.Salary = Convert.ToInt32(txtSalary.Text);
            log.Gender = txtGender.Text;
            log.DepartmentID = Convert.ToInt16(txtDepartmentID.Text);
            log.AddressID = Convert.ToInt16(txtAddressID.Text);
            db.SaveChanges();
            MessageBox.Show("Çalışanın kaydı güncellendi!");

        }

        private void btnLinq_Click(object sender, EventArgs e)
        {
            if (rdbtnEmplyAvgSalary.Checked == true)
            {
                var query = (from x in db.Employees
                             select x.Salary).Average();
                MessageBox.Show("Çalışanların Ortalama Maaşı: " + query.ToString());
            }
            else if (rdbtnEmplySalarySum.Checked == true)
            {
                var query = (from x in db.Employees
                             select x.Salary).Sum();
                MessageBox.Show("Çalışanların Toplam Maaşı: " + query.ToString());
                            
            }
            else if (rdbtnMaxSalaryEmply.Checked == true)
            {
                var query = (from x in db.Employees
                             select x.Salary).Max();
                MessageBox.Show("En yüksek Maaş: " + query.ToString());
            }
            else if (rdbtnMinSalaryEmply.Checked == true)
            {
                // 2 farklı şekilde yapıldıgını gostermek icin yazdım :)


                //var query = (from x in db.Employees
                //             select x.Salary).Min();
                //MessageBox.Show("En düşük Maaş: " + query.ToString());

                var query2 = (from x in db.Employees
                              orderby x.Salary ascending
                              select x.Salary).FirstOrDefault();

                MessageBox.Show("En düşük Maaş: " + query2.ToString());
                
            }
            else if (rdbtnMaxSalaryEmpIInformation.Checked == true)
            {
                var query = (from x in db.Addresses
                            join y in db.Employees
                            on x.AddressID equals y.AddressID
                            join z in db.Departments
                            on y.DepartmentID equals z.DepartmentID
                            orderby y.Salary descending
                            select new
                            {
                                Ad=y.FirstName,
                                Soyad=y.SurName,
                                Ülke=x.Country,
                                Şehir=x.City,
                                SokakCadde=x.Street,
                                BinaNumarasi=x.BuildingNumber,
                                DepartmanAdi=z.Name,
                                DepartmanButcesi=z.Budget
                            }
                            ).FirstOrDefault();

                MessageBox.Show(query.ToString());
                                                        
            }
            else if (rdbtnStartMorFFirstName.Checked == true)
            {
                var query = (from x in db.Employees
                             join y in db.Departments
                             on x.DepartmentID equals y.DepartmentID
                             where x.FirstName.StartsWith("F") || x.FirstName.StartsWith("M")
                             orderby y.Budget descending
                             select new
                             {
                                 x.EmployeeID,
                                 x.FirstName,
                                 x.SurName,
                                 y.Name,
                                 y.Budget,
                                 y.DepartmentID
                             }
                          ).FirstOrDefault();

                MessageBox.Show(query.ToString());
            }
            else if (rdbtnTurkeyEndWithR.Checked == true)
            {
                var query = from x in db.Employees
                            join y in db.Addresses
                            on x.AddressID equals y.AddressID
                            where y.Country == "Türkiye" && x.Salary > 15000 && x.SurName.EndsWith("R")
                            select new
                            {
                                x.FirstName,
                                x.SurName,
                                x.Salary,
                                x.Gender,
                                y.Country,
                                y.City,
                                y.Street,
                                y.BuildingNumber,
                            };
                
                dataGridView1.DataSource = query.ToList();
                
            }
            else if (rdbtnContainMbutNotA.Checked == true)
            {
                var query = from x in db.Employees
                            join y in db.Addresses
                            on x.AddressID equals y.AddressID
                            join z in db.Departments
                            on x.DepartmentID equals z.DepartmentID
                            where x.FirstName.Contains("M") && !(x.FirstName.Contains("A"))
                            select new
                            {
                                x.FirstName,
                                x.SurName,
                                y.Country,
                                y.City,
                                y.Street,
                                y.BuildingNumber,
                                DepartmentName = z.Name,
                                DepartmentBudget = z.Budget

                            };

                dataGridView1.DataSource = query.ToList();                           
            }
            else if (rdbtn30000MuchTurkeyCount.Checked == true)
            {
                var query = (from x in db.Employees
                             join y in db.Addresses
                             on x.AddressID equals y.AddressID
                             where x.Salary > 30000 && y.Country != "Türkiye"
                             select x).Count();

                MessageBox.Show(query.ToString());
            }
            else if (rdbtnBiggestSalaryOrderBy.Checked == true)
            {
                var query = from x in db.Employees
                            orderby x.Salary ascending
                            select x;
                dataGridView1.DataSource = query.ToList();
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
            }
            else if (rdbtnAtoZFirstFourLog.Checked == true)
            {
                var query = (from x in db.Employees
                             orderby x.FirstName ascending
                             select x).Take(4);

                dataGridView1.DataSource = query.ToList();
            }
            else if (rdbtnEmployeeIDGet.Checked == true)
            {
                int id = Convert.ToInt16(txtEmployeeID.Text);

                var query = from x in db.Employees
                            where x.EmployeeID == id
                            select x;

                dataGridView1.DataSource = query.ToList();
                
                
              
                
            }
            else if (rdbtnProcedureGetFirstName.Checked == true)
            {
                string ad = txtFirstName.Text;
                dataGridView1.DataSource = db.GetEmployee(ad);
                           
            }
            else if (rdbtnSurNameContainsC.Checked == true)
            {
                var querySum = (from x in db.Employees
                                where x.SurName.Contains("Ç")
                                select x.Salary).Sum();

                var queryAvg = (from y in db.Employees
                                where y.SurName.Contains("Ç")
                                select y.Salary).Average();

                MessageBox.Show("Toplam maaşları: " + querySum.ToString() + "Ortalam Maaşları: " + queryAvg.ToString());
            }

            else if (rdbtnSaglikOrHizmet.Checked == true)
            {
                var query = from x in db.Employees
                            join y in db.Addresses
                            on x.AddressID equals y.AddressID
                            join z in db.Departments
                            on x.DepartmentID equals z.DepartmentID
                            where z.Name == "Sağlık" || z.Name == "Hizmet"
                            select new
                            {
                                Ad = x.FirstName,
                                Soyad = x.SurName,
                                Maaş = x.Salary,
                                Cinsiyet = x.Gender,
                                Ülke = y.Country,
                                Şehir = y.City,
                                SokakCadde = y.Street,
                                BinaNumarasi = y.BuildingNumber,
                                DerpartmanAdi = z.Name,
                                DepartmanButcesi = z.Budget

                            };

                dataGridView1.DataSource = query.ToList();
                            
            }
            else if (rdbtnGetProcedureCountry.Checked == true)
            {
                string country = txtAddressCountry.Text;
                dataGridView1.DataSource = db.GetCountry(country);
            }
            else if (rdbtnDepartmentNameSum.Checked == true)
            {
                string department = txtDepartmentName.Text;

                var query = (from x in db.Departments
                             join y in db.Employees
                             on x.DepartmentID equals y.DepartmentID
                             where x.Name == department
                             select y.Salary).Sum();

                MessageBox.Show(query.ToString());
            }
            else if (rdbtnErkekFarkKadın.Checked == true)
            {
                var queryErkek = (from x in db.Employees
                                  join y in db.Addresses
                                  on x.AddressID equals y.AddressID
                                  where y.Country == "Türkiye" && x.Gender == "Erkek"
                                  select x.Salary).Sum();

                var queryKadin= (from x in db.Employees
                                 join y in db.Addresses
                                 on x.AddressID equals y.AddressID
                                 where y.Country == "Almanya" && x.Gender == "Kadın"
                                 select x.Salary).Sum();

                int result = Math.Abs(Convert.ToInt32(queryErkek) - Convert.ToInt32(queryKadin));

                MessageBox.Show("Türkiyedeki Erkeklerin Almanyadaki Kadınlardan Aldıkları toplam Maaş farkı: " + result);
            }
            else if (rdbtnSaglikAlmanErkekHizmetTurki.Checked == true)
            {
                var query = from x in db.Employees
                            join y in db.Addresses
                            on x.AddressID equals y.AddressID
                            join z in db.Departments
                            on x.DepartmentID equals z.DepartmentID
                            where (z.Name == "Sağlık" && x.Gender == "Erkek" && y.Country == "Almanya") ||
                            (z.Name == "Hizmet" && x.Gender == "Kadın" && y.Country == "Türkiye")

                            select new
                            {
                                x.FirstName,
                                x.SurName,
                                x.Gender,
                                x.Salary,
                                y.Country,
                                y.City,
                                y.Street,
                                y.BuildingNumber,
                                DepartmentName = z.Name,
                                DepartmentBudget = z.Budget
                            };

                dataGridView1.DataSource = query.ToList();
            }
            else if (rdbtn20000MuchTurkeyKadin.Checked == true)
            {
                var query = (from x in db.Employees
                            join y in db.Addresses
                            on x.AddressID equals y.AddressID
                            where x.Gender == "Kadın" && x.Salary > 20000 && y.Country == "Türkiye"
                            orderby x.EmployeeID descending


                             select new
                            {
                                x.EmployeeID,
                                x.FirstName,
                                x.SurName,
                                x.Salary,
                                x.Gender,
                                y.Country,
                                y.City,
                                y.Street,
                                y.BuildingNumber
                            }).Take(2);
                dataGridView1.DataSource = query.ToList();
            }
            else if (rdbtnNotContainsEankaraErkekAvg.Checked == true)
            {
                var query = (from x in db.Employees
                            join y in db.Departments
                            on x.DepartmentID equals y.DepartmentID
                            join z in db.Addresses
                            on x.AddressID equals z.AddressID
                            where !(x.FirstName.Contains("E")) && (z.City=="Ankara") && 
                            (y.Name=="Reklam") && (x.Gender=="Erkek")
                            select x.Salary).Average();

                MessageBox.Show("İsminde E harfi Bulunmayan Ankaradaki Reklamcı Erkeklerin Ortalama Maaşı: " 
                    + query.ToString());

            }

            else if (rdbtnAvgMinMuch1000Fransa.Checked == true)
            {
                int avgMaas = Convert.ToInt32((from a in db.Employees
                               select a.Salary).Average());



                var query = from x in db.Employees
                            join y in db.Addresses
                            on x.AddressID equals y.AddressID
                            join z in db.Departments
                            on x.DepartmentID equals z.DepartmentID
                            where x.Gender == "Kadın" && y.Country == "Fransa" && z.Name == "Eğitim" && x.Salary >= avgMaas + 1000
                            select new
                            {
                                x.FirstName,
                                x.SurName,
                                x.Gender,
                                x.Salary,
                                y.Country,
                                z.Name
                            };

                dataGridView1.DataSource = query.ToList();
            }
            else if (rdbtnEndRGuvenlikErkek.Checked == true)
            {
                var query = from x in db.Employees
                            join y in db.Departments
                            on x.DepartmentID equals y.DepartmentID
                            join z in db.Addresses
                            on x.AddressID equals z.AddressID
                            where x.Gender == "Erkek" && y.Name == "Güvenlik" && z.Country == "Türkiye" && x.SurName.EndsWith("R")
                            select new
                            {
                                x.FirstName,
                                x.SurName,
                                x.Gender,
                                x.Salary,
                                y.Name,
                                z.Country
                            };
                dataGridView1.DataSource = query.ToList();
            }
            else if (rdbtnStartMorA.Checked == true)
            {
                var query = (from x in db.Employees
                             join y in db.Departments
                             on x.DepartmentID equals y.DepartmentID
                             where (x.FirstName.StartsWith("A") || x.FirstName.StartsWith("M")) &&
                             ((x.Gender == "Kadın" && y.Name == "Sporcu") || (x.Gender == "Erkek" && y.Name == "Sağlık"))
                             select x.Salary).Sum();

                MessageBox.Show(query.ToString());

            }
            else if (rdbtnMostBudgetAvgSalary.Checked == true)
            {
                var mostBudget = (from x in db.Departments
                                  select x.Budget).Max();

                var query = (from a in db.Employees
                             join b in db.Departments
                             on a.DepartmentID equals b.DepartmentID
                             where b.Budget == mostBudget
                             select a.Salary).Average();

                MessageBox.Show(query.ToString());
            }
            else if (rdbtnAlmanyaOrFransa.Checked == true)
            {
                var query = (from x in db.Employees
                             join y in db.Departments
                             on x.DepartmentID equals y.DepartmentID
                             join z in db.Addresses
                             on x.AddressID equals z.AddressID
                             where ((z.Country == "Almanya" || z.Country == "Fransa")) &&
                             ((x.Gender == "Erkek" && y.Name == "Eğitim") || (x.Gender == "Kadın" && y.Name == "Haber"))
                             select x.Salary).Sum();

                MessageBox.Show(query.ToString());


            }

        }
    }
}
