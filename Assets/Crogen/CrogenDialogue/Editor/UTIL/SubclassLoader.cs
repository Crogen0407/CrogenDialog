using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Crogen.CrogenDialogue.Editor.UTIL
{
    [InitializeOnLoad]
    public static class SubclassLoader
    {
		private static Dictionary<Type, List<Type>> Types { get; set; } = new();

        public static List<Type> Get<T>() where T : class
        {
			if (Types.ContainsKey(typeof(T)) == false)
			{
				Types.Add(typeof(T), GetSubclassesOf<T>());
			}

            return Types[typeof(T)];
        }

		private static List<Type> GetSubclassesOf<T>()
		{
			return AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(assembly => assembly.GetTypes())
				.Where(type => type.IsClass && !type.IsAbstract && typeof(T).IsAssignableFrom(type))
				.ToList();
		}
	}
}
