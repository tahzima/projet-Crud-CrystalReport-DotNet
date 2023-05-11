using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projetC_BD
{
    public partial class Form1 : Form
    {
        MySqlConnection connexion = new MySqlConnection("Server=localhost;database=adodb;Uid=root;Pwd=;");
        public Form1()
        {
            InitializeComponent();
            actualiser();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            actualiser();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void vid()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
        }
        public void actualiser()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM etudiant", connexion);
            DataTable dataTable = new DataTable();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;

        }

        public void ajout(string nom, string prenom, string cne)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO `etudiant` (nom,prenom,cne) values ('" + nom.ToString() + "','" + prenom.ToString() + "','" + cne.ToString() + "')", connexion);
            try
            {
                connexion.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Bien ajoutées");
                    actualiser();

                }
                connexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                connexion.Close();
            }
        }

        public void supprimer()
        {
            int l = dataGridView1.CurrentCell.RowIndex;
            int ID = (int)dataGridView1.Rows[l].Cells[0].Value;
            MySqlCommand cmd = new MySqlCommand("DELETE FROM etudiant WHERE `etudiant`.`id`=" + ID + "", connexion);
            try
            {
                connexion.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("L'étudiant N° " + ID.ToString() + " a été bien supprimer");
                    actualiser();
                }
                connexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                connexion.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            vid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nom = textBox1.Text;
            string prenom = textBox2.Text;
            string cne = textBox3.Text;
            ajout(nom, prenom, cne);
            vid();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            supprimer();
        }

        int indice = -1;
        int ID2;

        private void button6_Click(object sender, EventArgs e)
        {
            if (indice == -1)
            {
                indice = dataGridView1.CurrentCell.RowIndex;
                ID2 = (int)dataGridView1.Rows[indice].Cells[0].Value;
                textBox1.Text = dataGridView1.Rows[indice].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[indice].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.Rows[indice].Cells[3].Value.ToString();
                button1.Enabled = false;
                button2.Enabled = false;
                button5.Enabled = false;
                button6.Text = "Valider";
            }
            else
            {
                MySqlCommand cmd = new MySqlCommand("update etudiant set nom='" + textBox1.Text + "', prenom='" + textBox2.Text + "', cne='" + textBox3.Text + "' where id=" + ID2 + "", connexion);
                try
                {
                    connexion.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("L'etudiant N " + ID2.ToString() + " a ete bien modifier");
                        actualiser();
                    }
                    connexion.Clone();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connexion.Close();
                }
                button1.Enabled = true;
                button2.Enabled = true;
                button5.Enabled = true;
                button6.Text = "Modifier";
                actualiser();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }
    }
}
