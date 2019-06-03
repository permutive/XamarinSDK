using System;

using UIKit;



//using Permutive.Xamarin.iOS.Binding;
using Permutive.Xamarin.iOS;
using SharedSample;
using Foundation;

namespace SharedSample.iOS
{

    public class IOSLogger : Logger
    {
        public void Log(string message)
        {
            Console.WriteLine("Permutive: {0}", message);
        }
    }

    public partial class ViewController : UIViewController
    {
        Logger logger = new IOSLogger();
        PermutiveSdkTester tester;

        public ViewController(IntPtr handle) : base(handle)
        {
            Permutive.Xamarin.PermutiveImpl permutive = new Permutive.Xamarin.PermutiveImpl();
            tester = new PermutiveSdkTester(permutive, logger);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.

            StartButton.AccessibilityIdentifier = "startButton";
            sendEventButton.AccessibilityIdentifier = "sendEvent";
            setIdentityButton.AccessibilityIdentifier = "setIdentity";
        }

        partial void SetIdentityButton_TouchUpInside(UIButton sender)
        {
            tester.SetIdentity();
        }

        partial void SendEvent_TouchUpInside(UIButton sender)
        {
            tester.TrackEvent();
        }

        partial void StartButton_TouchUpInside(UIButton sender)
        {
            tester.Initialise(null);
            tester.Setup();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.		
        }

        //private PermutiveSdkTester getTester()
        //{
        //    if (tester == null)
        //    {
        //        tester = new PermutiveSdkTester()
        //    }

        //    return tester;
        //}

        //private void createPermutive()
        //{

        //    NSUuid projectId = new NSUuid("e0039147-51e7-4224-a814-0e2d438aabcd");
        //    NSUuid apiKey = new NSUuid("da4d09b5-843a-4bd5-bd79-8cea7f69f730");

        //    PermutiveOptions options = PermutiveOptions.OptionsWithProjectId(
        //        projectId, apiKey
        //    );

        //    PermutiveSdk.ConfigureWithOptions(options);

        //    //return null; 

        //}

        //private void createPermutiveA()
        //{
        //    Console.WriteLine("Permutive: creating permutive...");
        //    NSUuid projectId = new NSUuid("e0039147-51e7-4224-a814-0e2d438aabcd");
        //    NSUuid apiKey = new NSUuid("da4d09b5-843a-4bd5-bd79-8cea7f69f730");

        //    PermutiveOptions options = PermutiveOptions.OptionsWithProjectId(
        //        projectId, apiKey
        //    );


        //    //TriggersProvider triggersProvider = permutive.TriggersProvider();

        //    //var querySegmentsDisposable = triggersProvider.QuerySegments(result =>
        //    //{
        //    //    Android.Util.Log.Debug("Permutive", "Current user is in segments:");
        //    //    foreach (var value in result)
        //    //    {
        //    //        Android.Util.Log.Debug("Permutive", $"\t{value}");
        //    //    }
        //    //});

        //    PermutiveSdk.ConfigureWithOptions(options);

        //    Console.WriteLine("Permutive: setting context...");


        //    PermutiveEventActionContext context = new PermutiveEventActionContext();
        //    context.Url = NSUrl.FromString("http://www.what.com");


        //    PermutiveSdk.Context = new PermutiveEventActionContext();



        //    Console.WriteLine("Permutive: created!!!");


        //    //NSNumber[] segments = PermutiveInterface.GetPermutive().TriggersPr:wovider.QuerySegments;
        //    //Console.WriteLine("Permutive: gotsegments {segments.Length}");
        //    //foreach (var value in segments)
        //    //{
        //    //    Console.WriteLine("Permutive: {value}");
        //    //}
        //}
    }



}
