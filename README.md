The Xamarin SDK is a wrapper around the Android SDK, and in the near future the iOS SDK, and kit libraries. The interface between the two platforms will be identical.

Please refer to the Readme.md in Library.Interface for usage of the SDK, and look at the BindingTest library for an example/reference application.

## Structure

The Xamarin SDK is composed of (common) library interface, platform libraries, and dependency libraries.

## Installation
 
In the future we will support Nuget for easy installation, for now to install you will need to copy over the following libraries to use the Android Xamarin SDK:
- Library.Interface - common library interface for the two platforms (Android/iOS)
- Library.Android - library wrapper of the Android SDK (implementation of Library interface)
- Binding.Android - binding library of Android Java SDK 
- Binding-Duktape-Android - binding library of Android Duktape
- Binding-Room-Runtime - binding library of Android Room Runtime
- Binding-Arch-Persistence-DB - binding library of Android Architecture Persistence DB
- Binding-Arch-Persicence-DB-Framework - binding library of Android Architecture Persistence Framework DB
- Binding-Room-RxJava - binding library of Android Archicetcure Persistence Framework DB

Also add the following Nuget dependencies:
- Xamarin.Android.Support.Annotations
- Xamarin.Android.Arch.Core.Common
- Xamarin.Android.Arch.Lifecycle.Common
- Xamarin.Android.Arch.Lifecycle.Runtime
- Xamarin.Android.Support.Compat
- Xamarin.Android.Arch.Core.Runtime
