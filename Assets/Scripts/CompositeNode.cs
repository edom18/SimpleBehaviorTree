using System.Collections.Generic;

namespace BehaviorTreeSample
{
    /// <summary>
    /// 子を持つノードベースクラス
    /// </summary>
    public class CompositeNode : Node
    {
        protected IEnumerator<Node> _childNodes;

        public CompositeNode(Node[] nodes)
        {
            _childNodes = new List<Node>(nodes).GetEnumerator();
        }

        public override void OnStart()
        {
            base.OnStart();

            _childNodes.Reset();
            MoveNextNode();
        }

        public override BehaviorStatus OnUpdate()
        {
            return _status;
        }

        /// <summary>
        /// 次のノードに進める
        /// </summary>
        /// <returns></returns>
        protected bool MoveNextNode()
        {
            if (_childNodes.Current != null)
            {
                _childNodes.Current.OnEnd();
            }

            if (!_childNodes.MoveNext())
            {
                return false;
            }

            Node node = _childNodes.Current;
            node.OnStart();

            return true;
        }
    }
}
