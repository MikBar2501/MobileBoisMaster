using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnMenuObj : MonoBehaviour
{
    public void OnClick()
    {
        Ray MouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(MouseRay, out hit))
        {
            GameObject obj = hit.collider.gameObject;
            if(obj.GetComponent<MenuObjVirtual>())
            {
                obj.GetComponent<MenuObjVirtual>().DoStuff();
            }
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
    }
}
