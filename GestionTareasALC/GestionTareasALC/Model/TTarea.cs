using System;
using System.Collections.Generic;
using System.Text;

namespace GestionTareasALC.Model
{
    public class TTarea
    {
        public int Id;
        public String Tarea { get; set; }
        public String Notas { get; set; }
        public Boolean Realizada { get; set; }
    }
}
