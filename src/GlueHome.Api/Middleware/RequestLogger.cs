using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GlueHome.Api.Middleware;

namespace GlueHome.Api.Middleware
{
    public class RequestLogger
    {
        readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestLogger(ILogger logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var form = new Dictionary<string, StringValues>();
            if (context.Request.HasFormContentType)
            {
                form = Sanitise(await context.Request.ReadFormAsync());
            }

            var json = string.Empty;
            if (context.Request.ContentType != null && context.Request.ContentType.Contains("json"))
            {
                context.Request.Body = SanitiseJson(context.Request.Body, out json);
            }

            _logger.LogDebug("{method} {path} {query} {headers} {form} {json}",
                context.Request.Method,
                context.Request.Path,
                Sanitise(context.Request.Query),
                Sanitise(context.Request.Headers),
                form,
                json);

            await _next.Invoke(context);
        }

        private static HashSet<string> KeysWhichShouldBeSanitised = new HashSet<string>(new[] { "password" }, StringComparer.OrdinalIgnoreCase);

        public static Stream SanitiseJson(Stream stream, out string json)
        {
            // Read the HTTP stream into RAM.
            var bodyDouble = CopyToMemoryStream(stream);

            // Read the JSON.
            using (var sr = new StreamReader(new MemoryStream(bodyDouble.ToArray())))
            {
                json = SanitiseJson(sr.ReadToEnd());
            }

            // Reset the stream.
            bodyDouble.Position = 0;

            return bodyDouble;
        }

        public static string SanitiseJson(string json)
        {
            JObject jo;
            try
            {
                jo = JObject.Parse(json);
            }
            catch (JsonReaderException)
            {
                return json;
            }

            foreach (var n in jo.Descendants())
            {
                var parent = n.Parent as JProperty;
                if (parent != null)
                {
                    if (KeysWhichShouldBeSanitised.Contains(parent.Name))
                    {
                        n.Replace("*******");
                    }
                }
            }

            return jo.ToString();
        }

        public static Dictionary<string, StringValues> Sanitise(IFormCollection form)
        {
            return Sanitise(form.ToDictionary(k => k.Key, v => v.Value));
        }

        public static Dictionary<string, StringValues> Sanitise(IHeaderDictionary headers)
        {
            return Sanitise(headers.ToDictionary(k => k.Key, v => v.Value));
        }

        public static Dictionary<string, StringValues> Sanitise(IQueryCollection collection)
        {
            return Sanitise(collection.ToDictionary(k => k.Key, v => v.Value));
        }

        public static MemoryStream CopyToMemoryStream(Stream input)
        {
            var ms = new MemoryStream();

            // Copy the input stream to a memory stream.
            var buffer = new byte[256 * 1024];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }

            return ms;
        }

        public static Dictionary<string, StringValues> Sanitise(Dictionary<string, StringValues> dictionary)
        {
            return dictionary
                    .Select(d => new
                    {
                        Key = d.Key,
                        Value = KeysWhichShouldBeSanitised.Contains(d.Key) ? new StringValues("*******") : d.Value
                    })
                    .ToDictionary(k => k.Key, v => v.Value);
        }
    }
}

namespace Microsoft.AspNetCore.Builder
{
    public static class RequestLoggerExtensions
    {
        public static IApplicationBuilder UseRequestLogger(this IApplicationBuilder builder, ILogger logger)
        {
            return builder.UseMiddleware<RequestLogger>(logger);
        }
    }
}