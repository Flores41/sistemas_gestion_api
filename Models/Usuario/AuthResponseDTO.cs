using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Usuario
{
    public class AuthResponseDTO<T>
    {
        public Boolean IsSuccess { get; set; }
        public string Message { get; set; }
        public string MessageExeption { get; set; }
        public string Informacion { get; set; }
        public T data { get; set; }
        public string Token { get; set; }
    }

}
