using System;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private Receiver receiver;
    [SerializeField] private Camera raycastCamera;
    [SerializeField] private string layerName;
    private int _layerMask;

    private void Start()
    {
        _layerMask = 1 << LayerMask.NameToLayer(layerName);
    }

    private void Update()
    {
        Cast();
    }

    private void Cast()
    {
        for (int i = 0;i<receiver.uvPoint.Length; ++i)
        {
            if (float.IsNaN(receiver.uvPoint[i].y))
            {
                
                continue;
            }
            else
            {
                Vector3 screenPoint = new Vector3(receiver.uvPoint[i].x * raycastCamera.pixelWidth, receiver.uvPoint[i].y * raycastCamera.pixelHeight, 0f);
               
                Ray ray = raycastCamera.ScreenPointToRay(screenPoint);
                RaycastHit hit;
                Debug.DrawRay(ray.origin, Vector3.up, Color.yellow);
                if (Physics.Raycast(ray, out hit, raycastCamera.farClipPlane, _layerMask))
                {
                    // Debug.Log("hit "+ i +" "+ hit.point);
                    hit.transform.SendMessage("OnRaycast", SendMessageOptions.DontRequireReceiver); 
                    
                    Debug.DrawRay(hit.point, Vector3.up, Color.yellow);

                    // int LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
                    //
                    // hit.collider.gameObject.layer = LayerIgnoreRaycast;
                }
            }
        }
    }
    
    
    
}
