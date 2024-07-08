using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CW_CameraController : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] private Image _recImage;
    [SerializeField] Evereal.VideoCapture.VideoCapture _videoCapture;
    // ----- FIELDS ----- //

    private void Start()
    {
        _recImage.enabled = false;
    }

    public void UpdateCameraRecState(bool isRecording)
    {
        if (isRecording)
        {
            _recImage.enabled = true;
            _videoCapture.StartCapture();
        }
        else
        {
            _recImage.enabled = false;
            _videoCapture.StopCapture();
        }
    }

}
