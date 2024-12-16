using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaGryTaktycznejWinFormTEB;

public partial class SummaryForm : Form
{
    public SummaryForm(string title, string summary)
    {
        InitializeComponent();
        lblTitle.Text = title;
        lblSummary.Text = summary;
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
}
