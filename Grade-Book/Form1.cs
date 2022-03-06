using Grade_Book.Context;
using Grade_Book.Models;

namespace Grade_Book
{
    public partial class Form1 : Form
    {
        private Teacher? CourentTeacher = null;
        private static MyContext _context = new MyContext();
        public Form1()
        {
            InitializeComponent();

            _context.Database.EnsureCreated();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            ShowInfo();
        }
        private void ShowInfo()
        {
            string info = "";
            foreach (var student in CourentTeacher.Students)
            {
                info += $"{student.Name}\n";
            }
            MessageBox.Show(info);
        }
        private void AddTeacher(string Name, string Surname, string Login, string Password, string Subject)
        {
            var findedTeacher = _context.teachers.FirstOrDefault(x => x.Login == Login);
            var allT = _context.teachers.ToList();
            if (findedTeacher == null)
            {
                _context.teachers.Add(new Teacher { Name = Name, Login = Login, Password = Password, Subject = Subject });
                _context.SaveChanges();
                MessageBox.Show("Вы успешно зарегистрированы!");
            }
            else
                MessageBox.Show("Такой пользователь существует!");

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            CourentTeacher = null;
            MessageBox.Show("Успешный выход!");
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = tbLogin1.Text;
            string password = tbPassword1.Text;

            var findedTeacher = _context.teachers.FirstOrDefault(x => x.Login == login && x.Password == password);

            if (findedTeacher != null)
            {
                CourentTeacher = findedTeacher;
                MessageBox.Show("Успешный вход!");
            }
            else
                MessageBox.Show("Неверный логин или пароль!");

        }

        private void btnAddPuple_Click(object sender, EventArgs e)
        {
            string Name = tbName.Text;
            string Surname = tbSurname.Text;
            string Class = tbClass.Text;
            int Age = Convert.ToInt32(tbAge.Text);

            if (!String.IsNullOrEmpty(Name) &&
                !String.IsNullOrEmpty(Surname) &&
                !String.IsNullOrEmpty(Class))
            {
                _context.teachers.FirstOrDefault(x => x.Login == CourentTeacher.Login).Students.Add(new Student { Name = Name, Surname = Surname, Class = Class });
                _context.SaveChanges();
                MessageBox.Show("Ученик добавлен!");
            }
            else
            {
                MessageBox.Show("Некорректные данные ученика!");
            }
        }
    }
}