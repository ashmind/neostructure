using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace AshMind.Web.Mvc {
    public static class GravatarExtension {
        private static readonly MD5 MD5 = MD5.Create();

        public static string Gravatar(this HtmlHelper html, string email) {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(email));
            return GetImageTag(GetGravatar(email));
        }

        public static string Gravatar(this HtmlHelper html, string email, object gravatarAttributes) {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(email));
            return GetImageTag(GetGravatar(email, gravatarAttributes));
        }

        public static string Gravatar(this HtmlHelper html, string email, object gravatarAttributes, object htmlAttributes) {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(email));
            return GetImageTag(GetGravatar(email, gravatarAttributes), htmlAttributes);
        }

        private static string GetImageTag(string source) {
            return GetImageTag(source, null);
        }

        private static string GetImageTag(string source, object htmlAttributes) {
            IDictionary<string, object> attributes = 
                (htmlAttributes == null 
                     ? new RouteValueDictionary() 
                     : new RouteValueDictionary(htmlAttributes));

            var builder = new TagBuilder("img");
            builder.Attributes["src"] = source;
            builder.MergeAttributes(attributes);

            return builder.ToString(TagRenderMode.SelfClosing);
        }

        private static string GetGravatar(string email) {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(email));
            return string.Format("http://www.gravatar.com/avatar/{0}", EncryptMD5(email));
        }

        private static string GetGravatar(string email, object gravatarAttributes) {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(email));

            var attributes = (gravatarAttributes == null  ? new RouteValueDictionary() 
                                                          : new RouteValueDictionary(gravatarAttributes));
        
            var returnVal = GetGravatar(email);
            var first = true;
            foreach (var key in attributes.Keys) {
                if (first) {
                    first = false;
                    returnVal += string.Format("?{0}={1}", key, attributes[key]);
                    continue;
                }

                returnVal += string.Format("&{0}={1}", key, attributes[key]);
            }

            return returnVal;
        }

        private static string EncryptMD5(string value) {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(value));

            var valueArray = Encoding.ASCII.GetBytes(value);
            valueArray = MD5.ComputeHash(valueArray);

            var encrypted = new StringBuilder();
            foreach (var @byte in valueArray) {
                encrypted.Append(@byte.ToString("x2").ToLower());
            }
            return encrypted.ToString();
        }
    }
}