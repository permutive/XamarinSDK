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
        private bool useRelease = false;
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

            if (useRelease)
            {
                options.ApiKey= "e0039147-51e7-4224-a814-0e2d438aabcd";
                options.ProjectId = "da4d09b5-843a-4bd5-bd79-8cea7f69f730";
            }
            else
            {
                options.ApiKey = "5c2b415d-7b20-4bc9-84fb-4f04bf0e5743";
                options.ProjectId = "be668577-07f5-444d-98e0-222b990951b1";
            }

            var newPermutive = new PermutiveImpl(this);
            newPermutive.Initialize(options);

            return newPermutive;
        }
    }
}
