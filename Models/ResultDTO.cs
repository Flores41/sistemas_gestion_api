using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ResultDTO<T>
    {
		public int Code { get; set; }
		public Boolean IsSuccess { get; set; }
		public string Message { get; set; }
		public string MessageExeption { get; set; }
		public string StackTrace { get; set; }
		public string InnerException { get; set; }
		public string Informacion { get; set; }
		public List<T> Data { get; set; }
		public T Item { get; set; }
        public string Token { get; set; }

        public byte[] File { get; set; }
		public int iTotal_record { get; set; }
    }
}
