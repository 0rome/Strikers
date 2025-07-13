using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    public string screenshotFolder = "Assets"; // ����� ��� ���������� ����������
    public string screenshotFileName = "Screenshot"; // ������� ��� �����
    public KeyCode screenshotKey = KeyCode.F12; // ������� ��� �������� ���������
    public int superSize = 1; // ������� ��� ���������� ����������

    private void Start()
    {
        // ������ �����, ���� � ���
        if (!System.IO.Directory.Exists(screenshotFolder))
        {
            System.IO.Directory.CreateDirectory(screenshotFolder);
        }
    }

    private void Update()
    {
        // ���������, ������ �� ������� ��� ���������
        if (Input.GetKeyDown(screenshotKey))
        {
            TakeScreenshot();
        }
    }

    private void TakeScreenshot()
    {
        // ��������� ��� ����� � ����� � ��������
        string timeStamp = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string fileName = $"{screenshotFileName}_{timeStamp}.png";
        string filePath = System.IO.Path.Combine(screenshotFolder, fileName);

        // ������ ��������
        ScreenCapture.CaptureScreenshot(filePath, superSize);
        Debug.Log($"�������� �������: {filePath}");
    }
}
