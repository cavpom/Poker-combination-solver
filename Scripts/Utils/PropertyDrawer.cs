using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Inspector
{
	[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public class ButtonAttribute  : Attribute
	{
		public string Name = null;

		public ButtonAttribute()
        {
        }

		public ButtonAttribute(string name)
        {
            this.Name = name;
        }
	}

#if UNITY_EDITOR
	[UnityEditor.CanEditMultipleObjects]
    [UnityEditor.CustomEditor(typeof(UnityEngine.Object), true)]
    public class ObjectEditor : UnityEditor.Editor
	{
        public override void OnInspectorGUI()
        {
			// Draw the rest of the inspector as usual
			base.OnInspectorGUI();	

			DrawButtons();
        }

		public void DrawButtons()
        {
            // Loop through all methods with no parameters
            var methods = target.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(m => m.GetParameters().Length == 0);
            foreach (MethodInfo method in methods)
            {
				// Get the ButtonAttribute on the method (if any)

				Attribute[] attributes = Attribute.GetCustomAttributes(method, typeof(ButtonAttribute));
                ButtonAttribute button = attributes != null && attributes.Length > 0 ? (attributes[0] as ButtonAttribute) : null;

                if (button != null)
                {
                    // Draw a button which invokes the method
                    var buttonName = button.Name ?? method.Name;
                    if (GUILayout.Button(buttonName))
                    {
                        foreach (UnityEngine.Object t in targets)
                        {
                            method.Invoke(t, null);
                        }
                    }
                }
            }
        }
    }
#endif
}