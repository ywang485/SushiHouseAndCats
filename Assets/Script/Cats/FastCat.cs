using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FastCat : Cat {

	public float speed = 2f;
	public int catPopValDecr = 2;

	public override float getSpeed() {
		return speed;
	}

	public override int getCatPopValDecr() {
		return catPopValDecr;
	}

	public override List<SushiManager.Sushi> getSushiWanted() {
		List<SushiManager.Sushi> sushiWanted = new List<SushiManager.Sushi>();

		sushiWanted.Add (SushiManager.Sushi.CaliforniaRoll);
		sushiWanted.Add (SushiManager.Sushi.SalmonNigiri);
		sushiWanted.Add (SushiManager.Sushi.SalmonRoll);
		sushiWanted.Add (SushiManager.Sushi.TamagoNigiri);
		sushiWanted.Add (SushiManager.Sushi.TunaNigiri);
		sushiWanted.Add (SushiManager.Sushi.WhiteTunaNigiri);

		return sushiWanted;
	}
}
