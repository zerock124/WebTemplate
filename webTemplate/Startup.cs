﻿using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(webTemplate.Startup))]
namespace webTemplate
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app) 
        {
            Configuration(app);
        }
    }
}