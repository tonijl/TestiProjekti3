using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{

    public GameObject[] weapons;

    private int currentWeapon = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwitchWeapon(currentWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(1);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if(scroll > 0f)
        {
            SwitchWeapon((currentWeapon + 1) % weapons.Length);
        }
        else if (scroll < 0f)
        {
            SwitchWeapon(currentWeapon - 1 + weapons.Length % weapons.Length);
        }


    }

    void SwitchWeapon(int index)
    {
        if(index < 0 || index >= weapons.Length)
        {
            return;
        }

        currentWeapon = index;

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == currentWeapon);
        }
    }



}
