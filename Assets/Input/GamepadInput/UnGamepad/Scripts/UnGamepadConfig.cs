using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class UnGamepadConfig : ScriptableObject
{	
	public enum InputTarget
	{
		Action1,
		Action2,
		Action3,
		Action4,
		Menu,
		Start,
		Back,
		DpadLeft,
		DpadUp,
		DpadRight,
		DpadDown,
		LeftBumper,
		RightBumper,
		LeftStickButton,
		RightStickButton,
		LeftStickX,
		LeftStickY,
		RightStickX,
		RightStickY,
		LeftTrigger,
		RightTrigger
	}

	public enum InputType
	{
		Axis,
		Button
	}


	[System.Serializable]
	public class InputMapping
	{
		public InputTarget target;
		public InputType type = InputType.Axis;
		public int inputId = -1;
		public float inputMin = 0;
		public float inputMax = 1;

		public override string ToString ()
		{
			return string.Format ("[InputMapping] {0} {1} {2} {3} {4}", target, type, inputId, inputMin, inputMax);
		}
	}

	public List<UnGamepadPlatform> restrictPlatforms = new List<UnGamepadPlatform> (); //use this for OS-specific config
	public string[] joystickNames = new string[0];
	public string displayName = "";
	public GamepadLayout layout = null;
	public float deadZone = 0.05f;
	public UnGamepadConfig.InputMapping[] inputMappings;

	public UnGamepadConfig ()
	{
		int[] buttonValues = (int[])System.Enum.GetValues (typeof(UnGamepadConfig.InputTarget));
		inputMappings = new UnGamepadConfig.InputMapping[buttonValues.Length];
		for (int i = 0; i < inputMappings.Length; i++)
		{
			inputMappings [i] = new UnGamepadConfig.InputMapping ();
			inputMappings [i].target = (UnGamepadConfig.InputTarget)buttonValues [i];
		}
	}

	public UnGamepadConfig.InputMapping GetInputMapping (UnGamepadConfig.InputTarget target)
	{
		for (int i = 0; i < inputMappings.Length; i++)
		{
			if (inputMappings [i].target == target)
				return inputMappings [i];
		}
		return null;
	}

	public bool IsCompatibleTo (string joystickName)
	{
		for (int j = 0; j < joystickNames.Length; j++)
		{
			Regex regex = new Regex (joystickNames [j], RegexOptions.IgnoreCase);
			if (regex.IsMatch (joystickName))
				return true;
		}
		return false;
	}

	public bool IsAvailableOnCurrentPlatform ()
	{
		if (restrictPlatforms.Count == 0)
			return true;

		if (Application.platform.ToString ().Contains ("Windows") && restrictPlatforms.Contains (UnGamepadPlatform.Windows))
			return true;

		if (Application.platform.ToString ().Contains ("OSX") && restrictPlatforms.Contains (UnGamepadPlatform.Mac))
			return true;

		if (Application.platform.ToString ().Contains ("Linux") && restrictPlatforms.Contains (UnGamepadPlatform.Linux))
			return true;

		return false;
	}
}