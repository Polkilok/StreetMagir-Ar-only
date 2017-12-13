using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletMove : MonoBehaviour
{

	public float TimeToDestruct;
	public bool RandomDamage;
	public float Damage;
	public float MinRandLimit;
	public float MaxRandLimit;
	public int StartSpeed;
	public GameObject ParticleHit;

	private Vector3 _previousStep;

	void Awake()
	{
		DestroyObject(gameObject, TimeToDestruct);

		var rgb = GetComponent<Rigidbody>();
		rgb.velocity = transform.TransformDirection(Vector3.forward * StartSpeed);
		_previousStep = gameObject.transform.position;
		if (RandomDamage)
			Damage += Random.Range(MinRandLimit, MaxRandLimit);
	}

	void FixedUpdate()
	{
		var currentStep = gameObject.transform.rotation;

		transform.LookAt(_previousStep, transform.position);
		RaycastHit hit = new RaycastHit();
		float distance = Vector3.Distance(_previousStep, transform.position);
		// ReSharper disable once CompareOfFloatsByEqualityOperator
		if (distance == 0.0f)
			distance = 1e-05f;
		Debug.DrawRay(_previousStep, transform.TransformDirection(Vector3.back) * distance * 0.9999f);
		if (Physics.Raycast(_previousStep, transform.TransformDirection(Vector3.back), out hit, distance * 0.9999f) &&
		    (hit.transform.gameObject != gameObject))
		{
			Debug.Log(string.Format("{0} hit at position", hit.point));
			InitExpoisen(hit.point, hit.normal);
			SendDamage(hit.transform.gameObject);
		}

		gameObject.transform.rotation = currentStep;

		_previousStep = gameObject.transform.position;
	}

	void SendDamage(GameObject hit)
	{
		Debug.Log(string.Format("{0} damage send", Damage));
		hit.SendMessage("ApplyDamage", Damage, SendMessageOptions.DontRequireReceiver);
		Destroy(gameObject);
	}


	void OnCollisionEnter(Collision collision)
	{
		foreach (ContactPoint contact in collision.contacts)
		{
			Debug.DrawRay(contact.point, contact.normal, Color.white);
			InitExpoisen(contact.point, contact.normal);
		}
		SendDamage(collision.gameObject);
	}

	void InitExpoisen(Vector3 point, Vector3 normal)
	{
		Debug.Log(string.Format("{0} hit at position", point));
		var instance = Instantiate(ParticleHit, point, Quaternion.FromToRotation(Vector3.up, normal));
		instance.SetActive(true);
		DestroyObject(instance, 2);
	}
}
