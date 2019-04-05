using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition2D : MonoBehaviour
{

    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private float _hp = 30f;
    private bool IsAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (_hp <= 0)
            IsAlive = false;
        /*if (IsAlive == false)
            SceneManager.LoadScene(1);
        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene(2);
            */           

    }
}
