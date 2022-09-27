namespace CVBuilder.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object id) : base($"{name} with {id} not found")
        {

        }
    }
}
