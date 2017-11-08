using System;

[Serializable]
public class CatRecord {

	public int numDefeated;
	public int numFed;

	public CatRecord(int numDefeated, int numFed) {
		this.numDefeated = numDefeated;
		this.numFed = numFed;
	}

}
