using FinesSE.Contracts.Infrastructure;
using LightInject;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
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
            => value
                .ToString()
                .Split(',')
                .Select(flag => (T)Enum.Parse(typeof(T), flag))
                .ToList();

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

        public static string GetImageBase64(this string imagePath, ImageFormat format)
        {
            using (var image = new Bitmap(imagePath))
            using (var stream = new MemoryStream())
            {
                image.Save(stream, format);
                byte[] imageBytes = stream.ToArray();
                return $"data:image/{format.ToString()};base64," + Convert.ToBase64String(imageBytes);
            }
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

        public static string GetRootedPath(this string path)
        {
            if (Path.IsPathRooted(path))
                return path;

            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
        }

        public static string EnsureDirectoryExistence(this string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }

        public static string ExpandErrorMessage(this Exception e)
        {
            IEnumerable<string> GetMessages(Exception exception)
            {
                yield return exception.Message;

                if (exception.InnerException != null)
                    foreach (var message in GetMessages(exception.InnerException))
                        yield return message;
            }

            return string.Join(System.Environment.NewLine, GetMessages(e));
        }

        public static double ToDouble(this string value)
            =>  double.Parse(
                    value
                    .Replace(",", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator)
                    .Replace(".", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));
    }
}
