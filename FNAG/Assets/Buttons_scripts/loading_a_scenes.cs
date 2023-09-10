using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading_a_scenes: MonoBehaviour
{
    public Image loading_screen;
    public Text loading_text;

    public void Loading_game()
    {
        loading_screen.gameObject.SetActive(true);
        StartCoroutine(LoadAsync(1));
    }
    
    public void Loading_main_menu()

    {
        loading_screen.gameObject.SetActive(true);
        StartCoroutine(LoadAsync(0));
    }

    IEnumerator LoadAsync(int num_of_scene)
    {
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(num_of_scene);
        loadAsync.allowSceneActivation = false;
        while (!loadAsync.isDone)
        {
            loading_text.text = $"Loading {Convert.ToInt32(loadAsync.progress * 100)}%...";
            if(loadAsync.progress >= .9f && !loadAsync.allowSceneActivation)
            {
                yield return new WaitForSeconds(1f);
                loadAsync.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
