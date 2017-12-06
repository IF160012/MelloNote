using Android.App;
using Android.Widget;
using Android.OS;

namespace Mello_Note
{
    [Activity(Label = "Mello_Note", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }
    }
}

