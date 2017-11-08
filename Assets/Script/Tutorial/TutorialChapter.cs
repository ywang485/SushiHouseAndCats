using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialChapter : MonoBehaviour {

	protected bool goToNextTutorialWhenDestroyed = true;

	void OnDestroy() {
		if (goToNextTutorialWhenDestroyed) {
			GameObject.Find ("GameTutorial").GetComponent<GameTutorial> ().nextTutorial ();
		}
	}

}
