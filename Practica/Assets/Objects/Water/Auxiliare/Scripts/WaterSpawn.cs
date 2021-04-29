using ilhamhe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class WaterSpawn : MonoBehaviour
{

    public DynamicWater2D water; //Apa prefabricata

    public Material waterMaterial;
    public GameObject splash;

    public int quality = 60;

    public float springconstant = 0.05f;
    public float damping = 0.08f;
    public float spread = 0.8f;
    public float collisionVelocityFactor = 0.03f;




    // Start is called before the first frame update
    void Start()
    {
        Renderer b = GetComponent<Renderer>();

        water.waterMaterial = waterMaterial;
        water.splash = splash;

        water.bound.left = b.transform.position.x- (b.bounds.max.x-b.bounds.center.x);
        water.bound.right = b.bounds.max.x;
        water.bound.top = b.bounds.max.y;
        water.bound.bottom = b.transform.position.y - (b.bounds.max.y - b.bounds.center.y);

        water.quality = quality;
        water.springconstant = springconstant;
        water.damping = damping;
        water.spread = spread;
        water.collisionVelocityFactor = collisionVelocityFactor;

        Sprite.Destroy(this);

        DynamicWater2D realWater = Instantiate(water, new Vector3(0, 0, 0), Quaternion.identity);
        realWater.parentRender = this.gameObject.GetComponent<Renderer>();



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
