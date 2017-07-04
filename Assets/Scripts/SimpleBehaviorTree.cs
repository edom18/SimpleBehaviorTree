using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace BehaviorTreeSample
{
    public enum BehaviorStatus
    {
        Inactive,
        Sucess,
        Failure,
        Running,
        Completed,
    }

    /// <summary>
    /// Behavior Treeをシンプルに実装する
    /// </summary>
    public class SimpleBehaviorTree
    {
        /// <summary>
        /// 再評価する際の諸々の情報を格納する
        /// </summary>
        public class ConditionalReevaluate
        {
            public int Index { get; set; }
            public BehaviorStatus Status { get; set; }
            public int CompositeIndex { get; set; }
            public int StackIndex { get; set; }
            public void Initialize(int i, BehaviorStatus status, int stack, int composite)
            {
                Index = i;
                Status = status;
                StackIndex = stack;
                CompositeIndex = composite;
            }
        }

        // Behavior Treeを保持しているオーナーオブジェクト
        private GameObject _owner;
        private Node _rootNode;

        private List<Node> _nodeList = new List<Node>();
        private List<ConditionalReevaluate> _reevaluateList = new List<ConditionalReevaluate>();

        private bool _isCompleted = false;

        // コンストラクタ
        public SimpleBehaviorTree(GameObject owner)
        {
            _owner = owner;
        }

        /// <summary>
        /// 指定したふたつのノードIDのノードの共通の祖先を見つける
        /// </summary>
        /// <param name="node1">ノード1のID</param>
        /// <param name="node2">ノード2のID</param>
        /// <returns>見つかった祖先ノードID</returns>
        private int CommonAncestorNode(int node1, int node2)
        {
            // TODO: あとで実装
            return -1;
        }

        /// <summary>
        /// 対象ノードを起動（Awake）する
        /// </summary>
        /// <param name="node">起動するノード</param>
        private void CallOnAwake(Node node)
        {
            // ノードに全体のグラフの通し番号を設定する
            node.Index = _nodeList.Count;

            _nodeList.Add(node);

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

        private int ReevaluateConditionalTasks()
        {
            for (int i = 0; i < _reevaluateList.Count; i++)
            {
                ConditionalReevaluate cr = _reevaluateList[i];
                BehaviorStatus status = _nodeList[cr.Index].OnUpdate();

                // 前回の状態と変化していたら、祖先まで遡って処理を停止する
                if (cr.Status != status)
                {

                }

                return cr.Index;
            }

            return -1;
        }

        /// <summary>
        /// 再帰的にノードを実行する
        /// </summary>
        /// <param name="node">実行するノード</param>
        private BehaviorStatus Execute(Node node)
        {
            int abortIndex = ReevaluateConditionalTasks();
            if (abortIndex != -1)
            {

            }

            // Compositeノードの場合は再帰処理
            if (node is CompositeNode)
            {
                CompositeNode cnode = node as CompositeNode;

                while (cnode.CanExecute())
                {
                    Node child = cnode.Children[cnode.CurrentChildIndex];
                    BehaviorStatus childStatus = Execute(child);

                    // 再評価が必要な場合はそれをリストに追加
                    if (cnode.NeedsConditionalAbort)
                    {
                        if (child is ConditionalNode)
                        {
                            _reevaluateList.Add(new ConditionalReevaluate
                            {
                                Index = child.Index,
                                CompositeIndex = cnode.Index,
                                Status = childStatus,
                            });
                        }
                    }

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

            BehaviorStatus status = Execute(_rootNode);
            if (status == BehaviorStatus.Completed)
            {
                _isCompleted = true;
            }
        }

        /// <summary>
        /// ルートノードを設定する
        /// </summary>
        /// <param name="node">ルートノードとして設定するノード</param>
        public void SetRootNode(Node node)
        {
            _rootNode = node;
        }
    }
}
