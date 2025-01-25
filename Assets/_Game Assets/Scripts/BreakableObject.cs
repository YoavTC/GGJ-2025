using System.Collections;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    private GameObject intactObject;
    private GameObject fragmentedObject;
    private BoxCollider parentCollider;
    private Renderer intactChildRenderer;
    public int maxHP = 100;
    public float timeout = 2f;
    private int currentHP;
    public AudioClip breakSound;

    private Renderer originalRenderer;

    void Start()
    {
        intactObject = gameObject.transform.GetChild(0).gameObject;
        fragmentedObject = gameObject.transform.GetChild(1).gameObject;
        // Pre-cache renderers
        // disable the destroy version

        parentCollider = GetComponent<BoxCollider>();
        intactChildRenderer = intactObject.GetComponent<Renderer>();

        // Automatically size collider to child's renderer bounds
        parentCollider.center = intactChildRenderer.bounds.center - transform.position;
        parentCollider.size = intactChildRenderer.bounds.size;


        intactObject.SetActive(true);
        fragmentedObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

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
        // used for debug mode - break all walls
        //if (Input.GetKeyDown(KeyCode.F))
        //    BreakObject();
    }

    public void BreakObject()
    {
        //NO Need to spawn - we will use the sibling version attached
        //OLD//GameObject destroyedInstance = Instantiate(Destroyed_Version, transform.position, transform.rotation);
        //OLD//Debug.Log($"{transform.position}, {transform.localScale} vs new {destroyedInstance.transform.position}, {destroyedInstance.transform.localScale}");
        //OLD//// After instantiation, we set the local scale of the clone to match the source object's scale
        //OLD//destroyedInstance.transform.localScale = transform.localScale;

        //OLD//Renderer[] childRenderers = destroyedInstance.GetComponentsInChildren<Renderer>();
        parentCollider.enabled = false;
        intactObject.SetActive(false);
        fragmentedObject.SetActive(true);
        Renderer[] childRenderers = fragmentedObject.GetComponentsInChildren<Renderer>();

        if (intactChildRenderer && intactChildRenderer.material != null)
        {
            foreach (Renderer childRenderer in childRenderers)
            {
                if (childRenderer != null)
                {
                    childRenderer.material = intactChildRenderer.material;
                }
            }
        }
        if (breakSound != null) AudioManager.Instance.PlayAudioClip(breakSound);
        StartCoroutine(DeactivateChunksAfterTimeout(fragmentedObject));
    }

    private IEnumerator DeactivateChunksAfterTimeout(GameObject gameObject)
    {
        Debug.Log("Coroutine entered");
        yield return new WaitForSeconds(timeout);
        Debug.Log("Timeout completed");
        gameObject.SetActive(false);
    }

    // Static method to handle deactivation
}