using System;
using Android.App;
using Android.Runtime;
using Android.Util;
using Permutive.Xamarin;

namespace BindingTest
{
    [Application]
    public class BindingTestApplication : Application
    {
        private Permutive.Xamarin.Permutive permutive;

        public BindingTestApplication(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
        }

        public Permutive.Xamarin.Permutive GetPermutive()
        {
            if (permutive == null)
            {
                permutive = CreatePermutive();
            }

            return permutive;
        }


        private PermutiveImpl CreatePermutive()
        {

            PermutiveOptions options = new PermutiveOptions();
            options.ApiKey= YOUR_API_KEY;
            options.ProjectId = YOUR_PROJECT_ID;

            var newPermutive = new PermutiveImpl(this);
            newPermutive.Initialize(options);


            //when we want to shut down/stop processing, say when the app is destroyed, call:
            //newPermutive.Dispose();

            return newPermutive;
        }
    }
}
