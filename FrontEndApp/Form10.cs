using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrontEndApp
{
    public partial class Form10 : Form
    {
        private string connstring = String.Format("Server ={0};Port={1};" + "User Id={2};Password={3};Database={4};"
                                    , "localhost", 5432, "postgres", "kh7vv819", "database");
        private NpgsqlConnection conn;
        private string sql;
        private NpgsqlCommand cmd;
        private DataTable dt;
        private int rowIndex = -1;
        public Form10()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connstring);
            Select();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void Select()
        {
            try
            {
                conn.Open();
                sql = @"select * from docente";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                
                conn.Close();
                dgvData.DataSource = null;
                dgvData.DataSource = dt;
            }
            catch(Exception ex)
            {
                conn.Close();
                MessageBox.Show("Erro: " + ex.Message);

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                rowIndex = e.RowIndex;
                textSaldo.Text = dgvData.Rows[e.RowIndex].Cells["saldoconta"].Value.ToString();
                textNome.Text = dgvData.Rows[e.RowIndex].Cells["nome"].Value.ToString();
                textEmail.Text = dgvData.Rows[e.RowIndex].Cells["email"].Value.ToString();
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            rowIndex = -1;
            textEmail.Text = textSaldo.Text = textNome.Text = null;
            textNome.Select();
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(rowIndex < 0)
            {
                MessageBox.Show("Escolha o docente");
                return;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int result = 0;
            if (rowIndex < 0)
            {
                MessageBox.Show("Escolha o docente");
                return;
            }
            try
            {
                conn.Open();
                sql = @"select * from st_delete(:id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("id", int.Parse(dgvData.Rows[rowIndex].Cells["id"].Value.ToString()));
                result = (int)cmd.ExecuteScalar();
                conn.Close();
                if (result == 1)
                {
                    MessageBox.Show("Deleted com sucesso");
                    rowIndex = -1;
                    Select();
                }
                
            }
            catch(Exception ex)
            {
                conn.Close();
                MessageBox.Show("Delete falhou. Erro : " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int result = 0;
            if(rowIndex < 0)
            {
                try
                {
                    conn.Open();
                    sql = "select * from st_insert(:saldoconta,:email,:nome)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("nome",textNome.Text);
                    cmd.Parameters.AddWithValue("email",textEmail.Text);
                    cmd.Parameters.AddWithValue("saldoconta", int.Parse(textSaldo.Text));
                    result = (int)cmd.ExecuteScalar();
                    conn.Close();
                    if(result == 1)
                    {
                        MessageBox.Show("Insert com sucesso");
                        Select();
                    }
                    else
                    {
                        MessageBox.Show("Insert falhou");
                    }
                   
                    
                }
                catch(Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("Inserted falhou. Erro : " + ex.Message);
                }
            }
            else
            {
                try
                {
                    conn.Open();
                    sql = @"select * from st_update(:id,:saldoconta,:email,:nome)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("id", int.Parse(dgvData.Rows[rowIndex].Cells["id"].Value.ToString()));
                    cmd.Parameters.AddWithValue("nome", textNome.Text);
                    cmd.Parameters.AddWithValue("email", textEmail.Text);
                    cmd.Parameters.AddWithValue("saldoconta", int.Parse(textSaldo.Text));
                    result = (int)cmd.ExecuteScalar();
                    conn.Close();
                    if (result == 1)
                    {
                        MessageBox.Show("Update com sucesso");
                        Select();
                    }
                    else
                    {
                        MessageBox.Show("Update falhou");
                    }
                    
                    
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("Update falhou. Erro : " + ex.Message);
                }
                result = 0;
            }
        }
    }
}
