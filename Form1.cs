using System.Diagnostics;

namespace javaw_wrapper
{
    public partial class Form1 : Form
    {

        private string javawPath = "";
        private string[] arguments;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            arguments = Environment.GetCommandLineArgs();
            richTextBox1.Text = String.Join(' ', arguments);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Java Executable|javaw.exe";
            DialogResult file = openFileDialog.ShowDialog(this);
            if (file != DialogResult.Cancel)
            {
                javawPath = openFileDialog.FileName;
                label3.Text = javawPath;
                button2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(new ThreadStart(threadRunner));
            th.Start();
        }

        public void threadRunner()
        {
            do
            {
                string[] args = arguments.Skip(1).ToArray();
                Process process = new Process();
                process.StartInfo.FileName = javawPath;
                process.StartInfo.Arguments = String.Join(' ', args);
                process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                process.Start();
                process.WaitForExit();
            } while (checkBox1.Checked);
        }
    }
}