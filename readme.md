

# UNNESSECARY INPUT MANAGER 0.0.0.0.0.0.1
for unity 4 (probably also 3.5)  
by Mario von Rickenbach  
www.mariov.ch


## WHY? 

Because the unity input manager is stupid.

The goal was to make an input manager which allows you to add different configurations for different type of controllers, so it works on Windows and Mac, with Keyboard and different Gamepads (like ps3 or xbox) at the same time, no matter which one you plug in first.

This code was initially written for the game Krautscape (www.krautscape.net). It was not intended to be distributed as a package so there are still some Krautscape-specific things inside (like button names etc.). But if anyone can use this or parts of it, feel free to use it in your projects!


## FEATURES

- Unplug & Plug new controllers while playing
- Different device configs for different Operating systems
- Always get input from the last active device (optional)
- Four gamepads & two keyboard configurations are predefined (for more gamepads just add more axes with ids in the input manager)


## INSTALLATION

If you use it in another project, copy the InputManager.asset.renameit file to your ProjectSettings directory (rename it to inputManager.asset)


## ADDING A NEW BUTTON/AXIS

You need to add your buttons in the file InputDeviceConfig:

1. Add a variable with the button/axis name to the config class e.g. 

    public string newButton = "space";
    // or
    public string newAxis = "Joystick # Horizontal"; 
    
    // # will be replaced by joystick number
    // the name of the axis is the unity inputmanager name

    Set the name 

2. Add the button/axis to the ButtonType or AxisType enum
3. Add the button/axis to the GetButton or GetAxis function
4. (only when adding a new axis) add the axis to the input manager

## CHANGE AND ADD CONFIGS

Each config is saved in a gameobject as a subobject of the input manager. You can just change the values in the inspector of existing configs. If you need a new config for a different controller type, copy an existing one and change the values. 

## GET INPUT

you can get input from the last active input device like this:

    InputManager.activeDevice.GetButton(ButtonType.Accelerate);
    InputManager.activeDevice.GetAxis(AxisType.Horizontal);

you can get input from other devices like this:

    InputManager.inputDevices[deviceId].GetButton(ButtonType.Accelerate);



	