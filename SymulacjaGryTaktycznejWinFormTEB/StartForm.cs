using System;
using System.Windows.Forms;

namespace SymulacjaGryTaktycznejWinFormTEB
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
            PopulateComboBoxes();
        }

        private void PopulateComboBoxes()
        {
            // Add available characters to cbCharacter
            cbCharacter.Items.Add("Wojownik");
            cbCharacter.Items.Add("Mag");

            // Add available opponents to cbOpponent
            cbOpponent.Items.Add("Wojownik");
            cbOpponent.Items.Add("Mag");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (cbCharacter.SelectedItem == null || cbOpponent.SelectedItem == null)
            {
                MessageBox.Show("Please select both a character and an opponent.");
                return;
            }

            string selectedCharacter = cbCharacter.SelectedItem.ToString();
            string selectedOpponent = cbOpponent.SelectedItem.ToString();

            Form1 mainForm = new Form1(selectedCharacter, selectedOpponent);
            mainForm.Show();
            Hide();
        }
    }
}
