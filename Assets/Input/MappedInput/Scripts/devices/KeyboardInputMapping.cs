using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyboardInputMapping : ScriptableObject
{
	public List<ButtonMapping> keyboardButtonMapping = new List<ButtonMapping> ();
	public List<AxisMapping> keyboardAxisMapping = new List<AxisMapping> ();

	public ButtonMapping GetButtonMapping(MappedButton target)
	{
		for (int i = 0; i < keyboardButtonMapping.Count; i++)
		{
			if( keyboardButtonMapping[i].target == target )
				return keyboardButtonMapping[i];
		}
		
		return null;
	}
	public AxisMapping GetAxisMapping(MappedAxis target)
	{
		for (int i = 0; i < keyboardAxisMapping.Count; i++)
		{
			if( keyboardAxisMapping[i].target == target )
				return keyboardAxisMapping[i];
		}
		
		return null;
	}


	[System.Serializable]
	public class ButtonMapping
	{
		public MappedButton target;
		public KeyCode[] buttons;
	}

	[System.Serializable]
	public class AxisMapping
	{
		public MappedAxis target;
		public KeyCode[] buttonsPositive;
		public KeyCode[] buttonsNegative;
	}
}

