﻿/*
Copyright (c) 2014 <a href="http://www.gutgames.com">James Craig</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation Configs (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

#region Usings

using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using Utilities.Configuration.Manager.Interfaces;

#endregion Usings

namespace Utilities.Configuration.Manager.Default
{
    /// <summary>
    /// Default config class for web.config and app.config
    /// </summary>
    public class SystemConfig : IConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SystemConfig()
        {
            AppSettings = new Dictionary<string, string>();
            ConnectionStrings = new Dictionary<string, ConnectionString>();
            if (HttpContext.Current == null)
            {
                foreach (ConnectionStringSettings Connection in System.Configuration.ConfigurationManager.ConnectionStrings)
                {
                    ConnectionStrings.Add(Connection.Name, new ConnectionString() { Connection = Connection.ConnectionString, ProviderName = Connection.ProviderName });
                }
                foreach (string Key in System.Configuration.ConfigurationManager.AppSettings.Keys)
                {
                    AppSettings.Add(Key, System.Configuration.ConfigurationManager.AppSettings[Key]);
                }
            }
            else
            {
                foreach (ConnectionStringSettings Connection in WebConfigurationManager.ConnectionStrings)
                {
                    ConnectionStrings.Add(Connection.Name, new ConnectionString() { Connection = Connection.ConnectionString, ProviderName = Connection.ProviderName });
                }
                foreach (string Key in WebConfigurationManager.AppSettings.Keys)
                {
                    AppSettings.Add(Key, WebConfigurationManager.AppSettings[Key]);
                }
            }
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Application settings
        /// </summary>
        public IDictionary<string, string> AppSettings { get; private set; }

        /// <summary>
        /// Connection strings
        /// </summary>
        public IDictionary<string, ConnectionString> ConnectionStrings { get; private set; }

        /// <summary>
        /// Name of the Config object
        /// </summary>
        public string Name { get { return "Default"; } }

        /// <summary>
        /// Gets the configuration section based on the name specified
        /// </summary>
        /// <param name="SectionName">Section name</param>
        /// <returns>The configuration section specified</returns>
        public object this[string SectionName]
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return System.Configuration.ConfigurationManager.GetSection(SectionName);
                }
                else
                {
                    return WebConfigurationManager.GetSection(SectionName);
                }
            }
        }

        #endregion Properties

        #region Functions

        /// <summary>
        /// Loads the config
        /// </summary>
        public void Load()
        {
        }

        /// <summary>
        /// Saves the config
        /// </summary>
        public void Save()
        {
        }

        #endregion Functions
    }

    #region Classes

    /// <summary>
    /// Connection string class
    /// </summary>
    public class ConnectionString
    {
        /// <summary>
        /// Actual connection string
        /// </summary>
        public string Connection { get; set; }

        /// <summary>
        /// Provider name
        /// </summary>
        public string ProviderName { get; set; }
    }

    #endregion Classes
}