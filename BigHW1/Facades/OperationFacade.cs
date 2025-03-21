using BigHW1.Entities;
using BigHW1.Factories;

namespace BigHW1.Facades
{
    public class OperationFacade
    {
        public List<Operation> _operations { get; set; }

        public OperationFacade()
        {
            _operations = new List<Operation>();
        }

        public void AddOperation(Operation operation) { 
            _operations.Add(operation);
        }
        public void EditOperation(Guid operationId, string newDescription)
        {
            var operation = _operations.FirstOrDefault(c => c.Id == operationId);
            if (operation == null)
            {
                throw new InvalidOperationException("Операция с таким ID не найдена");
            }
            operation.Description = newDescription;
        }
        public void DeleteOperation(Guid operationId)
        {
            var operation = _operations.FirstOrDefault(o => o.Id == operationId);
            if (operation == null)
            {
                throw new InvalidOperationException("Операция с таким ID не найдена");
            }
            _operations.Remove(operation);
            
        }
    }

}
