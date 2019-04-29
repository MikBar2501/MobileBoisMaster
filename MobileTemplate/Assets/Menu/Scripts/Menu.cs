using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Image cover;
    public float fadeSpeed = 1;

    public Slider slider;
	public GameObject text;
	public GameObject panel;

    public void Exit()
    {
        Application.Quit();
    }

    private void Start()
    {
        if(cover != null)
        StartCoroutine(FadeIn());

        panel.SetActive(false);
		text.SetActive(false);
		slider.value = 0;
    }

    public void Play()
    {
        //StartCoroutine(FadeOut());
        //WaitSecondForFade(10,2);
        cover.color = new Color(0, 0, 0, 255);;
        LoadScene(2);
    }

    public void LoadScene(int sceneIndex) {
		StartCoroutine(LoadAsynchronous(sceneIndex));
	}

    IEnumerator WaitSecondForFade(int time, int scene) {
        
        yield return new WaitForSeconds(time);
        //LoadScene(scene);
    }

    IEnumerator LoadAsynchronous(int sceneIndex) {
    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
    //StartCoroutine(FadeOut());
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
                if(progress >= 1f && (Input.touchCount > 0 || Input.GetMouseButton(0))) {
                    operation.allowSceneActivation = true;
                }
                
                yield return null;
            }  
    }

    public void Credits()
    {
        StartCoroutine(ToCredits());
    }

    public void OpenMenu()
    {
        LoadLevel(0);
    }

    public void LoadLevel(int id)
    {
        Application.LoadLevel(id);
    }

    IEnumerator ToCredits()
    {
        //cover.color = new Color(0,0,0,0);

        float value = 0;

        while(value < 1)
        {
            cover.color = new Color(0, 0, 0, value);
            value += fadeSpeed * Time.deltaTime;

            yield return null;
        }

        cover.color = new Color(0, 0, 0, 1);

        LoadLevel(1);
    }

    IEnumerator FadeIn()
    {
        //cover.color = new Color(0,0,0,0);

        float value = 1;

        while (value > 0)
        {
            cover.color = new Color(0, 0, 0, value);
            value -= fadeSpeed * Time.deltaTime;

            yield return null;
        }

        cover.color = new Color(0, 0, 0, 0);
        //cover.gameObject.SetActive(false);
    }

    IEnumerator FadeOut()
    {
        //cover.color = new Color(0,0,0,0);

        float value = 0;

        while(value < 1)
        {
            cover.color = new Color(0, 0, 0, value);
            value += fadeSpeed * Time.deltaTime;

            yield return null;
            
        }

        cover.color = new Color(0, 0, 0, 1);
        
        
    }



}
