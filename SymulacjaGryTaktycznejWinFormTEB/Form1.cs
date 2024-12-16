using System.Media;
using SymulacjaGryTaktycznejWinFormTEB.Classes;
using WMPLib;
using Timer = System.Timers.Timer;

namespace SymulacjaGryTaktycznejWinFormTEB;

public partial class Form1 : Form
{
    private WindowsMediaPlayer attackSound;
    private WindowsMediaPlayer magicalWhooshSound;

    public Form1()
    {
        InitializeComponent();
        LoadResources();
    }

    private void LoadResources()
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        picWojownik.Image =
            Image.FromFile(Path.Combine(basePath, "Resources", "wojownik.png"));
        picMag.Image =
            Image.FromFile(Path.Combine(basePath, "Resources", "mag.png"));
        pnlMap.BackgroundImage =
            Image.FromFile(Path.Combine(basePath, "Resources", "map.png"));
        attackSound = new WindowsMediaPlayer();
        attackSound.URL = Path.Combine(basePath, "Resources", "attack.mp3");
        magicalWhooshSound = new WindowsMediaPlayer();
        magicalWhooshSound.URL =
            Path.Combine(basePath, "Resources", "magicalWhoosh.mp3");
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

        AnimateAttack(picWojownik, picMag, wojownik, mag);
    }

    private void btnWalka_Click(object sender, EventArgs e)
    {
        Oddzia³ oddzial1 = new(new Wojownik(), 10, "Oddzia³ 1");
        Oddzia³ oddzial2 = new(new Mag(), 5, "Oddzia³ 2");

        using (StringWriter sw = new())
        {
            Console.SetOut(sw);
            Symulacja.Walka(oddzial1, oddzial2);
            txtWynik.Text = sw.ToString();
        }

        AnimateAttack(picWojownik, picMag, oddzial1.Jednostka,
            oddzial2.Jednostka);
    }

    private void btnWojna_Click(object sender, EventArgs e)
    {
        Oddzia³ oddzial1 = new(new Wojownik(), 10, "Oddzia³ 1");
        Oddzia³ oddzial2 = new(new Mag(), 5, "Oddzia³ 2");
        Oddzia³ oddzial3 = new(new Wojownik(), 8, "Oddzia³ 3");

        Armia armia1 = new(new List<Oddzia³> { oddzial1, oddzial3 },
            new Bohater(5, 5));
        Armia armia2 = new(new List<Oddzia³> { oddzial2 }, new Bohater(3, 3));

        using (StringWriter sw = new())
        {
            Console.SetOut(sw);
            Symulacja.Wojna(armia1, armia2);
            txtWynik.Text = sw.ToString();
        }

        AnimateAttack(picWojownik, picMag, oddzial1.Jednostka,
            oddzial2.Jednostka);
    }

    private void AnimateAttack(PictureBox attacker,
                               PictureBox defender,
                               Jednostka atakujacy,
                               Jednostka obronca
    )
    {
        if (atakujacy is Wojownik)
            attackSound.controls.play();
        else if (atakujacy is Mag) magicalWhooshSound.controls.play();

        defender.BackColor = Color.Red;
        System.Windows.Forms.Timer timer = new() { Interval = 500 };
        timer.Tick += (s, e) =>
        {
            defender.BackColor = Color.Transparent;
            timer.Stop();
        };
        timer.Start();
    }
}
