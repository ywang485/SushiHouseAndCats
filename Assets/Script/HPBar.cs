using UnityEngine;
using System.Collections;

public class HPBar : MonoBehaviour {

	private HPSubject subj;
	private GameObject HPBarContent;

	// Use this for initialization
	void Awake () {

		subj = GetComponentInParent<HPSubject> ();
		HPBarContent = transform.Find ("HPBarContent").gameObject;

	}
	
	// Update is called once per frame
	void Update () {

		HPBarContent.transform.localScale = new Vector2 ((float)subj.HP/(float)subj.maxHP, 1f);
	
	}
}
