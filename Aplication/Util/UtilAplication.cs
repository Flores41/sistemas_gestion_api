using Microsoft.Extensions.Configuration;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Abstraction.IAplication.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Model;
using Models.Util;
using System.Net.Mail;


namespace Aplication.Util
{
    public class UtilAplication : IUtilAplication
    {
        private smtpDTO smtp = new smtpDTO();
        private readonly IConfiguration iIConfiguration;
        //private readonly IPlantillaCorreoServices iIPlantillaCorreoServices;
        //private readonly ILogErrorServices iILogErrorServices;
        public UtilAplication(IConfiguration IConfiguration
            //, IPlantillaCorreoServices IPlantillaCorreoServices
            //, ILogErrorServices ILogErrorServices
            )
        {

            //this.iILogErrorServices = ILogErrorServices;
            //this.iIPlantillaCorreoServices = IPlantillaCorreoServices;
            this.iIConfiguration = IConfiguration;


            smtp.CredencialesPorDefecto = Convert.ToBoolean(this.iIConfiguration["CredencialesSTMPPorDefecto"].ToString());
            smtp.Puerto = Convert.ToInt32(this.iIConfiguration["PuertoSMTP"].ToString());
            smtp.Servidor = this.iIConfiguration["ServidorSMTP"].ToString();
            smtp.Usuario = this.iIConfiguration["ServidorSMTPUsuario"].ToString();
            smtp.Password = this.iIConfiguration["ServidorSMTPPass"].ToString();
        }

        public async Task<ResultDTO<bool>> envioMail(EmailDTO email)
        {
            ResultDTO<bool> res = new();

            email.De = new System.Net.Mail.MailAddress(smtp.Usuario);

            try
            {
                SmtpClient client = new()
                {
                    Host = smtp.Servidor,
                    Port = smtp.Puerto,
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };


                if (smtp.CredencialesPorDefecto)
                {

                    client.UseDefaultCredentials = true;
                    client.EnableSsl = true;
                }
                else
                {
                    client.UseDefaultCredentials = false;
                    client.EnableSsl = true;
                    client.Credentials = new System.Net.NetworkCredential(smtp.Usuario, smtp.Password);
                }

                MailMessage mensajeEmail = new()
                {
                    From = email.De
                };
                string destinatariosPara = "";
                if (email.Para != null)
                    foreach (string correo in email.Para)
                    {

                        mensajeEmail.To.Add(new MailAddress(correo));
                        destinatariosPara = destinatariosPara + correo + ";";

                    }
                string destinatariosConCopia = "";
                if (email.ConCopia != null)
                    foreach (string correo in email.ConCopia)
                    {

                        mensajeEmail.CC.Add(new MailAddress(correo));
                        destinatariosConCopia = destinatariosConCopia + correo + ";";

                    }
                string destinatariosConCopiaOculta = "";
                if (email.ConCopiaOculta != null)
                    foreach (string correo in email.ConCopiaOculta)
                    {

                        mensajeEmail.Bcc.Add(new MailAddress(correo));
                        destinatariosConCopiaOculta = destinatariosConCopiaOculta + correo + ";";

                    }

                if (string.IsNullOrEmpty(destinatariosPara) && string.IsNullOrEmpty(destinatariosConCopia) && string.IsNullOrEmpty(destinatariosConCopiaOculta))
                    throw new Exception("Ninguno de los correos especificados es válido");

                mensajeEmail.IsBodyHtml = true;
                mensajeEmail.Subject = email.Titulo;
                mensajeEmail.Body = email.Mensaje;

                List<string> detalleAdjuntos = [];

                if (email.Adjuntos != null)
                    foreach (string adjunto in (email.Adjuntos))
                    {
                        if (File.Exists(adjunto))
                        {
                            mensajeEmail.Attachments.Add(new Attachment(adjunto));
                            detalleAdjuntos.Add(adjunto + ": Adjuntado con éxito");
                        }
                        else
                            throw new Exception(adjunto + ": El archivo no existe o es inaccesible");

                    }
                client.Send(mensajeEmail);

                res.IsSuccess = true;
                res.Message = "Mensaje Enviado";

            }
            catch (Exception e)
            {
                res.IsSuccess = false;
                res.Message = "Mensaje no Enviado";
                res.MessageExeption = e.Message.ToString();

                //LogErrorDTO lg = new();
                ////  LogErrorDTO lg = new LogErrorDTO();
                //lg.iid_usuario_registra = email.iid_usuario_registra;
                //lg.iid_opcion = 1;
                //lg.vdescripcion = e.Message.ToString();
                //lg.vcodigo_mensaje = e.Message.ToString();
                //lg.vorigen = this.ToString();

                //_ = Task.Run(() => {
                //    this.iILogErrorServices.RegisterLogError(lg);
                //});
            }

            return res;
        }


    }
}
