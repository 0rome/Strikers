using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    public string screenshotFolder = "Assets"; // Папка для сохранения скриншотов
    public string screenshotFileName = "Screenshot"; // Базовое имя файла
    public KeyCode screenshotKey = KeyCode.F12; // Клавиша для создания скриншота
    public int superSize = 1; // Масштаб для увеличения разрешения

    private void Start()
    {
        // Создаём папку, если её нет
        if (!System.IO.Directory.Exists(screenshotFolder))
        {
            System.IO.Directory.CreateDirectory(screenshotFolder);
        }
    }

    private void Update()
    {
        // Проверяем, нажата ли клавиша для скриншота
        if (Input.GetKeyDown(screenshotKey))
        {
            TakeScreenshot();
        }
    }

    private void TakeScreenshot()
    {
        // Формируем имя файла с датой и временем
        string timeStamp = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string fileName = $"{screenshotFileName}_{timeStamp}.png";
        string filePath = System.IO.Path.Combine(screenshotFolder, fileName);

        // Создаём скриншот
        ScreenCapture.CaptureScreenshot(filePath, superSize);
        Debug.Log($"Скриншот сохранён: {filePath}");
    }
}
