using Soenneker.Copper.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Copper.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class CopperOpenApiClientUtilTests : HostedUnitTest
{
    private readonly ICopperOpenApiClientUtil _openapiclientutil;

    public CopperOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<ICopperOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
