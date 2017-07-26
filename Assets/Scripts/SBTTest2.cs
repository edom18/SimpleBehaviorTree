using UnityEngine;

using BehaviorTreeSample;

public class SBTTest2 : MonoBehaviour
{
    [SerializeField]
    private Transform _point1;

    [SerializeField]
    private Transform _point2;

    private bool _forPoint1 = true;
    private Transform _target;

    [SerializeField]
    private float _speed = 0.1f;

    private SimpleBehaviorTree _behaviorTree;

    private void Start()
    {
        _target = transform;

        _behaviorTree = new SimpleBehaviorTree(gameObject);

        // ポイント1へ向かっているかの分岐
        ConditionalNode condition = new ConditionalNode((owner) =>
        {
            return _forPoint1 ? BehaviorStatus.Sucess : BehaviorStatus.Failure;
        });

        #region ### ポイント1への移動 ###
        ActionNode rotateToPoint1 = new ActionNode((owner) =>
        {
            _target.LookAt(_point1);
            return BehaviorStatus.Sucess;
        });

        ActionNode movetoPoint1 = new ActionNode((owner) =>
        {
            Vector3 dir = (_point1.position - _target.position).normalized;
            _target.position += dir * _speed;

            const float ep = 0.1f;
            Vector3 dv = _point1.position - _target.position;
            if (Mathf.Abs(dv.x) <= ep && Mathf.Abs(dv.y) <= ep && Mathf.Abs(dv.z) <= ep)
            {
                _forPoint1 = false;
                return BehaviorStatus.Sucess;
            }

            return BehaviorStatus.Running;
        });

        Sequencer seq1 = new Sequencer();
        //seq1.NeedsConditionalAbort = true;
        seq1.AddNodes(condition, rotateToPoint1, movetoPoint1);
        #endregion ### ポイント1への移動 ###

        #region ### ポイント2への移動 ###
        ActionNode rotateToPoint2 = new ActionNode((owner) =>
        {
            _target.LookAt(_point2);
            return BehaviorStatus.Sucess;
        });

        ActionNode movetoPoint2 = new ActionNode((owner) =>
        {
            Vector3 dir = (_point2.position - _target.position).normalized;
            _target.position += dir * _speed;
            _target.Rotate(Vector3.up, 1f);

            const float ep = 0.1f;
            Vector3 dv = _point2.position - _target.position;
            if (Mathf.Abs(dv.x) <= ep && Mathf.Abs(dv.y) <= ep && Mathf.Abs(dv.z) <= ep)
            {
                return BehaviorStatus.Sucess;
            }

            return BehaviorStatus.Running;
        });

        ActionNode moveUp = new ActionNode((owner) =>
        {
            _target.position += Vector3.up * 0.01f;

            if (_target.position.y >= 3f)
            {
                _forPoint1 = true;
                return BehaviorStatus.Sucess;
            }

            return BehaviorStatus.Running;
        });

        Sequencer seq3 = new Sequencer();
        //seq3.NeedsConditionalAbort = true;
        seq3.AddNodes(movetoPoint2, moveUp);

        Sequencer seq2 = new Sequencer();
        //seq2.NeedsConditionalAbort = true;
        seq2.AddNodes(rotateToPoint2, seq3);
        #endregion ### ポイント2への移動 ###

        // 定義したビヘイビアを設定
        Selector selector = new Selector();
        selector.AddNodes(seq1, seq2);

        Repeater repeater = new Repeater();
        repeater.AddNode(selector);
        _behaviorTree.SetRootNode(repeater);

        _behaviorTree.Start();
    }

    private void Update()
    {
        _behaviorTree.Update();
    }
}
