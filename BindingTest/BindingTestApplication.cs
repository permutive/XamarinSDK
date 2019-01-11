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
            options.ApiKey = "da4d09b5-843a-4bd5-bd79-8cea7f69f730";
            options.ProjectId = "e0039147-51e7-4224-a814-0e2d438aabcd";

            var newPermutive = new PermutiveImpl(this);
            newPermutive.Initialize(options);


            //when we want to shut down/stop processing, say when the app is destroyed, call:
            //newPermutive.Dispose();

            return newPermutive;
        }
    }
}
