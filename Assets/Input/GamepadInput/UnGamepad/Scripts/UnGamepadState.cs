using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnGamepadState
{
	static Dictionary<int,float> outputMax = new Dictionary<int,float> ()
	{
		{(int)UnGamepadConfig.InputTarget.Action1,1},
		{(int)UnGamepadConfig.InputTarget.Action2,1},
		{(int)UnGamepadConfig.InputTarget.Action3,1},
		{(int)UnGamepadConfig.InputTarget.Action4,1},
		{(int)UnGamepadConfig.InputTarget.Menu,1},
		{(int)UnGamepadConfig.InputTarget.Back,1},
		{(int)UnGamepadConfig.InputTarget.Start,1},
		{(int)UnGamepadConfig.InputTarget.DpadDown,1},
		{(int)UnGamepadConfig.InputTarget.DpadLeft,1},
		{(int)UnGamepadConfig.InputTarget.DpadUp,1},
		{(int)UnGamepadConfig.InputTarget.DpadRight,1},
		{(int)UnGamepadConfig.InputTarget.LeftBumper,1},
		{(int)UnGamepadConfig.InputTarget.RightBumper,1},
		{(int)UnGamepadConfig.InputTarget.LeftStickButton,1},
		{(int)UnGamepadConfig.InputTarget.RightStickButton,1},
		{(int)UnGamepadConfig.InputTarget.LeftStickX,1},
		{(int)UnGamepadConfig.InputTarget.LeftStickY,1},	
		{(int)UnGamepadConfig.InputTarget.RightStickX,1},
		{(int)UnGamepadConfig.InputTarget.RightStickY,1},
		{(int)UnGamepadConfig.InputTarget.LeftTrigger,1},
		{(int)UnGamepadConfig.InputTarget.RightTrigger,1}
	};
	static Dictionary<int,float> outputMin = new Dictionary<int,float> ()
	{
		{(int)UnGamepadConfig.InputTarget.Action1,0},
		{(int)UnGamepadConfig.InputTarget.Action2,0},
		{(int)UnGamepadConfig.InputTarget.Action3,0},
		{(int)UnGamepadConfig.InputTarget.Action4,0},
		{(int)UnGamepadConfig.InputTarget.Menu,0},
		{(int)UnGamepadConfig.InputTarget.Back,0},
		{(int)UnGamepadConfig.InputTarget.Start,0},
		{(int)UnGamepadConfig.InputTarget.DpadDown,0},
		{(int)UnGamepadConfig.InputTarget.DpadLeft,0},
		{(int)UnGamepadConfig.InputTarget.DpadUp,0},
		{(int)UnGamepadConfig.InputTarget.DpadRight,0},
		{(int)UnGamepadConfig.InputTarget.LeftBumper,0},
		{(int)UnGamepadConfig.InputTarget.RightBumper,0},
		{(int)UnGamepadConfig.InputTarget.LeftStickButton,0},
		{(int)UnGamepadConfig.InputTarget.RightStickButton,0},
		{(int)UnGamepadConfig.InputTarget.LeftStickX,-1},
		{(int)UnGamepadConfig.InputTarget.LeftStickY,-1},	
		{(int)UnGamepadConfig.InputTarget.RightStickX,-1},
		{(int)UnGamepadConfig.InputTarget.RightStickY,-1},
		{(int)UnGamepadConfig.InputTarget.LeftTrigger,0},
		{(int)UnGamepadConfig.InputTarget.RightTrigger,0}
	};

	public static EnumValuesCache<UnGamepadConfig.InputTarget> allInputTargetValues = new EnumValuesCache<UnGamepadConfig.InputTarget> ();

	public double timestamp;

	public float[] values = new float[allInputTargetValues.Count];

	public void CopyFrom(UnGamepadState state)
	{
		timestamp = state.timestamp;
		for (int i = 0; i < state.values.Length; i++) {
			values [i] = state.values [i];
		}
	}

	public bool GetButton (GamepadButton buttonType)
	{
		
		switch (buttonType)
		{
		case GamepadButton.Action1:
			return GetInputBool (UnGamepadConfig.InputTarget.Action1);
		case GamepadButton.Action2:
			return GetInputBool (UnGamepadConfig.InputTarget.Action2);
		case GamepadButton.Action3:
			return GetInputBool (UnGamepadConfig.InputTarget.Action3);
		case GamepadButton.Action4:
			return GetInputBool (UnGamepadConfig.InputTarget.Action4);
		case GamepadButton.Menu:
			return GetInputBool (UnGamepadConfig.InputTarget.Menu);
		case GamepadButton.Start:
			return GetInputBool (UnGamepadConfig.InputTarget.Start);
		case GamepadButton.Back:
			return GetInputBool (UnGamepadConfig.InputTarget.Back);
		case GamepadButton.DpadDown:
			return GetInputBool (UnGamepadConfig.InputTarget.DpadDown);
		case GamepadButton.DpadLeft:
			return GetInputBool (UnGamepadConfig.InputTarget.DpadLeft);
		case GamepadButton.DpadRight:
			return GetInputBool (UnGamepadConfig.InputTarget.DpadRight);
		case GamepadButton.DpadUp:
			return GetInputBool (UnGamepadConfig.InputTarget.DpadUp);
		case GamepadButton.LeftBumper:
			return GetInputBool (UnGamepadConfig.InputTarget.LeftBumper);
		case GamepadButton.RightBumper:
			return GetInputBool (UnGamepadConfig.InputTarget.RightBumper);
		case GamepadButton.LeftStickButton:
			return GetInputBool (UnGamepadConfig.InputTarget.LeftStickButton);
		case GamepadButton.RightStickButton:
			return GetInputBool (UnGamepadConfig.InputTarget.RightStickButton);
		}
		
		throw new UnityException ();
	}

	public float GetAxis (GamepadAxis axisType)
	{
		
		switch (axisType)
		{
		case GamepadAxis.LeftStickX:
			return GetInputValue (UnGamepadConfig.InputTarget.LeftStickX);
		case GamepadAxis.LeftStickY:
			return GetInputValue (UnGamepadConfig.InputTarget.LeftStickY);
		case GamepadAxis.RightStickX:
			return GetInputValue (UnGamepadConfig.InputTarget.RightStickX);
		case GamepadAxis.RightStickY:
			return GetInputValue (UnGamepadConfig.InputTarget.RightStickY);
		case GamepadAxis.LeftTrigger:
			return GetTrigger(GamepadTrigger.Left);
		case GamepadAxis.RightTrigger:
			return GetTrigger(GamepadTrigger.Right);
		}

		throw new UnityException ();
	}

	public float GetTrigger (GamepadTrigger triggerType)
	{
		switch (triggerType)
		{
		case GamepadTrigger.Left:
			return GetInputValue (UnGamepadConfig.InputTarget.LeftTrigger);
		case GamepadTrigger.Right:
			return GetInputValue (UnGamepadConfig.InputTarget.RightTrigger);
		}
		
		throw new UnityException ();
	}

	bool GetInputBool (UnGamepadConfig.InputTarget input)
	{		
		return values [(int)input] == 1;
	}

	float GetInputValue (UnGamepadConfig.InputTarget input)
	{
		return values [(int)input];
	}
	
	public void SetInputNormalized (UnGamepadConfig.InputTarget input, float valNormalized)
	{
		values [(int)input] = Mathf.Lerp (outputMin [(int)input], outputMax [(int)input], valNormalized); // map to output values
	}
}
