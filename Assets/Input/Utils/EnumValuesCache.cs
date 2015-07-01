using System;
using UnityEngine;

public class EnumValuesCache<T> where T : struct, IConvertible
{

	T[] values;

	public T this[int i]
	{
		get{
			return values [i];
		}
	}
	public int Count
	{
		get{ 
			return values.Length;
		}
	}

	public EnumValuesCache()
	{
		if (!typeof(T).IsEnum) 
		{
			throw new ArgumentException("T must be an enumerated type");
		}

		Array array = Enum.GetValues (typeof(T));
		values = new T[array.Length];
		for (int i = 0; i < array.Length; i++)
			values [i] = (T)array.GetValue (i);
	}
}
