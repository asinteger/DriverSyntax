using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriverSyntax
{
    public partial class Fails : Form
    {
        public Fails()
        {
            InitializeComponent();
        }

        private void Fails_Load(object sender, EventArgs e)
        {
            foreach (var item in MainPage.failList)
            {
                lstbxFails.Items.Add(item);
            }
        }
    }
}
