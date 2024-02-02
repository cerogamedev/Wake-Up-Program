using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public Sprite surgery, surgerygood, surgerymiddle, surgerybad;
    public Sprite kindergarden0, kindergarden1, kindergarden2, kindergarden3;
    public Sprite bedroom, bedroommirror0, bedroommirror1, bedroommirror2, bedroomend, fog;
    public Sprite therapist;
    public TextAsset inkFile;
    public GameObject textBox;
    public GameObject customButton;
    public GameObject optionPanel;
    public bool isTalking = false;

    public bool isWriting = false;
    public Image BG;

    static Story story;
    TextMeshProUGUI message;
    List<string> tags;
    static Choice choiceSelected;

    public float waitingTime;
    public bool finisDialogue = false;

    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkFile.text);
        message = textBox.transform.GetComponent<TextMeshProUGUI>();
        tags = new List<string>();
        choiceSelected = null;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isWriting == false)
        {
            //Is there more to the story?
            if(story.canContinue )
            {
                AdvanceDialogue();
 

            }
            else
            {
                FinishDialogue();
            }

        }
        if (Input.GetKeyDown(KeyCode.F) && isWriting == true)
        {
            finisDialogue = true;
        }
    }

    // Finished the Story (Dialogue)
    private void FinishDialogue()
    {
        Debug.Log("End of Dialogue!");
    }

    // Advance through the story 
    public void AdvanceDialogue()
    {
        isWriting = true;
        string currentSentence = story.Continue();
        ParseTags();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }

    // Type out the sentence letter by letter and make character idle if they were talking
    IEnumerator TypeSentence(string sentence)
    {
        waitingTime = 0.05f;
        isWriting = true;
        WaitForSeconds wait = new WaitForSeconds(waitingTime);
        message.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            //SoundEffectManager.Instance.PlayButtonSound();
            if (letter.ToString() != "")
            {
                SoundEffectManager.Instance.PlayButtonSound();
            }
            message.text += letter;
            if (finisDialogue == true)
            {
                textBox.GetComponent<TextMeshProUGUI>().text = sentence;
                isWriting = false;
                finisDialogue = false;
                break;
            }
            yield return wait;
        }
        isWriting = false;
        yield return null;
        //Are there any choices?
        if (story.currentChoices.Count != 0)
        {
            StartCoroutine(ShowChoices());
        }
    }

    // Create then show the choices on the screen until one got selected
    IEnumerator ShowChoices()
    {
        Debug.Log("There are choices need to be made here!");
        List<Choice> _choices = story.currentChoices;

        for (int i = 0; i < _choices.Count; i++)
        {
            GameObject temp = Instantiate(customButton, optionPanel.transform);
            temp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _choices[i].text;
            if (_choices[i].text != "")
            {
                temp.AddComponent<Selectable>();
                temp.GetComponent<Selectable>().element = _choices[i];
                temp.GetComponent<Button>().onClick.AddListener(() => { temp.GetComponent<Selectable>().Decide(); });
            }

        }

        optionPanel.SetActive(true);

        yield return new WaitUntil(() => { return choiceSelected != null; });
        AdvanceFromDecision();

    }

    // Tells the story which branch to go to
    public static void SetDecision(object element)
    {
        choiceSelected = (Choice)element;
        story.ChooseChoiceIndex(choiceSelected.index);
    }

    // After a choice was made, turn off the panel and advance from that choice
    void AdvanceFromDecision()
    {
        optionPanel.SetActive(false);
        for (int i = 0; i < optionPanel.transform.childCount; i++)
        {
            Destroy(optionPanel.transform.GetChild(i).gameObject);
        }
        choiceSelected = null; // Forgot to reset the choiceSelected. Otherwise, it would select an option without player intervention.
        AdvanceDialogue();
    }

    /*** Tag Parser ***/
    /// In Inky, you can use tags which can be used to cue stuff in a game.
    /// This is just one way of doing it. Not the only method on how to trigger events. 
    void ParseTags()
    {
        tags = story.currentTags;
        foreach (string t in tags)
        {
            string prefix = t.Split(' ')[0];
            string param = t.Split(' ')[1];

            switch(prefix.ToLower())
            {
                case "anim":
                    SetAnimation(param);
                    break;
                case "color":
                    SetTextColor(param);
                    break;
                case "backround":
                    SetBackround(param);
                    break;
                case "music":
                    SetMusic(param);
                    break;
                case "end":
                    SceneManager.LoadScene("StartScreen");
                    break;
            }
        }
    }

    void SetBackround(string _backround)
    {
        
        switch (_backround)
        {
            case "normalquestion":
                BG.color = Color.white;
                break;
            case "black":
                BG.color = Color.black;
                break;
            case "therapist":
                BG.sprite = therapist;
                break;
            case "surgery":
                BG.sprite = surgery;
                break;
            case "surgerygood":
                BG.sprite = surgerygood;
                break;
            case "surgerymiddle":
                BG.sprite = surgerymiddle;
                break;
            case "surgerybad":
                BG.sprite = surgerybad;
                break;
            case "kindergarden0":
                BG.sprite = kindergarden0;
                break;
            case "kindergarden1":
                BG.sprite = kindergarden1;
                break;
            case "kindergarden2":
                BG.sprite = kindergarden2;
                break;
            case "kindergarden3":
                BG.sprite = kindergarden3;
                break;
            case "bedroom":
                BG.sprite = bedroom;
                break;
            case "bedroommirror0":
                BG.sprite = bedroommirror0;
                break;
            case "bedroommirror1":
                BG.sprite = bedroommirror1;
                break;
            case "bedroommirror2":
                BG.sprite = bedroommirror2;
                break;
            case "bedroomend":
                BG.sprite = bedroomend;
                break;
            case "fog":
                BG.sprite = fog;
                break;
            default:
                Debug.Log($"{_backround} is not available as a bg");
                break;
        }
    }
    void SetMusic(string _situaitonn)
    {

        switch (_situaitonn)
        {
            case "on":
                MusicManager.Instance.ChangeSituation(_situaitonn);
                break;
            case "off":
                MusicManager.Instance.ChangeSituation(_situaitonn);
                break;
            default:
                Debug.Log($"{_situaitonn} is not available as a bg");
                break;
        }
    }
    void SetAnimation(string _name)
    {
        CharacterScript cs = GameObject.FindObjectOfType<CharacterScript>();
        cs.PlayAnimation(_name);
    }
    void SetTextColor(string _color)
    {
        switch(_color)
        {
            case "red":
                message.color = Color.red;
                break;
            case "blue":
                message.color = Color.cyan;
                break;
            case "green":
                message.color = Color.green;
                break;
            case "white":
                message.color = Color.white;
                break;
            case "black":
                message.color = Color.black;
                break;
            default:
                Debug.Log($"{_color} is not available as a text color");
                break;
        }
    }

}
