using System.Diagnostics;
using System.Drawing.Imaging;
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
    private Stopwatch stopwatch;
    private Timer updateTimer;
    private Label lblWojownikHP;
    private Label lblMagHP;
    private bool isBattleActive;
    private Jednostka player;
    private Jednostka opponent;

    public Form1(string selectedCharacter, string selectedOpponent)
    {
        InitializeComponent();
        LoadResources(selectedCharacter, selectedOpponent);
        LoadInitialImages(); // Load initial images
        stopwatch = new Stopwatch();
        updateTimer = new Timer(1000);
        updateTimer.Elapsed += UpdateElapsedTime;
        isBattleActive = false;

        // Initialize player and opponent based on selection
        if (selectedCharacter == "Wojownik")
            player = new Wojownik() { Zycie = DefaultWojownikHP };
        else
            player = new Mag() { Zycie = DefaultMagHP };

        if (selectedOpponent == "Wojownik")
            opponent = new Wojownik() { Zycie = DefaultWojownikHP };
        else
            opponent = new Mag() { Zycie = DefaultMagHP };

        // Set initial HP values
        lblWojownikHP.Text = $"HP: {player.Zycie}";
        lblMagHP.Text = $"HP: {opponent.Zycie}";
    }

    private async void LoadInitialImages()
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        picWojownik.Image =
            await LoadImageAsync(Path.Combine(basePath, "Resources",
                "wojownik.png"));
        picWojownik.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
        picMag.Image =
            await LoadImageAsync(Path.Combine(basePath, "Resources",
                "mag.png"));
        pnlBattlefield.BackgroundImage =
            await LoadImageAsync(Path.Combine(basePath, "Resources",
                "map.png"));
    }

    private void LoadResources(string selectedCharacter,
                               string selectedOpponent
    )
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;

        // Initialize sound effects without playing them
        attackSound = new WindowsMediaPlayer();
        attackSound.URL = Path.Combine(basePath, "Resources", "attack.mp3");
        attackSound.settings.autoStart = false;
        attackSound.controls.stop(); // Ensure the sound is stopped

        magicalWhooshSound = new WindowsMediaPlayer();
        magicalWhooshSound.URL =
            Path.Combine(basePath, "Resources", "magicalWhoosh.mp3");
        magicalWhooshSound.settings.autoStart = false;
        magicalWhooshSound.controls.stop(); // Ensure the sound is stopped

        // Load new sound effects
        berserkSound = new WindowsMediaPlayer();
        berserkSound.URL = Path.Combine(basePath, "Resources", "berserk.mp3");
        berserkSound.settings.autoStart = false;
        berserkSound.controls.stop(); // Ensure the sound is stopped

        shieldBlockSound = new WindowsMediaPlayer();
        shieldBlockSound.URL =
            Path.Combine(basePath, "Resources", "shieldBlock.mp3");
        shieldBlockSound.settings.autoStart = false;
        shieldBlockSound.controls.stop(); // Ensure the sound is stopped

        doubleStrikeSound = new WindowsMediaPlayer();
        doubleStrikeSound.URL =
            Path.Combine(basePath, "Resources", "doubleStrike.mp3");
        doubleStrikeSound.settings.autoStart = false;
        doubleStrikeSound.controls.stop(); // Ensure the sound is stopped

        fireballSound = new WindowsMediaPlayer();
        fireballSound.URL = Path.Combine(basePath, "Resources", "fireball.mp3");
        fireballSound.settings.autoStart = false;
        fireballSound.controls.stop(); // Ensure the sound is stopped

        healSound = new WindowsMediaPlayer();
        healSound.URL = Path.Combine(basePath, "Resources", "heal.mp3");
        healSound.settings.autoStart = false;
        healSound.controls.stop(); // Ensure the sound is stopped

        manaShieldSound = new WindowsMediaPlayer();
        manaShieldSound.URL =
            Path.Combine(basePath, "Resources", "manaShield.mp3");
        manaShieldSound.settings.autoStart = false;
        manaShieldSound.controls.stop(); // Ensure the sound is stopped

        // Initialize new sound effects
        battleCrySound = new WindowsMediaPlayer();
        battleCrySound.URL =
            Path.Combine(basePath, "Resources", "battleCry.mp3");
        battleCrySound.settings.autoStart = false;
        battleCrySound.controls.stop(); // Ensure the sound is stopped

        arrowRainSound = new WindowsMediaPlayer();
        arrowRainSound.URL =
            Path.Combine(basePath, "Resources", "arrowRain.mp3");
        arrowRainSound.settings.autoStart = false;
        arrowRainSound.controls.stop(); // Ensure the sound is stopped

        warDrumSound = new WindowsMediaPlayer();
        warDrumSound.URL = Path.Combine(basePath, "Resources", "warDrum.mp3");
        warDrumSound.settings.autoStart = false;
        warDrumSound.controls.stop(); // Ensure the sound is stopped

        // Set initial HP values
        lblWojownikHP.Text = $"HP: {DefaultWojownikHP}";
        lblMagHP.Text = $"HP: {DefaultMagHP}";
    }


    private async Task<Image> LoadImageAsync(string filePath)
    {
        return await Task.Run(() => Image.FromFile(filePath));
    }

    private async void btnPojedynek_Click(object sender, EventArgs e)
    {
        if (isBattleActive) return;
        isBattleActive = true;

        StartBattle();
        Wojownik wojownik = new() { Zycie = DefaultWojownikHP };
        Mag mag = new() { Zycie = DefaultMagHP };

        // Clear previous results
        txtWynik.Clear();

        // Hide unit labels and progress bars for pojedynki
        lblWojownikUnits.Visible = false;
        lblMagUnits.Visible = false;
        pbWojownikHP.Visible = false;
        pbMagHP.Visible = false;

        // Show HP labels for pojedynki
        lblWojownikHP.Visible = true;
        lblMagHP.Visible = true;

        // Load images on demand
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        picWojownik.Image =
            await LoadImageAsync(Path.Combine(basePath, "Resources",
                "wojownik.png"));
        picWojownik.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
        picMag.Image =
            await LoadImageAsync(Path.Combine(basePath, "Resources",
                "mag.png"));
        pnlBattlefield.BackgroundImage =
            await LoadImageAsync(Path.Combine(basePath, "Resources",
                "map.png"));

        using (StringWriter sw = new())
        {
            Console.SetOut(sw);
            await Pojedynek.PojedynekAsync(wojownik, mag, UpdateUI,
                (atakujacy, obronca) => AnimateAttack(atakujacy, obronca),
                (message) =>
                    ShowVictoryScreen(message, null, null, wojownik, mag),
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
        Oddzia� oddzial1 = new(new Wojownik() { Zycie = DefaultWojownikHP }, 30,
            "Dziewczynki");
        Oddzia� oddzial2 = new(new Wojownik() { Zycie = DefaultWojownikHP }, 30,
            "Ch�opcy");

        // Clear previous results
        txtWynik.Clear();

        // Show unit labels and progress bars for walki
        lblWojownikUnits.Visible = true;
        lblMagUnits.Visible = true;
        pbWojownikHP.Visible = true;
        pbMagHP.Visible = true;

        // Hide HP labels for walki
        lblWojownikHP.Visible = false;
        lblMagHP.Visible = false;

        // Set initial HP values for progress bars
        pbWojownikHP.Maximum = oddzial1.Ilosc * DefaultWojownikHP;
        pbWojownikHP.Value = oddzial1.Ilosc * DefaultWojownikHP;
        pbMagHP.Maximum = oddzial2.Ilosc * DefaultWojownikHP;
        pbMagHP.Value = oddzial2.Ilosc * DefaultWojownikHP;

        // Load images on demand
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        picWojownik.Image =
            await LoadImageAsync(Path.Combine(basePath, "Resources",
                "oddzial1.jpg"));
        picWojownik.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
        picMag.Image =
            await LoadImageAsync(Path.Combine(basePath, "Resources",
                "oddzial2.jpg"));
        pnlBattlefield.BackgroundImage =
            await LoadImageAsync(
                Path.Combine(basePath, "Resources", "map2.png"));

        using (StringWriter sw = new())
        {
            Console.SetOut(sw);
            await Walka.WalkaOddzialowAsync(oddzial1, oddzial2, UpdateUI,
                (atakujacy, obronca) => AnimateAttack(oddzial1, oddzial2),
                (message, o1, o2) => ShowVictoryScreen(message, o1, o2),
                PlayBattleCryEffect,
                PlayArrowRainEffect,
                PlayWarDrumEffect);
            txtWynik.Text = sw.ToString();
        }

        EndBattle(oddzial1.Ilosc, oddzial2.Ilosc);
    }


    private async void btnWojna_Click(object sender, EventArgs e)
    {
        if (isBattleActive) return;
        isBattleActive = true;

        StartBattle();
        Oddzia� oddzial1 = new(new Wojownik() { Zycie = DefaultWojownikHP }, 10,
            "Oddzia� 1");
        Oddzia� oddzial2 =
            new(new Mag() { Zycie = DefaultMagHP }, 5, "Oddzia� 2");
        Oddzia� oddzial3 = new(new Wojownik() { Zycie = DefaultWojownikHP }, 8,
            "Oddzia� 3");

        Armia armia1 = new(new List<Oddzia�> { oddzial1, oddzial3 },
            new Bohater(5, 5));
        Armia armia2 = new(new List<Oddzia�> { oddzial2 }, new Bohater(3, 3));

        // Clear previous results
        txtWynik.Clear();

        // Show unit labels and progress bars for wojna
        lblWojownikUnits.Visible = true;
        lblMagUnits.Visible = true;
        pbWojownikHP.Visible = true;
        pbMagHP.Visible = true;

        // Hide HP labels for wojna
        lblWojownikHP.Visible = false;
        lblMagHP.Visible = false;

        // Set initial HP values for progress bars
        pbWojownikHP.Maximum = oddzial1.Ilosc * DefaultWojownikHP;
        pbWojownikHP.Value = oddzial1.Ilosc * DefaultWojownikHP;
        pbMagHP.Maximum = oddzial2.Ilosc * DefaultMagHP;
        pbMagHP.Value = oddzial2.Ilosc * DefaultMagHP;

        // Load images on demand
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        picWojownik.Image =
            await LoadImageAsync(Path.Combine(basePath, "Resources",
                "armia1.png"));
        picWojownik.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
        picMag.Image =
            await LoadImageAsync(Path.Combine(basePath, "Resources",
                "armia2.png"));
        pnlBattlefield.BackgroundImage =
            await LoadImageAsync(Path.Combine(basePath, "Resources",
                "map3.png"));

        using (StringWriter sw = new())
        {
            Console.SetOut(sw);
            await Wojna.WojnaAsync(armia1, armia2, UpdateUI,
                (atakujacy, obronca) => AnimateAttack(oddzial1, oddzial2),
                (message) => ShowVictoryScreen(message, null, null));
            txtWynik.Text = sw.ToString();
        }

        EndBattle();
    }

    private void StartBattle()
    {
        stopwatch.Restart();
        updateTimer.Start();
    }

    private void EndBattle(int wojownikUnits = 0, int magUnits = 0)
    {
        stopwatch.Stop();
        updateTimer.Stop();
        lblElapsedTime.Text = $"Czas: {stopwatch.Elapsed:hh\\:mm\\:ss\\.fff}";
        txtWynik.AppendText(
            $"Czas rozgrywki: {stopwatch.Elapsed:hh\\:mm\\:ss\\.fff}\n");
        isBattleActive = false;

        // Set unit counts and HP to actual values
        lblWojownikUnits.Text = $"Jednostki: {wojownikUnits}";
        lblMagUnits.Text = $"Jednostki: {magUnits}";
        lblWojownikHP.Text = $"HP: {DefaultWojownikHP}";
        lblMagHP.Text = $"HP: {DefaultMagHP}";
        pbWojownikHP.Maximum = wojownikUnits * DefaultWojownikHP;
        pbWojownikHP.Value = Math.Min(pbWojownikHP.Maximum,
            Math.Max(0, wojownikUnits * DefaultWojownikHP));
        pbMagHP.Maximum = magUnits * DefaultMagHP;
        pbMagHP.Value = Math.Min(pbMagHP.Maximum,
            Math.Max(0, magUnits * DefaultMagHP));
    }

    private void AnimateAttack(Jednostka atakujacy, Jednostka obronca)
    {
        PictureBox attacker = atakujacy is Wojownik ? picWojownik : picMag;
        PictureBox defender = obronca is Wojownik ? picWojownik : picMag;
        Label defenderHPLabel = obronca is Wojownik ? lblWojownikHP : lblMagHP;
        ProgressBar defenderHPBar =
            obronca is Wojownik ? pbWojownikHP : pbMagHP;

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

        // Update the defender's HP label and progress bar
        defenderHPLabel.Text = $"HP: {obronca.Zycie}";
        defenderHPBar.Value = Math.Min(defenderHPBar.Maximum,
            Math.Max(0, obronca.Zycie));
    }

    private void AnimateAttack(Oddzia� atakujacyOddzial, Oddzia� obroncaOddzial)
    {
        PictureBox attacker = atakujacyOddzial.Jednostka is Wojownik
            ? picWojownik
            : picMag;
        PictureBox defender = obroncaOddzial.Jednostka is Wojownik
            ? picWojownik
            : picMag;
        Label defenderUnitLabel = obroncaOddzial.Jednostka is Wojownik
            ? lblWojownikUnits
            : lblMagUnits;
        ProgressBar defenderHPBar = obroncaOddzial.Jednostka is Wojownik
            ? pbWojownikHP
            : pbMagHP;

        // Create a timer to handle the animation
        System.Windows.Forms.Timer timer = new() { Interval = 500 };
        timer.Tick += (s, e) =>
        {
            defender.BackColor = Color.Transparent;
            defender.Size = new Size(defender.Size.Width - 10,
                defender.Size.Height - 10);
            defender.BorderStyle = BorderStyle.None;
            Invalidate(new Rectangle(defender.Location, defender.Size));
            timer.Stop();
        };
        timer.Start();

        // Enlarge the defender's size by 10px and add a red border
        defender.Size = new Size(defender.Size.Width + 10,
            defender.Size.Height + 10);
        defender.BorderStyle = BorderStyle.FixedSingle;

        // Play the sound after the animation starts
        if (atakujacyOddzial.Jednostka is Wojownik)
            attackSound.controls.play();
        else if (atakujacyOddzial.Jednostka is Mag)
            magicalWhooshSound.controls.play();

        // Update the defender's unit label and progress bar
        defenderUnitLabel.Text = $"Jednostki: {obroncaOddzial.Ilosc}";
        int totalDefenderHP =
            obroncaOddzial.Ilosc * obroncaOddzial.Jednostka.Zycie;
        defenderHPBar.Maximum =
            obroncaOddzial.Ilosc *
            DefaultWojownikHP; // Ustaw maksymaln� warto�� paska post�pu
        defenderHPBar.Value = Math.Min(defenderHPBar.Maximum,
            Math.Max(0, totalDefenderHP));
    }


    private void UpdateUI(string message)
    {
        if (InvokeRequired)
            Invoke(new Action<string>(UpdateUI), message);
        else
        {
            if (message.StartsWith("UPDATE_HP"))
            {
                string[] parts = message.Split(' ');
                string nazwa = parts[1];
                int hp = int.Parse(parts[2]);

                if (nazwa == "Dziewczynki" || nazwa == "Oddzia� 1" ||
                    nazwa == "Oddzia� 3")
                    pbWojownikHP.Value = Math.Min(pbWojownikHP.Maximum,
                        Math.Max(0, hp));
                else if (nazwa == "Ch�opcy" || nazwa == "Oddzia� 2")
                    pbMagHP.Value = Math.Min(pbMagHP.Maximum, Math.Max(0, hp));
            } else if (message.StartsWith("UPDATE_UNITS"))
            {
                string[] parts = message.Split(' ');
                string nazwa = parts[1];
                int units = int.Parse(parts[2]);

                if (nazwa == "Dziewczynki" || nazwa == "Oddzia� 1" ||
                    nazwa == "Oddzia� 3")
                    lblWojownikUnits.Text = $"Jednostki: {units}";
                else if (nazwa == "Ch�opcy" || nazwa == "Oddzia� 2")
                    lblMagUnits.Text = $"Jednostki: {units}";
            } else if (message.StartsWith("UPDATE_DAMAGE"))
            {
                string[] parts = message.Split(' ');
                string nazwa = parts[1];
                int damage = int.Parse(parts[2]);

                txtWynik.AppendText($"{nazwa} zadaje {damage} obra�e�.\n");
            } else
                txtWynik.AppendText(message + Environment.NewLine);
        }
    }

    private void ShowVictoryScreen(string message,
                                   Oddzia�? oddzial1,
                                   Oddzia�? oddzial2,
                                   Jednostka? jednostka1 = null,
                                   Jednostka? jednostka2 = null
    )
    {
        if (InvokeRequired)
            Invoke(
                new Action<string, Oddzia�?, Oddzia�?, Jednostka?, Jednostka?>(
                    ShowVictoryScreen), message, oddzial1, oddzial2, jednostka1,
                jednostka2);
        else
        {
            // Stop the stopwatch and timer here
            int wojownikUnits = oddzial1?.Ilosc ?? 0;
            int magUnits = oddzial2?.Ilosc ?? 0;
            EndBattle(wojownikUnits, magUnits);

            // Create summary
            string summary =
                $"Czas rozgrywki: {stopwatch.Elapsed:hh\\:mm\\:ss\\.fff}\n" +
                $"Wynik: {message}\n";

            if (oddzial1 != null && oddzial2 != null)
                summary +=
                    $"Oddzia� {oddzial1.Nazwa} ma {oddzial1.Ilosc} jednostek.\n" +
                    $"Oddzia� {oddzial2.Nazwa} ma {oddzial2.Ilosc} jednostek.\n";
            else if (jednostka1 != null && jednostka2 != null)
                summary +=
                    $"HP {jednostka1.GetType().Name}: {jednostka1.Zycie}\n" +
                    $"HP {jednostka2.GetType().Name}: {jednostka2.Zycie}\n";

            // Show summary form
            using (SummaryForm summaryForm = new("Zwyci�zca", summary))
            {
                summaryForm.ShowDialog();
            }
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

        // Change the background color of the target to purple
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

    private void PlayBattleCryEffect(Oddzia� atakujacy)
    {
        // Play a battle cry sound effect
        battleCrySound.controls.play();
        PictureBox target = picWojownik;

        // Increase the attack power of the attacking unit for the next attack
        atakujacy.Jednostka.Atak += 10;

        // Create a timer to handle the animation
        System.Windows.Forms.Timer timer = new() { Interval = 500 };
        timer.Tick += (s, e) =>
        {
            target.BackColor = Color.Orange;
            Invalidate(new Rectangle(target.Location, target.Size));
            timer.Stop();
        };
        timer.Start();

        // Change the background color of the target to orange
        target.BackColor = Color.Orange;
    }

    private void PlayArrowRainEffect(Oddzia� obronca)
    {
        // Play an arrow rain sound effect
        arrowRainSound.controls.play();
        PictureBox target = picMag;

        // Deal additional damage to the defending unit
        obronca.Jednostka.Zycie -= 10;

        // Create a timer to handle the animation
        System.Windows.Forms.Timer timer = new() { Interval = 500 };
        timer.Tick += (s, e) =>
        {
            target.BackColor = Color.Gray;
            Invalidate(new Rectangle(target.Location, target.Size));
            timer.Stop();
        };
        timer.Start();

        // Change the background color of the target to gray
        target.BackColor = Color.Gray;
    }

    private void PlayWarDrumEffect(Oddzia� atakujacy)
    {
        // Play a war drum sound effect
        warDrumSound.controls.play();
        PictureBox target = picWojownik;

        // Increase the defense of the attacking unit for the next attack
        atakujacy.Jednostka.Obrona += 10;

        // Create a timer to handle the animation
        System.Windows.Forms.Timer timer = new() { Interval = 500 };
        timer.Tick += (s, e) =>
        {
            target.BackColor = Color.Brown;
            Invalidate(new Rectangle(target.Location, target.Size));
            timer.Stop();
        };
        timer.Start();

        // Change the background color of the target to brown
        target.BackColor = Color.Brown;
    }
}
