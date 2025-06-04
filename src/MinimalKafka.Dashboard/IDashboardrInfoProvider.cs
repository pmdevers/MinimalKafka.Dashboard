namespace MinimalKafka.Dashboard;

public interface IDashboardrInfoProvider
{
    /// <summary>
    /// Gets the dashboard information.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the dashboard information.</returns>
    Task<DashboardInfo> GetDashboardInfoAsync();
}
