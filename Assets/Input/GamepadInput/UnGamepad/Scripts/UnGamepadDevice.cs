
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
#define TRIGGER_HACK
#endif

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnGamepadDevice : GamepadDevice
{
	public UnGamepadConfig config;
	UnGamepadState lastState = new UnGamepadState ();
	UnGamepadState currentState = new UnGamepadState ();

	public override GamepadLayout layout {
		get {
			return config.layout;
		}
	}
		
	#if TRIGGER_HACK
	List<UnGamepadConfig.InputTarget> hasTriggerBeenMovedOnce = new List<UnGamepadConfig.InputTarget> (); // hack to fix trigger inputs before they were pressed once
	#endif

	public override string systemName {
		get {
			if (Input.GetJoystickNames ().Length > deviceId)
				return Input.GetJoystickNames () [deviceId - 1];
			return "";
		}
	}

	public override string displayName {
		get {
			return string.Format ("{0} {1}", config.displayName, deviceId);
		}
	}

	#region cache
	static int cacheMaxDevices = 8;
	static Dictionary<int,KeyCode> keyCodeCached = new Dictionary<int, KeyCode>();
	public static KeyCode GetKeyCode(int deviceId, int buttonId)
	{
		int uid = buttonId * cacheMaxDevices + deviceId;
		if (!keyCodeCached.ContainsKey (uid))
			keyCodeCached [uid] = (KeyCode) System.Enum.Parse (typeof(KeyCode), string.Format ("Joystick{0}Button{1}", deviceId, buttonId));

		return keyCodeCached [uid];
	}

	static Dictionary<int,string> axisStringCached = new Dictionary<int, string> ();
	public static string GetAxisString(int deviceId, int axisId)
	{
		int uid = axisId * cacheMaxDevices + deviceId;
		if (!axisStringCached.ContainsKey (uid))
			axisStringCached [uid] = string.Format ("joystick {0} axis {1}", deviceId,axisId);

		return axisStringCached [uid];
	}
	#endregion

	public override void Update ()
	{		
		// update state
		lastState.CopyFrom( currentState);

		currentState.timestamp = Time.time;
			
		// update input
		for (int i = 0; i < UnGamepadState.allInputTargetValues.Count; i++)
		{
			UnGamepadConfig.InputTarget input = UnGamepadState.allInputTargetValues[i];
			UnGamepadConfig.InputMapping mapping = config.GetInputMapping (input);
			float currentValRaw;
			if (mapping.type == UnGamepadConfig.InputType.Button)
			{
				currentValRaw = Input.GetKey (GetKeyCode(deviceId, mapping.inputId)) ? 1 : 0;
			}
			else
			{
				currentValRaw = Input.GetAxisRaw (GetAxisString(deviceId,mapping.inputId));
					
				#if TRIGGER_HACK
					// trigger hack on osx
				if(input == UnGamepadConfig.InputTarget.LeftTrigger ||
						input == UnGamepadConfig.InputTarget.RightTrigger )
					{
						if (!hasTriggerBeenMovedOnce.Contains (input)) // check if we receive valid values
						{
							if (!Mathf.Approximately (currentValRaw, 0f) )
							{
								hasTriggerBeenMovedOnce.Add (input);
							}
						}
						
						if (!hasTriggerBeenMovedOnce.Contains (input))
						{
							currentValRaw = mapping.inputMin;
						}
					}
				#endif
			}


			float normalizedVal = Mathf.InverseLerp (mapping.inputMin, mapping.inputMax, currentValRaw);

			currentState.SetInputNormalized (input, normalizedVal);
		}

		// check if state has changed
		if (StateChanged (lastState, currentState))
			lastInputTime = Time.time;
	}
		
	bool StateChanged (UnGamepadState s1, UnGamepadState s2)
	{
		bool valueChanged = false;
		for (int i = 0; i < s1.values.Length; i++)
		{
			valueChanged |= Mathf.Abs (s1.values [i] - s2.values [i]) / Time.deltaTime > 6f;
		}

		return valueChanged;
	}
		
	public override bool GetButton (GamepadButton buttonType)
	{
		return currentState.GetButton (buttonType);
	}

	public override bool GetButtonDown (GamepadButton buttonType)
	{
		return currentState.GetButton (buttonType) && !lastState.GetButton (buttonType);
	}
		
	public override bool GetButtonUp (GamepadButton buttonType)
	{
		return !currentState.GetButton (buttonType) && lastState.GetButton (buttonType);
	}
		
	public override float GetAxis (GamepadAxis axis)
	{
		float value = currentState.GetAxis (axis);
		return Mathf.InverseLerp (config.deadZone, 1, Mathf.Abs (value)) * Mathf.Sign (value);
	}

	public override float GetTrigger (GamepadTrigger trigger)
	{
		return currentState.GetTrigger (trigger);
		
	}
}