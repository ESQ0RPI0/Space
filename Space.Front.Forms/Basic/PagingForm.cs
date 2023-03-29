using Space.Front.Forms.Attributes;
using Space.Front.Forms.Basic;

namespace Space.Client.Forms.Basic
{
    public class PagingForm : QueryModelBase
    {
        [Query(nameof(Count))]
        public int Count { get; set; }
        [Query(nameof(Offset))]
        public int Offset { get; set; }
    }
}
