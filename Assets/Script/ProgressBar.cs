using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

	public int levelUpValue = 1;
	public int currValue = 0;
	private GameObject BarContent;

	// Use this for initialization
	void Awake () {
		BarContent = transform.Find ("BarContent").gameObject;

	}

	// Update is called once per frame
	void Update () {

		BarContent.transform.localScale = new Vector2 ((float)currValue/(float)levelUpValue, 1f);

	}
}
