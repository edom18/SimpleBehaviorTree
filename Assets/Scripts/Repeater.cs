namespace BehaviorTreeSample
{
    /// <summary>
    /// Targetのノードを繰り返す
    /// </summary>
    public class Repeater : Decorator
    {
        public Repeater(Node targetNode) : base(targetNode) { }

        public override BehaviorStatus OnUpdate()
        {
            BehaviorStatus status = _targetNode.OnUpdate();

            if (status != BehaviorStatus.Running)
            {
                Restart();
                return _status;
            }

            return BehaviorStatus.Running;
        }

        /// <summary>
        /// 全部の子ノードが成功・失敗して処理するノードがなくなったら最初に戻る
        /// </summary>
        private void Restart()
        {
            _status = BehaviorStatus.Active;
            _targetNode.OnEnd();
            _targetNode.OnStart();
        }
    }
}
