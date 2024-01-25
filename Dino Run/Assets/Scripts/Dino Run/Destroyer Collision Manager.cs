using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerCollisionManager : MonoBehaviour
{
    public GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;

       if (other.gameObject.CompareTag("Destroyable"))
        {
            //Debug.Log("Destroy");

            gameManager.DestroyFloor(go);
        }
    }
}
