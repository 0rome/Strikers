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
        // Получаем аниматор из DeathMenu
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
        // Увеличиваем счёт красного игрока
        score_1 += 1;
        Score_1.text = score_1.ToString();
        StartCoroutine(DelayedAction());
    }

    public void BlueWinsRound()
    {
        // Увеличиваем счёт синего игрока
        score_2 += 1;
        Score_2.text = score_2.ToString();
        StartCoroutine(DelayedAction());
    }

    IEnumerator DelayedAction()
    {
        // Запускаем анимацию
        screenAnimator.SetTrigger("Win");

        // Ждём окончания анимации
        yield return new WaitForSeconds(1f); 

        // Сбрасываем позиции игроков
        ResetPlayers();

    }
    private void Map()
    {
        switch (PlayerPrefs.GetInt("MapIndex"))
        {
            case 0: // Первый пункт
                SpawnMap();
                break;
            case 1: // Второй пункт
                SpawnMap();
                break;
            case 2: // Третий пункт
                SpawnMap();
                break;
            case 3: // Третий пункт
                SpawnMap();
                break;
            case 4: // Третий пункт
                SpawnMap();
                break;
            case 5: // Третий пункт
                SpawnMap();
                break;
            case 6: // Третий пункт
                SpawnMap();
                break;
            case 7: // Третий пункт
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
        // Перемещаем игроков на исходные позиции
        Player_1.transform.position = new Vector3(-20, 0, 0);
        Player_2.transform.position = new Vector3(20, 0, 0);

        Player_1.GetComponent<PlayerHealth>().Heal(100);
        Player_2.GetComponent<PlayerHealth>().Heal(100);

        // Включаем игроков, если они были отключены
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
        // Указываем слои, которые нужно уничтожить
        int bulletsLayer = LayerMask.NameToLayer("Bullets");
        int gunsLayer = LayerMask.NameToLayer("Guns");

        // Перебираем все объекты с коллайдерами в сцене
        Collider2D[] colliders = FindObjectsOfType<Collider2D>();

        foreach (Collider2D collider in colliders)
        {
            GameObject obj = collider.gameObject;

            // Проверяем слой объекта
            if (obj.layer == bulletsLayer || obj.layer == gunsLayer)
            {
                Destroy(obj); // Уничтожаем объект
            }
        }
    }
}
