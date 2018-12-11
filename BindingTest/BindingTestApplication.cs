using System;
using Android.App;
using Android.Runtime;
using Android.Util;
using Com.Permutive.Android;
using Java.Util;

namespace BindingTest
{
    [Application]
    public class BindingTestApplication : Application
    {
        private bool useRelease = false;
        private Permutive permutive;

        public BindingTestApplication(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            Log.Debug("WTF", $"Application: onCreate");

        }

        public Permutive GetPermutive()
        {
            if (permutive == null)
            {
                permutive = CreatePermutive();
            }

            return permutive;
        }


        private Permutive CreatePermutive()
        {

            UUID projectId;
            UUID apiKey;

            if (useRelease)
            {
                projectId = UUID.FromString("e0039147-51e7-4224-a814-0e2d438aabcd");
                apiKey = UUID.FromString("da4d09b5-843a-4bd5-bd79-8cea7f69f730");
            }
            else
            {
                projectId = UUID.FromString("5c2b415d-7b20-4bc9-84fb-4f04bf0e5743");
                apiKey = UUID.FromString("be668577-07f5-444d-98e0-222b990951b1");
            }


            return new Permutive.Builder()
                        .ApiKey(projectId)
                        .ProjectId(apiKey)
                        //.AliasProvider(null)
                        //.Identity("testIdentity@xamarin.com")
                        .Context(this)
                        .Build();

        }
    }
}
