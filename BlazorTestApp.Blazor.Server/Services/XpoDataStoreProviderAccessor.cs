﻿using System;
using DevExpress.ExpressApp.Xpo;

namespace BlazorTestApp.Blazor.Server.Services {
    public class XpoDataStoreProviderAccessor {
        public IXpoDataStoreProvider DataStoreProvider { get; set; }
    }
}
