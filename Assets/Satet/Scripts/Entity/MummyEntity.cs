using SSR.Player;
using UnityEngine;
public enum MummyStatus
{
    WalkingHidden,
    Emerging,
}
public class MummyEntity : MonoBehaviour
{
    // Use this for initialization
    public int id;
    public MummyWalkingPath walkingPath = null;
    float collapsedTime;
    int currentPos = 0;
    public MummyStatus status = MummyStatus.WalkingHidden;
    public Renderer render;
    public int score = 150;

    private void Awake()
    {
        this.render.enabled = false;
    }

    void Start()
    {

    }

    public void SetUp(int _id, MummyWalkingPath _walkingPath)
    {
        id = _id;
        walkingPath = _walkingPath;


    }

    public bool setEmergingPoint()
    {
        if (currentPos != -1)
        {
            Debug.Log(MummyManager.Instance);
            EmergingPoint currentEmergingPoint = MummyManager.Instance.emergingPoints[walkingPath.walkingSeq[currentPos]];
            transform.position = currentEmergingPoint.reference.position + currentEmergingPoint.position;
            transform.rotation = Quaternion.Euler(currentEmergingPoint.eulerAngles);
            return true;
        }
        return false;
    }
    void MoveToNextPoint()
    {
        currentPos++;
        if (currentPos >= walkingPath.walkingSeq.Count)
            currentPos = -1;
    }
    // Update is called once per frame
    void Update()
    {
        collapsedTime += Time.deltaTime;
        //Debug.Log(id);
        //Debug.Log(currentPos + " !!!! " + walkingPath.walkingSeq.Count);
        if (walkingPath.walkingSeq.Count != 0)
        {
            if (currentPos == -1)
            {
                PlayerEntity.Instance.UnderAttack();
                Destroy(gameObject);
                MummyManager.Instance.DestoryMummy(id);
            }
            else
            {
                switch (status)
                {
                    case MummyStatus.Emerging:
                        if (collapsedTime > walkingPath.lastingTime[currentPos])
                        {
                            // disappear
                            this.render.enabled = false;
                            // move to next position
                            MoveToNextPoint();
                            collapsedTime = 0;
                            status = MummyStatus.WalkingHidden;
                        }

                        break;
                    case MummyStatus.WalkingHidden:
                        if (collapsedTime > walkingPath.waitingTime[currentPos])
                        {
                            // appear
                            setEmergingPoint();
                            this.render.enabled = true;
                            collapsedTime = 0;
                            status = MummyStatus.Emerging;
                        }
                        break;
                }

            }
        }
        else
        {
            this.render.enabled = true;
        }
    }

    public void SendSpawnMessage()
    {
        MummyManager.Instance.SpawnMummy();
    }

    public void AddScore()
    {
        PlayerEntity.Instance.score += this.score;
    }

    public void Destroy()
    {
        GameObject.Destroy(this.gameObject);
    }

}