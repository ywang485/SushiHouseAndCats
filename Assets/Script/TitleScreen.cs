using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class TitleScreen : MonoBehaviour {

	public GameObject enterPlayerNamePanel;
	public GameObject newGameWarningPanel;
	public AudioClip PosBlipSFX;
	public AudioClip NegBlipSFX;

	public PlayerDataManager playerDataManager;

	private AudioSource audioSrc;

	void Start() {
		audioSrc = GetComponent<AudioSource> ();
	}

	public void getPlayerNameFromInput() {
		closeAllPanel ();
		enterPlayerNamePanel.SetActive (true);
	}

	public void closeAllPanel() {
		enterPlayerNamePanel.SetActive (false);
		newGameWarningPanel.SetActive (false);
	}

	public void showNewGameWarningPanel() {
		closeAllPanel ();
		newGameWarningPanel.SetActive (true);
	}

	public void startNewGameWarningCheck() {

		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			showNewGameWarningPanel ();
		} else {
			playerDataManager.startNewGame (enterPlayerNamePanel.GetComponentInChildren<InputField> ());
		}
	}

	public void quitGame() {
		Application.Quit ();
	}

	public void playPosBlipSFX() {
		audioSrc.PlayOneShot (PosBlipSFX, 1.0f);
	}

	public void playNegBlipSFX() {
		audioSrc.PlayOneShot (NegBlipSFX, 1.0f);
	}


}
