namespace SmCenterTestApp.Domain.Common
{
    public abstract class ServiceBase : IValidationStateService
    {
        public ValidationState ValidationState { get; } = new();

    }
}
