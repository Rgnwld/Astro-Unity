using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] float cameraVelocity = .1f;
    [SerializeField] float defaultSize = 9f;

    [HideInInspector]
    public CameraShake cameraShake;
    public static MainCamera instance;
    public Camera camComponent;

    public Transform focus;

    private void Awake()
    {
        instance = this;
        cameraShake = this.gameObject.GetComponent<CameraShake>();
        camComponent = this.gameObject.GetComponent<Camera>();
    }

    void Update()
    {
        if (focus != null)
        {
            GoToFocus(focus);
        }
        else
        {
            camComponent.orthographicSize = defaultSize;
        }
    }

    void GoToFocus(Transform _focus)
    {
        Vector3 camera = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        Vector3 focused = new Vector3(_focus.transform.position.x, _focus.transform.position.y, transform.position.y);
        transform.Translate((focused - camera) * Time.deltaTime * cameraVelocity, Space.World);
    }

    public void GoToPoint(Vector3 _focus)
    {
        focus = null;
        Vector3 camera = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        Vector3 focused = new Vector3(_focus.x, _focus.y, transform.position.y);
        transform.Translate((focused - camera) * Time.deltaTime * cameraVelocity, Space.World);
        camComponent.orthographicSize = defaultSize;
    }

    public void GoToPoint(Vector3 _focus, float size)
    {
        focus = null;
        Vector3 camera = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        Vector3 focused = new Vector3(_focus.x, _focus.y, transform.position.y);
        camComponent.orthographicSize = size;
        transform.Translate((focused - camera) * Time.deltaTime * cameraVelocity, Space.World);
    }

}
