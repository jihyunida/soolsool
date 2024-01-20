using System;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Serialization;
    
    
public class Receiver : MonoBehaviour
{
    private struct Data
    {
        // public int sensorIndex;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1081)]
        public float[] u;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1081)]
        public float[] v;
    }
    
    private Data _uvData;
    public Vector2[] uvPoint;
    
    private UdpClient _udpClient;
    
    [Header("Listening Port")]
    [SerializeField] private int port = 8001;
    
    private IPEndPoint _remoteEp;
    
    // Start is called before the first frame update
    void Start()
    {
        _udpClient = new UdpClient(port);
        _udpClient.Client.Blocking = false;
        Debug.Log(Marshal.SizeOf(_uvData));
            
        _remoteEp = new IPEndPoint(IPAddress.Any, 0);

        uvPoint = new Vector2[1081];
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
    try
    {
        byte[] dgram = _udpClient.Receive(ref _remoteEp);

        _uvData = (Data)fromBytes(dgram);

        Copy(_uvData);
    }
    catch (Exception ex)
    {
        Debug.Log(ex);
    }
        // PrintStruct(_uvData);

    }
    
    private static byte[] GetBytes(object data)
    {
        int size = Marshal.SizeOf(data);
        byte[] arr = new byte[size];
        IntPtr ptr = Marshal.AllocHGlobal(size);
        Marshal.StructureToPtr(data,ptr,true);
        Marshal.Copy(ptr,arr,0,size);
        return arr;
    }
    
    private static Data fromBytes (byte[] arr)
    {
        Data result = new Data();
    
        int size = Marshal.SizeOf(result);
        IntPtr ptr = Marshal.AllocHGlobal(size);
        Marshal.Copy(arr,0,ptr,size);
        //error here? but it works.
        result = (Data)Marshal.PtrToStructure(ptr, typeof(Data));
        Marshal.FreeHGlobal(ptr);
    
        return result;
    }
        
    
    private void Copy(Data data)
    {
        for (int i = 0; i < data.u.Length; ++i)
        {
            // Debug.Log(i + "("+data.u[i] +"," +  data.v[i] + ")");
            uvPoint[i] = new Vector2(data.u[i], data.v[i]);
        }
            
    }
        
        
    private void OnApplicationQuit()
    {
        if (_udpClient != null) _udpClient.Close();
    }



}
