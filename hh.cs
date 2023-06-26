using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hh : MonoBehaviour
{

    /*public List<GameObect> floodList;

    public void AddObjectsToContainer()
    {
        // Assign numbers to items in the list
        int countToIsland = 1;
        foreach (GameObject item in floodList)
        {
            item.GetComponent<ItemScript>().countToIsland = countToIsland;
            countToIsland++;
        }

        // Instantiate a certain number of items from the floodDeck list
        for (int i = 0; i < numberOfObjectsToAdd; i++)
        {
            int randomIndex = Random.Range(0, floodDeck.Count);
            GameObject objectToAdd = floodDeck[randomIndex];

            GameObject clone = Instantiate(objectToAdd);
            clone.transform.SetParent(holder.transform, false);

            // Perform actions based on matching indices
            if (objectToAdd.GetComponent<ItemScript>().countToIsland == 1)
            {
                // Deactivate the corresponding island card
                islandCards[i].SetActive(false);
                // Set countToIsland to 1
                objectToAdd.GetComponent<ItemScript>().countToIsland = 1;
            }
            else if (objectToAdd.GetComponent<ItemScript>().countToIsland == 0)
            {
                // Deactivate the corresponding sunken card
                sunkenCards[i].SetActive(false);
                // Remove the matching card from the floodDeck list
                floodDeck.RemoveAt(randomIndex);
            }
        }
    }*/
}
