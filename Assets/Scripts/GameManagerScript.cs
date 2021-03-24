using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public int woche = 0;
    public int phase = 0;
    public Player[] spieler;
    public Ressource[] ressourcen;
    private int momentanerSpieler = 0;
    private int momentaneRolle = 0;
    //UI elemente zum ressourcenverteilen
    public Ressource ausgewaehlteRessource;
    public MethodScript ausgewaehlteMethode;
    public Button plusButton;
    public Button minusButton;
    public Button bestaetigenButton;
    public Button zurueckButton;
    public Button ressourceButton;
    public Text methodeText;
    //Phasennamen
    //UI allgemein
    private string[] phasenNamen = { "Anforderungen", "Funktionen", "Synthese", "Lösungen" };
    public Text phasennameText;
    public Text wocheText;
    public Text spielernameText;
    public Text punktzahl;
    public Image rolle;
    public Text AnzeigeText;
    public Button musikButton;
    public Button bestaetigenButtonUI;

    //Images
    public Sprite[] rollenIcons;
    public Sprite ressourceButtonUp;
    public Sprite ressourceButtonDown;

    // Start is called before the first frame update
    void Start()
    {
        ausgewaehlteRessource = null;
        ausgewaehlteMethode = null;
        //Übernehmen der werte nach einem Setup
        if (FindObjectOfType<SetupManagerScript>())
        {
            spieler = FindObjectOfType<SetupManagerScript>().spieler;
            ressourcen = FindObjectOfType<SetupManagerScript>().ressourcen;
        }
        //TODO Phase wird eingeleitet hier
        foreach (Player p in spieler)
        {
            foreach (Ressource r in p.ressourcen)
            {
                r.momentaneAnzahl = r.maxAnzahl;
            }
        }
        UpdateWoche();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ZeigeRessourcenVerteilung()
    {
        ShowUI(false);
        bestaetigenButton.gameObject.SetActive(true);
        zurueckButton.gameObject.SetActive(true);
        //Ressourcen ID
        int rId = -1;

        for (int i = 0; i < ressourcen.Length; i++)
        {
            if (ressourcen[i].name == ausgewaehlteRessource.name)
            {
                rId = i;
            }
        }
        ressourceButton.gameObject.SetActive(true);
        methodeText.gameObject.SetActive(true);
        //Textzuweisung
        ressourceButton.GetComponentInChildren<Text>().text = "" + ausgewaehlteRessource.momentaneAnzahl;
        methodeText.text = "" + ausgewaehlteMethode.verteilteRessourcen[rId];

        if (int.Parse(methodeText.text) > 0)
        {
            minusButton.gameObject.SetActive(true);
        }
        if (int.Parse(ressourceButton.GetComponentInChildren<Text>().text) > 0)
        {
            plusButton.gameObject.SetActive(true);
        }
    }
    public void PressZurueckButton()
    {
        //UI Elemente werden ausgestellt
        ressourceButton.gameObject.SetActive(false);
        methodeText.gameObject.SetActive(false); 
        minusButton.gameObject.SetActive(false);
        plusButton.gameObject.SetActive(false); 
        bestaetigenButton.gameObject.SetActive(false);
        zurueckButton.gameObject.SetActive(false);
        //Auswahl wird weggenommen
        ausgewaehlteMethode = null;
        ausgewaehlteRessource = null;
        UpdateUI();
        ShowUI(true);
    }
    public void PressBestaetigenButton()
    {
        //Ressourcen Id
        int rId = -1;
        for (int i = 0; i < ressourcen.Length; i++)
        {
            if (ressourcen[i].name == ausgewaehlteRessource.name)
            {
                rId = i;
            }
        }
        //Übernehmen der werte
        ausgewaehlteRessource.momentaneAnzahl = int.Parse(ressourceButton.GetComponentInChildren<Text>().text);
        ausgewaehlteMethode.verteilteRessourcen[rId] = int.Parse(methodeText.text);
        //UI elemente werden wieder ausgestellt
        ressourceButton.gameObject.SetActive(false);
        methodeText.gameObject.SetActive(false);
        minusButton.gameObject.SetActive(false);
        plusButton.gameObject.SetActive(false);
        bestaetigenButton.gameObject.SetActive(false);
        zurueckButton.gameObject.SetActive(false);
        //Auswahl wird rausgenommen
        ausgewaehlteMethode = null;
        ausgewaehlteRessource = null;
        ressourceButton.image.sprite = ressourceButtonUp;
        UpdateUI();
        ShowUI(true);
    }
    public void PressPlusButton()
    {
        //Neue Anzeige
        ressourceButton.GetComponentInChildren<Text>().text = "" + (int.Parse(ressourceButton.GetComponentInChildren<Text>().text) - 1);
        methodeText.text = "" + (int.Parse(methodeText.text) + 1);
        //Checks ob Buttons noch angezeigt werden können
        if (int.Parse(methodeText.text) > 0)
        {
            minusButton.gameObject.SetActive(true);
        } else
        {
            minusButton.gameObject.SetActive(false);
        }
        if (int.Parse(ressourceButton.GetComponentInChildren<Text>().text) > 0)
        {
            plusButton.gameObject.SetActive(true);
        } else
        {
            plusButton.gameObject.SetActive(false);
        }
    }
    public void PressMinusButton()
    {
        //Neue Anzeige
        ressourceButton.GetComponentInChildren<Text>().text = "" + (int.Parse(ressourceButton.GetComponentInChildren<Text>().text) + 1);
        methodeText.text = "" + (int.Parse(methodeText.text) - 1);
        //Checks ob Buttons noch angezeigt werden können
        if (int.Parse(methodeText.text) > 0)
        {
            minusButton.gameObject.SetActive(true);
        }
        else
        {
            minusButton.gameObject.SetActive(false);
        }
        if (int.Parse(ressourceButton.GetComponentInChildren<Text>().text) > 0)
        {
            plusButton.gameObject.SetActive(true);
        }
        else
        {
            plusButton.gameObject.SetActive(false);
        }
    }

    //Zeigt das UI an
    void ShowUI(bool _show)
    {
        phasennameText.gameObject.SetActive(_show);
        wocheText.gameObject.SetActive(_show);
        spielernameText.gameObject.SetActive(_show);
        ressourceButton.gameObject.SetActive(_show);
        rolle.gameObject.SetActive(_show);
        punktzahl.gameObject.SetActive(_show);
        AnzeigeText.gameObject.SetActive(_show);
        bestaetigenButtonUI.gameObject.SetActive(_show);
        musikButton.gameObject.SetActive(_show);
    }

    void UpdateUI()
    {
        if (phase > 0 && phase <= phasenNamen.Length)
        {
            phasennameText.text = phasenNamen[phase - 1];
        }
        wocheText.text = "Woche: " + woche;

        spielernameText.text = spieler[momentanerSpieler].name;
        ressourceButton.GetComponentInChildren<Text>().text = "" + spieler[momentanerSpieler].ressourcen[momentaneRolle].momentaneAnzahl + "/" + spieler[momentanerSpieler].ressourcen[momentaneRolle].maxAnzahl;

        //Ressourcen Id
        int rId = -1;
        for (int i = 0; i < ressourcen.Length; i++)
        {
            if (ressourcen[i].name == spieler[momentanerSpieler].ressourcen[momentaneRolle].name)
            {
                rId = i;
            }
        }
        if (rId > -1)
        {
            rolle.sprite = rollenIcons[rId];
        }
        punktzahl.text = "" + spieler[momentanerSpieler].punktzahl;
        //Anzeige wird leer gemacht
        AnzeigeText.text = "";
    }
    public void PressBestaetigenButtonUI()
    {
        ausgewaehlteRessource = null;
        ressourceButton.image.sprite = ressourceButtonUp;
        if (momentaneRolle >= spieler[momentanerSpieler].ressourcen.Length-1)
        {
            //Woche wird geupdated
            if (momentanerSpieler >= spieler.Length -1)
            {
                //TODO Abfrage ob phase gewechselt werden soll
                momentanerSpieler = 0;
                momentaneRolle = 0;
                UpdateWoche();
            }
            //spieler wird geupdated
            else
            {
                momentanerSpieler++;
                momentaneRolle = 0;
            }
        } 
        else
        //Rolle wird geupdated
        {
            momentaneRolle++;
        }
        UpdateUI();
    }
    //Wochenwechsel
    void UpdateWoche()
    {
        if (woche >= 7)
        {
            GameOver();
            return;
        }
        woche++;
        foreach (Player p in spieler)
        {
            foreach (Ressource r in p.ressourcen)
            {
                r.momentaneAnzahl = r.maxAnzahl;
            }
        }
        UpdateUI();
        WochenEreignis();
    }
    void WochenEreignis()
    {
        int rnd = Random.Range(0,12);
        switch(rnd)
        {
            case 0: Debug.Log("Wochenereigniss 1"); break;
            case 1: Debug.Log("Wochenereigniss 2"); break;
            case 2: Debug.Log("Wochenereigniss 3"); break;
            case 3: Debug.Log("Wochenereigniss 4"); break;
            case 4: Debug.Log("Wochenereigniss 5"); break;
            case 5: Debug.Log("Wochenereigniss 6"); break;
            case 6: Debug.Log("Wochenereigniss 7"); break;
            case 7: Debug.Log("Wochenereigniss 8"); break;
            case 8: Debug.Log("Wochenereigniss 9"); break;
            case 9: Debug.Log("Wochenereigniss 10"); break;
            case 10: Debug.Log("Wochenereigniss 11"); break;
            case 11: Debug.Log("Wochenereigniss 12"); break;
            default: Debug.LogError("Kein Wochenereigniss von 0-11 ausgewählt"); break;
        }
        UpdateUI();
        ShowUI(true);
    }

    public void PressRessource()
    {
        
        if (ausgewaehlteRessource == null)
        {
            ausgewaehlteRessource = spieler[momentanerSpieler].ressourcen[momentaneRolle];
            ressourceButton.image.sprite = ressourceButtonDown;
        } else
        {
            ausgewaehlteRessource = null;
            ressourceButton.image.sprite = ressourceButtonUp;
        }
        if (ausgewaehlteMethode != null && ausgewaehlteRessource != null)
        {
            ZeigeRessourcenVerteilung();
        }

    }
    void GameOver()
    {
        Debug.Log("Game Over!");
        ShowUI(false);
    }

    public void SelectMethod(MethodScript _methodScript)
    {
        ausgewaehlteMethode = _methodScript;
        if (ausgewaehlteMethode != null && ausgewaehlteRessource != null)
        {
            ZeigeRessourcenVerteilung();
        }
    }
}
