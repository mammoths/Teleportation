using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScenePlay : MonoBehaviour
{

    public Transform player;
    public ConstantForce token;

    private bool didEnd;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (!token && !didEnd)
        {

            didEnd = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (didEnd) { return; }

        token.force = -player.up * Physics.gravity.magnitude;
    }
}