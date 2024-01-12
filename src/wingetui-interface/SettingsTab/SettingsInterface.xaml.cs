using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Python.Runtime;
using Microsoft.VisualBasic.FileIO;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ModernWindow.SettingsTab
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainInterface : Window
    {
        private MainApp _app;
        public MainInterface(MainApp app)
        {
            this.InitializeComponent();
            _app = app;
       
            PyDict lang_dict = new PyDict(_app.Core.Languages.LangData.languageReference);
            var lang_values = lang_dict.Keys();
            var lang_names = lang_dict.Values();
            bool isFirst = true;
            for (int i = 0; i < lang_values.Count(); i++)
            {
                LanguageSelector.AddItem(lang_names[i].ToString(), lang_values[i].ToString(), isFirst);
                isFirst = false;
            }
            LanguageSelector.ShowAddedItems();


            PyDict updates_dict = new PyDict(_app.Core.Tools.update_times_reference);
            var time_names = updates_dict.Keys();
            var time_keys = updates_dict.Values();
            for (int i = 0; i < time_names.Count(); i++)
            {
                UpdatesCheckIntervalSelector.AddItem(time_names[i].ToString(), time_keys[i].ToString(), false);
            }
            UpdatesCheckIntervalSelector.ShowAddedItems();

            ThemeSelector.AddItem("Light", "light");
            ThemeSelector.AddItem("Dark", "dark");
            ThemeSelector.AddItem("Follow system color scheme", "auto");
            ThemeSelector.ShowAddedItems();
        }

        public int GetHwnd()
        {
            return (int)WinRT.Interop.WindowNative.GetWindowHandle(this);
        }

        public void ShowWindow_SAFE()
        {
            Console.WriteLine("Called from Python!");
            DispatcherQueue.TryEnqueue(() => { this.Activate(); });
        }


    }
}
