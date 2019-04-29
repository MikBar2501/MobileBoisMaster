using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BackToMenu : MonoBehaviour
{

    public Slider slider;
	public GameObject text;
	public GameObject panel;

    // Start is called before the first frame update
    void Start() {
		panel.SetActive(false);
		text.SetActive(false);
		slider.value = 0;
		if(panel != null)
        StartCoroutine(FadeIn());
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

		#if (UnityEditor)
			if(progress >= 1f && Input.GetMouseButton(0) > 0) {
			operation.allowSceneActivation = true;
			
		}
		#endif
		if(progress >= 1f && (Input.touchCount > 0  || Input.GetMouseButton(0))) {
			operation.allowSceneActivation = true;
			
		}
		
        	yield return null;
    	}
	}

	IEnumerator FadeIn()
    {
        //cover.color = new Color(0,0,0,0);

        float value = 1;

        while (value > 0)
        {
            panel.GetComponent<Image>().color = new Color(0, 0, 0, value);
            value -= 1 * Time.deltaTime;

            yield return null;
        }

        panel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        //cover.gameObject.SetActive(false);
    }
}
