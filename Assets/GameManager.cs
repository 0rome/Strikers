using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public Text Score_1;
    public Text Score_2;

    [Header("Objects")]
    public GameObject DeathMenu;
    public GameObject Player_1;
    public GameObject Player_2;

    public GameObject RedWinsMenu;
    public GameObject RedWinsEffect;
    public GameObject BlueWinsMenu;
    public GameObject BlueWinsEffect;

    public GameObject PauseButton;

    public Spawner[] spawners;
    public GameObject[] Maps;

    private int score_1;
    private int score_2;
    private bool redWins;
    private bool blueWins;

    private Animator screenAnimator;

    void Start()
    {
        // �������� �������� �� DeathMenu
        screenAnimator = DeathMenu.GetComponent<Animator>();
        Map();
    }

    void Update()
    {
        CheckWinCondition();
    }
    private void CheckWinCondition()
    {
        if (!Player_1.activeSelf && !blueWins && score_2 < PlayerPrefs.GetInt("Rounds") + 2)
        {
            BlueWinsRound();
            blueWins = true;
        }
        else if(!Player_1.activeSelf && !blueWins)
        {
            Score_1.enabled = false;
            Score_2.enabled = false;
            enabled = false;
            BlueWinsMenu.SetActive(true);
            BlueWinsEffect.SetActive(true);
            PauseButton.SetActive(false);
        }

        if (!Player_2.activeSelf && !redWins && score_1 < PlayerPrefs.GetInt("Rounds") + 2)
        {
            RedWinsRound();
            redWins = true;
        }
        else if(!Player_2.activeSelf && !redWins)
        {

            Score_1.enabled = false;
            Score_2.enabled = false;
            enabled = false;
            RedWinsMenu.SetActive(true);
            RedWinsEffect.SetActive(true);
            PauseButton.SetActive(false);
        }
    }
    public void RedWinsRound()
    {
        // ����������� ���� �������� ������
        score_1 += 1;
        Score_1.text = score_1.ToString();
        StartCoroutine(DelayedAction());
    }

    public void BlueWinsRound()
    {
        // ����������� ���� ������ ������
        score_2 += 1;
        Score_2.text = score_2.ToString();
        StartCoroutine(DelayedAction());
    }

    IEnumerator DelayedAction()
    {
        // ��������� ��������
        screenAnimator.SetTrigger("Win");

        // ��� ��������� ��������
        yield return new WaitForSeconds(1f); 

        // ���������� ������� �������
        ResetPlayers();

    }
    private void Map()
    {
        switch (PlayerPrefs.GetInt("MapIndex"))
        {
            case 0: // ������ �����
                SpawnMap();
                break;
            case 1: // ������ �����
                SpawnMap();
                break;
            case 2: // ������ �����
                SpawnMap();
                break;
            case 3: // ������ �����
                SpawnMap();
                break;
            case 4: // ������ �����
                SpawnMap();
                break;
            case 5: // ������ �����
                SpawnMap();
                break;
            case 6: // ������ �����
                SpawnMap();
                break;
            case 7: // ������ �����
                SpawnMap();
                break;
            
        }
    }
    private void SpawnMap()
    {
        Instantiate(Maps[PlayerPrefs.GetInt("MapIndex")],transform.position,Quaternion.identity);
    }
    private void ResetPlayers()
    {
        // ���������� ������� �� �������� �������
        Player_1.transform.position = new Vector3(-20, 0, 0);
        Player_2.transform.position = new Vector3(20, 0, 0);

        Player_1.GetComponent<PlayerHealth>().Heal(100);
        Player_2.GetComponent<PlayerHealth>().Heal(100);

        // �������� �������, ���� ��� ���� ���������
        Player_1.SetActive(true);
        Player_2.SetActive(true);

        blueWins = false;
        redWins = false;

        DestroyObjectsByLayers();
        foreach (Spawner spawner in spawners)
        {
            spawner.RestartCoroutine();
        }
    }
    public void DestroyObjectsByLayers()
    {
        // ��������� ����, ������� ����� ����������
        int bulletsLayer = LayerMask.NameToLayer("Bullets");
        int gunsLayer = LayerMask.NameToLayer("Guns");

        // ���������� ��� ������� � ������������ � �����
        Collider2D[] colliders = FindObjectsOfType<Collider2D>();

        foreach (Collider2D collider in colliders)
        {
            GameObject obj = collider.gameObject;

            // ��������� ���� �������
            if (obj.layer == bulletsLayer || obj.layer == gunsLayer)
            {
                Destroy(obj); // ���������� ������
            }
        }
    }
}
