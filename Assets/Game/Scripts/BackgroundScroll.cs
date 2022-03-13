using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private Transform _cam;
    [SerializeField] private Transform middleBackground;
    [SerializeField] private Transform sideBackground;
    [SerializeField] private float imageWidth = 36.0f;

    private void Update()
    {
        if(_cam.position.x > middleBackground.position.x)
            sideBackground.position = middleBackground.position + Vector3.right * imageWidth;

        if(_cam.position.x < middleBackground.position.x)
            sideBackground.position = middleBackground.position + Vector3.left * imageWidth;

        if (_cam.position.x > sideBackground.position.x || _cam.position.x < sideBackground.position.x)
        {
            Transform oldMiddleBackground = middleBackground;
            middleBackground = sideBackground;
            sideBackground = oldMiddleBackground;
        }
            
    }
}
