using BigHW1.ExportData;

namespace BigHW1.Entities
{
    public interface IElement
    {
        void Accept(Visitor visitor);
    }
}
