using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Tamagotchi.WPF
{
    static class Helpers
    {
        public static MessageBoxResult ShowError(string messageBoxText, string caption)
        {
            return MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
