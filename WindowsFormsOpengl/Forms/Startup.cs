using System;
using System.Windows.Forms;

namespace OpenTKTest.Forms
{
    public partial class Startup : Form
    {
        public Startup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = textBox1.Text;
            openFileDialog1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CalculateMesh.GetFile(textBox1.Text);
            Game.framerate = int.Parse(numericUpDown2.Value.ToString());
            Game.rotation_speed_x = int.Parse(numericUpDown1.Value.ToString());
            Game.rotation_speed_y = int.Parse(numericUpDown3.Value.ToString());
            Game.rotation_speed_z = int.Parse(numericUpDown4.Value.ToString());

            Visible = false;

            using (Game game = new Game())
            {
                game.Run(60);
            }
            Environment.Exit(1);
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            textBox1.Text = openFileDialog1.FileName;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            return;
        }
    }
}
