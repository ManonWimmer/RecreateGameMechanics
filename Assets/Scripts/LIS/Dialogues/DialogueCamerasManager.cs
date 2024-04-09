using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum Dialogues
{
    LightHouse01,
    Test
}

[Serializable]
public class SerializableTuple<T1, T2>
{
    public T1 Item1;
    public T2 Item2;

    public SerializableTuple(T1 item1, T2 item2)
    {
        Item1 = item1;
        Item2 = item2;
    }

    public Tuple<T1, T2> ToTuple()
    {
        return new Tuple<T1, T2>(Item1, Item2);
    }
}

public class DialogueCamerasManager : MonoBehaviour
{
    // ----- FIELDS ----- //
    public static DialogueCamerasManager instance;

    [SerializeField] Camera _playerCamera;

    [SerializeField] List<SerializableTuple<Dialogues, List<Camera>>> _serializedListDialogueAndCams = new List<SerializableTuple<Dialogues, List<Camera>>>();
    private List<Tuple<Dialogues, List<Camera>>> listDialogueAndCams = new List<Tuple<Dialogues, List<Camera>>>();

    private Camera _currentlyActivatedCamera;
    // ----- FIELDS ----- //

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Create tuple
        foreach (var tuple in _serializedListDialogueAndCams)
        {
            Tuple<Dialogues, List<Camera>> newTuple = tuple.ToTuple();
            listDialogueAndCams.Add(newTuple);
            Debug.Log($"Item1: {newTuple.Item1}, Item2: {newTuple.Item2}");
        }

        BackToPlayerCamera();

        // ----- TESTS ----- //
        /*
        ActivateCamera(Dialogues.LightHouse01, 0);
        ActivateCamera(Dialogues.LightHouse01, 1);
        ActivateCamera(Dialogues.LightHouse01, 2);
        ActivateCamera(Dialogues.Test, 0);
        */
        // ----- TESTS ----- //
    }


    public void ActivateCamera(Dialogues dialogue, int index)
    {
        Debug.Log("Activate camera");
        foreach (var tuple in listDialogueAndCams)
        {
            if (tuple.Item1 == dialogue)
            {
                Debug.Log("Found dialogue");
                if (tuple.Item2.Count - 1 >= index)
                {
                    Debug.Log("Found camera at index");
                    _currentlyActivatedCamera.enabled = false;
                    tuple.Item2[index].enabled = true;
                    _currentlyActivatedCamera = tuple.Item2[index];
                }
                else
                {
                    Debug.Log("Index too high to find camera");
                }
            }
            else
            {
                Debug.Log("Didn't find dialogue");
            }
        }
        
    }

    public void BackToPlayerCamera() // need to be called at exit dialogue
    {
        _playerCamera.enabled = true;
        _currentlyActivatedCamera = _playerCamera;
        DesactivateAllDialogueCameras();
    }

    public void DesactivateAllDialogueCameras()
    {
        foreach (var tuple in listDialogueAndCams)
        {
            foreach (Camera cam in tuple.Item2)
            {
                //Debug.Log(cam.name);
                cam.enabled = false;
            }
        }
    }
}
