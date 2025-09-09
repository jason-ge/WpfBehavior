using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace WpfBehavior
{
    class MainWindowViewModel
    {
        public ObservableCollection<TreeNode> RootNodes { get; } = [];
        public ObservableCollection<ServiceItem> TextItems { get; } = [];
        public ICommand TextBoxMouseEnterCommand { get; }
        public ICommand TextBoxMouseLeaveCommand { get; }
        public MainWindowViewModel()
        {
            for (int i = 0; i < 10; i++)
            {
                RootNodes.Add(new TreeNode(new MockFileNode($"FileNode {i}.xml")
                {
                    Nodes = new List<MockNode>
                    {
                        new MockNode { Url = "http://example.com/api/1", MethodName = "GetData" },
                        new MockNode { Url = "http://example.com/api/2", MethodName = "PostData" },
                        new MockNode { Url = "http://example.com/api/3", MethodName = "DeleteData" }
                    }
                }));
                TextItems.Add(new ServiceItem(RootNodes[i].Children[0]));
                TextItems.Add(new ServiceItem(RootNodes[i].Children[1]));
                TextItems.Add(new ServiceItem(RootNodes[i].Children[2]));
            }
            TextBoxMouseEnterCommand = new RelayCommand<ServiceItem>(OnTextBoxMouseEnter);
            TextBoxMouseLeaveCommand = new RelayCommand<ServiceItem>(OnTextBoxMouseLeave);
        }

        private void OnTextBoxMouseEnter(ServiceItem? item)
        {
            var node = item?.TreeNodeRef as TreeNode;
            if (node != null)
            {
                node.IsHovered = true;
                node.Parent.IsHovered = true;
            }
        }

        private void OnTextBoxMouseLeave(ServiceItem? item)
        {
            var node = item?.TreeNodeRef as TreeNode;
            if (node != null)
            {
                node.IsHovered = false;
                node.Parent.IsHovered = false;
            }
        }
    }
}
