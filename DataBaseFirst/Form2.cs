using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseFirst
{
    public partial class Form2 : Form
    {
        Author a;
        bool addnewa;

        Book b;
        bool addnewb;
        public Form2(Author a, bool addnewa)
        {
            InitializeComponent();
            this.addnewa = addnewa;
            this.a = a;//Запомнили ссылку на товар
            if (!addnewa)
            {/* если форма открывается для редактирования,
               то сначала занесем информацию об изменяемом 
              товаре в текстовые поля */
                textBox1.Text = a.Name;
                Text = "Редактирование автора";//меняем заголовок
                label1.Text = "Введите имя автора";
            }
            else
            {
                Text = "Добавление автора";//меняем заголовок, если создание товара
                label1.Text = "Введите имя автора";
            }
        }



        public Form2(Book b, bool addnewb)
        {
            InitializeComponent();
            this.addnewb = addnewb;
            this.b = b;//Запомнили ссылку на товар
            if (!addnewb)
            {/* если форма открывается для редактирования,
               то сначала занесем информацию об изменяемом 
              товаре в текстовые поля */
                textBox1.Text = b.Name;
                Text = "Редактирование книги";//меняем заголовок
                label1.Text = "Введите название книги";
            }
            else
            {
                Text = "Добавление книги";//меняем заголовок, если создание товара
                label1.Text = "Введите название книги";
            }

        }




        private void OK_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {//Проверка заполнения полей
                MessageBox.Show("Заполните все поля");
                return;
            }

            if (a != null)
            {
                a.Name = textBox1.Text;
            }
            else if (b != null)
            {
                b.Name = textBox1.Text;
            }
        }
    }
}
