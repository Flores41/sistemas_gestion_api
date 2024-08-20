﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraction.IApplication.Auth;
using Abstraction.IService.Auth;

namespace Application.Auth
{
    public class CaptchaGoogleApplication : ICaptchaGoogleApplication
    {
        private ICaptchaGoogleService _captchaGoogleService;

        public CaptchaGoogleApplication(ICaptchaGoogleService captchaGoogleService)
        {
            _captchaGoogleService = captchaGoogleService;
        }
        //public bool ValidateCaptcha(string token)
        //{
        //    return _captchaGoogleService.ValidateCaptcha(token);
        //}
    }
}
