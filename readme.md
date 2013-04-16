

# UNNESSECARY INPUT MANAGER 0.0.0.0.0.0.1
for unity 4 (probably also 3.5)
by Mario von Rickenbach
www.mariov.ch


## WHY? 

Because the unity input manager is stupid.

This code was initially written for the game Krautscape (www.krautscape.net) to support multiple gamepads on different operating systems. It was not intended to be distributed as a package so there are still some Krautscape-specific things inside (like button names etc.). But if anyone can use this or parts of it, feel free to use it in your projects!


## FEATURES

- Unplug & Plug new controllers while playing
- Different device configs for different Operating systems
- Always get input from the last active device (optional)
- Four gamepads & two keyboard configurations are predefined (for more gamepads just add more axes with ids in the input manager)


## INSTALLATION

copy the InputManager.asset.renameit file to your ProjectSettings directory (rename it to inputManager.asset)


## ADDING A NEW BUTTON/AXIS

You need to add your buttons in the file InputDeviceConfig:

1. Add a variable with the button/axis name to the config class e.g. 

    public string newButton = "space";
    // or
    public string newAxis = "Joystick # Horizontal"; 
    
    // # will be replaced by joystick number
    // the name of the axis is the unity inputmanager name

2. Add the button/axis to the ButtonType or AxisType enum
3. Add the button/axis to the GetButton or GetAxis function
4. (only when adding a new axis) add the axis to the input manager


## GET INPUT

you can get input from the last active input device like this:

    InputManager.activeDevice.GetButton(ButtonType.Accelerate);
    InputManager.activeDevice.GetAxis(AxisType.Horizontal);

you can get input from other devices like this:

    InputManager.inputDevices[deviceId].GetButton(ButtonType.Accelerate);



	