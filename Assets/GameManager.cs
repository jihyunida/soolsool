using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;


public class GameManager : MonoBehaviour
{

    public UnityEvent OnPlayLight;
    public UnityEvent OnStopLight;

    public CanvasGroup glitchOutput;

    public UnityEvent OnPlayGlitch;
    public UnityEvent OnStopGlitch;

    public UnityEvent OnPlayGrass;
    public UnityEvent OnStopGrass;




    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.F1))
        //{
        //    OnPlayLight?.Invoke();
        //}
        //if(Input.GetKeyUp(KeyCode.Q))
        //{ 
        //    OnStopLight?.Invoke();
        //}



        //if (Input.GetKeyUp(KeyCode.F2))
        //{
        //    OnPlayGlitch?.Invoke();
        //}
        //if (Input.GetKeyUp(KeyCode.W))
        //{
        //    OnStopGlitch

        SoolSool();

    }


    public void SineWaveOn()
    {
        glitchOutput.DOFade(1f, 1f);

    }

    public void SineWaveOff()
    {
        glitchOutput.DOFade(0f, 1f);
    }


    public Receiver receiver;

    public int count;
    public int step1;   // ¸¹À» ¶§
    public int step2;
    public int step3;

    private void SoolSool()
    {
        count = 0;
        for (int i = 0; i < receiver.uvPoint.Length; i++)
        {
            if (!float.IsNaN(receiver.uvPoint[i].x))
            {
                count++;
            }
        }


        if (count > step1)
        {
            OnStopLight?.Invoke();
            OnStopGlitch?.Invoke();
            OnStopGrass?.Invoke();
            Debug.Log("»Ï»Ð1");

        }
        else if (count > step2)
        {
            Debug.Log("»Ï»Ð2");

            OnStopLight?.Invoke();
            OnStopGlitch?.Invoke();
            OnStopGrass?.Invoke();
        }
        else if (count > step3)
        {
            Debug.Log("»Ï»Ð3");

            OnPlayLight?.Invoke();
            OnPlayGlitch?.Invoke();
            OnPlayGrass?.Invoke();  
        }
        else
        {
            Debug.Log("»Ï»Ð»Ï»Ð");

            OnPlayLight?.Invoke();
            OnPlayGlitch?.Invoke();
            OnPlayGrass?.Invoke();
        }
    }

}
