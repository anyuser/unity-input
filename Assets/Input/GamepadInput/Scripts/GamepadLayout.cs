using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GamepadLayout : ScriptableObject
{
	[System.Serializable]
	public class AxisNameMapping
	{
		public GamepadAxis axis;
		public string name;
	}

	[System.Serializable]
	public class ButtonNameMapping
	{
		public GamepadButton button;
		public string name;
	}

	public List<ButtonNameMapping> buttonNames = new List<ButtonNameMapping> ();
	public List<AxisNameMapping> axisNames = new List<AxisNameMapping> ();

	public string GetButtonName (GamepadButton button)
	{
		for (int i = 0; i < buttonNames.Count; i++)
		{
			if (buttonNames [i].button == button)
				return buttonNames [i].name;
		}

		return button.ToString ();
	}

	public string GetAxisName (GamepadAxis axis)
	{
		for (int i = 0; i < axisNames.Count; i++)
		{
			if (axisNames [i].axis == axis)
				return axisNames [i].name;
		}

		return axis.ToString ();
	}
}