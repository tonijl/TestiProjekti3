using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{

    public GameObject[] weapons;

    private int currentWeapon = 0;
    private int previousWeapon = -1;


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

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeapon(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchWeapon(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchWeapon(4);
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

    /*void SwitchWeapon(int index)
    {

        previousWeapon = currentWeapon;


        



        if(index < 0 || index >= weapons.Length)
        {
            return;
        }

        currentWeapon = index;

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == currentWeapon);

        }
    }*/

    void SwitchWeapon(int index)
    {
        if(index < 0 || index >= weapons.Length || index == currentWeapon)
        {
            return;
        }

        /*WeaponBase previous = weapons[currentWeapon].GetComponent<WeaponBase>();

        WeaponBase next = weapons[index].GetComponent<WeaponBase>();

        next.boostPower = previous.boostPower;
        next.boostDuration = previous.boostDuration;
        next.timerBar = previous.timerBar;*/

        previousWeapon = currentWeapon;
        currentWeapon = index;

        for(int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == currentWeapon);
        }



    }



}
