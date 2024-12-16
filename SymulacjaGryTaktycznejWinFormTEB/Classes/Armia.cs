using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaGryTaktycznejWinFormTEB.Classes;

public class Armia
{
    public List<Oddział> Oddziały { get; set; }
    public Bohater Dowódca { get; set; }

    public Armia(List<Oddział> oddziały, Bohater dowódca)
    {
        Oddziały = oddziały;
        Dowódca = dowódca;
    }

    public int ObliczObrazenia()
    {
        return Oddziały.Sum(o => o.ObliczObrazenia());
    }

    public int ObliczZycie()
    {
        return Oddziały.Sum(o => o.ObliczZycie());
    }
}
