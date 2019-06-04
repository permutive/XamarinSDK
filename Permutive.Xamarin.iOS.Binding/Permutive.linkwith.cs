using ObjCRuntime;

[assembly: LinkWith("Permutive.framework",
    target: LinkTarget.Arm64 | LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Simulator | LinkTarget.Simulator64,
    SmartLink = true,
    ForceLoad = true,
    LinkerFlags = "-lxml2 -ObjC",
    Frameworks = "Foundation")]


//[assembly: LinkWith("libPermutiveStatic.a", 
//target: LinkTarget.Arm64 | LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Simulator | LinkTarget.Simulator64,
//SmartLink = true, 
//ForceLoad = true,
//LinkerFlags = "-lxml2 -ObjC",
//Frameworks = "Foundation")]
//Frameworks = "Foundation CoreGraphics QuartzCore UIKit")]
