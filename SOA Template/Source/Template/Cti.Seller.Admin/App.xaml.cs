﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using System.Windows;
using Cti.Seller.Client.Bootstrapper;
using Core.Common.Core;

namespace Cti.Seller.Admin
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ObjectBase.Container = MEFLoader.Init(new List<ComposablePartCatalog>() 
            {
                new AssemblyCatalog(Assembly.GetExecutingAssembly())
            });
        }
    }
}
