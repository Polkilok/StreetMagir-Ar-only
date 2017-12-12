using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

	public float HealthCount;

	void ApplyDamage(float damage)
	{
		Debug.Log(string.Format("{0} damege receive", HealthCount));
		if ((HealthCount -= damage) <= 0f)
			Destroy(gameObject);
	}
}
