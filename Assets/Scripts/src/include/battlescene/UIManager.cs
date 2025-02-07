using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI[] textBoxes = new TextMeshProUGUI[6];
    public TextMeshProUGUI[] levelBoxes = new TextMeshProUGUI[6];
    public TextMeshProUGUI hpBox;
    public Battle battle;

    // Start is called before the first frame update
    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        for (int i = 0; i < 6; i++)
        {
            if (battle.PokemonOnField[i].exists)
            {
                textBoxes[i].enabled = true;
                textBoxes[i].text = battle.PokemonOnField[i].PokemonData.monName;
                levelBoxes[i].enabled = true;
                levelBoxes[i].text = "Lv. " + battle.PokemonOnField[i].PokemonData.level;
                if (i == 3)
                {
                    hpBox.enabled = battle.battleType == BattleType.Single;
                    hpBox.text = battle.PokemonOnField[3].PokemonData.HP.ToString().LeadingZero2() + " / "
                        + battle.PokemonOnField[3].PokemonData.hpMax.ToString().LeadingZero2();
                    battle.xpController.spriteRenderer.enabled = true;
                }
            }
            else
            {
                textBoxes[i].enabled = false;
                levelBoxes[i].enabled = false;
                if (i == 3)
                {
                    hpBox.enabled = false;
                    battle.xpController.spriteRenderer.enabled = false;
                }
            }
        }
    }
}
