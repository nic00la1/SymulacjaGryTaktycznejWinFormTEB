using System.Diagnostics;
using System.Media;
using System.Timers;
using SymulacjaGryTaktycznejWinFormTEB.Classes;
using SymulacjaGryTaktycznejWinFormTEB.Classes.Symulacje;
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
    private bool isBattleActive;

    public Form1()
    {
        InitializeComponent();
        LoadResources();
        stopwatch = new Stopwatch();
        updateTimer = new Timer(1000);
        updateTimer.Elapsed += UpdateElapsedTime;
        isBattleActive = false;
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

        // Load new sound effects
        berserkSound = new WindowsMediaPlayer();
        berserkSound.URL = Path.Combine(basePath, "Resources", "berserk.mp3");
        berserkSound.settings.autoStart = false;

        shieldBlockSound = new WindowsMediaPlayer();
        shieldBlockSound.URL =
            Path.Combine(basePath, "Resources", "shieldBlock.mp3");
        shieldBlockSound.settings.autoStart = false;

        doubleStrikeSound = new WindowsMediaPlayer();
        doubleStrikeSound.URL =
            Path.Combine(basePath, "Resources", "doubleStrike.mp3");
        doubleStrikeSound.settings.autoStart = false;

        fireballSound = new WindowsMediaPlayer();
        fireballSound.URL = Path.Combine(basePath, "Resources", "fireball.mp3");
        fireballSound.settings.autoStart = false;

        healSound = new WindowsMediaPlayer();
        healSound.URL = Path.Combine(basePath, "Resources", "heal.mp3");
        healSound.settings.autoStart = false;

        manaShieldSound = new WindowsMediaPlayer();
        manaShieldSound.URL =
            Path.Combine(basePath, "Resources", "manaShield.mp3");
        manaShieldSound.settings.autoStart = false;

        try
        {
            // Load new visual effects
            fireballEffect =
                Image.FromFile(Path.Combine(basePath, "Resources",
                    "fireball.png"));
            healEffect =
                Image.FromFile(Path.Combine(basePath, "Resources", "heal.png"));
            manaShieldEffect =
                Image.FromFile(Path.Combine(basePath, "Resources",
                    "manaShield.png"));

            // Debugging: Check if images are loaded correctly
            Debug.WriteLine("Images loaded successfully.");
            Debug.WriteLine($"fireballEffect: {fireballEffect != null}");
            Debug.WriteLine($"healEffect: {healEffect != null}");
            Debug.WriteLine($"manaShieldEffect: {manaShieldEffect != null}");
        }
        catch (OutOfMemoryException ex)
        {
            MessageBox.Show(
                "Failed to load images due to insufficient memory. Please ensure the images are optimized and try again.",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Debug.WriteLine($"OutOfMemoryException: {ex.Message}");
        }
        catch (FileNotFoundException ex)
        {
            MessageBox.Show(
                "One or more image files were not found. Please check the file paths and try again.",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Debug.WriteLine($"FileNotFoundException: {ex.Message}");
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "An unexpected error occurred while loading images.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            Debug.WriteLine($"Exception: {ex.Message}");
        }

        picWojownik.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);

        // Set initial HP values
        lblWojownikHP.Text = $"HP: {DefaultWojownikHP}";
        lblMagHP.Text = $"HP: {DefaultMagHP}";
    }

    private async void btnPojedynek_Click(object sender, EventArgs e)
    {
        if (isBattleActive) return;
        isBattleActive = true;

        StartBattle();
        Wojownik wojownik = new() { Zycie = DefaultWojownikHP };
        Mag mag = new() { Zycie = DefaultMagHP };

        using (StringWriter sw = new())
        {
            Console.SetOut(sw);
            await Pojedynek.PojedynekAsync(wojownik, mag, UpdateUI,
                (atakujacy, obronca) => AnimateAttack(atakujacy, obronca),
                ShowVictoryScreen,
                PlayBerserkEffect,
                PlayShieldBlockEffect,
                PlayDoubleStrikeEffect,
                PlayFireballEffect,
                PlayHealEffect,
                PlayManaShieldEffect);
            txtWynik.Text = sw.ToString();
        }

        EndBattle();
    }

    private async void btnWalka_Click(object sender, EventArgs e)
    {
        if (isBattleActive) return;
        isBattleActive = true;

        StartBattle();
        Oddzia³ oddzial1 = new(new Wojownik() { Zycie = DefaultWojownikHP }, 10,
            "Oddzia³ 1");
        Oddzia³ oddzial2 =
            new(new Mag() { Zycie = DefaultMagHP }, 5, "Oddzia³ 2");

        using (StringWriter sw = new())
        {
            Console.SetOut(sw);
            await Walka.WalkaAsync(oddzial1, oddzial2, UpdateUI,
                (atakujacy, obronca) => AnimateAttack(atakujacy, obronca),
                ShowVictoryScreen);
            txtWynik.Text = sw.ToString();
        }

        EndBattle();
    }

    private async void btnWojna_Click(object sender, EventArgs e)
    {
        if (isBattleActive) return;
        isBattleActive = true;

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
            await Wojna.WojnaAsync(armia1, armia2, UpdateUI,
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
        isBattleActive = false;
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

    private void PlayBerserkEffect()
    {
        berserkSound.controls.play();
        PictureBox target = picWojownik;

        // Create a timer to handle the animation
        System.Windows.Forms.Timer timer = new() { Interval = 500 };
        timer.Tick += (s, e) =>
        {
            target.BackColor = Color.Transparent;
            Invalidate(new Rectangle(target.Location, target.Size));
            timer.Stop();
        };
        timer.Start();

        // Change the background color of the target to red
        target.BackColor = Color.Purple;
    }

    private void PlayShieldBlockEffect()
    {
        shieldBlockSound.controls.play();
        PictureBox target = picWojownik;

        // Create a timer to handle the animation
        System.Windows.Forms.Timer timer = new() { Interval = 500 };
        timer.Tick += (s, e) =>
        {
            target.BackColor = Color.Transparent;
            Invalidate(new Rectangle(target.Location, target.Size));
            timer.Stop();
        };
        timer.Start();

        // Change the background color of the target to blue
        target.BackColor = Color.Blue;
    }

    private void PlayDoubleStrikeEffect()
    {
        doubleStrikeSound.controls.play();
        PictureBox target = picWojownik;

        // Create a timer to handle the animation
        System.Windows.Forms.Timer timer = new() { Interval = 500 };
        timer.Tick += (s, e) =>
        {
            target.BackColor = Color.Transparent;
            Invalidate(new Rectangle(target.Location, target.Size));
            timer.Stop();
        };
        timer.Start();

        // Change the background color of the target to yellow
        target.BackColor = Color.Yellow;
    }

    private void PlayFireballEffect()
    {
        fireballSound.controls.play();
        PictureBox target = picMag;

        // Create a timer to handle the animation
        System.Windows.Forms.Timer animationTimer = new() { Interval = 20 };
        int animationStep = 0;
        int totalSteps = 25;
        int initialX = target.Location.X;
        int initialY = target.Location.Y;

        animationTimer.Tick += (s, e) =>
        {
            if (animationStep < totalSteps)
            {
                int offsetX =
                    (int)(Math.Sin(animationStep * Math.PI / totalSteps) * 20);
                int offsetY =
                    (int)(Math.Cos(animationStep * Math.PI / totalSteps) * 20);
                target.Location =
                    new Point(initialX + offsetX, initialY + offsetY);
                animationStep++;
            } else
            {
                target.Location = new Point(initialX, initialY);
                target.BackColor = Color.Transparent;
                Invalidate(new Rectangle(target.Location, target.Size));
                animationTimer.Stop();
            }
        };
        animationTimer.Start();

        // Change the background color of the target to orange
        target.BackColor = Color.Orange;
    }

    private void PlayHealEffect()
    {
        healSound.controls.play();
        PictureBox target = picMag;

        // Create a timer to handle the animation
        System.Windows.Forms.Timer timer = new() { Interval = 500 };
        timer.Tick += (s, e) =>
        {
            target.BackColor = Color.Transparent;
            Invalidate(new Rectangle(target.Location, target.Size));
            timer.Stop();
        };
        timer.Start();

        // Change the background color of the target to green
        target.BackColor = Color.Green;
    }

    private void PlayManaShieldEffect()
    {
        manaShieldSound.controls.play();
        PictureBox target = picMag;

        // Create a timer to handle the animation
        System.Windows.Forms.Timer timer = new() { Interval = 500 };
        timer.Tick += (s, e) =>
        {
            target.BackColor = Color.Transparent;
            Invalidate(new Rectangle(target.Location, target.Size));
            timer.Stop();
        };
        timer.Start();

        // Change the background color of the target to blue
        target.BackColor = Color.Blue;
    }
}
