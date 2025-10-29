using UnityEngine;

public class SInteractSpawning : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private GameObject mInteractabilityPrefab;
    private void OnTriggerEnter(Collider other) //this is for detecting when two objects touch or collide with eachother
    {
        if (other.gameObject.CompareTag("Player")) //Write what tag you want the gameobject to detect!!
        {
            Instantiate(mInteractabilityPrefab, new Vector3(0, 2 ,0), Quaternion.identity); //spawning whatever object you want!!
            Debug.Log("Player walked into the green box"); //change this to write whatever message you want!!  
        }
    }
}
