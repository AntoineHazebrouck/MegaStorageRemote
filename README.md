# MegaStorageRemote

A compact end-to-end embedded-to-mobile project: capture raw IR pulses with simple IoT hardware (RaspberryPi), transform and import them into the app, and control the MegaStorage CD player from an Android phone - demonstrates fullstack signal-to-mobile integration.

## The why

The MegaStorage 300 CD works like a jukebox, but you need to keep track of the CDs you add yourself, with paper notes (e.g. 'Depeche Mode - Music for the Masses' at position 23). This app rids me of the paper, and also allows me to control the CD player remotely via infrared.

## The how

I chose C# .NET Android among other technlologies because : 
- it has a good support in VSCode
- infrared only exist on Android Xiaomi so I do not need cross platform (.NET MAUI)
- it mimics the native Java Android infrared API which is easy to use (no additional library) 

### Step 1 : Retroengineering the MegaStorage official remote's IR codes

Infrared codes are pulses in microseconds e.g. :

```
pulse 2410
space 595
pulse 1205
space 602
```

Using the [KY-022](https://cdn.shopify.com/s/files/1/1509/1638/files/AZ148_B6-13_EN_B089QKGRTL.pdf?v=1721116058) we can receive infrared codes from the remote. Then we can integrate them into our Android app to use the Xiaomi's infrared emitter.



```shell
ssh antoine@raspberrypi
```

```shell
sudo ir-ctl -r -d /dev/lirc0
```

Make sure to reformat the IR codes accordingly, see [this file](Code/IrCodes.cs)

### Step 2 : Testing the IR codes

I simply created the most basic UI with some buttons to reproduce the initial remote behaviour.

I first tried with simple commands (power on/off, numbers, ...). Then I started chaining commands (Three, Two, Enter, ...). From that point onwards, I knew that the project was 100% doable.

### Step 3 : Building the UI and 'industrializing'

I chose to go for a 100% code approach so that I would not bother learning complicated stuff for such basic needs. 

I did not overcomplicate thing as the perimeter of the project is small and very predictable since it is limited to the hardware capabilities. The storage is 100% local to the phone, it avoids spending money on a server.

## Features

- Power on/off
- Register/replace a CD
- Play a CD among the registered list

## Running the app

The application is specifically designed for infrared capable phones (Xiaomi). Run/debug it with the .NET MAUI IDE tech stack.