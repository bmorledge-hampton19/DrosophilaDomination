using System.Collections.Generic;
using UnityEngine;

public enum GamePhase{
        research = 1,
        unity = 2,
        conquest = 3,
        exploration = 4
    }

public class GameManager : MonoBehaviour
{

    public GameObject mainPanel;

    public TabManager tabManager;
    public GameObject jarPrefab;

    public AddFliesManager addFliesManager;
    public FlySelectorManager flySelectorManager;
    
    public JarCustomizerManager jarCustomizerManager;

    public DialoguePopup dialoguePopup;

    public Storage storage;
    public PropertyDB propertyDB;

    public Player player;

    private GamePhase gamePhase;
    public GamePhase getGamePhase()=>gamePhase;
    public void advanceGamePhase(){gamePhase++;}

    public void createNewJar(){

        GameObject newJar = Instantiate(jarPrefab);
        JarManager newJarManager = newJar.GetComponent<JarManager>();
        
        newJarManager.mainPanel = mainPanel;

        newJarManager.storage = storage;
        newJarManager.traitDB = storage.traitDB;
        newJarManager.propertyDB = propertyDB;

        newJarManager.initializeJar();

        newJarManager.jarUIManager = tabManager.createNewJarUI();
        newJarManager.instatiateUIConnections();

        newJarManager.addFliesManager = addFliesManager;
        newJarManager.flySelectorManager = flySelectorManager;

        newJarManager.jarCustomizerManager = jarCustomizerManager;

        newJarManager.dialoguePopup = dialoguePopup;

    }

    void Awake() {
        gamePhase = GamePhase.research;
    }

    // Start is called before the first frame update
    void Start()
    {
        createNewJar();
        player.init(new Dictionary<Player.PlayerResource, float>{{Player.PlayerResource.money,20f}});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
