using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace FrontEndApp
{
    public partial class Form4 : Form
    {
        private string connstring = String.Format("Server ={0};Port={1};" + "User Id={2};Password={3};Database={4};"
                                    , "localhost", 5432, "postgres", "kh7vv819", "database");
        private NpgsqlConnection conn;
        private string sql;
        private NpgsqlCommand cmd;
        private DataTable dt;

        public Form4()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connstring);
            Select();
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void Select()
        {
            try
            {
                conn.Open();
                sql = @"select d.nome,h.qtdhoras,t.tipo
                        from horasextra h inner join docente d on d.id = h.docente_id
			                              inner join tipoaula t on t.id = h.tipoaula_id;";
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
    }
}
