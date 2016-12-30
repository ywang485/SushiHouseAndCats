using UnityEngine;
using System.Collections;

public class HPSubject : MonoBehaviour {

	public int HP = 100;
	public int maxHP = 100;
	public GameObject hpbar;

	public void beAttacked(int HPDec) {
		HP = HP - HPDec;
		if (HP < 0) {
			HP = 0;
		}

		if (!hpbar.activeInHierarchy) {
			hpbar.SetActive (true);
		}
	}

	public void resetHP() {
		HP = maxHP;
	}

}
