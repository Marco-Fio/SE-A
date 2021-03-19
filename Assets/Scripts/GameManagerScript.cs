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
    public Text ressourceText;
    public Text methodeText;
    //Phasennamen
    //UI allgemein
    private string[] phasenNamen = { "Anforderungen", "Funktionen", "Synthese", "Lösungen" };
    public Text phasennameText;
    public Text wocheText;
    public Text spielernameText;
    public Text ressourcenText;
    public Text punktzahl;
    public Image rolle;
    public Text AnzeigeText;
    public Button musikButton;
    public Button bestaetigenButtonUI;

    //Images
    public Sprite[] rollenIcons;
    // Start is called before the first frame update
    void Start()
    {
        //Übernehmen der werte nach einem Setup
        if (FindObjectOfType<SetupManagerScript>())
        {
            spieler = FindObjectOfType<SetupManagerScript>().spieler;
            ressourcen = FindObjectOfType<SetupManagerScript>().ressourcen;
        }
        ShowUI(true);
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ZeigeRessourcenVerteilung()
    {
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
        ressourceText.gameObject.SetActive(true);
        methodeText.gameObject.SetActive(true);
        //Textzuweisung
        ressourceText.text = "" + ausgewaehlteRessource.momentaneAnzahl;
        methodeText.text = "" + ausgewaehlteMethode.verteilteRessourcen[rId];

        if (int.Parse(methodeText.text) > 0)
        {
            minusButton.gameObject.SetActive(true);
        }
        if (int.Parse(ressourceText.text) > 0)
        {
            plusButton.gameObject.SetActive(true);
        }
    }
    public void PressZurueckButton()
    {
        //UI Elemente werden ausgestellt
        ressourceText.gameObject.SetActive(false);
        methodeText.gameObject.SetActive(false); 
        minusButton.gameObject.SetActive(false);
        plusButton.gameObject.SetActive(false); 
        bestaetigenButton.gameObject.SetActive(false);
        zurueckButton.gameObject.SetActive(false);
        //Auswahl wird weggenommen
        ausgewaehlteMethode = null;
        ausgewaehlteRessource = null;
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
        ausgewaehlteRessource.momentaneAnzahl = int.Parse(ressourceText.text);
        ausgewaehlteMethode.verteilteRessourcen[rId] = int.Parse(methodeText.text);
        //UI elemente werden wieder ausgestellt
        ressourceText.gameObject.SetActive(false);
        methodeText.gameObject.SetActive(false);
        minusButton.gameObject.SetActive(false);
        plusButton.gameObject.SetActive(false);
        bestaetigenButton.gameObject.SetActive(false);
        zurueckButton.gameObject.SetActive(false);
        //Auswahl wird rausgenommen
        ausgewaehlteMethode = null;
        ausgewaehlteRessource = null;
    }
    public void PressPlusButton()
    {
        //Neue Anzeige
        ressourceText.text = "" + (int.Parse(ressourceText.text) - 1);
        methodeText.text = "" + (int.Parse(methodeText.text) + 1);
        //Checks ob Buttons noch angezeigt werden können
        if (int.Parse(methodeText.text) > 0)
        {
            minusButton.gameObject.SetActive(true);
        } else
        {
            minusButton.gameObject.SetActive(false);
        }
        if (int.Parse(ressourceText.text) > 0)
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
        ressourceText.text = "" + (int.Parse(ressourceText.text) + 1);
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
        if (int.Parse(ressourceText.text) > 0)
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
        ressourcenText.gameObject.SetActive(_show);
        rolle.gameObject.SetActive(_show);
        punktzahl.gameObject.SetActive(_show);
        AnzeigeText.gameObject.SetActive(_show);
        bestaetigenButtonUI.gameObject.SetActive(_show);
        musikButton.gameObject.SetActive(_show);
    }

    void UpdateUI()
    {
        if (phase > 0 && phase < phasenNamen.Length)
        {
            phasennameText.text = phasenNamen[phase - 1];
        }
        wocheText.text = "Woche: " + woche;

        spielernameText.text = spieler[momentanerSpieler].name;
        ressourcenText.text = "" + spieler[momentanerSpieler].ressourcen[momentaneRolle].momentaneAnzahl + "/" + spieler[momentanerSpieler].ressourcen[momentaneRolle].maxAnzahl;

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
}
