using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuNew : MonoBehaviour {

	Animator CameraObject;

	[Header("Loaded Scene")]
	[Tooltip("The name of the scene in the build settings that will load")]
	public string sceneName = ""; 

	[Header("Panels")]
	[Tooltip("The UI Panel parenting all sub menus")]
	public GameObject mainCanvas;
	[Tooltip("The UI Panel that holds the CONTROLS window tab")]
	public GameObject PanelControls;
	[Tooltip("The UI Panel that holds the VIDEO window tab")]
	public GameObject PanelVideo;
	[Tooltip("The UI Panel that holds the GAME window tab")]
	public GameObject PanelGame;
	[Tooltip("The UI Panel that holds the KEY BINDINGS window tab")]
	public GameObject PanelKeyBindings;
	[Tooltip("The UI Sub-Panel under KEY BINDINGS for MOVEMENT")]
	public GameObject PanelMovement;
	[Tooltip("The UI Sub-Panel under KEY BINDINGS for COMBAT")]
	public GameObject PanelCombat;
	[Tooltip("The UI Sub-Panel under KEY BINDINGS for GENERAL")]
	public GameObject PanelGeneral;

	[Header("SFX")]
	[Tooltip("The GameObject holding the Audio Source component for the HOVER SOUND")]
	public GameObject hoverSound;
	[Tooltip("The GameObject holding the Audio Source component for the AUDIO SLIDER")]
	public GameObject sliderSound;
	[Tooltip("The GameObject holding the Audio Source component for the SWOOSH SOUND when switching to the Settings Screen")]
	public GameObject swooshSound;

	// campaign button sub menu
	[Header("Menus")]
	[Tooltip("The Menu for when the MAIN menu buttons")]
	public GameObject mainMenu;
	[Tooltip("The Menu for when the PLAY button is clicked")]
	public GameObject playMenu;
	[Tooltip("The Menu for when the EXIT button is clicked")]
	public GameObject exitMenu;

	// highlights
	[Header("Highlight Effects")]
	[Tooltip("Highlight Image for when GAME Tab is selected in Settings")]
	public GameObject lineGame;
	[Tooltip("Highlight Image for when VIDEO Tab is selected in Settings")]
	public GameObject lineVideo;
	[Tooltip("Highlight Image for when CONTROLS Tab is selected in Settings")]
	public GameObject lineControls;
	[Tooltip("Highlight Image for when KEY BINDINGS Tab is selected in Settings")]
	public GameObject lineKeyBindings;
	[Tooltip("Highlight Image for when MOVEMENT Sub-Tab is selected in KEY BINDINGS")]
	public GameObject lineMovement;
	[Tooltip("Highlight Image for when COMBAT Sub-Tab is selected in KEY BINDINGS")]
	public GameObject lineCombat;
	[Tooltip("Highlight Image for when GENERAL Sub-Tab is selected in KEY BINDINGS")]
	public GameObject lineGeneral;

	[Header("LOADING SCREEN")]
	public GameObject loadingMenu;
	public Slider loadBar;
	public TMP_Text finishedLoadingText;

	void Start(){
		CameraObject = transform.GetComponent<Animator>();
	}

	public void  PlayCampaign (){
		exitMenu.gameObject.SetActive(false);
		playMenu.gameObject.SetActive(true);
	}
	
	public void  PlayCampaignMobile (){
		exitMenu.gameObject.SetActive(false);
		playMenu.gameObject.SetActive(true);
		mainMenu.gameObject.SetActive(false);
	}

	public void  ReturnMenu (){
		playMenu.gameObject.SetActive(false);
		exitMenu.gameObject.SetActive(false);
		mainMenu.gameObject.SetActive(true);
	}

	public void NewGame(){
		if(sceneName != ""){
			StartCoroutine(LoadAsynchronously(sceneName));
			//SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
		}
	}

	public void  DisablePlayCampaign (){
		playMenu.gameObject.SetActive(false);
	}

	public void  Position2 (){
		DisablePlayCampaign();
		CameraObject.SetFloat("Animate",1);
	}

	public void  Position1 (){
		CameraObject.SetFloat("Animate",0);
	}

	public void  GamePanel (){
		PanelControls.gameObject.SetActive(false);
		PanelVideo.gameObject.SetActive(false);
		PanelGame.gameObject.SetActive(true);
		PanelKeyBindings.gameObject.SetActive(false);

		lineGame.gameObject.SetActive(true);
		lineControls.gameObject.SetActive(false);
		lineVideo.gameObject.SetActive(false);
		lineKeyBindings.gameObject.SetActive(false);
	}

	public void  VideoPanel (){
		PanelControls.gameObject.SetActive(false);
		PanelVideo.gameObject.SetActive(true);
		PanelGame.gameObject.SetActive(false);
		PanelKeyBindings.gameObject.SetActive(false);

		lineGame.gameObject.SetActive(false);
		lineControls.gameObject.SetActive(false);
		lineVideo.gameObject.SetActive(true);
		lineKeyBindings.gameObject.SetActive(false);
	}

	public void  ControlsPanel (){
		PanelControls.gameObject.SetActive(true);
		PanelVideo.gameObject.SetActive(false);
		PanelGame.gameObject.SetActive(false);
		PanelKeyBindings.gameObject.SetActive(false);

		lineGame.gameObject.SetActive(false);
		lineControls.gameObject.SetActive(true);
		lineVideo.gameObject.SetActive(false);
		lineKeyBindings.gameObject.SetActive(false);
	}

	public void  KeyBindingsPanel (){
		PanelControls.gameObject.SetActive(false);
		PanelVideo.gameObject.SetActive(false);
		PanelGame.gameObject.SetActive(false);
		PanelKeyBindings.gameObject.SetActive(true);

		lineGame.gameObject.SetActive(false);
		lineControls.gameObject.SetActive(false);
		lineVideo.gameObject.SetActive(true);
		lineKeyBindings.gameObject.SetActive(true);
	}

	public void  MovementPanel (){
		PanelMovement.gameObject.SetActive(true);
		PanelCombat.gameObject.SetActive(false);
		PanelGeneral.gameObject.SetActive(false);

		lineMovement.gameObject.SetActive(true);
		lineCombat.gameObject.SetActive(false);
		lineGeneral.gameObject.SetActive(false);
	}

	public void CombatPanel (){
		PanelMovement.gameObject.SetActive(false);
		PanelCombat.gameObject.SetActive(true);
		PanelGeneral.gameObject.SetActive(false);

		lineMovement.gameObject.SetActive(false);
		lineCombat.gameObject.SetActive(true);
		lineGeneral.gameObject.SetActive(false);
	}

	public void GeneralPanel (){
		PanelMovement.gameObject.SetActive(false);
		PanelCombat.gameObject.SetActive(false);
		PanelGeneral.gameObject.SetActive(true);

		lineMovement.gameObject.SetActive(false);
		lineCombat.gameObject.SetActive(false);
		lineGeneral.gameObject.SetActive(true);
	}

	public void PlayHover (){
		hoverSound.GetComponent<AudioSource>().Play();
	}

	public void PlaySFXHover (){
		sliderSound.GetComponent<AudioSource>().Play();
	}

	public void PlaySwoosh (){
		swooshSound.GetComponent<AudioSource>().Play();
	}

	// Are You Sure - Quit Panel Pop Up
	public void  AreYouSure (){
		exitMenu.gameObject.SetActive(true);
		DisablePlayCampaign();
	}

	public void  AreYouSureMobile (){
		exitMenu.gameObject.SetActive(true);
		mainMenu.gameObject.SetActive(false);
		DisablePlayCampaign();
	}

	public void  Yes (){
		Application.Quit();
	}

	IEnumerator LoadAsynchronously (string sceneName){ // scene name is just the name of the current scene being loaded
			AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
			operation.allowSceneActivation = false;
			mainCanvas.SetActive(false);
			loadingMenu.SetActive(true);

			while (!operation.isDone){
				float progress = Mathf.Clamp01(operation.progress / .9f);
				loadBar.value = progress;

				if(operation.progress >= 0.9f){
					finishedLoadingText.gameObject.SetActive(true);

					if(Input.anyKeyDown){
						operation.allowSceneActivation = true;
					}
				}
				
				yield return null;
			}
		}
}