﻿/*
MIT License

Copyright (c) 2019 Digital Ruby, LLC - https://www.digitalruby.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DigitalRuby.IPBan
{
    public class IPBanUnblockIPAddressesUpdater : IUpdater
    {
        private IUnblockIPAddresses service;
        private readonly string textFilePath;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service">Service</param>
        /// <param name="textFilePath">Path to text file to unban ip addresses from or null to require manual unban call</param>
        public IPBanUnblockIPAddressesUpdater(IUnblockIPAddresses service, string textFilePath)
        {
            service.ThrowIfNull();
            this.service = service;
            this.textFilePath = textFilePath;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Update - if the text file path exists, all ip addresses from each line will be unbanned
        /// </summary>
        public void Update()
        {
            try
            {
                if (File.Exists(textFilePath))
                {
                    UnbanIPAddresses(File.ReadLines(textFilePath));
                    File.Delete(textFilePath);
                }
            }
            catch (Exception ex)
            {
                IPBanLog.Error(ex);
            }
        }

        /// <summary>
        /// Unban ip addresses
        /// </summary>
        /// <param name="ipAddresses">IP addresses to unban</param>
        public void UnbanIPAddresses(IEnumerable<string> ipAddresses)
        {
            service.UnblockIPAddresses(ipAddresses);
        }
    }
}
