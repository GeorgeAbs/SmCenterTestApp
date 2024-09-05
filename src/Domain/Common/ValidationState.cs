using System.Collections.ObjectModel;

namespace SmCenterTestApp.Domain.Common
{
    public class ValidationState
    {
        public bool IsValid { get; private set; } = true;

        private readonly List<string> _errors = [];
        public IReadOnlyCollection<string> Errors => _errors.AsReadOnly();

        public void AddError(string message)
        {
            _errors.Add(message);

            IsValid = false;
        }

        public void AddErrorsRange(IEnumerable<string> messages)
        {
            _errors.AddRange(messages);

            IsValid = false;
        }
    }
}
