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
    public int reward = 150;
    private bool isDestory = false;
    private bool countDown = true;
    Animator animator;

    private void Awake()
    {
        this.render.enabled = false;
        animator = GetComponent<Animator>();
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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("MummyAppear"))
            this.render.enabled = true;
        if (!countDown && status == MummyStatus.WalkingHidden &&
            animator.GetCurrentAnimatorStateInfo(0).IsName("MummyIdleUnderground"))
        {
            this.render.enabled = false;
            Debug.Log("Change because of underground");
            countDown = true;
            MummyManager.Instance.leaveEmergingPoint(walkingPath.walkingSeq[currentPos]);
            MoveToNextPoint();
        }
        if (!countDown && status == MummyStatus.Emerging &&
            animator.GetCurrentAnimatorStateInfo(0).IsName("MummyIdle"))
        {
            Debug.Log("Change because of Idle");
            countDown = true;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddScore();
        }
        if (countDown)
        {
            collapsedTime += Time.deltaTime;
        }
        else collapsedTime = 0;
        if (walkingPath.walkingSeq.Count != 0)
        {
            if (currentPos == -1)
            {
                if (!isDestory)
                {
                    isDestory = true;
                    PlayerEntity.Instance.UnderAttack(walkingPath.achievedFinalPoint);
                    this.GetComponent<Destructible>().Destrory();
                    MummyManager.Instance.DestoryMummy(id);
                    MummyManager.Instance.SpawnMummy();
                }

            }
            else
            {
                switch (status)
                {
                    case MummyStatus.Emerging:
                        if (collapsedTime > walkingPath.lastingTime[currentPos])
                        {
                            status = MummyStatus.WalkingHidden;
                            countDown = false;
                            Debug.Log("Disappear!" + walkingPath.lastingTime[currentPos]);
                            // disappear
                            animator.SetBool("isAppearing", false);
                            animator.SetBool("isDisappearing", true);
                            // move to next position
                            collapsedTime = 0;

                        }

                        break;
                    case MummyStatus.WalkingHidden:
                        if (currentPos == -1)
                            Debug.Log(currentPos);
                        if (collapsedTime > walkingPath.waitingTime[currentPos] &&
                            MummyManager.Instance.enterToEmergingPoint(walkingPath.walkingSeq[currentPos], id))
                        {
                            status = MummyStatus.Emerging;
                            countDown = false;
                            Debug.Log("Appear!" + walkingPath.waitingTime[currentPos]);
                            // appear
                            setEmergingPoint();

                            animator.SetBool("isDisappearing", false);
                            animator.SetBool("isAppearing", true);

                            collapsedTime = 0;


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
        PlayerEntity.Instance.score += this.reward;
        Debug.Log("The player's score is " + PlayerEntity.Instance.score);
        PopupScore(this.reward);
    }

    public void ElimateScorePopupText()
    {
        gameObject.GetComponentInChildren<TextMesh>().GetComponent<Animator>().SetBool("IsPopup", false);
    }

    public void PopupScore(int score)
    {
        Invoke("ElimateScorePopupText", 2);
        gameObject.GetComponentInChildren<TextMesh>().text = "" + this.reward;
    }
}