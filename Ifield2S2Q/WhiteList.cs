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
    public partial class WhiteList : Form //istenen veya istenmeyen indexleri manuel olarak belirtip question makelerken kullanmak için
    {
        public WhiteList()
        {
            InitializeComponent();
        }
        public static List<int> whihiteList = new List<int>();
        public static bool isWhite = true;
        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (txtWhiteList.Text!=string.Empty)
            {
                whihiteList = new List<int>();
                string[] userText = txtWhiteList.Text.Split(',');
                for (int i = 0; i < userText.Length; i++)
                {
                    if (userText[i]!="") // yan yana 2 virgül yazılmış ise boş eleman yazıyor, böyle bir durum varsa atlıyoruz.
                    {
                        whihiteList.Add(Convert.ToInt32(userText[i]));
                    }
                }
            }
            this.Hide();
        }

        private void txtWhiteList_KeyPress(object sender, KeyPressEventArgs e)//txtWhiteListe sadece numeric giriş ve ',' girilebilsin diye
        {
            int isNum = 0;
            if (e.KeyChar == ',')
                e.Handled = false;
            else if(Char.IsControl(e.KeyChar))
                e.Handled = false;
            else if (!int.TryParse(e.KeyChar.ToString(), out isNum))
                e.Handled = true;
        }

        private void WhiteList_Load(object sender, EventArgs e)
        {            
            txtWhiteList.Text = string.Join(",", whihiteList.ToArray()); // sayfa açıldığında tanımlaşmış listeyi txtWhiteList de görmek için
            rbWhiteList.Checked = isWhite;
            rbBlackList.Checked = !isWhite;
        }

        private void rbWhiteList_CheckedChanged(object sender, EventArgs e)
        {
            isWhite = rbWhiteList.Checked;
        }
    }
}
