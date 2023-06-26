using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomisedCardPlacement : MonoBehaviour
{
    public GameObject[] islandCards; // Array of game objects to be placed
    public GameObject[] sunkenCards;
    public Transform[] placementTransforms; // Array of transforms where objects can be placed
    public List<GameObject> floodDeck;
    public GameObject floodDeckdisplay;

    public GameObject holder; // Parent GameObject to destroy its children
    public float destroyInterval = 5f; // Time interval to destroy the children
    public List<GameObject> floodList; // List of GameObjects to add
    int numberOfObjectsToAdd = 2; // Number of objects to add into the container

    public List<GameObject> pilotBlocks;

    public List<GameObject> playerrs;
    string waterRiseTag = "watermeter";

    public GameObject objectToMove; // Object to be moved
    public List<Transform> targetPositions; // List of target positions (game objects)

    public GameObject waterMeterCard;
    public ScriptiousMaximus sm;


    public int currentIndex = 0; // Current index of the target position
    public bool shouldMove = false; // Flag to indicate if the object should move

    public int maxPositions;
    public int stopAt;

    public void Start()
    {
        PlaceObjectsRandomly();
        foreach (Transform obj in placementTransforms)
        {
            obj.transform.gameObject.SetActive(false);
        }

        objectToMove.transform.position = targetPositions[0].transform.position;
        

        playerrs[0].transform.position = islandCards[0].transform.position;
        playerrs[1].transform.position = islandCards[1].transform.position;
        //playerrs[2].transform.position = islandCards[2].transform.position;
        //playerrs[3].transform.position = islandCards[3].transform.position;
        //playerrs[4].transform.position = islandCards[4].transform.position;
        //playerrs[5].transform.position = islandCards[5].transform.position;

        StartCoroutine(DestroyChildrenRoutine());

        if (objectToMove == null || targetPositions.Count == 0)
        {
            Debug.LogError("Object to move or target positions are not assigned!");
        }

        sm = GetComponent<ScriptiousMaximus>();

        maxPositions = targetPositions.Count;
        stopAt = currentIndex;
    }

    public void Update()
    {
        if (objectToMove.transform.position == targetPositions[2].transform.position)
        {
            numberOfObjectsToAdd = 3;
        }

        if (objectToMove.transform.position == targetPositions[3].transform.position)
        {
            numberOfObjectsToAdd = 3;
        }

        if (objectToMove.transform.position == targetPositions[4].transform.position)
        {
            numberOfObjectsToAdd = 3;
        }

        if (objectToMove.transform.position == targetPositions[5].transform.position)
        {
            numberOfObjectsToAdd = 4;
        }

        if (objectToMove.transform.position == targetPositions[6].transform.position)
        {
            numberOfObjectsToAdd = 4;
        }

        if (objectToMove.transform.position == targetPositions[7].transform.position)
        {
            numberOfObjectsToAdd = 5;
        }

        if (objectToMove.transform.position == targetPositions[8].transform.position)
        {
            numberOfObjectsToAdd = 5;
        }

        if (objectToMove.transform.position == targetPositions[9].transform.position)
        {
            SceneManager.LoadScene("gameover");
        }

        if (shouldMove)
        {
            // Move the object towards the current target position
            MoveObject(objectToMove, targetPositions[currentIndex].position);
        }
    }



    private void PlaceObjectsRandomly()
    {
        // Check if the number of objects and placement transforms match
        if (islandCards.Length != placementTransforms.Length)
        {
            Debug.LogError("Number of objects and placement transforms do not match.");
            return;
        }

        // Randomly shuffle the placement transforms array
        ShuffleTransformsArray();

        // Place each object on the randomized placement transforms
        for (int i = 0; i < islandCards.Length; i++)
        {
            GameObject objectToPlace = islandCards[i];
            GameObject sunkenObjectsToPlace = sunkenCards[i];
            GameObject pilotThings = pilotBlocks[i];
            Transform placementTransform = placementTransforms[i];

            objectToPlace.transform.position = placementTransform.position;
            objectToPlace.transform.rotation = placementTransform.rotation;
            sunkenObjectsToPlace.transform.position = objectToPlace.transform.position;
            sunkenObjectsToPlace.transform.rotation = objectToPlace.transform.rotation;
            pilotThings.transform.position = objectToPlace.transform.position;
            pilotThings.transform.rotation = objectToPlace.transform.rotation;
            sunkenObjectsToPlace.SetActive(false);
            //pilotThings.SetActive(false);
        }
    }

    private void ShuffleTransformsArray()
    {
        // Fisher-Yates shuffle algorithm to randomly shuffle the placement transforms array
        for (int i = 0; i < placementTransforms.Length - 1; i++)
        {
            int randomIndex = Random.Range(i, placementTransforms.Length);
            Transform temp = placementTransforms[i];
            placementTransforms[i] = placementTransforms[randomIndex];
            placementTransforms[randomIndex] = temp;
        }
    }

    public void CheckWhichCardsToSink(int index)
    {
        if (floodDeckdisplay)
        {

        }
    }

    public void FloodCardAllocation(int index)
    {
        // if (remainingActions >= 5)
        if (sunkenCards[index] == islandCards[index])
        {
            islandCards[index].SetActive(false);
            sunkenCards[index].transform.position = islandCards[index].transform.position;
        }
    }

    public void ShoreUp(int index)
    {
        GameObject moveToBlock = sunkenCards[index];
        int currentDigit = sm.currentPlayerIndex;

        float distance = Vector3.Distance(sm.players[currentDigit].transform.position, moveToBlock.transform.position);
        float accessDistance = 2.5f;

        if (distance <= accessDistance) // use else if for the second condition
        {
            sunkenCards[index].SetActive(false);
            islandCards[index].SetActive(true);
        }


    }

    private IEnumerator DestroyChildrenRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(destroyInterval);
            DestroyChildren();
        }
    }

    private void DestroyChildren()
    {
        // Destroy all children of the holder GameObject
        foreach (Transform child in holder.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void AddObjectsToContainer()
    {
        int[] numberArray = new int[islandCards.Length];

        for (int i = 0; i < islandCards.Length; i++)
        {
            numberArray[i] = 2;
        }

        bool foundOne = false;
        for (int i = 0; i < numberArray.Length; i++)
        {
            if (numberArray[i] == 1)
            {
                foundOne = true;
                break;
            }
        }

        if (foundOne)
        {
            for (int i = 0; i < numberOfObjectsToAdd; i++)
            {
                int randomIndex = Random.Range(0, floodDeck.Count);
                GameObject objectToAdd = floodDeck[randomIndex];

                GameObject clone = Instantiate(objectToAdd);
                clone.transform.SetParent(holder.transform, false);

                islandCards[randomIndex].SetActive(false);
                sunkenCards[randomIndex].SetActive(false);
                numberArray[randomIndex]--;
                floodDeck.RemoveAt(randomIndex);
            }
        }
        else
        {
            // Randomly select objects from the list and add them to the container
            for (int i = 0; i < numberOfObjectsToAdd; i++)
            {
                int randomIndex = Random.Range(0, floodDeck.Count);
                GameObject objectToAdd = floodDeck[randomIndex];

                GameObject clone = Instantiate(objectToAdd);
                clone.transform.SetParent(holder.transform, false);

                islandCards[randomIndex].SetActive(false);
                sunkenCards[randomIndex].SetActive(true);
                numberArray[randomIndex]--;
            }
        }
    }

    private void MoveObject(GameObject obj, Vector3 targetPosition)
    {
        // Move the object towards the target position
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, targetPosition, Time.deltaTime * 5f);

        // Check if the object has reached the target position
        if (obj.transform.position == targetPosition)
        {
            shouldMove = false;
        }
    }

    public void StartMoving()
    {
        shouldMove = true;
    }

    public void ChangeWaterPosition()
    {
        currentIndex++;

        if (currentIndex == stopAt)
        {
            shouldMove = false;
        }
        else
        {
            shouldMove = true;
        }
    }
        
}
