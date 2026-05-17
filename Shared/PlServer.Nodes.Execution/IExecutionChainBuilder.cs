
using PlServer.Domain.Nodes;

namespace PlServer.Nodes.Execution;

public interface IExecutionChainBuilder
{
    ICollection<ExecutionChain> Build(NodeGraph graph);
}
