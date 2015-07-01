using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GamepadInputMapping : ScriptableObject
{	
	public List<ButtonMapping> buttonMappings = new List<ButtonMapping> ();
	public List<AxisMapping> axisMappings = new List<AxisMapping> ();

	public ButtonMapping GetGamepadButtonMapping(MappedButton target)
	{
		for (int i = 0; i < buttonMappings.Count; i++)
		{
			if( buttonMappings[i].target == target )
				return buttonMappings[i];
		}
		
		return null;
	}
	public AxisMapping GetGamepadAxisMapping(MappedAxis target)
	{
		for (int i = 0; i < axisMappings.Count; i++)
		{
			if( axisMappings[i].target == target )
				return axisMappings[i];
		}
		
		return null;
	}

	[System.Serializable]
	public class ButtonMapping
	{
		public MappedButton target;
		public GamepadButton[] buttons;
	}


	[System.Serializable]
	public class AxisMapping
	{
		public MappedAxis target;
		public GamepadAxis[] axes;
		public GamepadButton[] buttonsPositive;
		public GamepadButton[] buttonsNegative;
		public bool inverted;
	}
}