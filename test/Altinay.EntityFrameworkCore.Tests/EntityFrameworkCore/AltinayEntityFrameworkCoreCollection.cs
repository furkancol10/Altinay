using Xunit;

namespace Altinay.EntityFrameworkCore;

[CollectionDefinition(AltinayTestConsts.CollectionDefinitionName)]
public class AltinayEntityFrameworkCoreCollection : ICollectionFixture<AltinayEntityFrameworkCoreFixture>
{

}
