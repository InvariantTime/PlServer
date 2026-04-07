
using PlServer.Domain.Results;

namespace PlServer.Domain.Nodes;

public interface INodeGraphPolicy
{
    UnitResult<NodeErrors> Validate();
}
