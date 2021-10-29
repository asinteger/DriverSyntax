using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSyntax.Class
{
    public class ParentQuestion :Variable
    {
        public List<Question> Childs { get; set; }
        public int TotalIteration { get; set; }
        public string strType { get; set; }                                           //gridde soru filtreleme kolay olsun diye soru typelarını da string olarak tutuyoruz              
        public ParentQuestion Create(Question qst, List<ParentQuestion> parentList,string parentName) 
        {
            ParentQuestion parent = new ParentQuestion();
            parent.ID = parentList.Count != 0 ? parentList.LastOrDefault().ID + 1 : 1; //listede eleman yoksa id ye 1 ver varsa en son idnin bir fazlasını ver
            parent.Name = parentName;                                                   
            qst.Parent = parent;                                                       //create foksiyonu ile gelen question'ın parentina oluşturduğumuz parentı set ediyoruz
            parent.Childs = new List<Question>();                                      
            parent.Childs.Add(qst);                                                    //question'ı parent sorunun child listesine ekliyoruz
            parent.TotalIteration = parent.Itreartion();                               //child questionların loop iterasyonlarının en büyüğünü total iterasyon olarak set ediyoruz 
            return parent;
        }
        public bool InLoop()                                                           //parent question loop un içinde mi
        {
            return this.Childs[0].Name.Contains(".");
        }
        public Type Type()                                                            
        {
            return this.Childs[0].GetType();
        }
        public string SetStrType()                                                   
        {
            if (this.Type() == typeof(SinglePunch))
            {
                return "SinglePunch";
            }
            else if (this.Type() == typeof(MultiPunch))
            {
                return "MultiPunch";
            }
            else if (this.Type() == typeof(RowGrid))
            {
                return "RowGrid";
            }
            else if (this.Type() == typeof(ColGrid))
            {
                return "ColGrid";
            }
            else if (this.Type() == typeof(MultiGrid))
            {
                return "MultiGrid";
            }
            return "";
        }
        public int Itreartion()
        {
            return this.Childs.OrderBy(x=>x.Iteration).LastOrDefault().Iteration;
        }
        public int IndexCount()                                                      //multipunch questionların kaç adet seçeneği var 
        {
            if (this.Type() == typeof(MultiPunch))
            {
                return this.Childs.Cast<MultiPunch>().ToList().OrderBy(x => x.AnswerIndex).LastOrDefault().AnswerIndex;
            }
            else
            {
                return 0;
            }
        }
        public int RowCount()
        {
            if(this.Type() == typeof(RowGrid) )
            {
                return this.Childs.Cast<RowGrid>().ToList().OrderBy(x=>x.RowIndex).LastOrDefault().RowIndex;
            }
            else if (this.Type() == typeof(MultiGrid))
            {
                return this.Childs.Cast<MultiGrid>().ToList().OrderBy(x => x.RowIndex).LastOrDefault().RowIndex;
            }
            else
            {
                return 0;
            }
        }
        public int ColumnCount()
        {
            if (this.Type() == typeof(ColGrid))
            {
                return this.Childs.Cast<ColGrid>().ToList().OrderBy(x => x.ColIndex).LastOrDefault().ColIndex;
            }
            else if (this.Type() == typeof(MultiGrid))
            {
                return this.Childs.Cast<MultiGrid>().ToList().OrderBy(x => x.ColIndex).LastOrDefault().ColIndex;
            }
            else
            {
                return 0;
            }
        }
    }
}
