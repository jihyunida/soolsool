using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;
using System.Runtime.InteropServices;


public class ReceivedDataToVFX : MonoBehaviour
{

    private VisualEffect vfx;
    private GraphicsBuffer gBuffer;

    public Receiver receiver;
    [VFXType(VFXTypeAttribute.Usage.GraphicsBuffer)]
    [Serializable]
    public struct CustomData
    {
        public float u;
        public float v;
    }
    
    private CustomData[] datas;


    private void Start()
    {
        datas = new CustomData[1081];

        gBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, datas.Length, Marshal.SizeOf(typeof(CustomData)));

        gBuffer.SetData(datas);

        vfx = GetComponent<VisualEffect>();
        vfx.SetGraphicsBuffer("Echo", gBuffer);

    }

    private void FixedUpdate()
    {
        for (int i = 0; i < datas.Length; ++i)
        {
            if (receiver.uvPoint.Length <= 0)
                return;
            datas[i].u = (float)receiver.uvPoint[i].x;
            datas[i].v = (float)receiver.uvPoint[i].y;

        }
        gBuffer.SetData(datas);
    }


    #region Dispose
    private void OnDestroy()
    {
        gBuffer.Dispose();
    }

    private void OnApplicationQuit()
    {
        gBuffer.Dispose();
    }
    #endregion
}
