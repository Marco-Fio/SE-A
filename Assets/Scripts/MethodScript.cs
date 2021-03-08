using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MethodScript : MonoBehaviour
{
    //Anzahl der Stufe und erreichte Stufen
    public int stufen = 3;
    public int momentaneStufe = 0;

    public int[] tresholds1 = new int[] { 0, 0, 0, 0, 0, 0 };
    public int[] tresholds2 = new int[] { 0, 0, 0, 0, 0, 0 };
    public int[] tresholds3 = new int[] { 0, 0, 0, 0, 0, 0 };

    public int[] erreichteArbeitszeit = new int[] {0,0,0,0,0,0};
    public int[] verteilteRessourcen = new int[] {0,0,0,0,0,0};
    public List<string> report;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Bewertet die momentane Verteilung und erhöht stufe falls nötig
    void Evaluate()
    {
        //Arbeitszeit wird verteilt
        for (int i = 0; i < erreichteArbeitszeit.Length; i++)
        {
            erreichteArbeitszeit[i] += verteilteRessourcen[i];
        }
        //Check ob Stufe 1 erreicht wurde
        bool stufeErreicht = false;          //Hilfsbool
        for (int i = 0; i < tresholds1.Length; i++)
        {
            stufeErreicht = true;
            if (tresholds1[i] < erreichteArbeitszeit[i])
            {
                stufeErreicht = false;
                continue;
            }
        }
        if (stufeErreicht)
        {
            momentaneStufe = 1;
        }
        //Check ob Stufe 2 erreicht wurde
        for (int i = 0; i < tresholds2.Length; i++)
        {
            stufeErreicht = true;
            if (tresholds2[i] < erreichteArbeitszeit[i])
            {
                stufeErreicht = false;
                continue;
            }
        }
        if (stufeErreicht)
        {
            momentaneStufe = 2;
        }
        //Check ob Stufe 3 erreicht wurde
        for (int i = 0; i < tresholds3.Length; i++)
        {
            stufeErreicht = true;
            if (tresholds3[i] < erreichteArbeitszeit[i])
            {
                stufeErreicht = false;
                continue;
            }
        }
        if (stufeErreicht)
        {
            momentaneStufe = 3;
        }

        //Report wird verfasst 
        report = new List<string>();
    }
}
