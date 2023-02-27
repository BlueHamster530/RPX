using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGlobalRule : MonoBehaviour
{
    static public GameGlobalRule Instance;
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
       // Application.targetFrameRate = 30;//최대프레임 설정
    }

    // Update is called once per frame
    void Update()
    {
    }
}
