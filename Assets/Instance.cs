using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instance : MonoBehaviour
{

    [SerializeField] GameObject charExp;
    void Start()
    {
        for(int i=0;i<10000;i++)
        {
            GameObject gobCopy = Instantiate(charExp);
            float x = Random.Range(-100f, 100f);
            float z = Random.Range(-100f, 100f);

            gobCopy.transform.position = new Vector3(x, 0, z);
            gobCopy.SetActive(true);
            CharacterMove._this.AddTransform(gobCopy.transform);
        }
    }

    
}
