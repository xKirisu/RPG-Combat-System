using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    [SerializeField] GameObject CharacterInfoHandler;
    UnityEngine.UI.Image CharacterIcon;
    GameObject Bar1;
    GameObject Bar2;
    RectTransform HPFill;
    RectTransform MPFill;
    TextMeshProUGUI HPText;
    TextMeshProUGUI MPText;

    [SerializeField] GameObject SpellsPanelHandler;
    List<TextMeshProUGUI> SpellsTexts = new List<TextMeshProUGUI>();
    List<Button> SpellsButtons = new List<Button>();


    [SerializeField] TextMeshProUGUI TextInfo;

    void Start()
    {
        InitSpellPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTextInfo(string text)
    {
        TextInfo.text = text;
    }

    #region Spell Panel
    void InitSpellPanel()
    {
        SpellsTexts = SpellsPanelHandler.GetComponentsInChildren<TextMeshProUGUI>().ToList<TextMeshProUGUI>();
        SpellsButtons = SpellsPanelHandler.GetComponentsInChildren<Button>().ToList<Button>();
    }

    public void showSpellPanel(Move[] moves, Character unit)
    {
        SpellsPanelHandler.SetActive(true);

        for(int i = 0; i < SpellsTexts.Count && i < moves.Length; i++) 
        {
            SpellsTexts[i].name = moves[i].name;
            SpellsButtons[i].onClick.RemoveAllListeners(); 
            
            int index = i;
            SpellsButtons[index].onClick.AddListener(() => unit.ButtonPress(index));
        }
    }
    public void hideSpellPanel()
    {
        SpellsPanelHandler.SetActive(false);
    }

    #endregion
    #region Character Info Panel
    void InitCharacterInfoPanel()
    {

    }
    public void setCharacterInfoPanel(Sprite sprite, short hp, float hp_persentage, short mp, float mp_persentage)
    {

        HPText.text = hp.ToString("D4");
        MPText.text = mp.ToString("D4");
    }

    #endregion
}
