using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GamepadInputDevice : InputDevice
{	
	public GamepadDevice gamepad;

	public override string GetButtonName(MappedButton button)
	{
		var mapping = MappedInput.instance.gamepadInputMapping.GetGamepadButtonMapping (button);
		if( mapping != null )
		{
			return gamepad.layout.GetButtonName(mapping.buttons[0]);
		}

		return "";
	}

	public override string GetAxisName(MappedAxis axis)
	{
		var mapping = MappedInput.instance.gamepadInputMapping.GetGamepadAxisMapping (axis);
		if( mapping != null )
		{
			return gamepad.layout.GetAxisName(mapping.axes[0]);
		}

		return "";
	}

	public override bool GetButton(MappedButton button)
	{
		var mapping = MappedInput.instance.gamepadInputMapping.GetGamepadButtonMapping (button);
		if (mapping != null)
		{
			for (int i = 0; i < mapping.buttons.Length; i++)
			{
				if (gamepad.GetButton (mapping.buttons [i]))
					return true;
			}
		}
		return false;
	}

	public override bool GetButtonDown(MappedButton button)
	{
		var mapping = MappedInput.instance.gamepadInputMapping.GetGamepadButtonMapping (button);
		if (mapping != null)
		{
			for (int i = 0; i < mapping.buttons.Length; i++)
			{
				if (gamepad.GetButtonDown (mapping.buttons [i]))
					return true;
			}
		}

		return false;
	}

	public override bool GetButtonUp(MappedButton button)
	{
		var mapping = MappedInput.instance.gamepadInputMapping.GetGamepadButtonMapping (button);
		if( mapping != null )
		{
			for(int i = 0; i < mapping.buttons.Length; i++)
			{
				if( gamepad.GetButtonUp(mapping.buttons[i]) )
					return true;
			}
		}

		return false;
	}

	protected override float GetAxisValueRaw (MappedAxis axis)
	{
		float rawVal = 0;
		var mapping = MappedInput.instance.gamepadInputMapping.GetGamepadAxisMapping (axis);
		if (mapping == null)
		{
			Debug.LogWarningFormat ("Axis {0} is not mapped on device {1}", axis, this);
			return 0;
		}

		for (int i = 0; i < mapping.axes.Length; i++)
		{
			rawVal += gamepad.GetAxis (mapping.axes [i]);
		}

		for (int i = 0; i < mapping.buttonsPositive.Length; i++)
		{
			if (gamepad.GetButton (mapping.buttonsPositive [i]))
				rawVal += 1;
		}


		for (int i = 0; i < mapping.buttonsNegative.Length; i++)
		{
			if (gamepad.GetButton (mapping.buttonsNegative [i]))
				rawVal -= 1;
		}


		if (mapping.inverted)
			rawVal = -rawVal;

		return Mathf.Clamp( rawVal,-1,1);
	}
}