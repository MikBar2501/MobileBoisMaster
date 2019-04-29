using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	public Slider slider;
	public GameObject text;
	public GameObject panel;

	void Start() {
		panel.SetActive(false);
		text.SetActive(false);
		slider.value = 0;
	}

	public void LoadScene(int sceneIndex) {
		StartCoroutine(LoadAsynchronous(sceneIndex));
	}

	IEnumerator LoadAsynchronous(int sceneIndex) {
    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
	panel.SetActive(true);
	text.SetActive(false);
	operation.allowSceneActivation = false;
    while (!operation.isDone) {
        float progress = Mathf.Clamp01(operation.progress / 0.9f);
        Debug.Log("Loading progress: " + operation.progress + "%");
		slider.value = progress;
		if(progress >= 1f) {
			text.SetActive(true);
		}
		if(progress >= 1f && Input.touchCount > 0) {
			operation.allowSceneActivation = true;
			
		}
		
        yield return null;
    }
}
}
