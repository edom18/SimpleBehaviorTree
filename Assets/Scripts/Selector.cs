namespace BehaviorTreeSample
{
    public class Selector : CompositeNode
    {
        public Selector(Node[] nodes) : base(nodes) { }

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

                if (status == BehaviorStatus.Sucess)
                {
                    _status = BehaviorStatus.Sucess;
                    return BehaviorStatus.Sucess;
                }
                else if (status == BehaviorStatus.Running)
                {
                    return BehaviorStatus.Running;
                }

                if (!MoveNextNode())
                {
                    // すべての子ノードがひとつもSucessを返さなかった場合はFailure
                    _status = BehaviorStatus.Failure;
                    return BehaviorStatus.Failure;
                }
            }
        }
    }
}
