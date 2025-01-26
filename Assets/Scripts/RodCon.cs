using UnityEngine;

public class RodCon : MonoBehaviour
{
    public int number = 0;
    public bool Active = false;
    SpriteRenderer sprite;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    void OnMouseDown()
    {
        if (!Active) Active = true;
        else if (Active) Active = false;
    }
    void Update()
    {
        if (Active)
        {
            //TODO: change texture to active
            sprite.color = Color.green;
        }
        else if(!Active)
        {
            //TODO: change texture to NOT active
            sprite.color = Color.red;
        }
    }
}
