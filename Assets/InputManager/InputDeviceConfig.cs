using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputDeviceConfig : MonoBehaviour
{
	public bool windowsOnly = false; //use this for OS-specific config
	public bool macOnly = false; //use this for OS-specific config
	public bool isJoystick = false; // uses joystick axes & buttons

	// all joystick names that should use this config are in this list
	// known problem: on mac the name of a xbox controller (non-wireless) is an empty string
	public List<string> joystickNames; 

	// sensitivity of this device config (input manager sensitivity is overriden)
	public float sensitivity = 1;

	// axis names
	// # gets replaced by the actual joytick id of the device if isJoystick is on, 
	// e.g. "joystick # Horizontal" > "joystick 0 Horizontal"
	public string horizontalAxis = "Keyboard Horizontal";
	public string verticalAxis = "Keyboard Vertical";

	// configurable buttons
	public string accelerateButton = "up";
	public string brakeButton = "down";
	public string flyButton = "space";
	public string respawnButton = "tab";
	public string actionButton = "left ctrl";
	public string cameraButton = "";
	// add more buttons here:
	// public string newbutton = "defaultkey";

	public string GetButton(ButtonType buttonType)
	{

		if( buttonType == ButtonType.Fly )
			return flyButton;
		
		if( buttonType == ButtonType.Camera )
			return cameraButton;
		
		if( buttonType == ButtonType.Action )
			return actionButton;
		
		if( buttonType == ButtonType.Respawn )
			return respawnButton;
		
		if( buttonType == ButtonType.Accelerate )
			return accelerateButton;
		
		if( buttonType == ButtonType.Brake )
			return brakeButton;

		return "";
	}

	public string GetAxis(AxisType axisType)
	{
		if( axisType == AxisType.Horizontal )
			return horizontalAxis;
		
		if( axisType == AxisType.Vertical )
			return verticalAxis;

		return "";
	}
}

[System.Flags]
public enum ButtonType
{
	None = 0,
	Fly = 1,
	Respawn = 2,
	Action = 4,
	Camera = 8,
	Accelerate = 16,
	Brake = 32
}

public enum AxisType
{
	Horizontal,
	Vertical
}

/*
X-Box360 Controller mapping (mac)

D-pad up: joystick button 5
D-pad down: joystick button 6
D-pad left: joystick button 7
D-pad right: joystick button 8
start: joystick button 9
back: joystick button 10
left stick(click): joystick button 11
right stick(click): joystick button 12
left bumper: joystick button 13
right bumper: joystick button 14
center("x") button: joystick button 15
A: joystick button 16
B: joystick button 17
X: joystick button 18
Y: joystick button 19

PS3 Controller mapping (mac)

Left stick X: X axis
Left stick Y: Y axis
Right stick X: 3rd axis
Right stick Y: 4th axis
Up: button 4
Right: 5
Down: 6
Left: 7
Triangle: 12
Circle: 13
X: 14
Square: 15
L1: 10
L2: 8
L3: 1
R1: 11
R2: 9
R3: 2
Start: 0
Select: 3 

*/