using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ClickyControllerGUI.Models
{
    public class Command
    {
        public Command()
        {

        }

        public string Parameters { get; set; }
        public string Method { get; set; }

        public void Run()
        {
            Type type = Type.GetType("ClickyControllerGUI.Models.Controller"); ;
            MethodInfo method = type.GetMethod(Method);
            object classObject = Activator.CreateInstance(type);

            if (method.GetParameters().Length > 0)
                method.Invoke(classObject, new object[] { Parameters });
            else
                method.Invoke(classObject, null);
        }
    }
}
