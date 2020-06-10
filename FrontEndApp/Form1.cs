using System;
using System.Windows.Forms;

namespace FrontEndApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            customizeDesign();
        }
        private void customizeDesign()
        {
            panelSubMenu.Visible = false;
            panelSubMenu2.Visible = false;
            panel1.Visible = false;
            panel2.Visible = false;
        }
        private void hideSubMenu()
        {
            if (panelSubMenu.Visible == true)
                panelSubMenu.Visible = false;
            if(panelSubMenu2.Visible == true)
                panelSubMenu2.Visible = false;
            if(panel1.Visible == true)
                panel1.Visible = false;
            if(panel2.Visible == true)
                panel2.Visible = false;

        }
        private void showSubMenu(Panel subMenu)
        {
            if(subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubMenu);
        }

        private void btnMedia_Click(object sender, EventArgs e)
        {
            openChildForm(new Form2());
            /* meu codigo*/
            hideSubMenu();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            openChildForm(new Form3());
            /* meu codigo*/
            hideSubMenu();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            /* meu codigo*/
            hideSubMenu();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            /* meu codigo*/
            hideSubMenu();

        }

        private void btnMedia2_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubMenu2);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            openChildForm(new Form4());
            /* meu codigo*/
            hideSubMenu();


        }

        private void button8_Click(object sender, EventArgs e)
        {
            openChildForm(new Form5());
            /* meu codigo*/
            hideSubMenu();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            openChildForm(new Form6());
            /* meu codigo*/
            hideSubMenu();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            /* meu codigo*/
            hideSubMenu();

        }
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChild.Controls.Add(childForm);
            panelChild.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }

        private void panelChild_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            showSubMenu(panel1);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            showSubMenu(panel2);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            openChildForm(new Form7());
            hideSubMenu();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openChildForm(new Form8());
            hideSubMenu();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            openChildForm(new Form9());
            hideSubMenu();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            openChildForm(new Form10());
            hideSubMenu();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
