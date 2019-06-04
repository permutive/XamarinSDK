using System;
using System.Runtime.InteropServices;
using CoreFoundation;
using Foundation;
using ObjCRuntime;

namespace Permutive.Xamarin.iOS.Binding
{
    [Native]
    public enum PermutiveTriggerType : long 
    {
        OnEnter,
        OnLeave,
        OnChange,
        Always
    }
}
