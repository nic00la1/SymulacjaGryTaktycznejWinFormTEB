using SymulacjaGryTaktycznejWinFormTEB.Classes;

namespace SymulacjaGryTaktycznejWinFormTEB;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void btnPojedynek_Click(object sender, EventArgs e)
    {
        Wojownik wojownik = new();
        Mag mag = new();

        using (StringWriter sw = new())
        {
            Console.SetOut(sw);
            Symulacja.Pojedynek(wojownik, mag);
            txtWynik.Text = sw.ToString();
        }
    }
}
