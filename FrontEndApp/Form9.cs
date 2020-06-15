using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace FrontEndApp
{
    public partial class Form9 : Form
    {
        private string connstring = String.Format("Server ={0};Port={1};" + "User Id={2};Password={3};Database={4};"
                                    , "localhost", 5432, "postgres", "kh7vv819", "database");
        private NpgsqlConnection conn;
        private string sql;
        private NpgsqlCommand cmd;
        private DataTable dt;
        public Form9()
        {
            InitializeComponent();
        }

        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form9_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connstring);
            Select();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Select()
        {
            try
            {
                conn.Open();
                sql = @"select d.nome , count(p.id_p) totalpedidos
                        from pedido p inner join docente d on d.id = p.docente_id
                        group by d.nome
                        order by totalpedidos DESC;";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());


                conn.Close();
                dgvData.DataSource = null;
                dgvData.DataSource = dt;

            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Erro: " + ex.Message);

            }
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
