using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace FrontEndApp
{
    public partial class Form6 : Form
    {
        private string connstring = String.Format("Server ={0};Port={1};" + "User Id={2};Password={3};Database={4};"
                                    , "localhost", 5432, "postgres", "kh7vv819", "database");
        private NpgsqlConnection conn;
        private string sql;
        private NpgsqlCommand cmd;
        private DataTable dt;
        private DataTable dt2;
        private int rowIndex = -1;

        public Form6()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connstring);
            Select();
            LoadData();
            comboBox.DataSource = dt2;
            comboBox.DisplayMember = "tipo_aula";
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int result = 0;
            if (rowIndex < 0)
            {

                try
                {
                    conn.Open();
                    sql = @"select * from ret_idtipo(:tipoa)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("tipoa", comboBox.Text);
                    
                    result = (int)cmd.ExecuteScalar();
                    sql = @"select * from delete_he(:id_d,:qthoras,:tipoa)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("id_d", int.Parse(textId.Text));
                    cmd.Parameters.AddWithValue("qthoras", int.Parse(textQtd.Text));
                    cmd.Parameters.AddWithValue("tipoa", result);
                    result = (int)cmd.ExecuteScalar();
                    conn.Close();
                    if (result == 1)
                    {
                        MessageBox.Show("Retirado com sucesso");
                        Select();
                    }

                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("Delete falhou. Erro : " + ex.Message);
                }
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int result = 0;
            if (rowIndex < 0)
            {

                try
                {
                    conn.Open();
                    sql = @"select * from ret_idtipo(:tipoa)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("tipoa", comboBox.Text);

                    result = (int)cmd.ExecuteScalar();
                    sql = @"select * from insert_he(:id_d,:qthoras,:tipoa)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("id_d", int.Parse(textId.Text));
                    cmd.Parameters.AddWithValue("qthoras", int.Parse(textQtd.Text));
                    cmd.Parameters.AddWithValue("tipoa", result);
                    result = (int)cmd.ExecuteScalar();
                    conn.Close();
                    if (result == 1)
                    {
                        MessageBox.Show("Adicionado com sucesso");
                        Select();
                    }

                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("Add falhou. Erro : " + ex.Message);
                }
            }
        }
        
        private void Select()
        {
            try
            {
                conn.Open();
                sql = @"select h.id,h.docente_id, h.qtdhoras,t.tipo
                        from horasextra h inner join tipoaula t on h.tipoaula_id = t.id
                        order by h.id;";
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

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                rowIndex = e.RowIndex;
                textId.Text = dgvData.Rows[e.RowIndex].Cells["docente_id"].Value.ToString();
                textQtd.Text = dgvData.Rows[e.RowIndex].Cells["qtdhoras"].Value.ToString();
                comboBox.Text = dgvData.Rows[e.RowIndex].Cells["tipoaula_id"].Value.ToString();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadData()
        {
            conn.Open();
            sql = @"select * from ret_tipo()";
            cmd = new NpgsqlCommand(sql, conn);
            dt2 = new DataTable();
            dt2.Load(cmd.ExecuteReader());
            conn.Close();

        }
    }
}
