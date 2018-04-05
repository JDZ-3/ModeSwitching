using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.HandInput;
using Meta;
using System;
using System.IO;

public class CameraSaver : MonoBehaviour {
    int counter = 0;
    HandsModule handManager;

    // Use this for initialization
    void Start () {
        handManager = FindObjectOfType<MetaContextBridge>().CurrentContext.Get<HandsModule>();
        handManager.OnHandEnterFrame += OnHandDataAppear;
    }

    private void OnHandDataAppear(HandData handData)
    {
        
    }

    // Update is called once per frame
    void Update () {
        if (handManager.RightHand == null && handManager.LeftHand == null) return;

        // Get hand position
        Vector3 screenPos = GameObject.Find("CompositeCamera").GetComponent<Camera>().WorldToViewportPoint(handManager.RightHand.Position);
        //Debug.Log("Pixel: " + screenPos);

        // Get frame
        Meta.Plugin.CameraApi.UpdateRgbFrame();
        Texture2D image = Meta.Plugin.CameraApi.GetRgbFrame();
        screenPos.x *= image.width;
        screenPos.y *= image.height;

        // For some reason, the RGB buffer is reversed. Fix it!
        byte[] rawPixels = image.GetRawTextureData();
        Array.Reverse(rawPixels);
        image.LoadRawTextureData(rawPixels);

        // Mark hand position
        image.SetPixel((int)screenPos.x, (int)screenPos.y, Color.green);
        
        // Convert to file format
        byte[] buffer = image.EncodeToPNG();
        BinaryWriter binaryWriter = new BinaryWriter(new FileStream(@"C:\Users\ruizlab\Desktop\images\image" + counter++ + ".png", FileMode.Create));
        binaryWriter.Write(buffer);
        binaryWriter.Close();
    }
}
