using Altinay.Samples;
using Xunit;

namespace Altinay.EntityFrameworkCore.Domains;

[Collection(AltinayTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<AltinayEntityFrameworkCoreTestModule>
{

}
