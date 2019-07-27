using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    #region Objects
    public Transform weapon;
    public LineRenderer lineRenderer;
    public PlayerController player;
    #endregion

    #region Properties
    Vector3 difference;
    float angle;
    bool m_shoot;
    int m_offset;
    float m_rayGunDamage = 100;
    Vector3 m_weaponOffset_Position;
    #endregion

    #region Start/Update
    void Start()
    {
        weapon = GetComponent<Transform>();
        m_offset = GetOffset();
    }

    // Update is called once per frame
    void Update()
    {
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        m_shoot = Input.GetButtonDown("Fire1");

        AdjustWeaponPosition();
        AdjustWeaponAngle();
        m_offset = GetOffset();

        if (m_shoot)
        {
            Shoot();
        }
    }
    #endregion

    #region Shoot
    void Shoot()
    {
        m_offset = GetOffset();
        RaycastHit2D hitInfo = Physics2D.Raycast(weapon.position, weapon.right * m_offset);

        if (hitInfo)
        {
            if (hitInfo.collider.tag == "Enemy")
            {
                Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                enemy.TakeDamage(m_rayGunDamage);
                LineRenderer ray = Instantiate(lineRenderer, weapon.position, Quaternion.identity);
                ray.SetPosition(0, weapon.position + weapon.right * 0.3f);
                ray.SetPosition(1, hitInfo.point);
            }
            if (hitInfo.collider.tag == "Ground")
            {
                LineRenderer ray = Instantiate(lineRenderer, weapon.position, Quaternion.identity);
                ray.SetPosition(0, weapon.position + weapon.right * 0.3f);
                ray.SetPosition(1, hitInfo.point);
            }
            // if the Ray hits the holder of the weapon
            if (hitInfo.collider.gameObject == weapon.parent.gameObject)
            {
                LineRenderer ray = Instantiate(lineRenderer, weapon.position, Quaternion.identity);
                ray.SetPosition(0, weapon.position + weapon.right * 0.3f);
                ray.SetPosition(1, weapon.position + weapon.right * 100 * m_offset);
            }
        }
        else
        {
            LineRenderer ray = Instantiate(lineRenderer, weapon.position, Quaternion.identity);
            ray.SetPosition(0, weapon.position + weapon.right * 0.3f);
            ray.SetPosition(1, weapon.position + weapon.right * 100 * m_offset);
        }
    }
    #endregion

    #region WeaponPosition/WeaponAngle/GetOffset

    #region AdjustWeaponPosition
    void AdjustWeaponPosition()
    {
        //angle = Mathf.Atan2(difference.y * m_offset, difference.x * m_offset);
        ////Debug.Log(difference);
        //Debug.Log(angle);
        //m_weaponOffset_Position = new Vector3(Mathf.Cos(angle) * 0.5f, Mathf.Sin(angle) * 0.5f, 0f);
        //weapon.position = player.transform.position + (m_weaponOffset_Position * m_offset);
    }
    #endregion

    #region AdjustWeaponAngle
    void AdjustWeaponAngle()
    {
        angle = Mathf.Atan2(difference.y * m_offset, difference.x * m_offset) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
    #endregion

    #region GetOffset
    int GetOffset()
    {
        if (player.m_FacingRight)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
    #endregion

    #endregion
}
