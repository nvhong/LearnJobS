using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CharaterManager : MonoBehaviour
{
    bool isAync;
    private void OnEnable()
    {
        isAync = true;
    }

    private void OnDisable()
    {
        isAync = false;
    }

}


