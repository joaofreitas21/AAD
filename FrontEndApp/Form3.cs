using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace FrontEndApp
{
    public partial class Form3 : Form
    {
        private string connstring = String.Format("Server ={0};Port={1};" + "User Id={2};Password={3};Database={4};"
                                    , "localhost", 5432, "postgres", "kh7vv819", "database");
        private NpgsqlConnection conn;
        private string sql;
        private NpgsqlCommand cmd;
        private DataTable dt;
        public Form3()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connstring);
            Select();
        }
        private void Select()
        {
            try
            {
                conn.Open();
                sql = @"select d.nome, p.datapedido,p.valorpedido,tp.tipo, p.dataatribuicao,p.valoratribuido,est.tipo 
                        from pedido p inner join docente d on p.docente_id = d.id
				                      inner join premio e on e.id_p = p.id_p
				                      inner join tipopremio tp on tp.id_tp = e.id_tp
				                      inner join estado est on est.id_est = p.estadoid_est;";
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
