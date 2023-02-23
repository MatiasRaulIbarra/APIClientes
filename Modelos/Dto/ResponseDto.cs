using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIClientes.Modelos.Dto
{
    public class ResponseDto
    {
        public bool IsSucces { get; set; }

        public object Result { get; set; }

        public string DisplayMessage { get; set; }

        public List<string> ErrorMessages { get; set; }
    }
}
