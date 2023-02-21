using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice_2
{
    public class Note
    {
        public string name = "New Note";

        public string description = "No description here.";

        // время, на которое оформлена заметка
        public DateTime dateTime = new DateTime();

        public override string ToString()
        {
            return name;
        }
    }
}
