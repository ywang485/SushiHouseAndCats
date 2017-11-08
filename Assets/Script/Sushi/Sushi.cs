using UnityEngine;
using System.Collections;

public class Sushi : MonoBehaviour {

	private HPSubject hp;
	private string sushiType;

	private GameManager gameManager;

	public GameObject discardBtn;

	public string getSushiType() {
		return sushiType;
	}

	public void setSushiType(string type) {
		sushiType = type;
		SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		spriteRenderer.sprite = Resources.Load(SushiManager.sushiTypes[type].getSpritePath_s(), typeof(Sprite)) as Sprite;
	}

	// Use this for initialization
	void Awake () {

		hp = GetComponent<HPSubject> ();
		gameManager = GameManager.getGameManager ();

	}
	
	// Update is called once per frame
	void Update () {

		if (hp.HP <= 0) {
			gameObject.SetActive(false);
		}

	}

	void OnEnable() {
		hp.resetHP ();
		GameObject hpBar = transform.Find ("HPBar").gameObject;
		hpBar.SetActive (false);
		if (discardBtn != null) {
			discardBtn.SetActive (false);
		}
	}
}
