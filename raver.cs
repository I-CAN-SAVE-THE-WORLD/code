using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class raver : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public float displayDuration = 3f;

    int totalPlayerCount;

    private ScriptiousMaximus SC;
    int cpIndex;

    private void Start()
    {
        SC = FindObjectOfType<ScriptiousMaximus>();

        cpIndex = SC.currentPlayerIndex;
        totalPlayerCount = SC.playerCount;
    }

    private void Update()
    {
        if (cpIndex != SC.currentPlayerIndex)
        {
            cpIndex = SC.currentPlayerIndex;
            tmp.text = "It's Player " + (cpIndex + 1) + "'s turn";
            StartCoroutine(DisplayText());
        }
    }

    private IEnumerator DisplayText()
    {
        tmp.gameObject.SetActive(true);
        yield return new WaitForSeconds(displayDuration);
        tmp.gameObject.SetActive(false);
    }
}
