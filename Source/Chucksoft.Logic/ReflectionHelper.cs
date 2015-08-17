using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System;

namespace Chucksoft.Logic
{
    public class ReflectionHelper<T>
    {
        /// <summary>
        /// Load all ICompiledTemplates into a collection
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="interfaceName">Name of Interface that we want to load.</param>
        /// <returns></returns>
        public static List<T> LoadTemplates(Assembly assembly, string interfaceName)
        {
            List<T> compiledTemplates = new List<T>();
            Type[] allTypes = assembly.GetTypes();

            foreach (Type type in allTypes)
            {
                if (type.GetInterface(interfaceName, true) != null && !type.IsAbstract && type.IsClass)
                {
                    T template = (T)Activator.CreateInstance(type);
                    compiledTemplates.Add(template);
                }
            }

            return compiledTemplates;
        }

        public static List<T> LoadTemplates(List<Assembly> assemblies, string interfaceName)
        {
            List<T> templates = new List<T>();

            foreach (Assembly assembly in assemblies)
            {
                templates.AddRange(LoadTemplates(assembly, interfaceName));
            }

            return templates;
        }

        /// <summary>
        /// Retrieve all dll's from the Template directory
        /// </summary>
        /// <returns></returns>
        public static List<Assembly> LoadAssemblies(string templateDirectory)
        {
            Assembly templates = Assembly.Load(new AssemblyName("Chucksoft.Templates"));
            List<Assembly> assemblies = new List<Assembly> {templates};

            return assemblies;
        }


    }
}
