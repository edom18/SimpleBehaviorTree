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
        /// ノードが実行されたら呼ばれる
        /// </summary>
        public virtual void OnStart()
        {
            _status = BehaviorStatus.Active;
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
    }
}
