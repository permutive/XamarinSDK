using System;

using UIKit;

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
    }



}
