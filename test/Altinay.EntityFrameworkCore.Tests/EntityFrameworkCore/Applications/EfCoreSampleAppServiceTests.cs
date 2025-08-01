using Altinay.Samples;
using Xunit;

namespace Altinay.EntityFrameworkCore.Applications;

[Collection(AltinayTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<AltinayEntityFrameworkCoreTestModule>
{

}
