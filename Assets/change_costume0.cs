using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets._2D {
    public class change_costume0 : MonoBehaviour {

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
                other.gameObject.GetComponent<PlatformerCharacter2D>().CostumeChange(0);
    
    }
    }
}