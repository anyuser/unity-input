using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UnGamepadManager : GamepadManager
{
	public UnGamepadManager(List<UnGamepadConfig> configs)
	{
		this.configs = configs;
		if (this.configs == null)
			this.configs = new List<UnGamepadConfig> ();
	}

	int initalizedJoystickCount = 0;

	public List<UnGamepadConfig> configs;

	float lastCheckTime = -1;
	float checkInterval = 1;
	
	internal UnGamepadConfig defaultConfig {
		
		get {
			for (int i = 0; i < configs.Count; i++) {

				if (configs [i].IsAvailableOnCurrentPlatform())
					return configs [i];
			}
			
			throw new UnityException ("No default joystick config found for current platform");
		}
	}
	
	public override void Init ()
	{		
		if(!Application.isEditor)
			Debug.Log (string.Format ("{0} gamepad configs loaded...", configs.Count));

		RefreshDevices ();
	}

	public override void Update ()
	{
		if (Time.time - lastCheckTime > checkInterval ) 
		{
			lastCheckTime = Time.time;

			if( initalizedJoystickCount != Input.GetJoystickNames ().Length)
				RefreshDevices ();
		}
		for (int i = 0; i < gamepads.Count; i++)
			gamepads [i].Update ();
	}
	
	void RefreshDevices ()
	{
		if (Input.GetJoystickNames ().Length > 0)
		{
			string str = "Connected Joysticks:\n";
			for (int i = 0; i < Input.GetJoystickNames().Length; i++)
				str += "Joystick " + i + " (" + Input.GetJoystickNames () [i] + ") ";
			Debug.Log (str);
		}
		else
		{
			Debug.Log ("No Joysticks/Gamepads connected");
		}
		
		// remove disconnected devices
		for (int i = gamepads.Count-1; i >= 0; i--)
		{
			RemoveIfDisconnected (gamepads [i]);
		}
		
		// add new devices
		for (int i = 0; i < configs.Count; i++) 
		{
			if (!configs [i].IsAvailableOnCurrentPlatform ())
				continue;
			
			RefreshJoystickDevicesWithConfig (configs [i]);
		}
		
		// check if everything is configured, use default config for unknown devices
		string[] joystickNames = Input.GetJoystickNames ();
		for (int i = joystickNames.Length-1; i >= 0; i--)
		{
			
			bool joystickConfigured = false;
			foreach (GamepadDevice dev in gamepads)
			{
				if (dev.deviceId == i + 1)
					joystickConfigured = true;
			}
			
			if (!joystickConfigured)
				CreateGamepad (i + 1, defaultConfig);
			
		}
		
		initalizedJoystickCount = Input.GetJoystickNames ().Length;
	}
	
	void RefreshJoystickDevicesWithConfig (UnGamepadConfig config)
	{	
		string[] joystickNames = Input.GetJoystickNames ();
		
		for (int i = 0; i < joystickNames.Length; i++)
		{
			if (config.IsCompatibleTo (joystickNames [i]))
			{
				bool alreadyExists = false;
				foreach (GamepadDevice d in gamepads)
				{
					if ((d as UnGamepadDevice).config == config && d.deviceId == i + 1)
					{
						alreadyExists = true; // already existing
						break;
					}
				}
				
				if (!alreadyExists)
				{
					CreateGamepad (i + 1, config);
				}
			}
			
		}
	}
	
	void CreateGamepad (int deviceId, UnGamepadConfig config)
	{
		UnGamepadDevice gamepad = new UnGamepadDevice ();
		gamepad.config = config;
		gamepad.deviceId = deviceId;

		AddDevice (gamepad);
	}
	
	void RemoveIfDisconnected (GamepadDevice gamepad)
	{
		UnGamepadConfig config = (UnGamepadConfig)((UnGamepadDevice)gamepad).config;
		
		if (Input.GetJoystickNames ().Length < gamepad.deviceId ||
			!config.IsCompatibleTo (Input.GetJoystickNames () [gamepad.deviceId - 1]))
		{			
			RemoveDevice(gamepad);
		}
	}
	
}
