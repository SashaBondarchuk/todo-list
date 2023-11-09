namespace TodoList.Common.Exceptions
{
    public sealed class NotFoundException : Exception
    {
        public NotFoundException(string name, int id)
            : base($"{name} with id ({id}) was not found.") { }

        public NotFoundException(string name) : base($"{name} was not found.") { }
    }
}
