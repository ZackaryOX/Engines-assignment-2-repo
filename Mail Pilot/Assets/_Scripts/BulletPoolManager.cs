using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Bonus - make this class a Singleton!

[System.Serializable]


public class BulletContainer
{
    public int ID;
    public GameObject Bulletobj;
    public bool IsActive;

    public BulletContainer(int tempid, GameObject tempobj, bool active)
    {
        ID = tempid;
        Bulletobj = tempobj;
        IsActive = active;
        Bulletobj.SetActive(active);
        Bulletobj.name = ID.ToString();
    }
    public void SetActive()
    {
        IsActive = true;
        this.Bulletobj.SetActive(true);
    }
    public void SetInactive()
    {
        this.IsActive = false;
        this.Bulletobj.SetActive(false);
    }
}
public class BulletPoolManager : MonoBehaviour
{
    public GameObject bullet;
    private static GameObject BulletPrefab;
    private static int BulletAmt = 25;
    private static int BulletsInPool = 0;
    private static BulletPoolManager Singleton;

    

    private BulletPoolManager()
    {
        

    }

    //TODO: create a structure to contain a collection of bullets
    List<BulletContainer> Bullets = new List<BulletContainer>();
    

    // Start is called before the first frame update
    void Start()
    {
        if(BulletPrefab == null)
        {
            BulletPrefab = bullet;
        }
        

        // TODO: add a series of bullets to the Bullet Pool
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO: modify this function to return a bullet from the Pool
    public GameObject GetBullet(Vector3 spawnpos, Quaternion temp)
    {
        
        BulletContainer returnval;
        for(int i = 0; i < BulletAmt; i++)
        {
            if(GetInstance().Bullets[i].IsActive == false)
            {
                GameObject tempobj = GetInstance().Bullets[i].Bulletobj;
                tempobj.transform.position = spawnpos;
                tempobj.transform.rotation = temp;
                GetInstance().Bullets[i].SetActive();
                Debug.Log("ISACTIVE?: " + GetInstance().Bullets[i].IsActive);
                return tempobj;
            }
        }
        return null;
    }

    //TODO: modify this function to reset/return a bullet back to the Pool 
    public void ResetBullet(int id)
    {

        for (int i = 0; i < BulletAmt; i++)
        {
            if (GetInstance().Bullets[i].IsActive == true)
            {
                if (GetInstance().Bullets[i].ID == id)
                {

                    GetInstance().Bullets[i].SetInactive();
                }

            }
        }
    }

    static public BulletPoolManager GetInstance()
    {
        if(Singleton == null)
        {
            Singleton = new GameObject("Container").AddComponent<BulletPoolManager>();
            for (int i = 0; i < BulletAmt; i++)
            {
                BulletContainer temp = new BulletContainer(BulletsInPool, Instantiate(BulletPrefab), false);
                BulletsInPool++;
                GetInstance().Bullets.Add(temp);

            }
        }

        return Singleton;

    }
}
