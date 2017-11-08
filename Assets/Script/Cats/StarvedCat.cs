using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarvedCat : CatObsolete {

	private static float speed = 1f;
	private static int catPopValDecr = 2;

	public float getSpeed() {
		return StarvedCat.speed;
	}

	public override int getCatPopValDecr() {
		return StarvedCat.catPopValDecr;
	}
		
}
