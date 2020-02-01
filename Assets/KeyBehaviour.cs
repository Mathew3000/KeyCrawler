using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    #region  Key init  
    // Start is called before the first frame update
    void Start()
    {
        
    }
    #endregion
    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player")
            Destroy(gameObject);

    }

}
