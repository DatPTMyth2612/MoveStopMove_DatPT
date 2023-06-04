using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : GameUnit
{
    private float timer;
    public Rigidbody rb;
    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    public override void OnInit()
    {
        timer = 5f;
        rb.isKinematic = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        CharacterCollider character = Cache.GetCharacterInParent(other);
        if (other.CompareTag("Ground"))
        {
            rb.isKinematic = true;
            TF.position = new(TF.position.x,0.5f,TF.position.z);
        }
        if(other.CompareTag(ConstString.TAG_BOT))
        {
            character.character.OnBoost();
            OnDespawn();
            timer -= Time.deltaTime;
            if(timer<=0)
            {
                character.character.currentStage.SpawnGift(1);
            }
        }
    }
}
