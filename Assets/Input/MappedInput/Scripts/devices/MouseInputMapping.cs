using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseInputMapping : ScriptableObject
{
	public List<ButtonMapping> buttonMappings;
	public List<AxisMapping> axisMappings;

	public ButtonMapping GetButtonMapping(MappedButton target)
	{
		for (int i = 0; i < buttonMappings.Count; i++)
		{
			if( buttonMappings[i].target == target )
				return buttonMappings[i];
		}

		return null;
	}
	public AxisMapping GetAxisMapping(MappedAxis target)
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
		public int mouseButtonId;
	}

	[System.Serializable]
	public class AxisMapping
	{
		public MappedAxis target;
		public int mouseAxisId;
	}
}	