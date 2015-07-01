using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

[RequireComponent(typeof(GamepadInput))]
public class MappedInput : MonoBehaviour {

	public float lastInputTime {get; private set;}

	public static MappedInput instance {
		get; private set;
	}

	public static bool useDisabledDevices;

	static InputDevice _activeDevice;
	public static InputDevice activeDevice
	{
		get{
			return _activeDevice;
		}
		set{
			if( _activeDevice == value )
				return;

			_activeDevice = value;
			if( OnActiveDeviceChanged != null )
				OnActiveDeviceChanged(_activeDevice);
		}
	}

	GamepadInput _gamepadInput;

	public GamepadInput gamepadInput {
		get {
			if (!_gamepadInput)
				_gamepadInput = GetComponent<GamepadInput> ();
			return _gamepadInput;
		}
	}

	public static System.Action<InputDevice> OnDeviceAdded;
	public static System.Action<InputDevice> OnDeviceRemoved;

	public static List<InputDevice> inputDevices = new List<InputDevice>();

	public GamepadInputMapping gamepadInputMapping;
	public KeyboardInputMapping keyboardInputMapping;
	public MouseInputMapping mouseInputMapping;

	public static System.Action<InputDevice> OnActiveDeviceChanged;

	void Awake()
	{
#if UNITY_STANDALONE || UNITY_EDITOR

		if( mouseInputMapping != null)
			AddMouseDevice ();

		if( keyboardInputMapping != null)
			AddKeyboardDevice ();
		
#elif UNITY_IOS || UNITY_ANDROID
		AddMouseDevice ();
#endif

		instance = this;
	}

	void Start()
	{
		for (int i = 0; i < gamepadInput.gamepads.Count; i++)
		{
			OnGamepadAdded (gamepadInput .gamepads[i]);
		}

		gamepadInput.OnGamepadAdded += OnGamepadAdded;
		gamepadInput.OnGamepadRemoved += OnGamepadRemoved;
	}

	void OnDestroy()
	{
		gamepadInput.OnGamepadAdded -= OnGamepadAdded;
		gamepadInput.OnGamepadRemoved -= OnGamepadRemoved;
	}

	void Update()
	{
		for (int i = 0; i < inputDevices.Count; i++)
		{
			if (!inputDevices [i].autoActive && !useDisabledDevices)
				continue;
			if (!activeDevice || inputDevices [i].lastInputTime > activeDevice.lastInputTime)
				activeDevice = inputDevices [i];
			lastInputTime = Mathf.Max (inputDevices [i].lastInputTime, lastInputTime);
		}
	}
		
	void OnGamepadAdded(GamepadDevice gamepad)
	{
		GameObject obj = new GameObject ();
		var device = obj.AddComponent<GamepadInputDevice> ();
		(device as GamepadInputDevice).gamepad = gamepad;
		obj.transform.parent = transform;

		obj.name = "Device: "+ gamepad.displayName;
		inputDevices.Add (device);
		
		if( OnDeviceAdded != null )
			OnDeviceAdded(device);
	}

	void OnGamepadRemoved(GamepadDevice gamepad)
	{
		for(int i = 0; i < inputDevices.Count; i++)
		{
			if( inputDevices[i] is GamepadInputDevice && (inputDevices[i] as GamepadInputDevice).gamepad == gamepad)
			{
				var device = inputDevices[i];
				inputDevices.Remove (device);

				if( OnDeviceRemoved != null )
					OnDeviceRemoved(device);
				Destroy(device.gameObject);
			}
		}
	}

	void AddKeyboardDevice()
	{
		GameObject obj = new GameObject ("Keyboard",typeof(KeyboardInputDevice));
		obj.transform.parent = transform;
		InputDevice device = obj.GetComponent<KeyboardInputDevice> ();
		inputDevices.Add(device);
	}

	void AddMouseDevice()
	{
		GameObject obj = new GameObject ("Mouse",typeof(MouseInputDevice));
		obj.transform.parent = transform;
		InputDevice device = obj.GetComponent<MouseInputDevice> ();
		inputDevices.Add(device);
	}
}