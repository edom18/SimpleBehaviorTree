using UnityEngine;

namespace BehaviorTreeSample
{
    public enum BehaviorStatus
    {
        Inactive,
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

        private bool _isCompleted = false;

        public SimpleBehaviorTree(GameObject owner)
        {
            _owner = owner;
        }

        /// <summary>
        /// 対象ノードを起動（Awake）する
        /// </summary>
        /// <param name="node">起動するノード</param>
        private void CallOnAwake(Node node)
        {
            // 対象ノードにオーナーを設定する
            node.Owner = _owner;

            // ノード起動
            node.OnAwake();

            // CompositeNodeの場合は再帰的に起動させる
            CompositeNode cnode = node as CompositeNode;
            if (cnode != null)
            {
                foreach (var child in cnode.Children)
                {
                    CallOnAwake(child);
                }
            }
        }

        /// <summary>
        /// 再帰的にノードを実行する
        /// </summary>
        /// <param name="node">実行するノード</param>
        private BehaviorStatus Execute(Node node)
        {
            if (node is CompositeNode)
            {
                CompositeNode cnode = node as CompositeNode;
                while (cnode.CanExecute())
                {
                    Node child = cnode.Children[cnode.CurrentChildIndex];
                    BehaviorStatus childStatus = Execute(child);
                    if (childStatus == BehaviorStatus.Running)
                    {
                        return BehaviorStatus.Running;
                    }
                    cnode.OnChildExecuted(childStatus);
                }
                return cnode.Status;
            }
            else
            {
                // ActionNodeの場合はただ実行だけする
                return node.OnUpdate();
            }
        }

        /// <summary>
        /// ツリーを開始
        /// </summary>
        public void Start()
        {
            Debug.Log("Tree start.");

            CallOnAwake(_rootNode);
        }

        public void Update()
        {
            if (_isCompleted)
            {
                return;
            }

            Execute(_rootNode);
        }

        public void SetRootNode(Node node)
        {
            _rootNode = node;
        }
    }
}
