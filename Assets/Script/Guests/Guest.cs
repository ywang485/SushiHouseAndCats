using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Guest : MonoBehaviour {

	[HideInInspector] public GameManager gameManager;

	[HideInInspector] public Animator animator;
	[HideInInspector] public Bubble bubble;
	[HideInInspector] public Icon icon;
	[HideInInspector] public Rigidbody2D rb;

	[HideInInspector] public GuestState currState;

	//private static int spawingInterval = 5;
	//private static int spawningLikelihood = 50;
	private static int defActPopThUp = 999;
	private static int defActPopThDown = -999;
	private static int waitTime = 20;
	private static int eatingTime = 40;
	private static int popValue = 5;
	private static float speed = 1f;
	private static int popValueDec = 5;

	[HideInInspector] public Table target_table;
	[HideInInspector] public SushiManager.Sushi target_sushi;

	[HideInInspector] public GuestGoTowardsCounterState goTowardsCounterState;
	[HideInInspector] public GuestGoTowardsTableState goTowardsTableState;
	[HideInInspector] public GuestEatingState eatingState;
	[HideInInspector] public GuestWaitState waitState;
	[HideInInspector] public GuestLeavingState leavingState;

//	public virtual int getSpawningInterval() {
//		return Guest.spawingInterval;
//	}
//
//	public virtual int getSpawningLikelihood() {
//		return Guest.spawningLikelihood;
//	}

	public virtual int getWaitTime() {
		int waitTime = Guest.waitTime + gameManager.guestManager.humanPopularity / 15;
		if (waitTime < gameManager.guestManager.minimalWaitTime) {
			return gameManager.guestManager.minimalWaitTime;
		} else {
			return waitTime;
		}
	}

	public virtual int getEatingTime() {
		return Guest.eatingTime;
	}

	public virtual int getPopValue() {
		return Guest.popValue;
	}

	public virtual int getPopValueDec() {
		return Guest.popValueDec;
	}

	public virtual float getSpeed() {
		return Guest.speed;
	}

//	public virtual bool shouldSpawn(int totalMinute) {
//		if(totalMinute % getSpawningInterval() == 0) {
//			return true;
//		}
//		else {
//			return false;
//		}
//	}

	public virtual int getActPopThUp() {
		return defActPopThUp;
	}

	public virtual int getActPopThDown() {
		return defActPopThDown;
	}

	public virtual SushiManager.Sushi[] getSushiWanted() {
		SushiManager.Sushi[] sushiWanted = new SushiManager.Sushi[6];

		sushiWanted[0] = SushiManager.Sushi.CaliforniaRoll;
		sushiWanted[1] = SushiManager.Sushi.SalmonNigiri;
		sushiWanted[2] = SushiManager.Sushi.SalmonRoll;
		sushiWanted[3] = SushiManager.Sushi.TamagoNigiri;
		sushiWanted[4] = SushiManager.Sushi.TunaNigiri;
		sushiWanted[5] = SushiManager.Sushi.WhiteTunaNigiri;

		return sushiWanted;
	}

	public void showMoodIcon(int mood) {
		icon.setIcon (gameManager.moodIcons[mood]);
		bubble.show ();
	}

	public void showSushiIcon (SushiManager.Sushi sushi) {
		icon.setIcon (gameManager.sushiManager.sushiSprite[SushiManager.sushi2number(sushi)]);
		bubble.show ();
	}

	public void hideBubble() {
		icon.setIcon (null);
		bubble.hide ();
	}

	public SushiManager.Sushi generateOrder() {
		
		SushiManager.Sushi[] sushiWanted = getSushiWanted ();

		int index = Random.Range (0, sushiWanted.Length);

		return sushiWanted [index];
	}

	public void init() {
		currState = goTowardsCounterState;
		waitState.resetWaitingStartTime ();
		eatingState.resetEatingStartTime ();

		animator.SetInteger ("State", 4);
	
		hideBubble ();

	}

	void Update() {
		currState.UpdateState ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Weapon") {
			GameObject.Destroy(other.gameObject);
			showMoodIcon (1);
			gameManager.guestManager.humanPopularity -= getPopValueDec ();
			currState.ToLeaving ();
			gameManager.playHurtSFX ();
		}
		currState.OnTriggerEnter2D (other);
	}

	// Initialization
	public void OnEnable() {
		bubble = gameObject.GetComponentInChildren<Bubble> ();
		icon = gameObject.GetComponentInChildren<Icon> ();
		animator = GetComponent<Animator> ();
		rb = gameObject.GetComponent<Rigidbody2D> ();
		gameManager = GameManager.getGameManager ();

		goTowardsCounterState = new GuestGoTowardsCounterState (this);
		goTowardsTableState = new GuestGoTowardsTableState(this);
		eatingState = new GuestEatingState (this);
		leavingState = new GuestLeavingState (this);
		waitState = new GuestWaitState (this);

		init ();

	}

}
