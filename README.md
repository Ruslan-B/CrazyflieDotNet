# CrazyflieDotNet
Dot.NET libraries written in C# for Crazyflie Quadcopters and Crazyradios.

This is fork of https://github.com/ckarcz/CrazyflieDotNet.
The with different implemenation of TransferProtocol API.  

*Please be advice work still in progress!*

# Change log
- Initial logging support - done
- Prameters API - in progress
- Logging packets observable - not started yet.

# About The CrazyFlie
The [Crazyflie](https://www.bitcraze.io/crazyflie/) is a tiny open source picocopter/quadcopter that began as a side project by a bunch of engineers and grew to great internet acclaim via a [HackADay posting in 2011](http://hackaday.com/2011/04/29/mini-quadrocopter-is-crazy-awesome).

A lot of hard and a couple years later, they were able to bring their Crazyflie open source quadcopter into mass production. 

[Check out their site here](http://www.bitcraze.se/).

* [Crazyflie Wiki](http://wiki.bitcraze.se/projects:crazyflie:index)

* [Crazyflie Documentation](http://wiki.bitcraze.se/doc:crazyflie:index)

* [Crazyradio Wiki](http://wiki.bitcraze.se/projects:crazyradio:index)

* [Crazyradio Documentation](https://wiki.bitcraze.io/doc:crazyradio:index)

## Windows

### Dev Environment:
1. Windows OS.
2. Visual Studio (with NuGet installed).
3. GitHub Windows client: https://windows.github.com/
4. Git for Windows libraries and tools: http://msysgit.github.io/
5. Open the GitHub Windows client and clone this repository to a folder specifically used for your GitHub clones/repos. (ex: C:\Dev\GitHub\...)
5. Open Visual Studio.
6. Tools > Options > Source Control > Microsoft Git Provider.
7. File > Open > Open From Source Control.
8. In Team Explorer Window: Local Git Repositories > Add. Browse to your GitHub repos folder (ex: C:\Dev\GitHub\).
9. Team Explorer should load all found git repos within that folder.
10. Double click this repo. Now you should see the solution(s) listed, which you can double click to open.
11. Go to Tools > Options > Text Editor > C# > Tabs > USE TABS!
12. If you use ReSharper, please load the shared dot settings file and use that for the solution! Use the provided clean to clean your files! Keep the code style the same! Once in a while the MASTER branch will be cleaned to ensure consistensy in style. Finally, please comment your code as done in already submitted files!

### Dev Standards:
1. Please attempt to follow the coding style throughout the solution including tabbing, spacing, and commenting.
2. ReSharper is recommended. Please use the supplied DotSettings to adhere to formatting/standards and use code cleanup regularly.

### OS Driver:
1. Get the Zadig USB Tool: http://zadig.akeo.ie/
2. Run the Zadig executable.
2. In the drop down, select the "Crazyradio USB Dongle".
3. Select "libusb-win32 (vx.x.x.x)" as the driver to use for the device.
4. Note the USB IDs. The default, assumed by this library, are: 1915 (vendor ID) and 7777 (product ID).
5. Click Install Driver.
6. Open Device Management (devmgmt.msc in run box).
7. Navigate to "libusb-win32 devices" to ensure that "Crazyradio USB Dongle" is listed.

# License
This work is licensed under:
[LGPL v3](https://www.gnu.org/licenses/lgpl-3.0.en.html)
Copyright (c) [2016] [Ruslan Balanukhin] 

With extra permission to backport or integrate any part of this project under original license to https://github.com/ckarcz/CrazyflieDotNet.

#[Original License](license.txt)
