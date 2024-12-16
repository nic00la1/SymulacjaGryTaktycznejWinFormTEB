using System.Diagnostics;
using System.Media;
using System.Timers;
using SymulacjaGryTaktycznejWinFormTEB.Classes;
using WMPLib;
using Timer = System.Timers.Timer;

namespace SymulacjaGryTaktycznejWinFormTEB;

public partial class Form1 : Form
{
    private const int DefaultWojownikHP = 50;
    private const int DefaultMagHP = 20;

    private WindowsMediaPlayer attackSound;
    private WindowsMediaPlayer magicalWhooshSound;
    private Image bloodEffect;
    private Stopwatch stopwatch;
    private Timer updateTimer;
    private Label lblWojownikHP;
    private Label lblMagHP;

    public Form1()
    {
        InitializeComponent();
        LoadResources();
        stopwatch = new Stopwatch();
        updateTimer = new Timer(1000);
        updateTimer.Elapsed += UpdateElapsedTime;
    }

    private void LoadResources()
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        picWojownik.Image =
            Image.FromFile(Path.Combine(basePath, "Resources", "wojownik.png"));
        picMag.Image =
            Image.FromFile(Path.Combine(basePath, "Resources", "mag.png"));
        pnlBattlefield.BackgroundImage =
            Image.FromFile(Path.Combine(basePath, "Resources", "map.png"));

        attackSound = new WindowsMediaPlayer();
        attackSound.URL = Path.Combine(basePath, "Resources", "attack.mp3");
        attackSound.settings.autoStart = false;

        magicalWhooshSound = new WindowsMediaPlayer();
        magicalWhooshSound.URL =
            Path.Combine(basePath, "Resources", "magicalWhoosh.mp3");
        magicalWhooshSound.settings.autoStart = false;

        // Load the blood effect image
        bloodEffect =
            Image.FromFile(Path.Combine(basePath, "Resources", "blood.png"));

        // Flip the warrior image horizontally
        picWojownik.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);

        // Set initial HP values
        lblWojownikHP.Text = $"HP: {DefaultWojownikHP}";
        lblMagHP.Text = $"HP: {DefaultMagHP}";
    }

    private async void btnPojedynek_Click(object sender, EventArgs e)
    {
        StartBattle();
        Wojownik wojownik = new() { Zycie = DefaultWojownikHP };
        Mag mag = new() { Zycie = DefaultMagHP };

        using (StringWriter sw = new())
        {
            Console.SetOut(sw);
            await Symulacja.PojedynekAsync(wojownik, mag, UpdateUI,
                (atakujacy, obronca) => AnimateAttack(atakujacy, obronca),
                ShowVictoryScreen);
            txtWynik.Text = sw.ToString();
        }

        EndBattle();
    }

    private async void btnWalka_Click(object sender, EventArgs e)
    {
        StartBattle();
        Oddzia³ oddzial1 = new(new Wojownik() { Zycie = DefaultWojownikHP }, 10,
            "Oddzia³ 1");
        Oddzia³ oddzial2 =
            new(new Mag() { Zycie = DefaultMagHP }, 5, "Oddzia³ 2");

        using (StringWriter sw = new())
        {
            Console.SetOut(sw);
            await Symulacja.WalkaAsync(oddzial1, oddzial2, UpdateUI,
                (atakujacy, obronca) => AnimateAttack(atakujacy, obronca),
                ShowVictoryScreen);
            txtWynik.Text = sw.ToString();
        }

        EndBattle();
    }

    private async void btnWojna_Click(object sender, EventArgs e)
    {
        StartBattle();
        Oddzia³ oddzial1 = new(new Wojownik() { Zycie = DefaultWojownikHP }, 10,
            "Oddzia³ 1");
        Oddzia³ oddzial2 =
            new(new Mag() { Zycie = DefaultMagHP }, 5, "Oddzia³ 2");
        Oddzia³ oddzial3 = new(new Wojownik() { Zycie = DefaultWojownikHP }, 8,
            "Oddzia³ 3");

        Armia armia1 = new(new List<Oddzia³> { oddzial1, oddzial3 },
            new Bohater(5, 5));
        Armia armia2 = new(new List<Oddzia³> { oddzial2 }, new Bohater(3, 3));

        using (StringWriter sw = new())
        {
            Console.SetOut(sw);
            await Symulacja.WojnaAsync(armia1, armia2, UpdateUI,
                (atakujacy, obronca) => AnimateAttack(atakujacy, obronca),
                ShowVictoryScreen);
            txtWynik.Text = sw.ToString();
        }

        EndBattle();
    }

    private void StartBattle()
    {
        stopwatch.Restart();
        updateTimer.Start();
    }

    private void EndBattle()
    {
        stopwatch.Stop();
        updateTimer.Stop();
        lblElapsedTime.Text = $"Czas: {stopwatch.Elapsed:hh\\:mm\\:ss\\.fff}";
        txtWynik.AppendText(
            $"Czas rozgrywki: {stopwatch.Elapsed:hh\\:mm\\:ss\\.fff}\n");
    }

    private void AnimateAttack(Jednostka atakujacy, Jednostka obronca)
    {
        PictureBox attacker = atakujacy is Wojownik ? picWojownik : picMag;
        PictureBox defender = obronca is Wojownik ? picWojownik : picMag;
        Label defenderHPLabel = obronca is Wojownik ? lblWojownikHP : lblMagHP;

        // Create a timer to handle the animation
        System.Windows.Forms.Timer timer = new() { Interval = 500 };
        timer.Tick += (s, e) =>
        {
            defender.BackColor = Color.Transparent;
            Invalidate(new Rectangle(defender.Location, defender.Size));
            timer.Stop();
        };
        timer.Start();

        // Draw the blood effect
        using (Graphics g = CreateGraphics())
        {
            g.DrawImage(bloodEffect,
                new Rectangle(defender.Location.X + defender.Width / 2 - 25,
                    defender.Location.Y + defender.Height / 2 - 25, 50, 50));
        }

        // Change the background color of the defender to red
        defender.BackColor = Color.Red;

        // Play the sound after the animation starts
        if (atakujacy is Wojownik)
            attackSound.controls.play();
        else if (atakujacy is Mag)
            magicalWhooshSound.controls.play();

        // Update the defender's HP label
        defenderHPLabel.Text = $"HP: {obronca.Zycie}";
    }

    private void UpdateUI(string message)
    {
        if (InvokeRequired)
            Invoke(new Action<string>(UpdateUI), message);
        else
            txtWynik.AppendText(message + Environment.NewLine);
    }

    private void ShowVictoryScreen(string message)
    {
        if (InvokeRequired)
            Invoke(new Action<string>(ShowVictoryScreen), message);
        else
        {
            EndBattle(); // Stop the stopwatch and timer here
            MessageBox.Show(message, "Zwyciêzca", MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            // Reset HP to default values
            lblWojownikHP.Text = $"HP: {DefaultWojownikHP}";
            lblMagHP.Text = $"HP: {DefaultMagHP}";
        }
    }

    private void UpdateElapsedTime(object sender, ElapsedEventArgs e)
    {
        if (InvokeRequired)
            Invoke(new Action(() =>
                lblElapsedTime.Text =
                    $"Czas: {stopwatch.Elapsed:hh\\:mm\\:ss\\.fff}"));
        else
            lblElapsedTime.Text =
                $"Czas: {stopwatch.Elapsed:hh\\:mm\\:ss\\.fff}";
    }
}
