using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DriverSyntax.Class
{
    public class Question
    {
        public string Name { get; set; }
        public int Iteration { get; private set; } = 0;
        public ParentQuestion Parent { get; set; }
        public string ParentName { get; set; }
        public SpssLib.SpssDataset.Variable Variable { get; set; }
        public Question Create(SpssLib.SpssDataset.Variable variable, List<ParentQuestion> parentList)
        {
            string name = EditDotPossition(variable.Name);
            string notDot = name.IndexOf(".") > -1 ? name.Substring(0, name.IndexOf(".")) : name; //variable.name i . dan kurıyoruz
            var seperateName = notDot.Split('_');                                                 //Q1_r1_c1 formattında olduğu için nameler _ den ayırıyoruz
            int index = parentList.FindIndex(x => x.Name == seperateName[0]);                     //niye var hatırlamıyorum
            if (variable.Name.IndexOf("_r") > -1 && variable.Name.IndexOf("_c") > -1)             // içinde hem _r hem _c varsa multiGrid
            {
                MultiGrid multiGrid = new MultiGrid();
                multiGrid.Name = name;
                multiGrid.ParentName = seperateName[0];                                           //düzenlenmin name'i _ den böldüğümüzde ilk eleman parent question adı oluyor 
                multiGrid.RowIndex = TryGetIndex(seperateName[1], "r", variable.Name);                            //ikinci elamanın adından r yi çıkardığımızda row indexi buluyoruz
                multiGrid.ColIndex = TryGetIndex(seperateName[2], "c", variable.Name);                           //ikinci elamanın adından r yi çıkardığımızda row indexi buluyoruz
                multiGrid.Variable = variable;                                                    //variabların adlarını düzenlediğimiz için datadan gelen ham hallerini de saklıyoruz
                if (multiGrid.SetIteration(variable.Name) == -1 || multiGrid.RowIndex == -1 || multiGrid.ColIndex == -1) // eğer herhangi bir convertToInt işleminde fail olmuşsa null döndürüyorum
                    return null;
                return multiGrid;
            }
            else if (variable.Name.IndexOf("_r") > -1)                                            // içinde sadece _r varsa rowGrid
            {
                RowGrid rowGrid = new RowGrid();
                rowGrid.Name = name;
                rowGrid.ParentName = seperateName[0];
                rowGrid.RowIndex = TryGetIndex(seperateName[1], "r", variable.Name);
                rowGrid.Variable = variable;
                if (rowGrid.SetIteration(variable.Name) == -1 || rowGrid.RowIndex == -1)
                    return null;
                return rowGrid;
            }
            else if (variable.Name.IndexOf("_c") > -1)                                              // içinde sadece _r varsa rowGrid
            {
                ColGrid colGrid = new ColGrid();
                colGrid.Name = name;
                colGrid.ParentName = seperateName[0];
                colGrid.ColIndex = TryGetIndex(seperateName[1], "c", variable.Name);
                colGrid.Variable = variable;
                if (colGrid.SetIteration(variable.Name) == -1 || colGrid.ColIndex == -1)
                    return null;
                return colGrid;
            }
            else if (variable.Name.IndexOf("_") > -1 && seperateName[0] != "sys" && seperateName[0] != "SHELL")             // içinde sadece _ varsa multipunch
            {
                MultiPunch multiPunch = new MultiPunch();
                multiPunch.Name = name;
                multiPunch.ParentName = seperateName[0];
                multiPunch.AnswerIndex = TryGetIndex(seperateName[1], " ", variable.Name);
                multiPunch.Variable = variable;
                if (multiPunch.SetIteration(variable.Name) == -1 || multiPunch.AnswerIndex == -1)
                    return null;
                return multiPunch;
            }
            else
            {
                SinglePunch singlePunch = new SinglePunch();                               // hiçbiri yoksa singlepuch
                singlePunch.Name = name;
                singlePunch.ParentName = seperateName[0];
                singlePunch.Variable = variable;
                if (singlePunch.SetIteration(variable.Name) == -1)
                    return null;
                return singlePunch;
            }
        }
        private string EditDotPossition(string name)// eğer sorular SSI tarafında loop içine alınmayıp elle çoğaltıldı ise Q1_1.1 olması gereken variable.name Q1.1_1 olarak geliyor onu düzenliyoruz
        {
            if (name.IndexOf(".") > -1)
            {
                string dotAfter = name.Substring(name.IndexOf("."), name.Length - name.IndexOf("."));
                int n;
                string loopIndex = "";
                string editedName = name;
                var count = 1;
                while (count < dotAfter.Length && int.TryParse(dotAfter[count].ToString(), out n))// . dan sonra kaçınca karaktere kadar integer olabiliyorsa o kadarını alıyoruz
                {
                    loopIndex = loopIndex + dotAfter[count];
                    count++;
                }
                editedName = editedName.Replace("." + loopIndex, "") + "." + loopIndex;//aldığımız integerları . ekleyip en sona ekliyoruz
                return editedName;
            }
            else
            {
                return name;
            }
        }
        public int SetIteration(string name) //question'un hangi iterasyonda olduğunu anlamak için . dan sonraki integer karakterleri kurtarıp iterasyonunu belirliyoruz
        {
            if (this.Name.IndexOf(".") > -1)
            {
                try// formata uygun değilse convertToInt işlemi başarısız oluyorç o yüzden try ile deniyorun
                {
                    this.Iteration = Convert.ToInt32(this.Name.Substring(this.Name.IndexOf(".") + 1, this.Name.Length - this.Name.IndexOf(".") - 1)); 
                    return 0;
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            return 0;
        }
        private int TryGetIndex(string seperateName, string parameter, string variableName)
        {
            try
            {
                return Convert.ToInt32(seperateName.Replace(parameter, ""));// formata uygun değilse convertToInt işlemi başarısız oluyorç o yüzden try ile deniyorun
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
