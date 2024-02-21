using System;
using UnityEngine;

public class SheepMechanic : Animal , ITriggerAnimal
{

    public IState SheepIdleState { get; private set; }
    public IState SheepMoveState { get; private set; }

    public event Action OnAnimalDie;
    public event Action OnAnimalSave;
    public bool isInPaddock;

    [SerializeField] private GameObject materialHolder;
    [SerializeField] private ParticleSystem vfxShoot;
    
    private Vector3 _targetMove;
    private Rigidbody _rb;
    private SheepStateMachine _sheepStateMachine;
    private Material _sheepMaterial;

    private void Awake()
    {
        _sheepMaterial = materialHolder.GetComponent<SkinnedMeshRenderer>().material;
        _rb = GetComponent<Rigidbody>(); 
        
        AnimalAnimationController = new AnimalAnimationController(this, GetComponentInChildren<Animator>());

        _sheepStateMachine = new SheepStateMachine();
        SheepIdleState     = new SheepIdleState(this,_sheepStateMachine,"SheepIdle");
        SheepMoveState     = new SheepMoveState(this,_sheepStateMachine,"SheepMove");
        
        
        _sheepStateMachine.InitMachine(this,SheepIdleState);
    }

    private void OnDestroy()
    {
        OnAnimalSave = null;
        OnAnimalDie = null;
    }

    void FixedUpdate()
    {
        if(isMovable)
            _sheepStateMachine.Tick(Time.deltaTime);
    }

    public void SetNewSprite(Texture2D newTexture)
    {
        if (newTexture != null)
        {
            _sheepMaterial.mainTexture = newTexture;
        }
    }
    
    public void SaveSheep()
    {
        OnAnimalSave?.Invoke();
    }
    public void DieSheep()
    {
        OnAnimalDie?.Invoke();
    }

    public void SetNewPosition(Vector3 pos)
    {
        if (isInPaddock) return;
        _sheepStateMachine.ChangeState(SheepMoveState);
        vfxShoot.Play();
        _targetMove = pos;
    }

    public void SetFinishPosition(Vector3 pos)
    {
        _sheepStateMachine.ChangeState(SheepIdleState);
        transform.position = pos;
    }
    


    public void Move()
    {
        if (_targetMove == Vector3.zero || !isMovable) { return;}


        // Вычисляем скорость перемещения
        float distanceToTarget = Vector3.Distance(transform.position, _targetMove);
        float speed = 10;
        float moveTime = 1;
        float actualTime = 0;

        actualTime += speed * Time.fixedDeltaTime / 4;
        
        // Плавное перемещение по направлению к цели
        Vector3 moveLerp = Vector3.Lerp(transform.position, _targetMove, actualTime / moveTime);
        moveLerp.y = 1;
        _rb.MovePosition(moveLerp);
        
        // Проверяем, достигли ли мы цели
        if (distanceToTarget <= 0.1f) // Устанавливаем небольшое значение, чтобы избежать точных совпадений
        {
            _sheepStateMachine.ChangeState(SheepIdleState);
            vfxShoot.Stop();
        }

        // Плавный поворот по Y оси
        Vector3 lookTarget = (_targetMove - transform.position).normalized;
        if(lookTarget != Vector3.zero) // Добавляем проверку, чтобы избежать деления на ноль
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookTarget, Vector3.up);
            Quaternion smoothRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 2f);
            _rb.MoveRotation(smoothRotation);
        }
    }

}
