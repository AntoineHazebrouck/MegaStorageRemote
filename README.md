# Fetching the infrared codes

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

# Running the app

The application is specifically designed for infrared capable phones.