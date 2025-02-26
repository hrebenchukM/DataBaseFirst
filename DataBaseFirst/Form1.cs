using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Azure;
using Microsoft.EntityFrameworkCore;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DataBaseFirst
{
    // Для работы с существующей БД MS SQL Server необходимо добавить два пакета:
    // Microsoft.EntityFrameworkCore.SqlServer(представляет функциональность Entity Framework для работы с MS SQL Server)
    // Microsoft.EntityFrameworkCore.Tools(пакет для создания классов по базе существующей базе данных, т.е. reverse engineering)

    // Scaffold-DbContext "Server=DESKTOP-G30VB0K\MSSQLSERVER01;Database=AcademyGroup;Integrated Security=SSPI;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer
    // Scaffold-DbContext "Data Source=D:\\AcademyGroup.db" Microsoft.EntityFrameworkCore.Sqlite

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            try
            {
                using (var db = new BooksContext())
                {
                    var query = from b in db.Authors
                                select b;
                    comboBox1.DataSource = query.ToList();
                    comboBox1.DisplayMember = "Name";

                    var query2 = from b in db.Books
                                 select b;
                    listBox1.DataSource = query2.ToList();
                    listBox1.DisplayMember = "Name";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddGood_Click(object sender, EventArgs e)
        {
            try
            {
                //Добавление товара
                var a = new Author();
                Form2 addform = new Form2(a, true);

                if (addform.ShowDialog() == DialogResult.OK)
                {//если пользователь нажал ок, добавляем товар в список
                   
                    using (var db = new BooksContext())
                    {
                        db.Authors.Add(a);
                        db.SaveChanges();

                        var query = from b in db.Authors
                                    select b;

                        comboBox1.DataSource = null;
                        comboBox1.DataSource = query.ToList();
                        comboBox1.DisplayMember = "Name";
                        comboBox1.SelectedItem = a;

                        MessageBox.Show("Автор добавлен!");
                    

                    }

                }
     
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
       
        private void DeleteGood_Click(object sender, EventArgs e)
        {
           

            try
            {
                //Удаление выделенного элемента
                if (comboBox1.SelectedIndex == -1)//Если товар не выбран
                {
                    MessageBox.Show("Вы не выбрали автора");
                    return;
                }

                DialogResult result = MessageBox.Show("Вы действительно хотите удалить автора?", "Каталог авторов", MessageBoxButtons.YesNo);
               
                if (result != DialogResult.Yes)
                    return;


                using (var db = new BooksContext())
                {
                    List<Author> list = comboBox1.DataSource as List<Author>;

                    string authorname = list[comboBox1.SelectedIndex].Name;

                    var query = from b in db.Authors
                                where b.Name == authorname
                                select b;

                    db.Authors.RemoveRange(query);
                    db.SaveChanges();


                    query = from b in db.Authors
                            select b;
                    comboBox1.DataSource = query.ToList();
                    comboBox1.DisplayMember = "Name";


                    var query2 = from b in db.Books
                                 select b;
                    listBox1.DataSource = query2.ToList();
                    listBox1.DisplayMember = "Name";


                    MessageBox.Show("Автор удален!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void EditGood_Click(object sender, EventArgs e)
        {
          
            try
            {

                if (comboBox1.SelectedIndex == -1) //Если товар не выбран
                {
                    MessageBox.Show("Вы не выбрали автора");
                    return;
                }

                List<Author> list = comboBox1.DataSource as List<Author>;
                string oldName = list[comboBox1.SelectedIndex].Name;

                var a = (Author)comboBox1.SelectedItem;//Забираем ссылку на выделенный элемент


                Form2 editform = new Form2(a, false);
              

                if (editform.ShowDialog() == DialogResult.OK)
                {


                    using (var db = new BooksContext())
                    {
                        var query = (from b in db.Authors
                                     where b.Name == oldName
                                     select b).Single();

                        query.Name = a.Name;
                        db.SaveChanges();

                        var query2 = from b in db.Authors
                                     select b;
                        comboBox1.DataSource = query2.ToList();
                        comboBox1.DisplayMember = "Name";

                        comboBox1.SelectedItem = query;
                        MessageBox.Show("Автор  переименован!");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        private void Add2Good_Click(object sender, EventArgs e)
        {
           
            try
            {
                if (comboBox1.SelectedIndex == -1)//Если товар не выбран
                {
                    MessageBox.Show("Вы не выбрали автора");
                    return;
                }

                List<Author> list = comboBox1.DataSource as List<Author>;
                string authorname = list[comboBox1.SelectedIndex].Name;


                //Добавление товара
                var newBook = new Book();
                Form2 addform = new Form2(newBook, true);

            

                if (addform.ShowDialog() == DialogResult.OK)
                {//если пользователь нажал ок, добавляем товар в список

                 

                    using (var db = new BooksContext())
                    {
                        var query = (from b in db.Authors
                                     where b.Name == authorname
                                     select b).Single();

                        var book = new Book
                        {
                            Name = newBook.Name,
                            Author = query
                        };
                        db.Books.Add(book);
                        db.SaveChanges();

                        var query2 = from b in db.Books
                                     select b;
                        listBox1.DataSource = query2.ToList();
                        listBox1.DisplayMember = "Name";


                        MessageBox.Show("Книга добавлена!");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }





        private void Delete2Good_Click(object sender, EventArgs e)
        {
            try
            {
                //Удаление выделенного элемента
                if (listBox1.SelectedIndex == -1)//Если товар не выбран
                {
                    MessageBox.Show("Вы не выбрали книгу");
                    return;
                }
                DialogResult result = MessageBox.Show("Вы действительно хотите удалить книгу?", "Каталог книг", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                   
                    List<Book> list = listBox1.DataSource as List<Book>;
                    string bookname = list[listBox1.SelectedIndex].Name;


                    using (var db = new BooksContext())
                    {
                         var query = from b in db.Books
                                    where b.Name == bookname
                                    select b;
                        db.Books.RemoveRange(query);
                        db.SaveChanges();

                        var query2 = from b in db.Books
                                     select b;
                        listBox1.DataSource = query2.ToList();
                        listBox1.DisplayMember = "Name";


                        MessageBox.Show("Книга удалена!");
                    }
                }
                    
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }





        private void Edit2Good_Click(object sender, EventArgs e)
        {

            try
            {
                //редактирование товара
                if (listBox1.SelectedIndex == -1)//Если товар не выбран
                {
                    MessageBox.Show("Вы не выбрали книгу"); return;
                }

                int n = listBox1.SelectedIndex;//запоминаем выделенный элемент
                var newBook = (Book)listBox1.Items[n];//Забираем ссылку на выделенный элемент книгу
            
                Form2 editform = new Form2(newBook, false);

                List<Author> list = comboBox1.DataSource as List<Author>;
                string authorname = list[comboBox1.SelectedIndex].Name;
                List<Book> booklist = listBox1.DataSource as List<Book>;
                string bookname = booklist[listBox1.SelectedIndex].Name;

                if (editform.ShowDialog() == DialogResult.OK)
                {
                    using (var db = new BooksContext())
                    {
                        var query = (from b in db.Authors
                                     where b.Name == authorname
                                     select b).Single();
                        if (query == null)
                            return;

                        var query2 = (from b in db.Books
                                      where b.Name == bookname
                                      select b).Single();

                        query2.Author = query;
                        query2.Name = newBook.Name;

                        db.SaveChanges();

                        var query3 = from b in db.Books
                                     select b;

                        listBox1.DataSource = query3.ToList();
                        listBox1.DisplayMember = "Name";

                        MessageBox.Show("Данные о книге изменены!");
                    }
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetBooks((Author)comboBox1.SelectedItem);
        }



        private void ResetBooks(Author a)
        {
            listBox1.Items.Clear();
            using (var db = new BooksContext())
            {
                if (checkBox1.Checked)
                {
                    var query = from b in db.Books
                                where b.Author.Name == a.Name
                                select b;

                    foreach (var book in query)
                    {
                        listBox1.Items.Add(book);
                    }
                }
                else
                {
                    var query = from b in db.Books
                                select b;

                    foreach (var book in query)
                    {
                        listBox1.Items.Add(book);
                    }

                }

            }

        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(saveFileDialog1.FileName, "Сохранение файла", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Save(saveFileDialog1.FileName);

            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.InitialDirectory = "c:\\"; // зададим начальную папку, отображенную диалоговым окном файла
            openFileDialog1.Multiselect = false; // запретить множественный выбор файлов
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(openFileDialog1.FileName, "Открытие файла", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Load(openFileDialog1.FileName);

            }
        }

        public void Save(string path)
        {
            StreamWriter sw = new StreamWriter(path, false);

            try
            {
                using (var db = new BooksContext())
                {
                    var authors = db.Authors.Include(a => a.Books).ToList();

                    foreach (Author author in authors)
                    {
                        sw.WriteLine(author.Name);

                        foreach (Book book in author.Books)
                        {
                            sw.WriteLine(book.Name);
                        }

                        sw.WriteLine();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка сохранения: " + e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sw.Close();
            }
        }

        public void Load(string path)
        {
            StreamReader sr = new StreamReader(path);
            try
            {
                string line;
                var authors = new List<Author>();

                using (var db = new BooksContext())
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                
                        var author = db.Authors
                                       .Single(a => a.Name == line); 

                     
                        if (author == null)
                        {
                            author = new Author { Name = line }; 
                            db.Authors.Add(author);
                        }

                       
                        while ((line = sr.ReadLine()) != null && line != "")
                        {
                            var book = new Book 
                            {
                                Name = line,
                                Author = author
                            }; 

                            author.Books.Add(book); 
                        }
                    }

          
                    db.SaveChanges();

                    authors = db.Authors.ToList();

                   
                    comboBox1.DataSource = null; 
                    comboBox1.DataSource = authors; 
                    comboBox1.DisplayMember = "Name";

                    ResetBooks((Author)comboBox1.SelectedItem);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка загрузки: " + e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sr.Close();
            }
        }





    }
}
