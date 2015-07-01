using UnityEngine;
using System.Collections;

[System.Flags]
public enum UnGamepadPlatform
{
	All = 0,
	Windows = 1 << 0,
	Mac = 1 << 1,
	Linux = 1 << 2,
	Touch = 1 << 3
}
