using BaiTapAbp.Samples;
using Xunit;

namespace BaiTapAbp.EntityFrameworkCore.Domains;

[Collection(BaiTapAbpTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<BaiTapAbpEntityFrameworkCoreTestModule>
{

}
