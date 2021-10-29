using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverSyntax.Class
{
    public class Variable : Question
    {
        public int ID { get; set; }
        public bool Make { get; set; } = false;
        public bool Keep { get; set; } = false;
        public bool UseList { get; set; } = false; // kullanıcı tanımlı liste kullanmak için(black/white list)

    }
}

