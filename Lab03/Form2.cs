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

namespace Lab03
{
    public partial class Form2 : Form
    {
        SqlConnection conn;
        public Form2(SqlConnection conn)
        {
            this.conn = conn;
            InitializeComponent();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    List<Person> people = new List<Person>();
                    SqlCommand command = new SqlCommand("USP_GetPerson", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        people.Add(new Person
                        {
                            Id = dataReader[0].ToString(),
                            Name = dataReader[1].ToString()
                        });
                    }
                    conn.Close();
                    dgvListado.DataSource = people;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error en el listado: \n" + ex.ToString());
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                {

                    List<Person> people = new List<Person>();
                    SqlCommand command = new SqlCommand("USP_SearchPerson", conn);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter parameter1 = new SqlParameter();
                    parameter1.SqlDbType = SqlDbType.VarChar;
                    parameter1.Size = 50;
                    parameter1.Value = txtNombre.Text.Trim();
                    parameter1.ParameterName = "@UsuarioName";

                    command.Parameters.Add(parameter1);

                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        people.Add(new Person
                        {
                            Id = dataReader[0].ToString(),
                            Name = dataReader[1].ToString()
                        });
                    }
                    conn.Close();
                    dgvListado.DataSource = people;
                    dgvListado.Refresh();

                }
                else
                {
                    MessageBox.Show("La conexión está cerrada");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error en el listado: \n" + ex.ToString());
            }
        }
    }
}
