// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace SharedSample.iOS
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        UIKit.UIButton Button { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton sendEventButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton setIdentityButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton StartButton { get; set; }

        [Action ("SendEvent_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SendEvent_TouchUpInside (UIKit.UIButton sender);

        [Action ("SetIdentityButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SetIdentityButton_TouchUpInside (UIKit.UIButton sender);

        [Action ("StartButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void StartButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (sendEventButton != null) {
                sendEventButton.Dispose ();
                sendEventButton = null;
            }

            if (setIdentityButton != null) {
                setIdentityButton.Dispose ();
                setIdentityButton = null;
            }

            if (StartButton != null) {
                StartButton.Dispose ();
                StartButton = null;
            }
        }
    }
}