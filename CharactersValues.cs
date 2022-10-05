using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogo_da_velha
{
    public class CharactersValues
    {
        public IDictionary<int, string> Characters { get; set; } = new Dictionary<int, string>() 
        {
                {10, "X" },{11, "O" }
        };
    }
}
