﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Autofac;

namespace X.Web {
    public class WebModule : Autofac.Module {
        protected override void Load(Autofac.ContainerBuilder builder) {
            base.Load(builder);
        }
    }
}