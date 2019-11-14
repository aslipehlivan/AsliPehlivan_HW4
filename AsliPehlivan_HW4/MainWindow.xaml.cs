using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;

namespace AsliPehlivan_HW4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Student student = new Student();
            student.ID = Int32.Parse(txtStudentID.Text);
            student.Name = txtStudentName.Text;
            student.Surname = txtStudentSurname.Text;
            student.BirthDate = dtpBirthDate.SelectedDate.Value;


            CetDB db = new CetDB();
            db.Database.OpenConnection();
            db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Students ON");
            db.Students.Add(student);
            db.SaveChanges();
            

            MessageBox.Show("Öğrenci Kaydedildi.");
            lblStudentId.Content = "";
            txtStudentName.Text = "";
            txtStudentSurname.Text = "";
            dtpBirthDate.SelectedDate = DateTime.Now;
            LoadStudents();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Student student = dgStudents.SelectedItem as Student;
            if (student != null)
            {
                CetDB db = new CetDB();
                var studentnew = db.Students.Find(student.ID);
                studentnew.Name = txtStudentName.Text;
                studentnew.Surname = txtStudentSurname.Text;
                studentnew.BirthDate = dtpBirthDate.SelectedDate.Value;
                db.SaveChanges();
                LoadStudents();
                MessageBox.Show("Güncellendi.");
            }
            else
            {
                MessageBox.Show("Güncellemek için öğrenci seçmelisiniz!");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Student student = dgStudents.SelectedItem as Student;
            if (student != null)
            {
                CetDB db = new CetDB();
                db.Students.Remove(student);
                db.SaveChanges();
                MessageBox.Show("Öğrenci Silindi!");
                LoadStudents();

            }
            else
            {
                MessageBox.Show("Silmek için öğrenci seçmelisin!");
            }
        }

        private void btnOpenCourse_Click(object sender, RoutedEventArgs e)
        {
            Courses course = new Courses();
            course.Show();
        }

        private void dgStudents_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Student student = dgStudents.SelectedItem as Student;
            if (student != null)
            {
                lblStudentId.Content = student.ID;
                txtStudentName.Text = student.Name;
                txtStudentSurname.Text = student.Surname;
                dtpBirthDate.SelectedDate = student.BirthDate;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadStudents();
        }
        private void LoadStudents()
        {
            CetDB db = new CetDB();
            List<Student> students = db.Students.ToList();
            dgStudents.ItemsSource = students;
        }
    }
}
