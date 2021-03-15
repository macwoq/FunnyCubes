using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

public class CubeColors : MonoBehaviour
{
    CubeMovement cubeMovement;
    
    //Vector3 startPos = new Vector3(0, 0, 0);
    [Header("Colors")]
    [SerializeField] Material cubeMaterial;
    public Color[] cubeColors;
    public Color colorW = Color.white;
    public Color colorB = Color.black;

    [Header("Randomize")]
    public bool isRandom;
    private float timer;
    public float timerInterval = 3f;
    [Header("Black&White")]
    public bool smoothBW;
    public float cubeLerpTimer = 5;
    // Start is called before the first frame update
    void Start()
    {
        
        cubeMovement = FindObjectOfType<CubeMovement>();
        timer = timerInterval; 
    }

    public void StartRandom()
    {
        smoothBW = false;
        isRandom = true;
        timerInterval = timer;
    }
    // Update is called once per frame
    void Update()
    {
        if (isRandom || !isRandom)
        {
            transform.position = new Vector3(0, 0, 0);
        }
        timerInterval -= Time.deltaTime;
        if (!smoothBW && isRandom && timerInterval < 0)
        {
            RandomLerp();
            timerInterval = timer;
        }
    }

    private void RandomLerp()
    {
        var currentColor = Random.Range(0, cubeColors.Length);
        cubeMaterial.color = cubeColors[currentColor];
    }

    public void StartSmooth()
    {
        smoothBW = true;
        isRandom = false;
        if (smoothBW && !isRandom && !cubeMovement.isActiveAndEnabled)
        {
            cubeMaterial.color = colorW;
            StartCoroutine(LerpCol(colorB, cubeLerpTimer));
        }
    }

    IEnumerator LerpCol(Color endValue, float duration)
    {
        float time = 0;
        Color startValeu = cubeMaterial.color;
        while(time < duration)
        {
            cubeMaterial.color = Color.Lerp(startValeu, endValue, time/duration);
            time += Time.deltaTime;
            yield return null;

        }
        cubeMaterial.color = endValue;
        if (smoothBW && !isRandom)
            StartCoroutine(LerpBack(colorW, cubeLerpTimer));
    }

    IEnumerator LerpBack(Color endValue, float duration)
    {
        float time = 0;
        Color startValeu = cubeMaterial.color;
        while (time < duration)
        {
            cubeMaterial.color = Color.Lerp(startValeu, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;

        }
        cubeMaterial.color = endValue;
        if (smoothBW && !isRandom && !cubeMovement.isActiveAndEnabled)
            StartCoroutine(LerpCol(colorB, cubeLerpTimer));
    }
}
