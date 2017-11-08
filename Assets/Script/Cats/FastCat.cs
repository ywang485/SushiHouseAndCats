using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FastCat : CatObsolete {

	public float speed = 2f;
	public int catPopValDecr = 2;

	public float getSpeed() {
		return speed;
	}

	public override int getCatPopValDecr() {
		return catPopValDecr;
	}

}
