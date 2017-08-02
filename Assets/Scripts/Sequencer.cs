namespace BehaviorTreeSample
{
    /// <summary>
    /// 子ノードを順に評価し、どれかがFailerになった時点でFailerを返す
    /// </summary>
    public class Sequencer : CompositeNode
    {
        /// <summary>
        /// 子ノードの実行が終わった際に呼び出される
        /// </summary>
        /// <param name="childStatus">子ノードの実行結果</param>
        public override void OnChildExecuted(BehaviorStatus childStatus)
        {
            _currentChildIndex++;
            base.OnChildExecuted(childStatus);
        }

        public override bool CanExecute()
        {
            return _currentChildIndex < _children.Count && _status != BehaviorStatus.Failure;
        }
    }
}
