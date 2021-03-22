using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symbol : MonoBehaviour
{
    public Color defautlColor = Color.grey;
    public Color wrongEggColor = Color.red;
    public Color wrongPlaceColor = Color.yellow;
    public Color rightEggColor = Color.green;

    public string myEgg;
    public string expectedEggName = "goodEgg";
    public Symbol[] symbols;

    public void changeColor(string eggName)
    {
        if(eggName == expectedEggName)
        {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", rightEggColor);
        }
        else
        {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", wrongEggColor);
            for(int i=0; i<symbols.Length; ++i)
            {
                if(symbols[i].expectedEggName == eggName)
                {
                    GetComponent<Renderer>().material.SetColor("_EmissionColor", wrongPlaceColor);
                    return;
                }
            }
        }
    }

    public bool CheckWin()
    {
        for (int i = 0; i < symbols.Length; ++i)
        {
            if (symbols[i].myEgg == null || symbols[i].expectedEggName != symbols[i].myEgg)
            {
                return false;
            }
        }
        if(myEgg == null || myEgg != expectedEggName)
        {
            return false;
        }
        return true;
    }

    public void setDefaultColor()
    {
        GetComponent<Renderer>().material.SetColor("_EmissionColor", defautlColor);
    }

}
