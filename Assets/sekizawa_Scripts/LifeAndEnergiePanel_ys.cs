using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeAndEnergiePanel_ys : MonoBehaviour
{

    public GameObject[] lifePalen;
    public GameObject[] energiePanel;

    public void UpdateLife(int life)
    {
        for (int i=0;i<lifePalen.Length;i++)
        {
            if (i < life)
            {
                lifePalen[i].SetActive(true);
            }
            else
            {
                lifePalen[i].SetActive(false);
            }
        }
    }

    public void UpdateEnergie(int energie)
    {
        for (int i = 0; i < energiePanel.Length; i++)
        {
            if (i < energie)
            {
                energiePanel[i].SetActive(true);
            }
            else
            {
                energiePanel[i].SetActive(false);
            }
        }
    }

}
