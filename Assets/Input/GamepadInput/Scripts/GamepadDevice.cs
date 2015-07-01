using UnityEngine;
using System.Collections;

public abstract class GamepadDevice
{
	public int deviceId {get; set;}
	public float lastInputTime { get; protected set; }

	public abstract string systemName {
		get;
	}
	public abstract string displayName {
		get;
	}
	public abstract GamepadLayout layout {
		get;
	}

	public abstract void Update();

	public abstract bool GetButton (GamepadButton buttonType);	
	public abstract bool GetButtonDown (GamepadButton buttonType);	
	public abstract bool GetButtonUp (GamepadButton buttonType);	
	public abstract float GetAxis (GamepadAxis axis);
	public abstract float GetTrigger(GamepadTrigger trigger);
}

