

# UNNESSECARY INPUT MANAGER 0.0.2
for unity 4 (probably also 3.5)  
by Mario von Rickenbach  
www.mariov.ch


## WHY? 

Because the unity input manager is stupid. (Don't get me wrong, unity itself is fantastic, but the input manager is just... bad.)

The goal was to make an input manager which allows you to add different configurations for different type of controllers, so it works on Windows and Mac, with Keyboard and different gamepads mixed(like ps3 or xbox) at the same time, no matter which one you plug in first.

This code was initially written for the game Krautscape (www.krautscape.net). If you can use this or parts of it, feel free to use it in your projects!


## FEATURES

- Different device configs for different controller types and operating systems
- Unplug & Plug new controllers while playing, they get initialized automatically with the right configuration
- Support for up to 4 gamepads with mixed configurations & multiple keyboard configurations
- Always get input from the last active device (for singleplayer games)


## USE IT IN ANOTHER PROJECT

If you use it in another project, copy InputManager.asset from the ProjectSettings directory to your project.


## CHANGING AND ADDING CONFIGURATIONS

Each config is saved in a gameobject as a subobject of the input manager. You can just change the values in the inspector of existing configs. If you need a new config for a different controller type, copy an existing one and change the values. 

Adding button and axis mapping works like this:

- for joystick buttons, use "button 1", "button 2" etc.
- for joystick axes, use "Joystick # X", "Joystick # Y", "Joystick # Axis 3" etc.
  these are axis names from the unity input manager, # will be replaced by joystick id
  If you need more axes or joysticks, add them to the input manager
- for keyboard buttons, use "x", "space" etc.
- for keyboard axes, use "w s" etc. (positive and negative key separated by space)

It's convenient to change button names in the ButtonType enum for easier code reading.


## GET INPUT

you can get input from the last active input device like this:

    InputManager.activeDevice.GetButton(ButtonType.Action1);
    InputManager.activeDevice.GetAxis(AxisType.Horizontal);

you can get input from other devices like this:

    InputManager.inputDevices[deviceId].GetButton(ButtonType.Action1);

## CONTROLLER MAPPING
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
	