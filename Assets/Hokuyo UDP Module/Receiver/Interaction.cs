using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent onRaycastEvent; 


    public void OnRaycast()
    {
        onRaycastEvent?.Invoke();
    }

    public void DoSomething()
    {

    }
}
