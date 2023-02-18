using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLine : MonoBehaviour
{
    public GameObject playZone;
    public GameObject playPanel;
    public GameObject gameOverPanel;
    private AimControl aimControl;

    // Start is called before the first frame update
    void Start()
    {
        aimControl = FindObjectOfType<AimControl>();

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Block")){
           Debug.Log("trigger"+other.gameObject.tag);
            aimControl.AimStateToEnd();
            playPanel.SetActive(false);
            gameOverPanel.SetActive(true);
            playZone.SetActive(false);
        }
        else if(other.gameObject.CompareTag("AddOn")){
            other.gameObject.SetActive(false);
        }
    }
}
