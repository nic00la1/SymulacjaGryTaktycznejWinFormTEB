using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaGryTaktycznejWinFormTEB.Classes;

public abstract class Jednostka
{
    public int Zycie { get; set; }
    public int Atak { get; set; }
    public int Obrona { get; set; }
    public int MinObrazenia { get; set; }
    public int MaxObrazenia { get; set; }
    public int Szybkosc { get; set; }

    public abstract int ObliczObrazenia();
}
