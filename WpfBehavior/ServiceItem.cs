using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBehavior
{
    class ServiceItem
    {
        public ServiceItem(TreeNode treeNode) 
        {
            TreeNodeRef = treeNode;
        }

        public TreeNode TreeNodeRef { get; set; }

        public string Text
        {
            get
            {
                if (TreeNodeRef.Tag is MockNode mockNode)
                {
                    return $"{mockNode.Url} - {mockNode.MethodName}";
                }
                return string.Empty;
            }
        }
    }
}
