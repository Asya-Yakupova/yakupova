using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string connectionString = @"Data Source=DESKTOP-9KMRM6H;Initial Catalog=TUR_FIRMSQL2;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connectionString;
            SqlCommand myCommand = conn.CreateCommand();
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "[proc_p1]";
            string FamilyParameter = Convert.ToString(textBox1.Text);

            myCommand.Parameters.Add("@Фамилия", SqlDbType.NVarChar, 50);
            myCommand.Parameters["@Фамилия"].Value = FamilyParameter;
            conn.Open();
            SqlDataReader dataReader = myCommand.ExecuteReader();
            while (dataReader.Read())
            {
                // Создаем переменные, получаем для них значения из объекта dataReader,
                //используя метод GetТипДанных
                int TouristID = dataReader.GetInt32(0);
                string Family = dataReader.GetString(1);
                string FirstName = dataReader.GetString(2);
                string MiddleName = dataReader.GetString(3);
                //Выводим данные в элемент  listBox1
                listBox1.Items.Add("Код туриста:  " + TouristID + "   Фамилия:   " + Family + "   Имя:   " + FirstName + "   Отчество:   " + MiddleName);
            }
            conn.Close();
        }//конец  button1_Click

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connectionString;
            SqlCommand myCommand = conn.CreateCommand();
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "[proc_p5]";
            string NameTourParameter = Convert.ToString(textBox2.Text);
            double KursParameter = double.Parse(this.textBox3.Text);
            myCommand.Parameters.Add("@nameTour", SqlDbType.NVarChar, 50);
            myCommand.Parameters["@nameTour"].Value = NameTourParameter;
            myCommand.Parameters.Add("@Курс", SqlDbType.Float, 8);
            myCommand.Parameters["@Курс"].Value = KursParameter; conn.Open();
            int UspeshnoeIzmenenie = myCommand.ExecuteNonQuery();
            if (UspeshnoeIzmenenie != 0)
            {

                MessageBox.Show("Изменения внесены", "Изменение записи");
            }
            else
            {
                MessageBox.Show("Не удалось внести изменения", "Изменение записи");
            }
            conn.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connectionString;
            SqlCommand myCommand = conn.CreateCommand();
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "[proc6]";
            conn.Open();
            string MaxPrice = Convert.ToString(myCommand.ExecuteScalar());
            label4.Text = MaxPrice;
            conn.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
            this.Hide();
        }
    }
}

