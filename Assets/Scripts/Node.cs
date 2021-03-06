﻿using UnityEngine;

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

        private int _index = -1;
        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        protected Node _parentNode;
        public Node ParentNode
        {
            get { return _parentNode; }
            set { _parentNode = value; }
        }

        protected string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        // 現在のステータス
        protected BehaviorStatus _status = BehaviorStatus.Inactive;
        public BehaviorStatus Status
        {
            get { return _status; }
        }

        public Node()
        {
            _name = GetType().ToString();
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
            Debug.Log("[OnStart] " + Name);
            _status = BehaviorStatus.Running;
        }

        /// <summary>
        /// ノード実行中（Running）に毎フレーム呼ばれる
        /// </summary>
        public virtual BehaviorStatus OnUpdate()
        {
            if (_status == BehaviorStatus.Completed)
            {
                Debug.Log("This task already has been completed.");
                return _status;
            }

            if (_status == BehaviorStatus.Inactive)
            {
                OnStart();
            }

            return _status;
        }

        /// <summary>
        /// ノードの実行が終了したら呼ばれる
        /// </summary>
        public virtual void OnEnd()
        {
            if (_status == BehaviorStatus.Completed)
            {
                return;
            }

            _status = BehaviorStatus.Inactive;
        }

        /// <summary>
        /// ノードが中断された際に呼び出される
        /// </summary>
        public virtual void OnAbort()
        {
            OnEnd();
        }

        /// <summary>
        /// 子ノードを追加する
        /// </summary>
        /// <param name="child">追加する子ノード</param>
        public virtual void AddNode(Node child)
        {
            // do nothing.
        }

        /// <summary>
        /// 子ノードを複数追加する
        /// </summary>
        /// <param name="nodes">追加する子ノード郡</param>
        public virtual void AddNodes(params Node[] nodes)
        {
            // do nothing.
        }
    }
}
