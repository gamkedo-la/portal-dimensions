using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickUp : MonoBehaviour
{
    [SerializeField] float invulnTime = 3f;
    [SerializeField] float respawnTime = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(SetShield(other));
        }

    }

    private IEnumerator SetShield(Collider other)
    {
        other.gameObject.GetComponent<Player>().SetInvuln();
        SetState();

        yield return new WaitForSeconds(invulnTime);

        other.gameObject.GetComponent<Player>().SetInvuln();

        yield return new WaitForSeconds(respawnTime);

        SetState();
    }

    private void SetState()
    {
        MeshRenderer mesh = gameObject.GetComponent<MeshRenderer>();
        BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();

        mesh.forceRenderingOff = !mesh.forceRenderingOff;
        boxCollider.enabled = !boxCollider.enabled;
    }
}
