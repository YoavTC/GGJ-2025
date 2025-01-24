using System.Collections;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public GameObject Destroyed_Version;
    public int maxHP = 100;
    public float timeout = 2f;
    private int currentHP;

    private Renderer originalRenderer;

    void Start()
    {
        currentHP = maxHP;

        // Pre-cache renderers
        originalRenderer = GetComponent<Renderer>();
    }

    void OnCollisionEnter(Collision collision)
    {
        float impactSpeed = collision.relativeVelocity.magnitude;
        int damage = Mathf.RoundToInt(impactSpeed * 10);
        TakeDamage(damage);
    }

    void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            BreakObject();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            BreakObject();
    }

    public void BreakObject()
    {
        GameObject destroyedInstance = Instantiate(Destroyed_Version, transform.position, transform.rotation);
        Renderer[] childRenderers = destroyedInstance.GetComponentsInChildren<Renderer>();
        Destroy(gameObject);
        if (originalRenderer && originalRenderer.material != null)
        {
            foreach (Renderer childRenderer in childRenderers)
            {
                if (childRenderer != null)
                {
                    childRenderer.material = originalRenderer.material;
                }
            }
        }
        StartCoroutine(ActivateChunksAfterTimeout(destroyedInstance));
    }

    private IEnumerator ActivateChunksAfterTimeout(GameObject destroyedInstance)
    {
        yield return new WaitForSeconds(timeout);
        destroyedInstance.SetActive(false);
    }
}