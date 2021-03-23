using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public int woche;
    public int phase;
    public Player[] spieler;
    public Ressource[] ressourcen;
    // Start is called before the first frame update
    void Start()
    {
        spieler = FindObjectOfType<SetupManagerScript>().spieler;
        ressourcen = FindObjectOfType<SetupManagerScript>().ressourcen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
