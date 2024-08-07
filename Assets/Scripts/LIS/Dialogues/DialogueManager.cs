using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using System;

public class DialogueManager : MonoBehaviour
{
    // ----- FIELDS ----- //
    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    //[SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialogueText;
    //[SerializeField] private TextMeshProUGUI displayNameText;
    //[SerializeField] private Animator portraitAnimator;
    //private Animator layoutAnimator;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    /*
    [Header("Audio")]
    [SerializeField] private DialogueAudioInfoSO defaultAudioInfo;
    [SerializeField] private DialogueAudioInfoSO[] audioInfos;
    [SerializeField] private bool makePredictable;
    private DialogueAudioInfoSO currentAudioInfo;
    private Dictionary<string, DialogueAudioInfoSO> audioInfoDictionary;
    private AudioSource audioSource;*/


    private Story currentStory;
    private bool dialogueIsPlaying;

    private bool canContinueToNextLine = false;

    private Coroutine displayLineCoroutine;

    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";
    /*
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    */
    private const string AUDIO_TAG = "audio";

    private const string DIALOGUE_TAG = "dialogue";
    private Dialogues currentDialogue;

    private const string CAMERA_TAG = "camera";

    private AudioSource currentAudioSource;
    private AudioClip currentAudioClip;



    private DialogueVariables dialogueVariables;
    private InkExternalFunctions inkExternalFunctions;

    private List<Choice> currentChoices = new List<Choice>();
    private bool displayChoices = false;
    private bool waitingForChoice = false;

    public bool DialogueIsPlaying { get => dialogueIsPlaying; set => dialogueIsPlaying = value; }
    // ----- FIELDS ----- //

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;

        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
        inkExternalFunctions = new InkExternalFunctions();

        //audioSource = this.gameObject.AddComponent<AudioSource>();
        //currentAudioInfo = defaultAudioInfo;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        // get the layout animator
        //layoutAnimator = dialoguePanel.GetComponent<Animator>();

