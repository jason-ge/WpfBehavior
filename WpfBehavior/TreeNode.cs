using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace WpfBehavior
{
    public enum NodeTypes
    {
        MockFile,
        MockItem
    }
    public class TreeNode : INotifyPropertyChanged
    {
        public ObservableCollection<TreeNode> Children { get; set; } = [];
        public TreeNode? Parent { get; set; }
        public object? Tag { get; set; }
        public string Header
        {
            get
            {
                if (Tag is MockFileNode fileNode)
                {
                    return Path.GetFileNameWithoutExtension(fileNode.MockFile);
                }
                else if (Tag is MockNode mockNode)
                {
                    return $"{mockNode.Url} - {mockNode.MethodName}";
                }
                return "";
            }
        }

        public TreeNode(MockFileNode fileNode)
        {
            NodeType = NodeTypes.MockFile;
            Tag = fileNode;

            foreach (MockNode node in fileNode.Nodes)
            {
                var child = new TreeNode(node) { Parent = this };
                this.Children.Add(child);
            }
        }

        public TreeNode(MockNode node)
        {
            NodeType = NodeTypes.MockItem;
            Tag = node;
        }

        public NodeTypes NodeType { get; set; }

        private bool _isHovered;
        public bool IsHovered
        {
            get => _isHovered;
            set
            {
                if (_isHovered != value)
                {
                    _isHovered = value;
                    OnPropertyChanged(nameof(IsHovered));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
