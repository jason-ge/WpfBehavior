
namespace WpfBehavior
{
    public class MockFileNode
    {
        public MockFileNode(string fileName)
        {
            Nodes = new List<MockNode>();
            MockFile = fileName;
        }

        public string MockFile { get; set; }
        public List<MockNode> Nodes { get; set; }
    }
}
