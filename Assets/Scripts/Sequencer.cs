namespace BehaviorTreeSample
{
    /// <summary>
    /// 子ノードを順に評価し、どれかがFailerになった時点でFailerを返す
    /// </summary>
    public class Sequencer : CompositeNode
    {
        public Sequencer(Node[] nodes) : base(nodes) { }

        public override BehaviorStatus OnUpdate()
        {
            // すでに成功している場合はなにもしない
            if (_status == BehaviorStatus.Sucess)
            {
                return BehaviorStatus.Sucess;
            }

            while (true)
            {
                Node node = _childNodes.Current;
                BehaviorStatus status = node.OnUpdate();

                if (status == BehaviorStatus.Failure)
                {
                    _status = BehaviorStatus.Failure;
                    return BehaviorStatus.Failure;
                }
                else if (status == BehaviorStatus.Running)
                {
                    return BehaviorStatus.Running;
                }

                // 子ノードがすべてSucessだった場合はSucess
                if (!MoveNextNode())
                {
                    _status = BehaviorStatus.Sucess;
                    return BehaviorStatus.Sucess;
                }
            }
        }
    }
}
