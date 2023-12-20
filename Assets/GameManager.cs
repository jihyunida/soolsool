using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;
public class GameManager : MonoBehaviour
{

    public UnityEvent OnPlayLight;
    public UnityEvent OnStopLight;

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
        if (Input.GetKeyUp(KeyCode.F1))
        {
            OnPlayLight?.Invoke();
        }
        if(Input.GetKeyUp(KeyCode.Q))
        { 
            OnStopLight?.Invoke();
        }



        if (Input.GetKeyUp(KeyCode.F2))
        {
            OnPlayGlitch?.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            OnStopGlitch?.Invoke();
        }



        if (Input.GetKeyUp(KeyCode.F3))
        {
            OnPlayGrass?.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            OnStopGrass?.Invoke();
        }
    }

    
}
