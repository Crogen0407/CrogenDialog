using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Crogen.CrogenDialogue.Editor
{
    [InitializeOnLoad]
    public static class NodeTypeList
    {
		private static List<Type> List { get; set; }

        public static List<Type> Get()
        {
            return List;
        }

		static NodeTypeList()
        {
			List = GetSubclassesOf<NodeSO>();
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
