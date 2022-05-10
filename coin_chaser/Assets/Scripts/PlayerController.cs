using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public Vector3 dir;
    [SerializeField] private int speed;
    [SerializeField] private GameObject PanelLose;
    [SerializeField] private int coins;
    [SerializeField] private Text coinsCounter;

    private int lineToMove = 1;
    public float lineDistance = 3;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        dir.z = speed;
        controller.Move(dir * Time.fixedDeltaTime);
    }

    private void Update()
    {
        if (SwipeController.swipeRight)
        {
            if (lineToMove < 1)
                lineToMove++;
        }

        if (SwipeController.swipeLeft)
        {
            if (lineToMove > 0)
                lineToMove--;
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (lineToMove == 0)
            targetPosition += Vector3.left * lineDistance;
        else if (lineToMove == 1)
            targetPosition += Vector3.right * lineDistance;

        if (transform.position == targetPosition)
        {
            return;
        }
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            controller.Move(moveDir);
        }
        else 
        {
            controller.Move(diff);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "handshit")
        {
            PanelLose.SetActive(true);
            Time.timeScale = 0;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            coins++;
            coinsCounter.text = coins.ToString();
            Destroy(other.gameObject);
        } 
    }

}
