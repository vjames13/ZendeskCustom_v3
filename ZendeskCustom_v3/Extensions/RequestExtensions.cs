﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Linq;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using System.Globalization;

namespace ZendeskCustom.Extensions
{
    public static class RequestExtensions
    {
        //public static void AddAndSerializeParam(this RestRequest request, object obj, ParameterType parameterType)
        //{                  
        //    var json = JsonConvert.SerializeObject(obj, new JsonSerializerSettings(){NullValueHandling = NullValueHandling.Ignore});
        //    request.AddParameter("application/json", json, parameterType);
        //}

        public static WebResponse GetWebResponse(this WebRequest request)
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);

            IAsyncResult asyncResult = request.BeginGetResponse(r => autoResetEvent.Set(), null);

            // Wait until the call is finished
            autoResetEvent.WaitOne();

            return request.EndGetResponse(asyncResult);
        }

        public static Stream GetWebRequestStream(this WebRequest request)
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);

            IAsyncResult asyncResult = request.BeginGetRequestStream(r => autoResetEvent.Set(), null);

            // Wait until the call is finished
            autoResetEvent.WaitOne();

            return request.EndGetRequestStream(asyncResult);
        }

        public static string ToCsv(this List<long> ids)
        {
            return string.Join(",", ids.Select(x => x.ToString(CultureInfo.InvariantCulture)).ToArray());
        }

        public static T ConvertToObject<T>(this string json)
        {
            var obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }

        public static int GetEpoch(this DateTime date)
        {
            var t = date - new DateTime(1970, 1, 1);
            return (int)t.TotalSeconds;
        }
    }
}
