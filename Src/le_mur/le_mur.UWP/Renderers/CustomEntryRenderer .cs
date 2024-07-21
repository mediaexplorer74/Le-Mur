using le_mur.UWP.Renderers;
using le_mur.View.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace le_mur.UWP.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context)
            : base()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null && e.NewElement is CustomEntry customEntry)
            {
                Control.Background = default;//new Android.Graphics.Drawables.GradientDrawable();

                //var gd = new Android.Graphics.Drawables.GradientDrawable();
                //gd.SetColor(Android.Graphics.Color.Transparent);
                //gd.SetStroke(customEntry.BorderWidth, customEntry.BorderColor.ToAndroid());

                //Control.SetBackground(gd);
            }
        }
    }

    public class Context
    {
    }
}