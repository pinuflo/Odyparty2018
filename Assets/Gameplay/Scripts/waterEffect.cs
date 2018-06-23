using UnityEngine;
using System.Collections;

public class waterEffect : MonoBehaviour {


        void Start()
        {
           
            this.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Background";
            this.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = -5;
        }
    
}
