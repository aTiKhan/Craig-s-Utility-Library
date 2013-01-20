﻿/*
Copyright (c) 2012 <a href="http://www.gutgames.com">James Craig</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
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
using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using Utilities.DataTypes.ExtensionMethods;
using System.Globalization;
using System.Diagnostics.Contracts;
#endregion

namespace Utilities.Web.ExtensionMethods
{
    /// <summary>
    /// Extensions for HttpRequest classes
    /// </summary>
    public static class HTTPRequestExtensions
    {
        #region Functions

        #region UserIPAddress

        /// <summary>
        /// Gets the user's IP address if it exists, null is returned otherwise
        /// </summary>
        /// <param name="Request">Request</param>
        /// <returns>The IPAddress object if it exists, null otherwise</returns>
        public static IPAddress UserIPAddress(this HttpRequestBase Request)
        {
            Contract.Requires<ArgumentNullException>(Request != null, "Request");
            IPAddress Address = null;
            if (!IPAddress.TryParse(Request.UserHostAddress, out Address))
                Address = null;
            return Address;
        }

        /// <summary>
        /// Gets the user's IP address if it exists, null is returned otherwise
        /// </summary>
        /// <param name="Request">Request</param>
        /// <returns>The IPAddress object if it exists, null otherwise</returns>
        public static IPAddress UserIPAddress(this HttpRequest Request)
        {
            Contract.Requires<ArgumentNullException>(Request != null, "Request");
            IPAddress Address = null;
            if (!IPAddress.TryParse(Request.UserHostAddress, out Address))
                Address = null;
            return Address;
        }

        #endregion

        #region IfModifiedSince

        /// <summary>
        /// Converts the If-Modified-Since header value to a DateTime object
        /// </summary>
        /// <param name="Request">Request</param>
        /// <returns>The If-Modified-Since header value expressed as a DateTime object</returns>
        public static DateTime IfModifiedSince(this HttpRequestBase Request)
        {
            DateTime Result = DateTime.MinValue;
            return DateTime.TryParse(Request.Headers["If-Modified-Since"], out Result) ? Result : DateTime.MinValue;
        }

        /// <summary>
        /// Converts the If-Modified-Since header value to a DateTime object
        /// </summary>
        /// <param name="Request">Request</param>
        /// <returns>The If-Modified-Since header value expressed as a DateTime object</returns>
        public static DateTime IfModifiedSince(this HttpRequest Request)
        {
            DateTime Result = DateTime.MinValue;
            return DateTime.TryParse(Request.Headers["If-Modified-Since"], out Result) ? Result : DateTime.MinValue;
        }

        #endregion

        #region IsMobile

        /// <summary>
        /// Detects if a browser is on a mobile device or not (does a more thorough job than simply Request.Browser.IsMobileDevice)
        /// </summary>
        /// <param name="Request">Request object</param>
        /// <returns>True if it is, false otherwise</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
        public static bool IsMobile(this HttpRequestBase Request)
        {
            Contract.Requires<ArgumentNullException>(Request != null, "Request");
            Contract.Requires<ArgumentNullException>(Request.Browser != null, "Request.Browser");
            if (Request.Browser.IsMobileDevice
                || !string.IsNullOrEmpty(Request.ServerVariables["HTTP_X_WAP_PROFILE"])
                || (!string.IsNullOrEmpty(Request.ServerVariables["HTTP_ACCEPT"])
                    && (Request.ServerVariables["HTTP_ACCEPT"].ToLower(CultureInfo.InvariantCulture).Contains("wap")
                       || Request.ServerVariables["HTTP_ACCEPT"].ToLower(CultureInfo.InvariantCulture).Contains("wml+xml"))))
                return true;
            Regex Regex1 = new Regex(@"android.+mobile|avantgo|bada\\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\\/|plucker|pocket|psp|symbian|treo|up\\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Regex Regex2 = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\\-(n|u)|c55\\/|capi|ccwa|cdm\\-|cell|chtm|cldc|cmd\\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\\-s|devi|dica|dmob|do(c|p)o|ds(12|\\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\\-|_)|g1 u|g560|gene|gf\\-5|g\\-mo|go(\\.w|od)|gr(ad|un)|haie|hcit|hd\\-(m|p|t)|hei\\-|hi(pt|ta)|hp( i|ip)|hs\\-c|ht(c(\\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\\-(20|go|ma)|i230|iac( |\\-|\\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\\/)|klon|kpt |kwc\\-|kyo(c|k)|le(no|xi)|lg( g|\\/(k|l|u)|50|54|e\\-|e\\/|\\-[a-w])|libw|lynx|m1\\-w|m3ga|m50\\/|ma(te|ui|xo)|mc(01|21|ca)|m\\-cr|me(di|rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\\-2|po(ck|rt|se)|prox|psio|pt\\-g|qa\\-a|qc(07|12|21|32|60|\\-[2-7]|i\\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\\-|oo|p\\-)|sdk\\/|se(c(\\-|0|1)|47|mc|nd|ri)|sgh\\-|shar|sie(\\-|m)|sk\\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\\-|v\\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\\-|tdg\\-|tel(i|m)|tim\\-|t\\-mo|to(pl|sh)|ts(70|m\\-|m3|m5)|tx\\-9|up(\\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|xda(\\-|2|g)|yas\\-|your|zeto|zte\\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (Regex1.IsMatch(Request.ServerVariables["HTTP_USER_AGENT"]) || Regex2.IsMatch(Request.ServerVariables["HTTP_USER_AGENT"].Substring(0, 4)))
                return true;
            return false;
        }

        /// <summary>
        /// Detects if a browser is on a mobile device or not (does a more thorough job than simply Request.Browser.IsMobileDevice)
        /// </summary>
        /// <param name="Request">Request object</param>
        /// <returns>True if it is, false otherwise</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
        public static bool IsMobile(this HttpRequest Request)
        {
            Contract.Requires<ArgumentNullException>(Request != null, "Request");
            Contract.Requires<ArgumentNullException>(Request.Browser != null, "Request.Browser");
            if (Request.Browser.IsMobileDevice
                || !string.IsNullOrEmpty(Request.ServerVariables["HTTP_X_WAP_PROFILE"])
                || (!string.IsNullOrEmpty(Request.ServerVariables["HTTP_ACCEPT"])
                    && (Request.ServerVariables["HTTP_ACCEPT"].ToLower(CultureInfo.InvariantCulture).Contains("wap")
                       || Request.ServerVariables["HTTP_ACCEPT"].ToLower(CultureInfo.InvariantCulture).Contains("wml+xml"))))
                return true;
            Regex Regex1 = new Regex(@"android.+mobile|avantgo|bada\\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\\/|plucker|pocket|psp|symbian|treo|up\\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Regex Regex2 = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\\-(n|u)|c55\\/|capi|ccwa|cdm\\-|cell|chtm|cldc|cmd\\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\\-s|devi|dica|dmob|do(c|p)o|ds(12|\\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\\-|_)|g1 u|g560|gene|gf\\-5|g\\-mo|go(\\.w|od)|gr(ad|un)|haie|hcit|hd\\-(m|p|t)|hei\\-|hi(pt|ta)|hp( i|ip)|hs\\-c|ht(c(\\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\\-(20|go|ma)|i230|iac( |\\-|\\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\\/)|klon|kpt |kwc\\-|kyo(c|k)|le(no|xi)|lg( g|\\/(k|l|u)|50|54|e\\-|e\\/|\\-[a-w])|libw|lynx|m1\\-w|m3ga|m50\\/|ma(te|ui|xo)|mc(01|21|ca)|m\\-cr|me(di|rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\\-2|po(ck|rt|se)|prox|psio|pt\\-g|qa\\-a|qc(07|12|21|32|60|\\-[2-7]|i\\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\\-|oo|p\\-)|sdk\\/|se(c(\\-|0|1)|47|mc|nd|ri)|sgh\\-|shar|sie(\\-|m)|sk\\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\\-|v\\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\\-|tdg\\-|tel(i|m)|tim\\-|t\\-mo|to(pl|sh)|ts(70|m\\-|m3|m5)|tx\\-9|up(\\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|xda(\\-|2|g)|yas\\-|your|zeto|zte\\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (Regex1.IsMatch(Request.ServerVariables["HTTP_USER_AGENT"]) || Regex2.IsMatch(Request.ServerVariables["HTTP_USER_AGENT"].Substring(0, 4)))
                return true;
            return false;
        }

        #endregion

        #endregion
    }
}