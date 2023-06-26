using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptiousMaximus : MonoBehaviour
{
    public List<GameObject> players; // Array of player game objects
    public int currentPlayerIndex; // Index of the current player
    public int actionsRemaining; // Number of actions remaining for the current player
    private bool isGameOver; // Flag to indicate if the game is over
    public int playerCount;
    public List<GameObject> playersCards;

    public List<GameObject> cards;
    public List<GameObject> cards2;

    public int pIndex;
    GameObject newObj;

    public GameObject[] islands;
    public float accessDistance = 2f;

    public RandomisedCardPlacement rcp;
    public Flood flood;

    public int pilotCount = 1;

    string waterRiseTag = "watermeter";

    private void Start()
    {
        currentPlayerIndex = 0; // Start with the first player
        playerCount = players.Count;
        actionsRemaining = 2;

        cards2 = new List<GameObject>(cards);

        rcp = GetComponent<RandomisedCardPlacement>();
        flood = GetComponent<Flood>();
    }



    private void Update()
    {
        if (cards.Count == 0)
        {
            cards.Clear();
            cards.AddRange(cards2);

            // Refill FloodCards with the contents of FloodCards2
            //FloodCards = new List<GameObject>(FloodCards2);
        }
    }

    private void PerformAction()
    {
        // Check if the current player has actions remaining
        if (actionsRemaining > 0)
        {
            // Perform an action for the current player
            Debug.Log("Player " + (currentPlayerIndex + 1) + " performs an action. Actions remaining: " + actionsRemaining);



            // Decrease the number of actions remaining
            actionsRemaining--;
        }
        else
        {
            PickingUpCards();
            // Move to the next player's turn or loop back to the first player
            currentPlayerIndex++;
            if (currentPlayerIndex >= players.Count)
            {
                rcp.AddObjectsToContainer();
                pilotCount = 1;
                currentPlayerIndex = 0; // Loop back to the first player if all players have taken a turn
            }

            // Reset the actions for the current player
            actionsRemaining = 2;

            Debug.Log("End of Player " + (currentPlayerIndex + 1) + "'s turn.");

            // Check if the lose condition is true
            /*if (IsGameOverConditionMet())
            {
                isGameOver = true;
                Debug.Log("Game Over");
            }*/
        }
    }

    /* private bool IsGameOverConditionMet()
     {
         if(flood.currentIndex == flood.maxPositions)
         {
             Application.Quit();
             Debug.Log("game quit");
         }
     }*/

    public void PickingUpCards()
    {
        int max = 6;
        int five = 5;

        if (playersCards[currentPlayerIndex].transform.childCount == five)
        {
            // Check if there are available cards to pick
            if (cards.Count > 0)
            {
                // Randomly select a card index
                int cardIndex = Random.Range(0, cards.Count);

                // Get the card prefab at the selected index
                GameObject cardPrefab = cards[cardIndex];

                // Instantiate a new card object
                GameObject newCardObject = Instantiate(cardPrefab);

                // Set the parent of the new card to the current player's cards container
                newCardObject.transform.SetParent(playersCards[currentPlayerIndex].transform, false);

                // Set the new card's local position and scale
                newCardObject.transform.localPosition = Vector3.zero;
                newCardObject.transform.localScale = Vector3.one;

                // Remove the picked card from the card list
                //cards.RemoveAt(cardIndex);
            }
        }

        if (playersCards[currentPlayerIndex].transform.childCount != max)
        {
            // Distribute 2 randomized cards
            for (int i = 0; i < 2; i++)
            {
                // Check if there are available cards to pick
                if (cards.Count > 0)
                {
                    // Randomly select a card index
                    int cardIndex = Random.Range(0, cards.Count);

                    // Get the card prefab at the selected index
                    GameObject cardPrefab = cards[cardIndex];

                    // Instantiate a new card object
                    GameObject newCardObject = Instantiate(cardPrefab);

                    // Set the parent of the new card to the current player's cards container
                    newCardObject.transform.SetParent(playersCards[currentPlayerIndex].transform, false);

                    // Set the new card's local position and scale
                    newCardObject.transform.localPosition = Vector3.zero;
                    newCardObject.transform.localScale = Vector3.one;

                    // Remove the picked card from the card list
                    //cards.RemoveAt(cardIndex);
                }
            }
        }

        // Check for the tagged object after card instantiation
        foreach (Transform child in playersCards[currentPlayerIndex].transform)
        {
            if (child.CompareTag(waterRiseTag))
            {
                // Move to the next target position and stop
                rcp.currentIndex++;
                rcp.shouldMove = true;
                rcp.stopAt = rcp.currentIndex + 1;

                // Remove the child object
                Destroy(child.gameObject);

                break;
            }
        }

        if (playersCards[currentPlayerIndex].transform.childCount == max)
        {
            Debug.Log("Maximum cards reached.");
        }
    }


    /*public void GivingCard(int index)
    {
        if (playersCards[index].transform.childCount <= 5)
        {
            playersCards[currentPlayerIndex].transform.SetParent(playersCards[index])
        }

        else
        {
            Debug.Log("full or some shit");
        }
    }*/

    public void DiscardCard(int index)
    {
        Destroy(cards[index]);
    }



    public void Move(int index)
    {
        if (index >= 0 && index < islands.Length)
        {
            GameObject moveToBlock = islands[index];

            int pilotCount = 1;

            float distance = Vector3.Distance(players[currentPlayerIndex].transform.position, moveToBlock.transform.position);

            if (players[currentPlayerIndex].CompareTag("explorer") && distance <= 2.5 && moveToBlock.transform.childCount <= 2) // use 3 instead of accessDistance
            {
                players[currentPlayerIndex].transform.position = moveToBlock.transform.position;

                Debug.Log("Moved to block: " + index);
                PerformAction();
            }


            else if (distance <= accessDistance && moveToBlock.transform.childCount <= 2) // use else if for the second condition
            {
                players[currentPlayerIndex].transform.position = moveToBlock.transform.position;

                Debug.Log("Moved to block: " + index);
                PerformAction();
            }
            else
            {
                Debug.Log("Cannot move to block: " + index);
            }
        }
        else
        {
            Debug.Log("Invalid block index: " + index);
        }
    }

    public void MoveWithPilot(int index)
    {
        if (index >= 0 && index < islands.Length)
        {
            GameObject moveToBlock = islands[index];

            float distance = Vector3.Distance(players[currentPlayerIndex].transform.position, moveToBlock.transform.position);

            if (distance <= 1000 && pilotCount > 0 && moveToBlock.transform.childCount <= 2)
            {
                players[currentPlayerIndex].transform.position = moveToBlock.transform.position;
                pilotCount--;

                Debug.Log("Moved to block: " + index);
                PerformAction();
            }
            else
            {
                Debug.Log("Cannot move to block: " + index);
            }
        }
        else
        {
            Debug.Log("Invalid block index: " + index);
        }
    }
}