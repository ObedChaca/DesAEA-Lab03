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
using System.Data;
using System.Data.SqlClient;

namespace WpfSemana4
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SqlConnection connection = new SqlConnection("Data Source=LAPTOP-OBED\\SQLEXPRESS;Initial Catalog=DesAEA_Lab03;Integrated Security=True");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            List<Person> people = new List<Person>();

            connection.Open();
            SqlCommand command = new SqlCommand("USP_GetPerson", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                people.Add(new Person
                {
                    Id = dataReader[0].ToString(),
                    Name = dataReader[1].ToString(),
                    Date = dataReader[3].ToString()
                });
            }
            connection.Close();
            dgvPeople.ItemsSource = people;
        }
    }
}
