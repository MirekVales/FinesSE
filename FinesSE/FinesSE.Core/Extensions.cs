using FinesSE.Contracts.Infrastructure;
using LightInject;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FinesSE.Core
{
    public static class Extensions
    {
        public static IEnumerable<string> GetGenericArgumentsName(this MethodInfo methodInfo)
            => methodInfo
            .GetGenericArguments()
            .Select(x => x.FullName);

        public static string FallbackEmptyString(this string value, Func<string> fallback)
            => string.IsNullOrWhiteSpace(value) ? fallback() : value;

        public static bool IsDynamic<T>(this T value)
            where T : struct
        {
            var memberInfo = (typeof(T)).GetMember(value.ToString());
            if (memberInfo.Length == 0)
                throw new Exception($"Member value '{value}' not found");
            return memberInfo[0].GetCustomAttributes(typeof(DynamicAttribute), true).Any();
        }

        public static IEnumerable<T> Split<T>(this T value)
            where T : struct
        {
            return value
                .ToString()
                .Split(',')
                .Select(flag => (T)Enum.Parse(typeof(T), flag))
                .ToList();
        }

        public static byte[] ToByteArray(this Image bitmap)
        {
            using (var memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, ImageFormat.Png);
                return memoryStream.ToArray();
            }
        }

        public static Bitmap ToBitmap(this byte[] array)
        {
            // You must keep the stream open for the lifetime of the Bitmap.
            var stream = new MemoryStream(array);
            return new Bitmap(stream);
        }

        public static void RegisterRequiredAssembly(this IServiceRegistry registry, string parentDirectory, string fileName)
        {
            var directory = string.IsNullOrEmpty(parentDirectory)
                ? AppDomain.CurrentDomain.BaseDirectory
                : parentDirectory;

            if (!File.Exists(Path.Combine(directory, fileName)))
                throw new FileNotFoundException("Required component was not found", fileName);

            registry.RegisterAssembly(fileName);
        }
    }
}
