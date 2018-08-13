using System;
using System.Collections.Generic;
using System.Text;

namespace ExtensionsCoreUtils.Factories
{
    /// <summary>
    /// Internal injection factory.
    /// </summary>
    internal static class InjectionFactory
    {

        static readonly Dictionary<string, dynamic> objectList = new Dictionary<string, dynamic>();

        /// <summary>
        /// Remove all items from list.
        /// </summary>
        public static void Clear()
        {
            objectList.Clear();
        }

        /// <summary>
        /// Add a new entry with the specific name.
        /// </summary>
        /// <param name="key">The name.</param>
        /// <param name="zclass">The class reference.</param>
        public static void Add(string key, dynamic zclass)
        {
            if (key is null)
            {
                throw new ArgumentException("The key attribute must be not null.");
            }
            if (zclass is null)
            {
                throw new ArgumentException("The class attribute must be not null.");
            }
            if (objectList.ContainsKey(key))
            {
                throw new Exception(String.Format("The key {0} already exists!", key));
            }
            objectList.Add(key, zclass);

        }

        /// <summary>
        /// Get value or default.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetValueOrDefault(string key)
        {
            if (key is null)
            {
                throw new ArgumentException("The key attribute must be not null.");
            }

            return objectList.GetValueOrDefault(key);
        }


    }
}
