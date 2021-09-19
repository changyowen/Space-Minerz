using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIUpdate : MonoBehaviour
{
    public Text healthText_text;
    public Text[] resourcesTotalText_text;

    private void Update()
    {
        healthText_text.text = "" + PlayerProfile.playerHealth;

        for (int i = 0; i < resourcesTotalText_text.Length; i++)
        {
            resourcesTotalText_text[i].text = "" + PlayerProfile.resourcesTotal[i];
        }
    }
}