        // get all of the choices text 
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }

        //InitializeAudioInfoDictionary();
    }

    /*
    private void InitializeAudioInfoDictionary()
    {
        audioInfoDictionary = new Dictionary<string, DialogueAudioInfoSO>();
        audioInfoDictionary.Add(defaultAudioInfo.id, defaultAudioInfo);
        foreach (DialogueAudioInfoSO audioInfo in audioInfos)
        {
            audioInfoDictionary.Add(audioInfo.id, audioInfo);
        }
    }

    private void SetCurrentAudioInfo(string id)
    {
        DialogueAudioInfoSO audioInfo = null;
        audioInfoDictionary.TryGetValue(id, out audioInfo);
        if (audioInfo != null)
        {
            this.currentAudioInfo = audioInfo;
        }
        else
        {
            Debug.LogWarning("Failed to find audio info for id: " + id);
        }
    }
    */

    private void Update()
    {
        // return right away if dialogue isn't playing
        if (!dialogueIsPlaying)
        {
            //Debug.Log("end");
            return;
        }
        else
        {
            // check choices
            if (currentChoices.Count > 0 && waitingForChoice)
            {
                CheckMakeChoice();
            }
        }

        // handle continuing to the next line in the dialogue when submit is pressed
        // NOTE: The 'currentStory.currentChoiecs.Count == 0' part was to fix a bug after the Youtube video was made
        if (canContinueToNextLine && !waitingForChoice
            )
        {
            Debug.Log("ici");
            ContinueStory();
        }
       
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        dialogueVariables.StartListening(currentStory);
        inkExternalFunctions.Bind(currentStory);

        // reset portrait, layout, and speaker
        
        //displayNameText.text = "???";
        /*
        portraitAnimator.Play("default");
        layoutAnimator.Play("right");*/

        ContinueStory();
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        dialogueVariables.StopListening(currentStory);
        inkExternalFunctions.Unbind(currentStory);
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        DialogueCamerasManager.instance.BackToPlayerCamera();

        // go back to default audio
        //SetCurrentAudioInfo(defaultAudioInfo.id);
    }

    public void ContinueStory()
    {
        Debug.Log("continue story");
        if (currentStory.canContinue && !displayChoices)
        {
            // set text for the current dialogue line
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            string nextLine = currentStory.Continue();

            // handle case where the last line is an external function
            if (nextLine.Equals("") && !currentStory.canContinue)
            {
                StartCoroutine(ExitDialogueMode());
            }
            // otherwise, handle the normal case for continuing the story
            else
            {
                // handle tags
                HandleTags(currentStory.currentTags);
                displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
            }
        }
        else
        {
            Debug.Log("else");
            if (!displayChoices)
            {
                //Debug.Log("exit");
                StartCoroutine(ExitDialogueMode());
            }
            else
            {
                Debug.Log("ici");
                DisplayChoices();
            }
            
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogueText.gameObject.SetActive(true);
        //displayNameText.gameObject.SetActive(true);

        currentAudioSource.PlayOneShot(currentAudioClip);

        // set the text to the full line, but set the visible characters to 0
        dialogueText.text = line;

        //dialogueText.maxVisibleCharacters = 0;
        // hide items while text is typing
        //continueIcon.SetActive(false);
        HideChoices();

        canContinueToNextLine = false;

        /*
        bool isAddingRichTextTag = false;

        
        // display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
            // if the submit button is pressed, finish up displaying the line right away
            if (InputManager.instance.GetSouthPressed()) 
            {
                dialogueText.maxVisibleCharacters = line.Length;
                break;
            }

            // check for rich text tag, if found, add it without waiting
            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            // if not rich text, add the next letter and wait a small time
            else
            {
                //PlayDialogueSound(dialogueText.maxVisibleCharacters, dialogueText.text[dialogueText.maxVisibleCharacters]);
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
        */

        // actions to take after the entire line has finished displaying
        //continueIcon.SetActive(true);

        yield return new WaitUntil(() => !currentAudioSource.isPlaying);
        yield return new WaitForSeconds(0.5f);

        CheckDisplayChoices();
        canContinueToNextLine = true;
    }

    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    
    private void HandleTags(List<string> currentTags)
    {
        // loop through each tag and handle it accordingly
        foreach (string tag in currentTags)
        {
            // parse the tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            // handle the tag
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    Debug.Log(tagKey + tagValue);
                    //displayNameText.text = tagValue;
                    SetCurrentAudioSource(tagValue);
                    break;
                case DIALOGUE_TAG:
                    currentDialogue = (Dialogues)Enum.Parse(typeof(Dialogues), tagValue);
                    break;
                case CAMERA_TAG:
                    DialogueCamerasManager.instance.ActivateCamera(currentDialogue, int.Parse(tagValue));
                    break;
                case AUDIO_TAG:
                    SetCurrentAudioClip(tagValue);
                    break;
                
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }

    private void SetCurrentAudioClip(string name)
    {
        currentAudioClip = AudioManager.instance.GetAudioClip(name);
    }

    private void SetCurrentAudioSource(string name)
    {
        currentAudioSource = AudioManager.instance.GetAudioSource(name);
    }

    private void CheckDisplayChoices()
    {
        Debug.Log("check display choices");

        if (displayChoices)
        {
            DisplayChoices();
        }
        else
        {
            currentChoices = currentStory.currentChoices;
            Debug.Log(currentChoices.Count);

            if (currentChoices.Count > 0)
            {
                displayChoices = true;
            }
        }
    }

    private void DisplayChoices()
    {
        Debug.Log("display choices");
        if (displayChoices)
        {
            waitingForChoice = true;

            dialogueText.gameObject.SetActive(false);
            //displayNameText.gameObject.SetActive(false);

            //List<Choice> currentChoices = currentStory.currentChoices;
            Debug.Log(currentChoices.Count);


            // defensive check to make sure our UI can support the number of choices coming in
            if (currentChoices.Count > choices.Length)
            {
                Debug.LogError("More choices were given than the UI can support. Number of choices given: "
                    + currentChoices.Count);
            }

            int index = 0;
            // enable and initialize the choices up to the amount of choices for this line of dialogue
            foreach (Choice choice in currentChoices)
            {
                choices[index].gameObject.SetActive(true);
                choicesText[index].text = choice.text;
                index++;
            }
            // go through the remaining choices the UI supports and make sure they're hidden
            for (int i = index; i < choices.Length; i++)
            {
                choices[i].gameObject.SetActive(false);
            }

            StartCoroutine(SelectFirstChoice());

            displayChoices = false;
        }  
    }

    private IEnumerator SelectFirstChoice()
    {
        // Event System requires we clear it first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void CheckMakeChoice()
    {
        Debug.Log("check make choice");
        int choiceIndex = -1; 
        
        if (canContinueToNextLine)
        {
            Debug.Log("can continue");
            if (InputManager.instance.GetEastPressed() && currentChoices.Count >= 1)
            {
                choiceIndex = 0;
            }
            else if (InputManager.instance.GetWestPressed() && currentChoices.Count >= 2)
            {
                choiceIndex = 1;
            }
            else if (InputManager.instance.GetNorthPressed() && currentChoices.Count >= 3)
            {
                choiceIndex = 2;
            }
            else if (InputManager.instance.GetSouthPressed() && currentChoices.Count == 4)
            {
                choiceIndex = 3;
            }

            if (choiceIndex != -1)
            {
                waitingForChoice = false;
                Debug.Log("Make choice " + choiceIndex);
                currentStory.ChooseChoiceIndex(choiceIndex);
                // NOTE: The below two lines were added to fix a bug after the Youtube video was made
                //InputManager.instance.GetSouthPressed(); // this is specific to my InputManager script
                ContinueStory();
            }     
        }
    }

    
    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }

    /*
    // This method will get called anytime the application exits.
    // Depending on your game, you may want to save variable state in other places.
    public void OnApplicationQuit()
    {
        dialogueVariables.SaveVariables();
    }*/

}
