﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;


namespace Models.Util
{
    public class EmailDTO
    {
        public MailAddress? De { get; set; }
        //[DataType(DataType.EmailAddress)]
        //[EmailAddress]
        public List<string>? Para { get; set; }
        //[EmailAddress]
        //[DataType(DataType.EmailAddress)]
        public List<string>? ConCopia { get; set; }
        //[EmailAddress]
        //[DataType(DataType.EmailAddress)]

        public List<string>? ConCopiaOculta { get; set; }
        public string Titulo { get; set; }
        public string Mensaje { get; set; }
        public List<string> Adjuntos { get; set; }

        public int iid_usuario_registra { get; set; }
    }
    public class smtpDTO
    {
        public string Servidor { get; set; }
        public int Puerto { get; set; }
        public bool CredencialesPorDefecto { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
    }

}
