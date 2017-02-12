using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using WebCommands.Infrastructure.Commands;

namespace WebCommands.Infrastructure.Extensions
{
    public static class HttpExtensions
    {
        private static readonly JsonSerializer serializer = new JsonSerializer();

        public static Command ReadAsCommand(this HttpRequest request, Type commandType)
        {
            using (var sr = new StreamReader(request.Body))
            using (var jr = new JsonTextReader(sr))
                return serializer.Deserialize(jr, commandType) as Command;
        }

        public static string ToJson(this object @object)
        {
            return JsonConvert.SerializeObject(@object);
        }
    }
}