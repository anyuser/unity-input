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
	public float deadZone = 0;
	
	public List<ButtonMapping> buttonMappings = new List<ButtonMapping>();
	public List<AxisMapping> axisMappings = new List<AxisMapping>();

	public string GetButton(ButtonType buttonType)
	{
		foreach(ButtonMapping mapping in buttonMappings)
		{
			if( mapping.target == buttonType )
				return mapping.button;
		}
		return "";
	}

	public string GetAxis(AxisType axisType)
	{
		foreach(AxisMapping mapping in axisMappings)
		{
			if( mapping.target == axisType )
				return mapping.axis;
		}

		return "";
	}
	
	[System.Serializable]
	public class ButtonMapping
	{
		public ButtonType target;
		public string button;
	}
	
	[System.Serializable]
	public class AxisMapping
	{
		public AxisType target;
		public string axis;
	}
}

[System.Flags]
public enum ButtonType
{
	None = 0,
	Action1 = 1,
	Action2 = 2,
	Action3 = 4,
	Action4 = 8,
	Action5 = 16,
	Action6 = 32,
	Action7 = 64
}

public enum AxisType
{
	Horizontal,
	Vertical,
	Axis3,
	Axis4
}