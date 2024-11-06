using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponStatusHandle : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] public Image StatusWeapon;

    public void ChangeStatus(bool isRange)
    {
        if (isRange)
        {
            StatusWeapon.sprite = sprites[1];
        }
        else
        {
            StatusWeapon.sprite = sprites[0];
        }
    }
}
