using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{

	public GameObject Bullet;

	public GameObject Gun;

	public Button Button;

	void Start()
	{
		Button btn = Button.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		var copy = Instantiate(Bullet, Gun.transform.position, Gun.transform.rotation);
		copy.SetActive(true);
	}
}
