﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace YahooFantasyWrapper.Client.Fantasy
{
    public abstract class Manager
    {
        protected readonly HttpClient client;

        protected Manager(HttpClient client)
        {
            this.client = client;
        }
    }
}
