﻿using ECommerce.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ECommerce.Controllers.Base
{
    public class AndControllerBase:Controller
    {
        //Kullanici Login Kontrolü
        public bool IsLogin { get; private set; }
        //Giriş yapmış kişinin id si
        public int LoginUserID { get; private set; }
       
        public User LoginUserEntity { get; private set; }
        protected override void Initialize(RequestContext requestContext)
        {
            //Todo:Üye giriş işlemlerinden sonra değişecek
            base.Initialize(requestContext);
        }
    }
}