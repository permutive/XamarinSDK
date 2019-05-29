using System;
using System.Runtime.InteropServices;
using CoreFoundation;
using Foundation;
using ObjCRuntime;

namespace PermutiveX.Xamarin.iOS.Binding
{
    [Native]
    public enum PermutiveTriggerType : long 
    {
        OnEnter,
        OnLeave,
        OnChange,
        Always
    }

    //[Native]
    //public enum PermutiveInterfaceBackendType : long 
    //{
    //    Production,
    //    Staging
    //}
}
