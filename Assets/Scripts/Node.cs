using UnityEngine;

namespace BehaviorTreeSample
{
    /// <summary>
    /// Simple Behavior Treeのノードベースクラス
    /// </summary>
    public abstract class Node
    {
        protected GameObject _owner;
        public GameObject Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        // 現在のステータス
        protected BehaviorStatus _status = BehaviorStatus.Inactive;
        public BehaviorStatus Status
        {
            get { return _status; }
        }

        /// <summary>
        /// Behavior Tree起動時に一度だけ呼ばれる
        /// </summary>
        public virtual void OnAwake()
        {
            // do nothing.
        }

        /// <summary>
        /// ノードが実行されたら呼ばれる
        /// </summary>
        public virtual void OnStart()
        {
            _status = BehaviorStatus.Running;
        }

        /// <summary>
        /// ノード実行中（Running）に毎フレーム呼ばれる
        /// </summary>
        public abstract BehaviorStatus OnUpdate();

        /// <summary>
        /// ノードの実行が終了したら呼ばれる
        /// </summary>
        public virtual void OnEnd()
        {
            _status = BehaviorStatus.Inactive;
        }

        /// <summary>
        /// 子ノードを追加する
        /// </summary>
        /// <param name="child">追加する子ノード</param>
        public virtual void AddNode(Node child)
        {

        }

        /// <summary>
        /// 子ノードを複数追加する
        /// </summary>
        /// <param name="nodes">追加する子ノード郡</param>
        public virtual void AddNodes(params Node[] nodes)
        {

        }
    }
}
