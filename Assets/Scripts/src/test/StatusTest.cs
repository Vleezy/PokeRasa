using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusTest : MonoBehaviour
{
    public Battle battle;
    public Status status;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            status = (Status)(((int)status + 1) % 7);
        }
        battle.PokemonOnField[3].PokemonData.status = status;
    }
}
