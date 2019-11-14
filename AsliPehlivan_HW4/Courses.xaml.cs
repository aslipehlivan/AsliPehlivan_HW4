using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AsliPehlivan_HW4
{
    /// <summary>
    /// Interaction logic for Courses.xaml
    /// </summary>
    public partial class Courses : Window
    {
        public Courses()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Course course = new Course();
            course.CName = txtCourseName.Text;
            course.Code = Int32.Parse(txtCourseCode.Text);
            course.Credit = Int32.Parse(txtCourseCredit.Text);
            course.Quota = Int32.Parse(txtCourseQuota.Text);

         

            CetDB db = new CetDB();
            db.Courses.Add(course);

            db.SaveChanges();
            MessageBox.Show("Ders kaydedildi!");
            txtCourseName.Text = "";
            txtCourseCode.Text = "";
            txtCourseQuota.Text = "";
            txtCourseCredit.Text = "";
            LoadCourses();
      
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Course course = dgCourses.SelectedItem as Course;
            if (course != null)
            {
                CetDB db = new CetDB();
                db.Courses.Remove(course);
                db.SaveChanges();
                MessageBox.Show("Ders silindi!");
                LoadCourses();
            }
            else
            {
                MessageBox.Show("Silmek için ders seçmelisiniz!");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Course course = dgCourses.SelectedItem as Course;
            if (course != null)
            {
                CetDB db = new CetDB();
                var coursenew = db.Courses.Find(course.ID);
                coursenew.CName = txtCourseName.Text;
                coursenew.Code = Int32.Parse(txtCourseCode.Text);
                coursenew.Quota = Int32.Parse(txtCourseQuota.Text);
                coursenew.Credit = Int32.Parse(txtCourseCredit.Text);
                db.SaveChanges();
                LoadCourses();
                MessageBox.Show("Ders güncellendi!");

            }
            else
            {
                MessageBox.Show("Güncellemek için ders seçmelisiniz!");
            }
        }

        private void dgCourses_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Course course = dgCourses.SelectedItem as Course;
            if (course != null)
            {
                txtCourseName.Text = course.CName;
                txtCourseCode.Text = course.Code.ToString();
                txtCourseQuota.Text = course.Quota.ToString();
                txtCourseCredit.Text = course.Credit.ToString();

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCourses();
        }

        private void LoadCourses()
        {
            CetDB db = new CetDB();
            List<Course>courses = db.Courses.ToList();
            dgCourses.ItemsSource = courses;
        }

        private void txtCourseID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
