using UnityEngine;

namespace BehaviorTreeSample
{
    public enum BehaviorStatus
    {
        Inactive,
        Active,
        Sucess,
        Failure,
        Running,
    }

    /// <summary>
    /// Behavior Treeをシンプルに実装する
    /// </summary>
    public class SimpleBehaviorTree
    {
        // Behavior Treeを保持しているオーナーオブジェクト
        private GameObject _owner;
        private Node _rootNode;

        public SimpleBehaviorTree(GameObject owner)
        {
            _owner = owner;
        }

        public void Start()
        {
        }

        public void Update()
        {
            if (_rootNode.Status == BehaviorStatus.Inactive)
            {
                _rootNode.OnStart();
            }
            _rootNode.OnUpdate();
        }

        public void SetRootNode(Node node)
        {
            _rootNode = node;
        }
    }
}
