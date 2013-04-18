using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {
		
	public float lastInputTime {get; private set;}
	
	InputDeviceConfig[] inputDeviceConfigs;
	public static InputDevice activeDevice;
	public static List<InputDevice> inputDevices = new List<InputDevice>();
	
	bool keyboardInitialized = false;
	int initalizedJoystickCount = 0;
	
	void Start()
	{
		inputDeviceConfigs = GetComponentsInChildren<InputDeviceConfig>();
		
		RefreshDevices();
	}
	
	void RefreshDevices()
	{
		if( Input.GetJoystickNames().Length > 0 )
		{
			string str = "Connected Joysticks:\n";
			for(int i = 0; i < Input.GetJoystickNames().Length; i++)
				str += "Joystick "+i+" ("+Input.GetJoystickNames()[i]+") ";
			Debug.Log(str);
		}
		else
		{
			Debug.Log("No Joysticks/Gamepads connected");
		}
				
		List<InputDeviceConfig> availableConfigs = new List<InputDeviceConfig>();
		foreach(InputDeviceConfig config in inputDeviceConfigs)
		{
			if( !SystemInfo.operatingSystem.Contains("Windows") && config.windowsOnly )
				continue;
			if( !SystemInfo.operatingSystem.Contains("OS X") && config.macOnly )
				continue;
			availableConfigs.Add(config);
		}
		
		// add new devices
		foreach(InputDeviceConfig config in availableConfigs)
		{	
			if( config.isJoystick )
			{
				RefreshJoystickDevicesWithConfig(config);
			}
			else
			{
				if( !keyboardInitialized)
					AddKeyboardDeviceWithConfig(config);
			}		
		}
		
		// remove disconnected joysticks
		for(int i = inputDevices.Count-1; i >= 0; i--)
		{
			InputDevice d = inputDevices[i];
			if( !d.config.isJoystick )
				continue;
			
			if( Input.GetJoystickNames().Length < d.deviceId ||
				!d.config.joystickNames.Contains( Input.GetJoystickNames()[ d.deviceId-1 ] ))
			{
				inputDevices.Remove(d);
				Destroy(d.gameObject);
			}
		}
		
		keyboardInitialized = true;
		activeDevice = inputDevices[0];
		
		initalizedJoystickCount = Input.GetJoystickNames().Length;
	}

	void AddKeyboardDeviceWithConfig (InputDeviceConfig config)
	{		
		GameObject deviceObj = new GameObject(config.name);
		deviceObj.transform.parent = transform;
		
		InputDevice device = deviceObj.AddComponent<InputDevice>();
		device.config = config;
		inputDevices.Add(device);
	}
	
	void RefreshJoystickDevicesWithConfig (InputDeviceConfig config)
	{	
		string[] joystickNames = Input.GetJoystickNames();
		for(int i = 0; i < joystickNames.Length; i++)
		{
			bool nameOk = false;
			for(int j = 0; j < config.joystickNames.Count; j++)
			{
				if(joystickNames[i] == config.joystickNames[j])
				{
					nameOk = true;
					break;
				}
				
			}
			if(nameOk)
			{
				foreach(InputDevice d in inputDevices)
				{
					if( d.config == config && d.deviceId-1 == i)
						return; // already existing
				}
				GameObject deviceObj = new GameObject(config.name + "(id = "+(i+1)+")");
				deviceObj.transform.parent = transform;
				
				InputDevice device = deviceObj.AddComponent<InputDevice>();
				device.config = config;
				device.deviceId = i+1;
				inputDevices.Add(device);
			}
			
		}
	}
	
	void Update()
	{
		foreach(InputDevice device in inputDevices)
		{
			if( device.lastInputTime > activeDevice.lastInputTime )
				activeDevice = device;
			
			lastInputTime = Mathf.Max(device.lastInputTime,lastInputTime);
		}
		
		if( initalizedJoystickCount != Input.GetJoystickNames().Length)
			RefreshDevices();
			
		print( activeDevice.name + " " + activeDevice.state );
	}
}