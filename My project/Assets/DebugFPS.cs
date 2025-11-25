using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugFPS : MonoBehaviour
{
    public int defaultFPS = 60;
    public int increment = 10;
    public float updateRate = 1;
    public TMP_Text textField;
    string currentFrameRate = "0";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(UpdateText());
    }

    // Update is called once per frame
    void Update()
    {
        textField.text = Application.targetFrameRate.ToString() + "  " + currentFrameRate;
        if(Input.GetKeyDown(KeyCode.R))
        {
            Application.targetFrameRate = defaultFPS;
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Application.targetFrameRate -= increment;
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            Application.targetFrameRate += increment;
        }
    }

    IEnumerator UpdateText()
    {
        while(1>0)
        {
            currentFrameRate = ((int)(1.0f/Time.deltaTime)).ToString();
            yield return new WaitForSecondsRealtime(1/updateRate);
        }
    }
}
