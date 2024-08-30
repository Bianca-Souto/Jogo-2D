using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float speed;
    private float initialSpeed;
    private Animator anim;
    private int index;
    private int nextIndex;

    public List<Transform> paths = new List<Transform>();

    void Start()
    {
        initialSpeed = speed;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        if(DialogueControl.instance.IsShowing())
        {
            speed = 0f;
            anim.SetBool("isWalking", false);
        }
        else{
            speed = initialSpeed;
            anim.SetBool("isWalking", true);
        }
        // Move o NPC em direção ao próximo ponto na lista
        if (index >= 0 && index < paths.Count) // Verifica se o índice está dentro dos limites da lista
        {
            transform.position = Vector2.MoveTowards(transform.position, paths[index].position, speed * Time.deltaTime);

            // Verifica a distância entre o NPC e o próximo ponto
            if (Vector2.Distance(transform.position, paths[index].position) < 0.1f)
            {
                // Avança para o próximo ponto ou reinicia se for o último ponto
                index = (index + 1) % paths.Count;
            }
        }
        Vector2 direction = paths[index].position - transform.position;

        if(direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0,0);
        }
        if(direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0,180);
        }
    }
}
