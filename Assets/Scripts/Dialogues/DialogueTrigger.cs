using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] TextAsset _inkDialogue;

    private bool _playerInRange;

    // show visual cues comme bunker cat
    // ----- FIELDS ----- //

    private void Update()
    {
        
        if (_playerInRange && !DialogueManager.GetInstance().DialogueIsPlaying) // Le joueur est dans le trigger et le dialogue n'est pas déjà en cours
        {
            //showVisualCues.device = InputManager.GetInstance().GetDevice();
            //showVisualCues.ActivateCueForDevice(); // On affiche le visual cue

            if (InputManager.instance.GetSouthPressed())
            {
                DialogueManager.GetInstance().EnterDialogueMode(_inkDialogue);
            }
        }
        else // Le joueur n'est pas / plus dans le trigger
        {
            //showVisualCues.DesactivateAllCues(); // On cache le visual cue
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }
}
