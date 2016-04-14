using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Com.QueoFlow.TrackingtoolLogistik.Wpf.Utils {
    /// <summary>
    ///   Hilfklasse um Fensterinstanzen zu halten. Wird benötigt, um den Owner eines Fensters auf ein anderes Fenster setzen zu können, damit die StartupLocation des neuen Fensters korrekt funktioniert. Sollte ausschließlich in den ViewCommands Verwendung finden. Wird ein Fenster geschlossen, so entfernt es sich selbst aus der Liste registrierter Fenster.
    /// </summary>
    /// <code>// Registriert ein Window am Parent Helper um später dieses Window als Owner für ein anderes Fenster festzulegen.
    ///   WindowParentHelper.Instance.RegisterWindow(view);
    /// 
    ///   // Legt den Owner eines Fensters auf ein Fenster vom Typ MainView
    ///   view.Owner = WindowParentHelper.Instance.GetWindowBySpecificType(typeof(MainView));</code>
    public class WindowParentHelper : IWindowParentHelper
    {
        private readonly IList<Window> _registeredWindows = new List<Window>();

        /// <summary>
        ///   Liefert ein Fenster anhand eines Typen.
        /// </summary>
        /// <param name="type"> </param>
        /// <returns> </returns>
        public Window GetWindowBySpecificType(Type type) {
            return _registeredWindows.FirstOrDefault(x => x.GetType() == type);
        }

        /// <summary>
        ///   Fügt ein Fenster zur Auflistung hinzu.
        /// </summary>
        /// <param name="window"> </param>
        public void RegisterWindow(Window window) {
            if (!_registeredWindows.Contains(window)) {
                window.Closed += WindowClosed;
                _registeredWindows.Add(window);
            }
        }

        /// <summary>
        ///   Entfernt ein Fenster aus der Auflistung.
        /// </summary>
        /// <param name="window"> </param>
        public void RemoveWindow(Window window) {
            if (_registeredWindows.Contains(window)) {
                window.Closed -= WindowClosed;
                _registeredWindows.Remove(window);
            }
        }

        private void WindowClosed(object sender, EventArgs e) {
            RemoveWindow(GetWindowBySpecificType(sender.GetType()));
        }
    }
}