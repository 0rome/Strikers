using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public GameObject BlackScreen;

    private Animator blackScreenAnimator;
    private void Start()
    {
        blackScreenAnimator = BlackScreen.GetComponent<Animator>();
        Invoke("StartScreen",0.3f);
    }
    public void LoadScene(int sceneIndex)
    {
        blackScreenAnimator.SetTrigger("End");
        // ��������� �������� � ���������
        StartCoroutine(LoadSceneWithDelay(sceneIndex, 0.5f));
    }

    private IEnumerator LoadSceneWithDelay(int sceneIndex, float delay)
    {
        // ���� ��������� ���������� ������
        yield return new WaitForSeconds(delay);

        // �������� ����� ��� �������� �����
        LoadSceneBlack(sceneIndex);
    }

    private void LoadSceneBlack(int index)
    {
        // �������� �����
        SceneManager.LoadScene(index);
    }

    private void StartScreen()
    {
        blackScreenAnimator.SetBool("Start",false);
    }
}
