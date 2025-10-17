using Domain.Enums;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimacOptiWork.Utils
{
    public static class UIHelpers
    {
        /// <summary>
        /// Cambia la visibilidad de un control por nombre dentro del árbol visual.
        /// </summary>
        public static void ToggleVisibility(FrameworkElement parent, string elementName, bool visible)
        {
            var element = parent.FindName(elementName) as UIElement;
            if (element != null)
                element.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Aplica una configuración de visibilidad según el rol.
        /// </summary>
        public static void ApplyRoleUI(FrameworkElement parent, userRole role, Dictionary<userRole, List<string>> roleVisibility)
        {
            // Oculta todo
            var allElements = roleVisibility.SelectMany(r => r.Value).Distinct();
            foreach (var name in allElements)
                ToggleVisibility(parent, name, false);

            // Muestra los que correspondan al rol actual
            if (roleVisibility.TryGetValue(role, out var visibleItems))
            {
                foreach (var name in visibleItems)
                    ToggleVisibility(parent, name, true);
            }
        }

        // <summary>
        // Aplica una función por cada rol definido en el diccionario.
        // </summary>
        public static void ForEachRoleAction(userRole role , Dictionary<userRole, Func<System.Threading.Tasks.Task>> actions)
        {
            if(actions.TryGetValue(role, out var action))
            {
                action?.Invoke();
            }
        }
    }
}
