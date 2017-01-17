using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {

	public string foodId;
	public Sprite finishedSprite;
	public bool active = false;
	public bool finished = false;

	private Vector3 lastLoc;
	private float checkTimeInterval = 0.2f;

	public virtual string getFoodId() {
		return foodId;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	protected void Update () {
		
		if ((lastLoc == transform.position) && !active) {
			active = true;
		}
		lastLoc = transform.position;
	}
}
