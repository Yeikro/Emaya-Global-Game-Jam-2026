using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    public int damage = 6;
    public GameObject warning;
    public GameObject ray;
    public float warningDuration = 1f;
    public float rayDuration = 0.5f;
    public Collider col;

    void Start()
    {
        StartCoroutine(WarningAndStrike());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerLife player))
        {
            player.GetDamage(damage, Vector3.zero);
        }
    }

    IEnumerator WarningAndStrike()
    {
        warning.SetActive(true);
        yield return new WaitForSeconds(warningDuration);
        col.enabled = true;
        warning.SetActive(false);
        ray.SetActive(true);
        yield return new WaitForSeconds(rayDuration);
        ray.SetActive(false);
        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
