using System;

using UIKit;



using Permutive.Xamarin.iOS.Binding;
using Foundation;

namespace SharedSample.iOS
{
    public partial class ViewController : UIViewController
    {
        int count = 1;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            Button.AccessibilityIdentifier = "myButton";
            Button.TouchUpInside += delegate
            {
                var title = string.Format("Permutive {0} clicks!!!", count++);
                Button.SetTitle(title, UIControlState.Normal);

                createPermutive();
            };
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.		
        }

        private void createPermutive()
        {
            Console.WriteLine("Permutive: creating permutive...");
            NSUuid projectId = new NSUuid("e0039147-51e7-4224-a814-0e2d438aabcd");
            NSUuid apiKey = new NSUuid("da4d09b5-843a-4bd5-bd79-8cea7f69f730");

            PermutiveOptions options = PermutiveOptions.OptionsWithProjectId(
                projectId, apiKey
            );





            //TriggersProvider triggersProvider = permutive.TriggersProvider();

            //var querySegmentsDisposable = triggersProvider.QuerySegments(result =>
            //{
            //    Android.Util.Log.Debug("Permutive", "Current user is in segments:");
            //    foreach (var value in result)
            //    {
            //        Android.Util.Log.Debug("Permutive", $"\t{value}");
            //    }
            //});

            PermutiveSdk.ConfigureWithOptions(options);

            Console.WriteLine("Permutive: setting context...");


            PermutiveEventActionContext context = new PermutiveEventActionContext();
            context.Url = NSUrl.FromString("http://www.what.com");


            PermutiveSdk.Context = new PermutiveEventActionContext();



            Console.WriteLine("Permutive: created!!!");


            //NSNumber[] segments = PermutiveInterface.GetPermutive().TriggersPr:wovider.QuerySegments;
            //Console.WriteLine("Permutive: gotsegments {segments.Length}");
            //foreach (var value in segments)
            //{
            //    Console.WriteLine("Permutive: {value}");
            //}
        }
    }



}
