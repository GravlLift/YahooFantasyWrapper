﻿using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using YahooFantasyWrapper.Configuration;
using YahooFantasyWrapper.Models;

namespace YahooFantasyWrapper.Client
{
    public interface IYahooAuthClient
    {
        /// <summary>
        /// Returns URI of service which should be called in order to start authentication process. 
        /// You should use this URI when rendering login link.
        /// </summary>
        string GetLoginLinkUri();

        AuthModel Auth { get; set; }

        /// <summary>
        /// Client configuration object.
        /// </summary>
        IOptions<YahooConfiguration> Configuration { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters">QS Parameters to parse for auth</param>
        /// <returns></returns>
        Task<UserInfo> GetUserInfo(NameValueCollection parameters);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="refreshToken">refresh token used for generation of new access token </param>
        /// <param name="forceUpdate">flag to force generation of new access token</param>
        /// <returns></returns>
        Task<string> GetCurrentToken(string refreshToken = null, bool forceUpdate = false);
    }
}
