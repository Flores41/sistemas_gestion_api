using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Abstraction.IApplication.Auth;
using Abstraction.IService.Auth;
using Services;
using Models.Usuario;

namespace Application.Auth
{
    public class AuthenticationApplication : IAuthenticationApplication
    {
        private readonly ICaptchaGoogleApplication _captchaGoogleApp;
       
        
        public AuthenticationApplication(   ICaptchaGoogleApplication captchaGoogleApp)
        {        
            _captchaGoogleApp = captchaGoogleApp;
        }
  
        //public  Boolean validarGoogleCaptcha(LoginDTO user)
        //{
        //    return _captchaGoogleApp.ValidateCaptcha(user.GoogleToken);           
        //}

    }
}
