using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugin.CurrentActivity;
using StatuBar_DGGR.Droid.statusbar;
using StatuBar_DGGR.VistaModelo;
using Xamarin.Forms;

[assembly:Dependency(typeof(StatusBar))]

namespace StatuBar_DGGR.Droid.statusbar
{
    public class StatusBar : VMstatusbar
    {
        WindowManagerFlags _originalFlag;
        Window GetCurrentwindow()
        {
            var window = CrossCurrentActivity.Current.Activity.Window;
            window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            return window;
        }
        public void CambiarColor()
        {
            MostrarStatusBar();
            if (Build.VERSION.SdkInt>= BuildVersionCodes.M)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var currentWindow = GetCurrentwindow();
                    currentWindow.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LayoutStable;
                    currentWindow.SetStatusBarColor(Android.Graphics.Color.Rgb(18,18,18));
                });
            }
        }

        public void MostrarStatusBar()
        {
            var activity = (Activity)Forms.Context;
            var atras = activity.Window.Attributes;
            atras.Flags = _originalFlag;
            activity.Window.Attributes = atras;
        }

        public void OcultarStatusBar()
        {
            var activity = (Activity)Forms.Context;
            var atras = activity.Window.Attributes;
            _originalFlag = atras.Flags;
            atras.Flags = WindowManagerFlags.Fullscreen;
            activity.Window.Attributes = atras;
        }

        public void Transparente()
        {
            MostrarStatusBar();
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var currentWindow = GetCurrentwindow();
                    currentWindow.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LayoutFullscreen;
                    currentWindow.SetStatusBarColor(Android.Graphics.Color.Transparent);
                });
            }
        }

        public void Traslucido()
        {
            MostrarStatusBar();
            var activity = (Activity)Forms.Context;
            var atras = activity.Window.Attributes;
            _originalFlag = atras.Flags;
            atras.Flags = WindowManagerFlags.TranslucentStatus;
            activity.Window.Attributes = atras;
        }
    }
}