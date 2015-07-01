using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class GamepadManager
{
	public event System.Action<GamepadDevice> OnGamepadAdded;
	public event System.Action<GamepadDevice> OnGamepadRemoved;
	
	List<GamepadDevice> _gamepads = new List<GamepadDevice> ();
	public List<GamepadDevice> gamepads { 
		get{
			return _gamepads;
		} 
	}

	protected void AddDevice(GamepadDevice device)
	{
		gamepads.Add (device);
		
		//Debug.Log (string.Format ("Gamepad added: deviceId={0} systemName={1} displayName={2}", device.deviceId, device.systemName, device.displayName));

		if (OnGamepadAdded != null)
			OnGamepadAdded (device);
	}

	protected void RemoveDevice(GamepadDevice device)
	{
		gamepads.Remove (device);
		
		//Debug.Log (string.Format ("Gamepad removed: deviceId={0} systemName={1} displayName={2}", device.deviceId, device.systemName, device.displayName));
		
		if (OnGamepadRemoved != null)
			OnGamepadRemoved (device);
	}


	public abstract void Init();
	public abstract void Update();
}

