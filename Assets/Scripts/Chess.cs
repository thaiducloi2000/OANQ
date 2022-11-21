using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chess : MonoBehaviour
{
    public Face faces;

    public GameObject SmileBody;
    private Material faceMaterial;
    public Animator animator;

    void SetFace(Texture tex)
    {
        faceMaterial.SetTexture("_MainTex", tex);
    }

    private void Start()
    {
        faceMaterial = SmileBody.GetComponent<Renderer>().materials[1];
        this.animator = this.gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(this.gameObject.transform.position.x, 0f, Camera.main.transform.position.z);

        this.transform.LookAt(pos);
    }

    public void GetSlime()
    {
        animator.SetTrigger("Damage");
        animator.SetInteger("DamageType", 1);
        SetFace(faces.damageFace);
        Destroy(this.gameObject);
    }
    
}
