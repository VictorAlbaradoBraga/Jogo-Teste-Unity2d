using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class door : MonoBehaviour
{
    private Animator anim;
    public string lvlName;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(10, 15, true);
        Physics2D.IgnoreLayerCollision(15, 12, true);
    }

    // Update is called once per frame
    void Update()
    {
        Open();
    }
    void Open()
    {
        if(GameController.instance.totalScore == 100)
        {
            anim.SetBool("open", true);
            Physics2D.IgnoreLayerCollision(10, 15, false);
            Physics2D.IgnoreLayerCollision(15, 12, false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(lvlName);
        }
    }
}
