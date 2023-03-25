using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : SingletonBase<UIManager>
{
    [Header("UI Components")]
    [SerializeField]
    private TextMeshProUGUI totalShipsNumber;
    [SerializeField]
    private TextMeshProUGUI currentFibonacciNumber;
    [SerializeField]
    private TextMeshProUGUI fpsCounter;

    //aux vars
    private float fps;
    private float currentShipsNumber;
    private void Start()
    {
        Application.targetFrameRate = 60;//just for lock frameRate
        attFinobacciNumber(1);
    }
    private void Update()
    {
        attFps();
    }
    public void attFinobacciNumber(int n)
    {
        currentFibonacciNumber.text = "FibonacciNumber: " + n.ToString();
    }
    public void attShipsNumber()
    {
        currentShipsNumber++;
        totalShipsNumber.text = "Ships Spawned: " + currentShipsNumber.ToString();
    }
    private void attFps()
    {
        fps += (Time.deltaTime - fps) * 0.1f;
        fpsCounter.text = "FPS: " + ((int)(1 / fps)).ToString();
    }
}
