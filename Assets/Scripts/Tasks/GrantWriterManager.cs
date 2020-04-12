using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class GrantWriterManager : MonoBehaviour
{
    public GameObject mainPanel;
    public Player player;

    private List<string> activeVerbs;
    private List<string> adjectives;
    private List<string> adverbs;
    private List<string> conjunctions;
    private List<string> directObjects;
    private List<string> interjections;
    private List<string> passiveVerbs;
    private List<string> prepositionAction;
    private List<string> prepositions;
    private List<string> subjectsResearchers;
    private List<string> subjectsSubjects;

    private int sentencesLeftInParagraph;
    private string sentence;
    private int currentChar;

    private int length;
    
    public Text grantText;
    public Text successRateText;
    public Text acceptanceText;
    private float acceptanceTextTimer = 0f;

    public ScrollRect scrollRect;
    private float maxContentSize = 0;

    private float baseGrantPayout = 5;
    private float grantPayoutMultiplier = 1;
    public float getGrantPayout() {return baseGrantPayout * grantPayoutMultiplier;}

    void Awake()
    {
        
        activeVerbs = System.IO.File.ReadLines("Assets\\Text\\Active_Verbs.txt").ToList();
        adjectives = System.IO.File.ReadLines("Assets\\Text\\Adjectives.txt").ToList();
        adverbs = System.IO.File.ReadLines("Assets\\Text\\Adverbs.txt").ToList();
        conjunctions = System.IO.File.ReadLines("Assets\\Text\\Conjunctions.txt").ToList();
        directObjects = System.IO.File.ReadLines("Assets\\Text\\Direct_Objects.txt").ToList();
        interjections = System.IO.File.ReadLines("Assets\\Text\\Interjections.txt").ToList();
        passiveVerbs = System.IO.File.ReadLines("Assets\\Text\\Passive_Verbs.txt").ToList();
        prepositionAction = System.IO.File.ReadLines("Assets\\Text\\Preposition_Action.txt").ToList();
        prepositions = System.IO.File.ReadLines("Assets\\Text\\Prepositions.txt").ToList();
        subjectsResearchers = System.IO.File.ReadLines("Assets\\Text\\Subjects(Researchers).txt").ToList();
        subjectsSubjects = System.IO.File.ReadLines("Assets\\Text\\Subjects(Subjects).txt").ToList();

        sentence = "";
        currentChar = 0;
        sentencesLeftInParagraph = UnityEngine.Random.Range(2,4);

    }

    void Update()
    {
        if(Input.anyKeyDown) {
            addCharacter();
        }

        if (acceptanceTextTimer < 0) {
            acceptanceText.text = "";
        } else {
            acceptanceTextTimer -= Time.deltaTime;
        }

        if (maxContentSize != scrollRect.content.rect.y) {
            //print("Attempting to auto scroll.");
            maxContentSize = scrollRect.content.rect.y;
            scrollRect.verticalNormalizedPosition = 0;
        }

    }

    public void activate() {
        this.gameObject.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void deactivate() {
        this.gameObject.SetActive(false);
        mainPanel.SetActive(true);
    }

    private void addCharacter() {

        if (currentChar == sentence.Length) {
             sentence = getNewSentence(false);
             currentChar = 0;
        }

        grantText.text += sentence[currentChar];
        currentChar++;
        successRateText.text = "Success Chance: " + (getSuccessChance()*100f).ToString("00.00") + "%";

    }

    public void submitGrant() {

        if (UnityEngine.Random.Range(0f,1f) < getSuccessChance()) {
            acceptanceText.text = "Grant accepted! +" + getGrantPayout().ToString("C2");
            player.addResource(Player.PlayerResource.money, getGrantPayout());
        } else {
            acceptanceText.text = "Grant rejected...";
        }
        acceptanceTextTimer = 5f;

        grantText.text = "";
        successRateText.text = "Success Chance: 00.00%";
        sentence = "";
        currentChar = 0;
        sentencesLeftInParagraph = UnityEngine.Random.Range(2,4);

    }

    private string getNewSentence(bool isCompoundComponent) {

        string newSentence = "";
        bool subjectIsActive = false;

        if (UnityEngine.Random.Range(0,4) == 0 && !isCompoundComponent)
            newSentence += interjections[UnityEngine.Random.Range(0,interjections.Count)] + " ";
        
        if (UnityEngine.Random.Range(0,3) == 0)
            newSentence += adjectives[UnityEngine.Random.Range(0,adjectives.Count)] + " ";

        if (UnityEngine.Random.Range(0,3) == 0) {
            newSentence += subjectsResearchers[UnityEngine.Random.Range(0,subjectsResearchers.Count)] + " ";
            subjectIsActive = true;
        } else newSentence += subjectsSubjects[UnityEngine.Random.Range(0,subjectsSubjects.Count)] + " ";

        if (subjectIsActive) {
            newSentence += activeVerbs[UnityEngine.Random.Range(0,activeVerbs.Count)] + " ";
            newSentence += directObjects[UnityEngine.Random.Range(0,directObjects.Count)] + " ";
        } else {
            newSentence += passiveVerbs[UnityEngine.Random.Range(0,passiveVerbs.Count)] + " ";
        }

        if (UnityEngine.Random.Range(0,3) == 0)
            newSentence += adverbs[UnityEngine.Random.Range(0,adverbs.Count)] + " ";

        if (UnityEngine.Random.Range(0,6) == 0) {
            newSentence += prepositions[UnityEngine.Random.Range(0,prepositions.Count)] + " ";

            if (UnityEngine.Random.Range(0,3) == 0) {
            newSentence += subjectsResearchers[UnityEngine.Random.Range(0,subjectsResearchers.Count)] + " ";          
            } else newSentence += subjectsSubjects[UnityEngine.Random.Range(0,subjectsSubjects.Count)] + " ";

            newSentence += prepositionAction[UnityEngine.Random.Range(0,prepositionAction.Count)] + " ";
        }

        if (!isCompoundComponent) {

            if (UnityEngine.Random.Range(0,7) == 0) {
                newSentence = newSentence.TrimEnd() + conjunctions[UnityEngine.Random.Range(0,conjunctions.Count)] + " ";
                newSentence += getNewSentence(true);
            }

            newSentence = newSentence.TrimEnd() + ".  ";

            sentencesLeftInParagraph--;
            if (sentencesLeftInParagraph == 0) {
                newSentence += "\n\n";
                sentencesLeftInParagraph = UnityEngine.Random.Range(2,10);
            }

            return char.ToUpper(newSentence[0]) + newSentence.Substring(1);

        }


        return newSentence;

    }

    private float getSuccessChance() {
        return (float)grantText.text.Length/((float)grantText.text.Length+200f);
    }

    public void increasePayout(float increase, IncreaseFunction increaseFunction) {
        if (increaseFunction == IncreaseFunction.add) baseGrantPayout += increase;
        else grantPayoutMultiplier *= increase;
    }

}