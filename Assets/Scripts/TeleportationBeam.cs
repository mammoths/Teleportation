using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeleportationBeam : MonoBehaviour
{

    public Valve.VR.EVRButtonId buttonId = Valve.VR.EVRButtonId.k_EButton_Axis0;

    public GameObject laserPrefab;
    public GameObject reticlePrefab;
    public Transform player;

    private Reticle reticle;
    private Laser laser;

    public float range = 20f;

    public Color enabledColor = Color.white;
    public Color disabledColor = Color.red;

    private SteamVR_TrackedObject controller;

    private RaycastHit target;
    private bool canTeleport;

    // Use this for initialization
    void Start()
    {

        GameObject laserObj = (GameObject)Instantiate(laserPrefab);
        GameObject reticleObj = (GameObject)Instantiate(reticlePrefab);

        laserObj.transform.SetParent(player);
        reticleObj.transform.SetParent(player);

        reticle = reticleObj.GetComponent<Reticle>();
        laser = laserObj.GetComponent<Laser>();

        controller = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
       
        laser.gameObject.SetActive(false);
        reticle.gameObject.SetActive(false);

        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)controller.index);

        if (device.GetPress(buttonId))
        {

            canTeleport = false;

            laser.gameObject.SetActive(true);
            reticle.gameObject.SetActive(true);

            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);

            List<Vector3> waypoints = new List<Vector3>();
            waypoints.Add(transform.position);

            reticle.transform.position = ray.origin + ray.direction * range;
            Physics.Raycast(ray, out hit, range, 1, QueryTriggerInteraction.Ignore);
            if (Physics.Raycast(ray, out hit, range))
            {
               
                target = hit;
                canTeleport = hit.collider.CompareTag("Teleportable");

                reticle.transform.position = target.point;
                reticle.transform.up = target.normal;

            }

            waypoints.Add(reticle.transform.position);

            laser.SetWaypoints(waypoints.ToArray());

            Color color = canTeleport ? enabledColor : disabledColor;
            laser.SetColor(color);
            reticle.SetColor(color);
            reticle.ShowPlayArea(canTeleport);
        }

        if (device.GetPressUp(buttonId) && canTeleport)
        {

            player.position = target.point;
            player.up = target.normal;
        }
    }
}
