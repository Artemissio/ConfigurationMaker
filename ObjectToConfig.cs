using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace ConfigurationMaker
{
    public static class ObjectToConfig<T> where T : class, new()
    {
        public static void WriteToFile(string path, T value)
        {
            if (File.Exists(path))
            {
                XDocument document = XDocument.Load(path);

                if (document == null)
                    throw new NullReferenceException();

                document.Root.Elements().Remove();

                var fields = typeof(T).GetProperties().ToList();
                var values = typeof(T).GetProperties().Select(x => x.GetValue(value)).ToList();

                for (int i = 0; i < fields.Count; i++)
                {
                    document.Root.Add(new XElement(fields[i].Name, values[i]?.ToString()));
                }

                document.Save(path);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        public static T ReadFromFile(string path)
        {
            T tObj = new T();

            if (File.Exists(path))
            {
                XDocument document = XDocument.Load(path);

                if (document == null)
                    throw new NullReferenceException();

                Type type = typeof(T);
                var fields = typeof(T).GetProperties().ToList();
                var values = document.Root.Elements().ToList();

                if (values.Count == fields.Count)
                {
                    foreach (var value in values)
                    {
                        type.GetProperty(value.Name.ToString()).SetValue(tObj, value.Value.ToString());
                    }
                }
                else
                {
                    if (values.Count > fields.Count)
                    {
                        foreach (var value in values)
                        {
                            var property = type.GetProperty(value.Name.ToString());
                            if (property != null)
                            {
                                property.SetValue(tObj, value.Value.ToString());
                            }
                            else
                            {
                                document.Root.Elements(value.Name).Remove();
                                document.Save(path);
                            }
                        }
                    }
                    else
                    {
                        foreach (var value in values)
                        {
                            type.GetProperty(value.Name.ToString()).SetValue(tObj, value.Value.ToString());
                        }

                        WriteToFile(path, tObj);
                    }
                }

                return tObj;
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
    }
}
