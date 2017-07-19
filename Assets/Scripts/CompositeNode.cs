using System.Collections.Generic;

namespace BehaviorTreeSample
{
    /// <summary>
    /// 子を持つノードベースクラス
    /// </summary>
    public class CompositeNode : Node
    {
        //protected IEnumerator<Node> _childNodes;

        private bool _needsConditionalAbort = false;
        public bool NeedsConditionalAbort
        {
            get { return _needsConditionalAbort; }
            set { _needsConditionalAbort = value; }
        }

        protected int _currentChildIndex = 0;
        public int CurrentChildIndex
        {
            get { return _currentChildIndex; }
        }

        protected List<Node> _children = new List<Node>();
        public List<Node> Children
        {
            get { return _children; }
        }

        public CompositeNode()
        {

        }

        #region ### CompositeNode ###
        public virtual bool CanExecute()
        {
            return true;
        }

        /// <summary>
        /// 中断が検知された際に呼び出される
        /// </summary>
        /// <param name="childNodeIndex">中断を呼び出した子ノードの全体でのインデックス</param>
        public virtual void OnConditionalAbort(int childNodeIndex)
        {
            OnEnd();
            _currentChildIndex = 0;
            //for (int i = 0; i < _children.Count; i++)
            //{
            //    if (_children[i].Index == childNodeIndex)
            //    {
            //        _currentChildIndex = i;
            //        break;
            //    }
            //}
        }

        /// <summary>
        /// 子ノードの実行が終わった際に呼び出される
        /// </summary>
        /// <param name="childStatus">子ノードの実行結果</param>
        public virtual void OnChildExecuted(BehaviorStatus childStatus)
        {
            // do nothing.
        }
        #endregion ### CompositeNode ###

        /// <summary>
        /// 起動。すべての子ノードのOnAwakeを呼ぶ
        /// </summary>
        public override void OnAwake()
        {
            _currentChildIndex = 0;
        }

        public override void OnStart()
        {
            if (CanExecute())
            {
                Node current = _children[_currentChildIndex];
            }
        }

        public override void OnAbort()
        {
            base.OnAbort();
            _currentChildIndex = 0;
        }

        public override BehaviorStatus OnUpdate()
        {
            return _status;
        }

        public override void AddNode(Node child)
        {
            if (!_children.Contains(child))
            {
                child.ParentNode = this;
                _children.Add(child);
            }
        }

        public override void AddNodes(params Node[] nodes)
        {
            foreach (var node in nodes)
            {
                AddNode(node);
            }
        }
    }
}
