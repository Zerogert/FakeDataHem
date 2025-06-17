using FakeDataHem.Models.Canvas;
using System.Collections.ObjectModel;

namespace FakeDataHem.ViewModels.Controls
{
    public class MapViewModels
    {
        public ObservableCollection<Node> Nodes { get; } = new ObservableCollection<Node>();
    }
}
