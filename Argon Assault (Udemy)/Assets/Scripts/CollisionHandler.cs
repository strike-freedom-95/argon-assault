using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] GameObject shipPart;
    [SerializeField] GameObject explosionFX;

    GameObject parentGameObject;

    private void Start()
    {
        parentGameObject = GameObject.FindWithTag("Spawn");
    }

    private void OnTriggerEnter(Collider other)
    {
        Invoke("Reloadlevel", loadDelay);
        Debug.Log(gameObject.name + " triggered with " + other.name);
        ShipDeathSequence();
        GetComponent<PlayerControl>().enabled = false;        
    }

    void Reloadlevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void ShipDeathSequence()
    {
        GetComponent<PlayerControl>().SetLasersActive(false);
        PlayParticles();
        GetComponent<MeshRenderer>().enabled = false;
        shipPart.gameObject.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<PlayerControl>() .enabled = false;
    }

    public void PlayParticles()
    {
        GameObject instance = Instantiate(explosionFX, transform.position, Quaternion.identity);
        instance.transform.parent = parentGameObject.transform;
    }
}
