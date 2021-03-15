using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetupManagerScript : MonoBehaviour
{
    public Text aufforderungsText;
    public GameObject nummerText;
    public Button plusButton;
    public Button minusButton;
    public Button bestaetigenButton;
    public Button zurueckButton;
    public int state = 1;
    public int anzahlSpieler;
    public Player[] spieler;
    public InputField inputField;
    public Ressource[] ressourcen;
    public Button[] ressourcenButton;
    public bool[] vergebeneRessourcen;
    public bool[] clickedRessourcenButtons;
    public Color normalColor;
    public Color highlightColor;
    public Color selectedColor;
    public Color pressedColor;
    public Color fontColor;
    public Font font;
    public int fontSize;
    // Start is called before the first frame update
    void Start()
    {
        state = 1;
        vergebeneRessourcen = new bool[ressourcen.Length];
        clickedRessourcenButtons = new bool[ressourcenButton.Length];
        for (int i = 0; i < ressourcenButton.Length; i++)
        {
            ressourcenButton[i].GetComponentInChildren<Text>(true).text = ressourcen[i].name;
            vergebeneRessourcen[i] = false;
            clickedRessourcenButtons[i] = false;
        }
        foreach (Button b in ressourcenButton)
        {
            var colors = b.colors;
            colors.selectedColor = selectedColor;
            colors.normalColor = normalColor;
            colors.pressedColor = pressedColor;
            colors.highlightedColor = highlightColor;
            b.colors = colors;
            b.GetComponentInChildren<Text>().color = fontColor;
            b.GetComponentInChildren<Text>().font = font;
            b.GetComponentInChildren<Text>().fontSize = fontSize;
        }
        var colorsB = bestaetigenButton.colors;
        colorsB.selectedColor = selectedColor;
        colorsB.normalColor = normalColor;
        colorsB.pressedColor = pressedColor;
        colorsB.highlightedColor = highlightColor;
        plusButton.colors = colorsB;
        plusButton.GetComponentInChildren<Text>().color = fontColor;
        plusButton.GetComponentInChildren<Text>().fontSize = fontSize;
        plusButton.GetComponentInChildren<Text>().font = font;
        minusButton.colors = colorsB;
        minusButton.GetComponentInChildren<Text>().color = fontColor;
        minusButton.GetComponentInChildren<Text>().fontSize = fontSize;
        minusButton.GetComponentInChildren<Text>().font = font;
        bestaetigenButton.colors = colorsB;
        bestaetigenButton.GetComponentInChildren<Text>().color = fontColor;
        bestaetigenButton.GetComponentInChildren<Text>().fontSize = fontSize;
        bestaetigenButton.GetComponentInChildren<Text>().font = font;
        zurueckButton.colors = colorsB;
        zurueckButton.GetComponentInChildren<Text>().color = fontColor;
        zurueckButton.GetComponentInChildren<Text>().fontSize = fontSize;
        zurueckButton.GetComponentInChildren<Text>().font = font;
        aufforderungsText.font = font;
        aufforderungsText.color = fontColor;
        inputField.GetComponentsInChildren<Text>()[0].font = font;
        inputField.GetComponentsInChildren<Text>()[0].fontSize = fontSize;
        inputField.GetComponentsInChildren<Text>()[0].color = fontColor;
        inputField.GetComponentsInChildren<Text>()[1].font = font;
        inputField.GetComponentsInChildren<Text>()[1].fontSize = fontSize;
        inputField.GetComponentsInChildren<Text>()[1].color = fontColor;
        nummerText.GetComponentInChildren<Text>().font = font;
        nummerText.GetComponentInChildren<Text>().color = fontColor;
        nummerText.GetComponentInChildren<Text>().fontSize = fontSize;
        UpdateState(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressZurueck()
    {
        state--;
        UpdateState(false);
    }
    public void PressPlus()
    {
        anzahlSpieler++;
        if (anzahlSpieler >= ressourcen.Length)
        {
            anzahlSpieler = ressourcen.Length;
            plusButton.gameObject.SetActive(false);
        }
        if (anzahlSpieler > 1)
        {
            minusButton.gameObject.SetActive(true);
        }
        nummerText.GetComponentInChildren<Text>().text = "" + anzahlSpieler;
    }
    public void PressMinus()
    {
        anzahlSpieler--;
        if (anzahlSpieler <= 1)
        {
            anzahlSpieler = 1;
            minusButton.gameObject.SetActive(false);
        }
        if (anzahlSpieler <= ressourcen.Length)
        {
            plusButton.gameObject.SetActive(true);
        }
        nummerText.GetComponentInChildren<Text>().text = "" + anzahlSpieler;
    }
    public void PressBestaetigen()
    {
        state++;
        UpdateState(true);
    }
    //Handelt die state logic _increase entscheidet ob es von einer loweren state kommt. 
    void UpdateState(bool _increase)
    {
        //Alles wird aussgestellt
        aufforderungsText.gameObject.SetActive(false);
        nummerText.gameObject.SetActive(false);
        plusButton.gameObject.SetActive(false);
        minusButton.gameObject.SetActive(false);
        inputField.gameObject.SetActive(false);
        bestaetigenButton.gameObject.SetActive(false);
        zurueckButton.gameObject.SetActive(false);
        foreach(Button b in ressourcenButton)
        {
            b.gameObject.SetActive(false);
            var colors = b.colors;
            colors.selectedColor = selectedColor;
            colors.normalColor = normalColor;
            colors.pressedColor = pressedColor;
            colors.highlightedColor = highlightColor;
            b.colors = colors;
        }
        
        
        //Erster Screen <- Zurück
        if (state < 1)
        {
            SceneManager.LoadScene("Startscreen");
        }
        //Erster Screen
        if (state == 1)
        {
            bestaetigenButton.gameObject.SetActive(true);
            zurueckButton.gameObject.SetActive(true);
            aufforderungsText.gameObject.SetActive(true);
            aufforderungsText.text = "Wie viele Spieler gibt es?";
            nummerText.gameObject.SetActive(true);
            nummerText.GetComponentInChildren<Text>().text = "" + anzahlSpieler;
            if (anzahlSpieler < ressourcen.Length)
            {
                plusButton.gameObject.SetActive(true);
            }
            if (anzahlSpieler > 1)
            {
                minusButton.gameObject.SetActive(true);
            }
        }
        //Spieleranzahl -> Bestätigen
        if (_increase && state == 2)
        {
            spieler = new Player[anzahlSpieler];
            for (int i = 0; i < spieler.Length; i++)
            {
                spieler[i] = new Player();
                spieler[i].name = "";
                spieler[i].ressourcen = new Ressource[0];
            }
        }
        //Spielernamen -> letzte Bestätigung
        if (state == 2 + anzahlSpieler && _increase)
        {
            foreach(Player p in spieler)
            {
                p.ressourcen = new Ressource[0];
            }
        }
        //Spielername -> Bestätigen
        if (state >= 3 && state <= 2 + anzahlSpieler && _increase)
        {
            spieler[state - 3].name = inputField.text;
        }
        //Spielernamen Eingeben
        if (state >= 2 && state <= 1 + anzahlSpieler)
        {
            zurueckButton.gameObject.SetActive(true);
            aufforderungsText.gameObject.SetActive(true);
            aufforderungsText.text = "Gebe Namen an für Spieler " + (state-1);
            inputField.gameObject.SetActive(true);
            inputField.text = spieler[state - 2].name;
            for (int i = 0; i < clickedRessourcenButtons.Length; i++)       //Notwendig falls der spieler von Ressourcen vergeben zu Spielernamen zurück wechselt
            {
                clickedRessourcenButtons[i] = false;
            }
        }
        //Ressourcen vergeben -> Bestätigen
        if (state >= 3 + anzahlSpieler && state <= 2 + anzahlSpieler * 2 && _increase)
        {
            bestaetigenButton.GetComponentInChildren<Text>().text = "Bestätigen";
            for (int i = 0; i < clickedRessourcenButtons.Length; i++)
            {
                if (clickedRessourcenButtons[i])
                {
                    vergebeneRessourcen[i] = true;
                    Player current = spieler[state - 3 - anzahlSpieler];
                    Ressource[] temp = new Ressource[current.ressourcen.Length + 1];
                    for (int j = 0; j < current.ressourcen.Length; j++)
                    {
                        temp[j] = current.ressourcen[j];
                    }
                    temp[temp.Length - 1] = ressourcen[i];
                    current.ressourcen = temp;
                    clickedRessourcenButtons[i] = false;
                }
            }
        }
        //Kann das spiel gestarted werden check
        if (_increase)
        {
            //Überprüfung ob alle ressourcen vergeben wurden
            bool spielStarten = true;
            for (int i = 0; i < ressourcen.Length; i++)
            {
                if (!clickedRessourcenButtons[i] && !vergebeneRessourcen[i])
                {
                    spielStarten = false;
                }
            }
            //Überprüfung ob jeder Spieler mindestens eine Ressource/Rolle hat
            for (int i = 0; i < spieler.Length; i++)
            {
                if (spieler[i].ressourcen.Length < 1)
                {
                    spielStarten = false;
                }
            }
            //Spiel wird gestartet
            if (spielStarten)
            {
                GameStart();
                return;
            }
        }
        //Extra Ressourcen sollen an Spieler Unverteilt gegeben werden?
        if (state == (2 + anzahlSpieler * 2))
        {
            bestaetigenButton.gameObject.SetActive(true);
            zurueckButton.gameObject.SetActive(true);
            aufforderungsText.gameObject.SetActive(true);
            aufforderungsText.text = "Unverteilte Rollen werden an neuen Spieler \"Unverteilt\" gegeben?";
            bestaetigenButton.GetComponentInChildren<Text>().text = "Spiel Starten";
        }
        //Verteilung der extra ressourcen an Unverteilt
        if (state == 3 + anzahlSpieler * 2 && _increase)
        {
            Player[] temp = new Player[spieler.Length + 1];
            for (int i = 0; i < spieler.Length; i++)
            {
                temp[i] = spieler[i];
            }
            temp[temp.Length - 1] = new Player();
            temp[temp.Length - 1].name = "Unverteilt";
            temp[temp.Length - 1].ressourcen = new Ressource[0];
            for (int i = 0; i < vergebeneRessourcen.Length; i++)
            {
                if (!vergebeneRessourcen[i])
                {
                    Ressource[] tempRessource = new Ressource[temp[temp.Length - 1].ressourcen.Length + 1];
                    for (int j = 0; j < temp[temp.Length - 1].ressourcen.Length; j++)
                    {
                        tempRessource[j] = temp[temp.Length - 1].ressourcen[j];
                    }
                    tempRessource[tempRessource.Length - 1] = ressourcen[i];
                    temp[temp.Length - 1].ressourcen = tempRessource;
                }
            }
            spieler = temp;
            GameStart();
        }
        //Ressourcen vergeben 
        if (state >= 2 + anzahlSpieler && state <= 1 + anzahlSpieler * 2)
        {
            bestaetigenButton.gameObject.SetActive(true);
            bestaetigenButton.GetComponentInChildren<Text>().text = "Bestätigen";
            zurueckButton.gameObject.SetActive(true);
            aufforderungsText.gameObject.SetActive(true);
            aufforderungsText.text = "Welche Rolle(n) soll Spieler \"" + spieler[state-2-anzahlSpieler].name + "\" übernehmen?";
            bool alleRollenVergeben = true;
            for (int i = 0; i < ressourcenButton.Length; i++)
            {
                clickedRessourcenButtons[i] = false;
                if (!vergebeneRessourcen[i])
                {
                    ressourcenButton[i].gameObject.SetActive(true);
                    alleRollenVergeben = false;
                }
                for (int j = 0; j < spieler[state - 2 - anzahlSpieler].ressourcen.Length; j++)
                {
                    string r = spieler[state - 2 - anzahlSpieler].ressourcen[j].name;
                    if (ressourcenButton[i].GetComponentInChildren<Text>().text == r)
                    {
                        ressourcenButton[i].gameObject.SetActive(true);
                        alleRollenVergeben = false;
                        var colors = ressourcenButton[i].colors;
                        colors.normalColor = pressedColor;
                        colors.selectedColor = selectedColor;
                        colors.highlightedColor = highlightColor;
                        clickedRessourcenButtons[i] = true;
                        vergebeneRessourcen[i] = false;
                        ressourcenButton[i].colors = colors;
                    } 
                }
            }
            if (alleRollenVergeben)
            {
                aufforderungsText.text = "Jeder Spieler muss mindestens eine Rolle haben!";
            }
            spieler[state - 2 - anzahlSpieler].ressourcen = new Ressource[0];
            //Test ob mindestens eine Rolle/Ressource ausgewählt wurde und der Bestätigen Button angezeigt werden kann
            bestaetigenButton.gameObject.SetActive(false);
            for (int i = 0; i < ressourcenButton.Length; i++)
            {
                if (clickedRessourcenButtons[i])
                {
                    bestaetigenButton.gameObject.SetActive(true);
                }
            }
        }
    }

    public void PressResource(int _ressource)
    {
        var colors = ressourcenButton[_ressource].colors;
        if (clickedRessourcenButtons[_ressource])
        {
            colors.normalColor = normalColor;
            colors.selectedColor = selectedColor;
            clickedRessourcenButtons[_ressource] = false;
        } else
        {
            colors.normalColor = pressedColor;
            colors.selectedColor = colors.normalColor;
            clickedRessourcenButtons[_ressource] = true;
        }
        ressourcenButton[_ressource].colors = colors;
        //Test ob mindestens eine Rolle/Ressource ausgewählt wurde und der Bestätigen Button angezeigt werden kann
        bestaetigenButton.gameObject.SetActive(false);
        for (int i = 0; i < ressourcenButton.Length; i++)
        {
            if (clickedRessourcenButtons[i])
            {
                bestaetigenButton.gameObject.SetActive(true);
            }
        }
        //Test ob das Spiel schon gestartet werden kann um den Text des Bestätigen Buttons zu ändern
        bestaetigenButton.GetComponentInChildren<Text>().text = "Bestätigen";
        for (int i = 0; i < ressourcenButton.Length; i++)
        {
            if (!clickedRessourcenButtons[i] && !vergebeneRessourcen[i])
            {
                return;
            }
        }
        bestaetigenButton.GetComponentInChildren<Text>().text = "Spiel Starten";
    }
    public void InputFieldValueChanged()
    {
        if (inputField.text.Length > 0) 
        {
            bestaetigenButton.gameObject.SetActive(true);
        } else 
        {
            bestaetigenButton.gameObject.SetActive(false);
        }
    }
    public void GameStart()
    {
        bestaetigenButton.gameObject.SetActive(false);
        zurueckButton.gameObject.SetActive(false);
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("Spiel");
    }

}
