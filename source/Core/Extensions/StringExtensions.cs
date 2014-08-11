﻿/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see license
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    static class StringExtensions
    {
        public static string ToBase64UrlEncoded(this string s)
        {
            if (s == null) return null;

            var bytes = Encoding.UTF8.GetBytes(s);
            
            s = Convert.ToBase64String(bytes);
            s = s.Split('=')[0]; // Remove any trailing '='s
            s = s.Replace('+', '-'); // 62nd char of encoding
            s = s.Replace('/', '_'); // 63rd char of encoding

            return s;
        }
        
        public static string FromBase64UrlEncoded(this string s)
        {
            if (s == null) return null;

            s = s.Replace('-', '+'); // 62nd char of encoding
            s = s.Replace('_', '/'); // 63rd char of encoding

            switch (s.Length % 4) // Pad with trailing '='s
            {
                case 0: break; // No pad chars in this case
                case 2: s += "=="; break; // Two pad chars
                case 3: s += "="; break; // One pad char
                default: throw new Exception("Illegal base64url string!");
            }

            var bytes = Convert.FromBase64String(s);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
