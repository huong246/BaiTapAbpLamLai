using BaiTapAbp.Samples;
using Xunit;

namespace BaiTapAbp.EntityFrameworkCore.Applications;

[Collection(BaiTapAbpTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<BaiTapAbpEntityFrameworkCoreTestModule>
{

}
