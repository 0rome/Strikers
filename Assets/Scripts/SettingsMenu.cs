using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject BlackScreen;


    private Animator blackScreenAnimator;

    private void Start()
    {
        blackScreenAnimator = BlackScreen.GetComponent<Animator>();
        Invoke("StartScreen", 0.3f);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume",volume);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void LoadScene(int sceneIndex)
    {
        blackScreenAnimator.SetTrigger("End");
        // Запускаем корутину с задержкой
        StartCoroutine(LoadSceneWithDelay(sceneIndex, 0.5f));
    }
    public void SetMap(int mapIndex)
    {
        PlayerPrefs.SetInt("MapIndex",mapIndex);
    }
    private IEnumerator LoadSceneWithDelay(int sceneIndex, float delay)
    {
        // Ждем указанное количество секунд
        yield return new WaitForSeconds(delay);

        // Вызываем метод для загрузки сцены
        LoadSceneBlack(sceneIndex);
    }

    private void LoadSceneBlack(int index)
    {
        // Загрузка сцены
        SceneManager.LoadScene(index);
    }

    private void StartScreen()
    {
        blackScreenAnimator.SetBool("Start", false);
    }
}
