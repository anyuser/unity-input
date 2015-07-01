
#if UNITY_STANDALONE_WIN
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class XGamepadDevice : GamepadDevice
{
	
	public override string systemName {
		get {
			return string.Format ("{0} {1}", "XInput Controller", deviceId);
		}
	}
	
	public override string displayName {
		get {
			return string.Format ("{0} {1}", "XInput Controller", deviceId);
		}
	}
	GamepadLayout _layout;
	public override GamepadLayout layout {
		get {
			return _layout;
		}
	}

	GamePadState state;
	GamePadState prevState;

	public XGamepadDevice(GamepadLayout layout)
	{
		_layout = layout;
		Update ();
	}
	
	public override void Update ()
	{	
		prevState = state;
		state = GamePad.GetState ((PlayerIndex)deviceId);

		if( StateChanged(prevState,state))
			lastInputTime = Time.time;
	}

	float thresholdVelocity = 6;
	bool StateChanged (GamePadState s1, GamePadState s2)
	{
		bool changed = false;
		changed |= s1.Buttons.A != s2.Buttons.A;
		changed |= s1.Buttons.B != s2.Buttons.B;
		changed |= s1.Buttons.X != s2.Buttons.X;
		changed |= s1.Buttons.Y != s2.Buttons.Y;
		changed |= s1.Buttons.Start != s2.Buttons.Start;
		changed |= s1.Buttons.Back != s2.Buttons.Back;
		changed |= s1.Buttons.LeftShoulder != s2.Buttons.LeftShoulder;
		changed |= s1.Buttons.RightShoulder != s2.Buttons.RightShoulder;
		changed |= s1.Buttons.LeftStick != s2.Buttons.LeftStick;
		changed |= s1.Buttons.RightStick != s2.Buttons.RightStick;
		changed |= s1.DPad.Up != s2.DPad.Up;
		changed |= s1.DPad.Down != s2.DPad.Down;
		changed |= s1.DPad.Left != s2.DPad.Left;
		changed |= s1.DPad.Right != s2.DPad.Right;
		changed |= Mathf.Abs (s1.Triggers.Left - s2.Triggers.Left) / Time.deltaTime > thresholdVelocity;
		changed |= Mathf.Abs (s1.Triggers.Right - s2.Triggers.Right) / Time.deltaTime > thresholdVelocity;
		changed |= Mathf.Abs (s1.ThumbSticks.Left.X - s2.ThumbSticks.Left.X) / Time.deltaTime > thresholdVelocity;
		changed |= Mathf.Abs (s1.ThumbSticks.Left.Y - s2.ThumbSticks.Left.Y) / Time.deltaTime > thresholdVelocity;
		changed |= Mathf.Abs (s1.ThumbSticks.Right.X - s2.ThumbSticks.Right.X) / Time.deltaTime > thresholdVelocity;
		changed |= Mathf.Abs (s1.ThumbSticks.Right.Y - s2.ThumbSticks.Right.Y) / Time.deltaTime > thresholdVelocity;
		
		return changed;
	}
	
	public override bool GetButton (GamepadButton buttonType)
	{
		return GetButtonState (buttonType,state) == ButtonState.Pressed;
	}
	
	public override bool GetButtonDown (GamepadButton buttonType)
	{
		return GetButtonState (buttonType,prevState) == ButtonState.Released && GetButtonState (buttonType,state) == ButtonState.Pressed;
	}
	
	public override bool GetButtonUp (GamepadButton buttonType)
	{
		return GetButtonState (buttonType,prevState) == ButtonState.Pressed && GetButtonState (buttonType,state) == ButtonState.Released;
	}
	
	public override float GetAxis (GamepadAxis axis)
	{
		switch(axis)
		{
		case GamepadAxis.LeftStickX:
			return state.ThumbSticks.Left.X;
		case GamepadAxis.LeftStickY:
			return state.ThumbSticks.Left.Y;
		case GamepadAxis.RightStickX:
			return state.ThumbSticks.Right.X;
		case GamepadAxis.RightStickY:
			return state.ThumbSticks.Right.Y;
		case GamepadAxis.LeftTrigger:
			return GetTrigger(GamepadTrigger.Left);
		case GamepadAxis.RightTrigger:
			return GetTrigger(GamepadTrigger.Right);
		}
		return 0;
	}
		
	public override float GetTrigger (GamepadTrigger trigger)
	{
		switch(trigger)
		{
		case GamepadTrigger.Left:
			return state.Triggers.Left;
		case GamepadTrigger.Right:
			return state.Triggers.Right;
		}
		return 0;
	}

	public ButtonState GetButtonState(GamepadButton button, GamePadState state)
	{
		switch (button)
		{
		case GamepadButton.Action1:
			return state.Buttons.A;
		case GamepadButton.Action2:
			return state.Buttons.B;
		case GamepadButton.Action3:
			return state.Buttons.X;
		case GamepadButton.Action4:
			return state.Buttons.Y;
		case GamepadButton.Menu:
			return ButtonState.Released;
		case GamepadButton.Start:
			return state.Buttons.Start;
		case GamepadButton.Back:
			return state.Buttons.Back;
		case GamepadButton.DpadDown:
			return state.DPad.Down;
		case GamepadButton.DpadLeft:
			return state.DPad.Left;
		case GamepadButton.DpadRight:
			return state.DPad.Right;
		case GamepadButton.DpadUp:
			return state.DPad.Up;
		case GamepadButton.LeftBumper:
			return state.Buttons.LeftShoulder;
		case GamepadButton.RightBumper:
			return state.Buttons.RightShoulder;
		case GamepadButton.LeftStickButton:
			return state.Buttons.LeftStick;
		case GamepadButton.RightStickButton:
			return state.Buttons.RightStick;
		}
		
		throw new UnityException ();
	}
}

#endif