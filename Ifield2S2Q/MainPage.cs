using DriverSyntax.Class;
using FastMember;
using Microsoft.WindowsAPICodePack.Dialogs;
using SpssLib.DataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriverSyntax
{
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
        }
        /*
        v0.0.2
        Make yapılan her soru tipi için classlar oluşturuldu. Bunların hepsi Question classında kalıtım alıyor. Make Keepleri grid üzerinden gösterebilmek için 
        Variable diye bir class daha yapıldı. Spsste ayrı ayrı gösterilen soruların iterasyonları, multipuch soruların soru olarak gösterilen seçenekleri, gridlerin
        rowları ve columları parent child ilişkisine sokularak ParentQuestion Classında toplandı. ParentQuestion sorusu grid üzerinde gösterileceği için Variable 
        Clasından kalıtım aldı
         */
        List<ParentQuestion> parentList = new List<ParentQuestion>();
        public static List<string> failList = new List<string>();
        string vartoCases = "";
        string keep = "";
        DataTable dt = new DataTable();
        private void btnChSav_Click(object sender, EventArgs e)
        {
            ofdSav.Filter = "SPSS Dosyası |*.sav";
            if (ofdSav.ShowDialog() == DialogResult.OK)
            {
                parentList = new List<ParentQuestion>();
                failList = new List<string>();
                FileStream fileStream = new FileStream(ofdSav.FileName, FileMode.Open, FileAccess.Read, FileShare.Read, 2048 * 10, FileOptions.SequentialScan);
                SpssReader spssDataset = new SpssReader(fileStream);
                foreach (var item in spssDataset.Variables)
                {
                    if (item.Type != SpssLib.SpssDataset.DataType.Text) // text tipi sorular make yapılmadığı için kontrol etmiyoruz
                    {
                        Question qst = new Question();
                        qst = qst.Create(item, parentList);// açıklamaları classın içinde
                        if (qst == null) // eğer formata uygun olmayan bir variable name varsa fonkiyon null olarak dönüyor
                        {
                            failList.Add(item.Name); // bu durunda failListe ekleyim fail ekraranında bunları gösteriyorum
                            continue;
                        }
                        UpdateList(qst, qst.ParentName);
                    }


                }
                FillGrid();
                lblFailCount.Text = failList.Count.ToString();
            }

        }

        private void FillGrid()
        {
            dt = new DataTable(); //gride filtre eklemek için datatable kullanmak zorunda kaldım
            using (var reader = ObjectReader.Create(parentList.Select(x => new { x.ID, x.Name, x.strType, x.TotalIteration, x.Make, x.Keep, x.UseList }).ToList()))// objectreader fast memeber refferance ında geliyor
            {
                dt.Load(reader);
            }
            //datatable da column sıralarını elle belirliyoruz
            dt.Columns["ID"].SetOrdinal(0);
            dt.Columns["Name"].SetOrdinal(1);
            dt.Columns["strType"].SetOrdinal(2);
            dt.Columns["TotalIteration"].SetOrdinal(3);
            dt.Columns["Make"].SetOrdinal(4);
            dt.Columns["Keep"].SetOrdinal(5);
            dt.Columns["UseList"].SetOrdinal(6);
            dgVariables.DataSource = dt;
            dgVariables.AutoResizeRowHeadersWidth(0, DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            dgVariables.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgVariables.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgVariables.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgVariables.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgVariables.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgVariables.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgVariables.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgVariables.Columns[0].ReadOnly = true;
            dgVariables.Columns[1].ReadOnly = true;
            dgVariables.Columns[2].ReadOnly = true;
        }

        private void UpdateList(Question qst, string parentName)
        {
            int index = parentList.FindIndex(x => x.Name == parentName);// ekleyeceğimiz questionın parentquestionı daha önce parentListe eklenmiş mi kontrol ediyoruz.
            if (index == -1) // eklenmediyse yeni oluşturup ekliyoruz
            {
                ParentQuestion parent = new ParentQuestion();
                parent = parent.Create(qst, parentList, parentName);
                parentList.Add(parent);
                parent.strType = parent.SetStrType();
            }
            else // varsa update ediyoruz
            {
                var parent = (ParentQuestion)parentList[index];
                qst.Parent = parent;
                parent.Childs.Add(qst);
                parent.TotalIteration = parent.Itreartion();
                parentList[index] = parent;
            }
        }

        private void bntWriteSyntax_Click(object sender, EventArgs e)
        {
            vartoCases = "VARSTOCASES";
            keep = "\n/KEEP= ";
            int index = 0;
            List<ParentQuestion> notInLoopList = new List<ParentQuestion>();
            foreach (var item in parentList)
            {
                if (rbLoop.Checked) // 2 tip make yöntemi var iterasyona göre yada questiona göre hangisi işaretli ise ona göre yapıyoruz
                {
                    if (item.Make == true)
                    {
                        if (item.Childs.Count > 1) //parentQuestionun 1 den fazla chid ı var ise make işlemini yapıyoruz
                        {
                            foreach (var child in item.Childs.Where(x => x.Iteration == 1).ToList())            // tüm child questioların her itersyonsda olduğunu varsayarak(Q1.1,Q1.2,Q.4 ise child list biz yine Q1.1 Q1.2, Q1.3, Q1.4 varmış gibi make yapıyoruz)
                            {                                                                                   // 1. itererasyon için chidları grupluyoruz
                                vartoCases = vartoCases + "\n/MAKE " + child.Name.Replace(".1", "") + " FROM";  //Make den sonra oluşacak name için .1 'i uçuruyoruz
                                for (int i = 1; i <= item.TotalIteration; i++)
                                {
                                    if (item.Childs.Where(x => x.Variable.Name == child.Variable.Name.Replace(".1", "." + i)).FirstOrDefault() != null)// eğer bu adda bir variable gerçekten child listesinde var ise bu işlemi yapıyoruz. Çünkü .1 e göre grupladık diğer iterasyonlarda bu değişken olmayabilir düzenlenmiş bir data ise
                                        vartoCases = vartoCases + "\n" + child.Variable.Name.Replace(".1", "." + i);//yine .1 i for içinedki itersyon değeri ile replace yapıyoruz
                                }
                            }
                            index = item.TotalIteration;

                        }
                        else // yoksa diğer tek elemanı olan ve make=true olan questionlar ile make yapabiliyor muyuz diye kontrol etmek için listeye ekliyoruz
                        {
                            notInLoopList.Add(item);
                        }
                    }
                }
                else if (rbQuestion.Checked)
                {
                    if (item.Make == true)
                    {
                        if (item.Childs.Count > 1)
                        {
                            if (item.Type() == typeof(SinglePunch))
                            {
                                if (item.InLoop()) // eğer singlepuch ise ve loop içinde ise tüm childları make ediyoruz
                                {
                                    vartoCases = vartoCases + "\n/MAKE " + item.Name + " FROM";
                                    foreach (var var in item.Childs.ToList())
                                    {
                                        vartoCases = vartoCases + "\n" + var.Name;
                                    }

                                }
                                else
                                {
                                    notInLoopList.Add(item); // loop içinde değilse diğer tek elemanı olan ve make=true olan questionlar ile make yapabiliyor muyuz diye kontrol etmek için listeye ekliy
                                }
                            }
                            else if (item.Type() == typeof(MultiPunch))
                            {
                                MakeSingle_(item);
                            }
                            else if (item.Type() == typeof(RowGrid))
                            {
                                MakeSingle_(item);
                            }
                            else if (item.Type() == typeof(ColGrid))
                            {
                                MakeSingle_(item);
                            }
                            else if (item.Type() == typeof(MultiGrid))
                            {
                                if (rbRow.Checked)
                                {
                                    MakeDouble_(item, "RowIndex", "_r");
                                }
                                else if (rbColumn.Checked)
                                {
                                    MakeDouble_(item, "ColIndex", "_c");
                                }
                            }
                        }
                    }
                }
                if (item.Keep == true)
                {
                    foreach (var var in item.Childs)
                    {
                        keep = keep + " " + var.Name;
                    }
                }
            }
            var cantMake = "AŞAĞIDAKİ VARIABLE'LAR MAKE YAPILAMADI\n";
            if (rbLoop.Checked) // make seçeneği işaretlenmiş fakat loop içerisinde olmayan questionlar URUN1 URUN2 URUN3 gibi digitler açıkarıldığında aynı ada sahipse make yapıyourz
            {
                var doneList = new List<string>();
                foreach (var item in notInLoopList.ToList())
                {
                    var maybeName = Regex.Replace(item.Name, @"[\d-]", string.Empty);
                    var sameNameList = notInLoopList.Where(x => Regex.Replace(x.Name, @"[\d-]", string.Empty) == maybeName).ToList();
                    if (sameNameList.Count == index)
                    {
                        vartoCases = vartoCases + "\n/MAKE " + maybeName + " FROM";
                        for (int i = 0; i < sameNameList.Count; i++)
                        {
                            vartoCases = vartoCases + "\n" + sameNameList[i].Childs[0].Variable.Name;
                            notInLoopList.Remove(sameNameList[i]);
                            doneList.Add(sameNameList[i].Name);
                        }
                    }
                    else
                    {
                        if (doneList.Where(x => x == item.Name).FirstOrDefault() == null)
                        {
                            cantMake = cantMake + "\n" + item.Name;
                        }
                    }
                }
            }
            string outputPath = Path.GetDirectoryName(ofdSav.FileName) + @"\" + Path.GetFileName(ofdSav.FileName).Replace(".sav", "");
            string saveTranslate = @"SAVE TRANSLATE OUTFILE='" + outputPath + "_Driver_Data.xlsx'" + "\n/TYPE=XLS\n/VERSION=12\n/MAP\n/FIELDNAMES VALUE=NAMES\n/CELLS=VALUES\n/EXCELOPTIONS SHEET='Driver'\n/APPEND.";
            string syntax = @"GET FILE='" + ofdSav.FileName + "'." + "\n" + vartoCases + "\n/INDEX=INDEX" + keep + "\n/NULL=KEEP." + "\n" + saveTranslate;
            System.IO.File.WriteAllText(Path.GetDirectoryName(ofdSav.FileName) + "/" + Path.GetFileName(ofdSav.FileName).Replace(".sav", "") + " Driver.sps", syntax, Encoding.Default);
            if (cantMake != "AŞAĞIDAKİ VARIABLE'LAR MAKE YAPILAMADI\n")
            {
                MessageBox.Show(cantMake, "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            MessageBox.Show("Syntax Dosyası " + Path.GetDirectoryName(ofdSav.FileName) + " Adresine Kayıt Edildi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DialogResult dialogResult = MessageBox.Show("SYNTAX CMD ÜZERİNDE ÇALIŞTIRILIP DATA EXEL OLARAK SAVE AS ALINSIN MI?\n\n\n(KLASÖR VEYA SPSS DOSYASI ADINDA TÜRKÇE KARAKTER VARSA ÇALIŞMAZ\nSPSS'TEN GİRİP ELLE ÇALIŞTIRMAYA GÖRE DAHA YAVAŞ ÇALIŞIR)", "SAVE AS .xlsx", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)// spj dosyası oluştururulup syntaxı otomtikk çalıştırmak için olan bölüm.
            {
                string[] spjText = File.ReadAllLines("spj.txt");// dizinde bir adet spj.txt olmak zorunda
                spjText[0] = spjText[0].Replace("$$$$output", outputPath + " output.txt").Replace("$$$$syntax", outputPath + " Driver.sps");
                System.IO.File.WriteAllText(outputPath + " Driver.spj", spjText[0], Encoding.Default);
                var p = new Process();
                p.StartInfo = new ProcessStartInfo("stats")
                {
                    //UseShellExecute = false,
                    Arguments = @"-production silent " + outputPath + " Driver.spj",
                    WorkingDirectory = @"C:\Program Files\IBM\SPSS\Statistics\24",
                };
                p.Start();
                p.WaitForExit();
                MessageBox.Show("CMD ÜZERİNDE ÇALIŞTIRMA İŞLEMİ TAMAMLANDI. SPPS İLE AYNI ADDA BİR XLSX DOSYASI DİZİNDE OLUŞMADIYSA SYNTAX'I SPSS ÜZERİNDE KENDİNİZ ÇALIŞTIRINIZ", "COPMLETE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MakeDouble_(ParentQuestion item, string propName, string dimen)
        {
            int length = 0;
            if (dimen == "_r") // içinde 1 den fazla _ var ise kullanıyoruz yani multigrid
            {
                length = item.RowCount();
            }
            else
            {
                length = item.ColumnCount();
            }
            if (item.InLoop())
            {
                int itLength = item.TotalIteration;
                for (int f = 1; f <= itLength; f++)// her iterasyon için itrasyona göre ve grid makeType a göre row yada column dan gruplayıp her iterasyon için ayrı ayrı make yapıyoruz
                {
                    for (int i = 1; i <= length; i++)
                    {
                        vartoCases = vartoCases + "\n/MAKE " + item.Name + dimen + i + "." + f + " FROM";
                        foreach (var var in item.Childs.Where(x => (int)((MultiGrid)x).GetType().GetProperty(propName).GetValue((MultiGrid)x) == i && x.Iteration == f).ToList())
                        {
                            if (CheckWhiteList(var))
                                continue;
                            vartoCases = vartoCases + "\n" + var.Variable.Name;
                        }
                    }
                }

            }
            else
            {
                for (int i = 1; i <= length; i++) // sadece row yada colunm a göre gruplayıp make yapıyouruz
                {
                    vartoCases = vartoCases + "\n/MAKE " + item.Name + dimen + i + " FROM";
                    foreach (var var in item.Childs.Where(x => (int)((MultiGrid)x).GetType().GetProperty(propName).GetValue((MultiGrid)x) == i).ToList())
                    {
                        if (CheckWhiteList(var))
                            continue;
                        vartoCases = vartoCases + "\n" + var.Variable.Name;
                    }
                }
            }
        }

        private void MakeSingle_(ParentQuestion item) // içinde 1 adet _ olan soruları question olarak make etmek için
        {
            if (item.InLoop()) // loopun içersinde ise aynı iterasyonda olan tüm childları make ediyoruz 
            {
                int length = item.TotalIteration;
                for (int i = 1; i <= length; i++)
                {
                    vartoCases = vartoCases + "\n/MAKE " + item.Name + "." + i + " FROM"; //Q1.1 gibi her iterasyon için ayrı ayrı make edilmiş oluyor
                    foreach (var var in item.Childs.Where(x => x.Iteration == i).ToList())
                    {
                        if (CheckWhiteList(var))
                            continue;

                        vartoCases = vartoCases + "\n" + var.Variable.Name;
                    }
                }

            }
            else //değilse tüm childları make ediyoruz
            {
                vartoCases = vartoCases + "\n/MAKE " + item.Name + " FROM";
                foreach (var var in item.Childs.ToList())
                {
                    if (CheckWhiteList(var))
                        continue;
                    vartoCases = vartoCases + "\n" + var.Name;
                }
            }
        }

        public bool CheckWhiteList(Question qst) // büyük datalarda bir çok index(row,column,answer) kullanılmıyor bunlara göre işlem yapmak için.
        {
            if (!qst.Parent.UseList)
            {
                return false;
            }
            int index = 0;
            if (qst.Parent.Type() == typeof(MultiPunch))
            {
                index = ((MultiPunch)qst).AnswerIndex;
            }
            else if (qst.Parent.Type() == typeof(RowGrid))
            {
                index = ((RowGrid)qst).RowIndex;
            }
            else if (qst.Parent.Type() == typeof(ColGrid))
            {
                index = ((ColGrid)qst).ColIndex;
            }
            else if (qst.Parent.Type() == typeof(MultiGrid))
            {
                if (rbRow.Checked)
                {
                    index = ((MultiGrid)qst).ColIndex;
                }
                else if (rbColumn.Checked)
                {
                    index = ((MultiGrid)qst).RowIndex;
                }
            }
            if (WhiteList.whihiteList.IndexOf(index) > -1 && WhiteList.isWhite) // listede  varsa liste white ise
                return false;
            else if (WhiteList.whihiteList.IndexOf(index) > -1 && !WhiteList.isWhite)// listede  varsa liste black ise
                return true;
            else if (WhiteList.whihiteList.IndexOf(index) == -1 && WhiteList.isWhite)// listede  yoksa liste white ise
                return true;
            else if (WhiteList.whihiteList.IndexOf(index) == -1 && !WhiteList.isWhite)// listede  yoksa liste black ise
                return false;
            return true;
        }

        private void dgVariables_CellContentClick(object sender, DataGridViewCellEventArgs e)// row içinde make ve keep alanları editlenebiliyor, oradaki tıklamalar göre hem make 
        {                                                                                    // hem keep aynı anda yapılamayacağı için bir true olursa diğeri false yapılıyor. parentListe'de seçime göre questionun seçimini düzenliyorum
            if (e.RowIndex != -1)
            {
                int id = Convert.ToInt32(dgVariables.Rows[e.RowIndex].Cells[0].Value);
                var ff = failList;
                if (e.ColumnIndex == 4)
                {
                    dgVariables.Rows[e.RowIndex].Cells[5].Value = false;
                    parentList.Where(x => x.ID == id).FirstOrDefault().Make = !parentList.Where(x => x.ID == id).FirstOrDefault().Make;
                    parentList.Where(x => x.ID == id).FirstOrDefault().Keep = false;

                }
                else if (e.ColumnIndex == 5)
                {
                    dgVariables.Rows[e.RowIndex].Cells[4].Value = false;
                    parentList.Where(x => x.ID == id).FirstOrDefault().Make = false;
                    parentList.Where(x => x.ID == id).FirstOrDefault().Keep = !parentList.Where(x => x.ID == id).FirstOrDefault().Keep;
                }
                else if (e.ColumnIndex == 6)
                {
                    parentList.Where(x => x.ID == id).FirstOrDefault().UseList = !parentList.Where(x => x.ID == id).FirstOrDefault().UseList;
                }
            }
            dgVariables.EndEdit();

        }

        private void dgVariables_FilterStringChanged(object sender, EventArgs e)// adgv de filtreleme için gerekiyor
        {
            dt.DefaultView.RowFilter = dgVariables.FilterString;
        }

        private void dgVariables_SortStringChanged(object sender, EventArgs e)
        {
            dt.DefaultView.Sort = dgVariables.SortString;
        }

        private void dgVariables_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) // grid üzerinde question name'lere gelindiğinde childları tooltip olarak göstermek için 
        {
            int id = Convert.ToInt32(dgVariables.Rows[e.RowIndex].Cells[0].Value);
            DataGridViewCell cell = this.dgVariables.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (e.ColumnIndex == 1)
            {
                var current = parentList.Where(x => x.ID == id).FirstOrDefault();
                string text = "Index Count: " + current.IndexCount() + " Row Count:" + current.RowCount() + " Column Count:" + current.ColumnCount() + "\n";
                var count = 0;
                foreach (var item in current.Childs.ToList())
                {
                    text = text + item.Name + "\n";
                    count++;
                    if (count==30)
                    {
                        text = text + ".\n.\n.";
                        break;
                    }
                }
                cell.ToolTipText = text;
            }

        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            rbLoop.Checked = true;
            grpGridType.Enabled = false;
            rbRow.Checked = true;
            btnWhiteList.Enabled = false;
        }

        private void rbLoop_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLoop.Checked)
            {
                grpGridType.Enabled = false;
                btnWhiteList.Enabled = false;
            }
            else
            {
                grpGridType.Enabled = true;
                btnWhiteList.Enabled = true;
            }
        }

        private void dgVariables_Click(object sender, EventArgs e) // tüm filtreleri kaldırmak için ve gridi refresh etmek için yazıldı. Gridin sol köşesindeki hücreye tıklayında çalışıyor
        {
            MouseEventArgs args = (MouseEventArgs)e;
            DataGridView dgv = (DataGridView)sender;
            DataGridView.HitTestInfo hit = dgv.HitTest(args.X, args.Y);
            if (hit.Type == DataGridViewHitTestType.TopLeftHeader)
            {
                FillGrid();
                dgVariables.ClearFilter(true);
            }
        }

        private void btnWhiteList_Click(object sender, EventArgs e)
        {
            WhiteList wht = new WhiteList();
            wht.ShowDialog();
        }
        public static void RestartApp(string name)// fail ekranı ekleyip eklenemeyenleri orada gösterdiğim için bu fonksiyonu şu an kullanmıyorum
        {
            System.Windows.Forms.MessageBox.Show(name + " Değişken adı uygun formatta değil düzeltip tekrar deneyin");
            System.Windows.Forms.Application.Restart();
            Environment.Exit(0);
        }

        private void lblFailText_MouseClick(object sender, MouseEventArgs e)
        {
            Fails fls = new Fails();
            fls.ShowDialog();
        }
    }
}

