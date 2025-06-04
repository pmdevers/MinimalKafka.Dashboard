namespace MinimalKafka.Dashboard.Gen;

internal class DashboardInfoGenerator : IDashboardrInfoProvider
{
    public Task<DashboardInfo> GetDashboardInfoAsync()
    {
        return Task.FromResult(new DashboardInfo());
    }
}
