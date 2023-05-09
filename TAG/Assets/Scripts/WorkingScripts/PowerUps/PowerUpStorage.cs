using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpStorage : MonoBehaviour
{
    public List<GameObject> equippedPowerUp;
    public bool PowerUpEquipped;

    private void Update()
    {
        if(equippedPowerUp.Count <= 0)
        {
            PowerUpEquipped = false;
        }

        else
        {
            PowerUpEquipped=true;
        }

        if(equippedPowerUp.Count >= 1)
        {
            Debug.LogError("More than one power up equipped");
        }
    }

    public void ExecuteCurrentPowerUp()
    {
        equippedPowerUp[0].GetComponent<Powerup>().Execute();
    }
}
