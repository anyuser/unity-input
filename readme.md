

# UNNECESSARY INPUT MANAGER 0.2.0
for Unity 5
by Mario von Rickenbach  
www.mariov.ch


## WHY? 

Because the unity input manager is stupid.

The goal was to make an input manager which allows you to add different configurations for different type of controllers, so it works on Windows and Mac, with Keyboard and different gamepads mixed (like ps3 or xbox) at the same time, no matter which one you plug in first.

This code was initially written for the game Krautscape (www.krautscape.net) and Drei (www.etter.co/drei). If you can use this or parts of it, feel free to use it in your projects! 


## FEATURES

- Unified gamepad input. 
- Different device configs for different controller types and operating systems. 
- Uses XInput.NET for gamepads on windows, the normal unity input system on Mac and Linux
- Input mapping configurations for gamepads, keyboard and mouse. 
- Map arbitrary button and axis names to different devices
- no documentation

## UNITY CONTROLLER MAPPING
### XBOX 360 CONTROLLER (MAC)

    D-pad up: button 5
    D-pad down: button 6
    D-pad left: button 7
    D-pad right: button 8
    start: button 9
    back: button 10
    left stick(click): button 11
    right stick(click): button 12
    left bumper: button 13
    right bumper: button 14
    center("x") button: button 15
    A: button 16
    B: button 17
    X: button 18
    Y: button 19

### PS3 CONTROLLER (MAC)

    Left stick X: X axis
    Left stick Y: Y axis
    Right stick X: 3rd axis
    Right stick Y: 4th axis
    Up: button 4
    Right: button 5
    Down: button 6
    Left: button 7
    Triangle: button 12
    Circle: button 13
    X: button 14
    Square: button 15
    L1: button 10
    L2: button 8
    L3: button 1
    R1: button 11
    R2: button 9
    R3: button 2
    Start: button 0
    Select: button 3 
	