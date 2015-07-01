using UnityEngine;
using System.Collections;

public class GamepadTestGUI : MonoBehaviour {

	GamepadInput _input;

	public GamepadInput input {
		get {
			if (!_input)
				_input = GetComponent<GamepadInput> ();
			return _input;
		}
	}

	public bool alwaysActive = false;

	// Use this for initialization
	void Start () {
	
		//PRINTInputDeviceManager ();
	}

	void PRINTInputDeviceManager()
	{
		string InputDeviceManager = "";
		for (int joyId = 0; joyId < 4; joyId++) 
		{
			for(int axisId = 0; axisId < 10; axisId++)
			{
				string str = @"  - serializedVersion: 3
    m_Name: joystick {0} axis {1}
    descriptiveName: 
    descriptiveNegativeName: 
    negativeButton: 
    positiveButton: 
    altNegativeButton: 
    altPositiveButton: 
    gravity: 0
    dead: 0
    sensitivity: 1
    snap: 0
    invert: 0
    type: 2
    axis: {2}
    joyNum: {3}"+"\n";

				InputDeviceManager += string.Format (str, joyId + 1, axisId + 1, axisId, joyId + 1);
			}
		}
		print (InputDeviceManager);
	}

	bool show = false;

	void Update()
	{
		show = Input.GetKey (KeyCode.O);
	}
	
	// Update is called once per frame
	void OnGUI () {

		if (!show && !alwaysActive)
			return;

		if (!Input.GetKey (KeyCode.LeftShift)) {
			if (input.gamepads.Count == 0)
				GUILayout.Label ("No gamepads connected");

			GUILayout.BeginHorizontal ();
			foreach (GamepadDevice gamepad in input.gamepads) {
				string str = "";
				str += "Device #" + gamepad.deviceId + "\n";
				str += "System name: " + gamepad.systemName + "\n";
				str += "Display name: " + gamepad.displayName + "\n";
				str += "\n";
				int[] buttonValues = (int[])System.Enum.GetValues (typeof(GamepadButton));
				for (int i = 0; i < buttonValues.Length; i++) {
					str += (GamepadButton)buttonValues [i] + ": " + gamepad.GetButton ((GamepadButton)buttonValues [i]) + "\n";
				}
				
				int[] axisValues = (int[])System.Enum.GetValues (typeof(GamepadAxis));
				for (int i = 0; i < axisValues.Length; i++) {
					str += (GamepadAxis)axisValues [i] + ": " + gamepad.GetAxis ((GamepadAxis)axisValues [i]) + "\n";
				}
				GUILayout.Label (str);
			}


			GUILayout.EndHorizontal ();
		} 
		else {
			GUILayout.BeginHorizontal ();
			for (int joyId = 0; joyId < 4; joyId++) 
			{
				string str = "";
				for(int axisId = 0; axisId < 10; axisId++)
				{
					string getstr = string.Format("joystick {0} axis {1}",joyId+1,axisId+1);
					str += getstr + ": "+Input.GetAxis(getstr)+"\n";
				}
				for(int buttonId = 0; buttonId < 20; buttonId++)
				{
					string getstr = string.Format("joystick {0} button {1}",joyId+1,buttonId);
					str += getstr + ": "+Input.GetKey(getstr)+"\n";
				}
				GUILayout.Label (str);
			}
			GUILayout.EndHorizontal ();
		}
	}
}
