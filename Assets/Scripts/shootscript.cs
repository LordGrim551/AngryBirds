using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class shootscript : MonoBehaviour
{
    Vector2 Direction;
    public float force;
    public GameObject pointPrefab;
    public GameObject[] points;
    public int numeberOfPoints;
    private bool isMouseDown = false;
    private Pajaro pajaroScript; // Referencia al script del pájaro


    // Start is called before the first frame update
    void Start()
    {
        points = new GameObject[numeberOfPoints];
        for(int i = 0 ; i < numeberOfPoints; i++)
        {
            points[i] = Instantiate(pointPrefab, transform.position, Quaternion.identity);
            points[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMouseDown)
        {
            Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 bowPos = transform.position;
            Direction = MousePos - bowPos;
            faceMouse();

            for (int i = 0; i < points.Length; i++)
            {
                points[i].SetActive(true);// 
                points[i].transform.position = PointPosition(i * 0.1f);
            }
        }
        else
        {
            // Y AQUI SI NO ESTA PRESIONADO SE DESCATIVAN 
            foreach (var point in points)
            {
                point.SetActive(false);
            }
        }

    }
    void faceMouse()
    {
        //LOS -DIRECTION YA QUE AJUSTAMOS LA POSICION DEL PAJARO DESDE ATRAS NO DESDE DELANTE;
        transform.right = -Direction;
    }
    Vector2 PointPosition(float t)
    {
        Vector2 currentPointPos = (Vector2)transform.position + (-Direction.normalized * force * t) + 0.5f * Physics2D.gravity * (t * t);
        return currentPointPos;
    }
      private void OnMouseDown()
    {
        isMouseDown = true;
    }

    // Método que se llama cuando se suelta el mouse
    private void OnMouseUp()
    {
        isMouseDown = false;
    }

}
