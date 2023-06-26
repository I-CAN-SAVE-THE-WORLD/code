using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newnew : MonoBehaviour
{
    public GameObject holder; // Parent GameObject to destroy its children
    public float destroyInterval = 5f; // Time interval to destroy the children
    public List<GameObject> objectList; // List of GameObjects to add
    public int numberOfObjectsToAdd = 5; // Number of objects to add into the container

    private void Start()
    {
        StartCoroutine(DestroyChildrenRoutine());
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
        // Randomly select objects from the list and add them to the container
        for (int i = 0; i < numberOfObjectsToAdd; i++)
        {
            int randomIndex = Random.Range(0, objectList.Count);
            GameObject objectToAdd = objectList[randomIndex];

            GameObject clone = Instantiate(objectToAdd);
            clone.transform.SetParent(holder.transform, false);
        }
    }
}
