
using PlServer.Domain.Results;

namespace PlServer.Domain.Nodes;

public interface INodeGraphHandler<T> where T : class
{
    UnitResult<NodeErrors> Handle(T command, NodeGraphContext context);
}