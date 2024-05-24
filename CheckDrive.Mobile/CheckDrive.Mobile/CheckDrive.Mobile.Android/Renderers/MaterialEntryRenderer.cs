using Android.Content;
using Android.Graphics.Drawables;
using AndroidX.Core.Content;
using CheckDrive.Mobile.Droid.Renderers;
using CheckDrive.Mobile.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using Android.Content.Res;

[assembly: ExportRenderer(typeof(CustomMaterialEntry), typeof(MaterialEntryRenderer))]
namespace CheckDrive.Mobile.Droid.Renderers
{
    public class MaterialEntryRenderer : ViewRenderer<CustomMaterialEntry, EditText>
    {
        public MaterialEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CustomMaterialEntry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    var editText = new EditText(Context);
                    SetNativeControl(editText);
                }

                UpdateTrailingIcon(e.NewElement);
            }
        }

        private void UpdateTrailingIcon(CustomMaterialEntry materialEntry)
        {
            if (materialEntry.TrailingIcon != null)
            {
                var trailingIconId = Resource.Drawable.eye_off;

                if(materialEntry.TrailingIcon == "eye.png")
                {
                    trailingIconId = Resource.Drawable.eye;
                }

                if (trailingIconId == 0)
                {
                    throw new Resources.NotFoundException($"Resource with name {materialEntry.TrailingIcon} not found");
                }

                var drawable = ContextCompat.GetDrawable(Context, trailingIconId);

                if (drawable != null)
                {
                    var width = (int)(materialEntry.TrailingIconWidthRequest * Resources.DisplayMetrics.Density);
                    var height = (int)(materialEntry.TrailingIconHeightRequest * Resources.DisplayMetrics.Density);
                    drawable.SetBounds(0, 0, width, height);

                    Control.SetCompoundDrawablesWithIntrinsicBounds(null, null, drawable, null);
                }
            }
        }

    }
}
