using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace system
{
    public partial class FormRunOption : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public FormRunOption()
        {
            InitializeComponent();
        }

        private void FormRunOption_Load(object sender, EventArgs e)
        {
            XmlReader xmlReader = null;
            try
            {
                //加载xml文档
                xmlReader = new XmlTextReader(Application.StartupPath + @"\xml\dbconnect.xml");//参数：存储的绝对路径
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlReader);
                XmlNode root = xmlDoc.DocumentElement;  //获取根节点

                XmlNode probability = root.SelectSingleNode("//probability");
                if (probability != null)
                {
                    qTextBoxProbability.Text = probability.InnerText;
                }
                XmlNode device = root.SelectSingleNode("//device");
                if (device != null)
                {
                    qComboBoxDevice.Text = device.InnerText;
                }
                XmlNode multithreading = root.SelectSingleNode("//multithreading");
                if (multithreading != null)
                {
                    qComboBoxMultithreading.Text = multithreading.InnerText;
                }
                XmlNode type = root.SelectSingleNode("//type");
                if (type != null)
                {
                    qComboBoxOutputType.Text = type.InnerText;
                }
                XmlNode picture = root.SelectSingleNode("//picture");
                if (picture.InnerText == "pf")
                {
                    qTextBoxPfNum.Enabled = true;
                }
                XmlNode pfNum = root.SelectSingleNode("//pfNum");
                if (picture != null)
                {
                    qComboBoxPrintPicture.Text = picture.InnerText;
                    if (picture.InnerText == "pf")
                    {
                        qTextBoxPfNum.Text = pfNum.InnerText;
                        qTextBoxPfNum.Enabled = true;
                    }

                }
                XmlNode InputPath = root.SelectSingleNode("//inputPath");
                if (InputPath != null)
                {
                    LbInputPath.Text = InputPath.InnerText;
                }
                XmlNode OutputPath = root.SelectSingleNode("//outputPath");
                {
                    LbOutputPath.Text = OutputPath.InnerText;
                }
                XmlNode DockerPath = root.SelectSingleNode("//dockerPath");
                {
                    LbDockerPath.Text = DockerPath.InnerText;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                xmlReader.Close();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                string url = Application.StartupPath + @"\xml\dbconnect.xml";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(url);

                XmlNode probability = xmlDoc.SelectSingleNode("//probability");
                if (qTextBoxProbability.Text != null)
                {
                    probability.InnerText = qTextBoxProbability.Text;

                }
                XmlNode device = xmlDoc.SelectSingleNode("//device");
                if (qComboBoxDevice.Text != null)
                {
                    device.InnerText = qComboBoxDevice.Text;
                }
                XmlNode multithreading = xmlDoc.SelectSingleNode("//multithreading");
                if (qComboBoxMultithreading.Text != null)
                {
                    multithreading.InnerText = qComboBoxMultithreading.Text;
                }
                XmlNode type = xmlDoc.SelectSingleNode("//type");
                if (qComboBoxOutputType.Text != null)
                {
                    type.InnerText = qComboBoxOutputType.Text;
                }
                XmlNode picture = xmlDoc.SelectSingleNode("//picture");
                XmlNode pfNum = xmlDoc.SelectSingleNode("//pfNum");
                if (qComboBoxPrintPicture.Text != null)
                {
                    picture.InnerText = qComboBoxPrintPicture.Text;
                    if (picture.InnerText == "pf" && IsInteger(qTextBoxPfNum.Text))
                    {
                        pfNum.InnerText = qTextBoxPfNum.Text;
                        xmlDoc.Save(url);
                        MessageBox.Show("Saved successfully！", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }else if (picture.InnerText == "pf")
                        MessageBox.Show("Please enter an integer!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        pfNum.InnerText = null;
                        xmlDoc.Save(url);
                        MessageBox.Show("Saved successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }               
                //xmlDoc.Save(url);
                //MessageBox.Show("Saved successfully！", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDefault_Click(object sender, EventArgs e)
        {
            try
            {
                string url = Application.StartupPath + @"\xml\dbconnect.xml";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(url);

                XmlNode probability = xmlDoc.SelectSingleNode("//probability");
                probability.InnerText = "0.5";
                qTextBoxProbability.Text = "0.5";

                XmlNode device = xmlDoc.SelectSingleNode("//device");
                device.InnerText = "cpu";
                qComboBoxDevice.Text = "cpu";

                XmlNode multithreading = xmlDoc.SelectSingleNode("//multithreading");
                multithreading.InnerText = "1";
                qComboBoxMultithreading.Text = "1";

                XmlNode type = xmlDoc.SelectSingleNode("//type");
                type.InnerText = "csv";
                qComboBoxOutputType.Text = "csv";

                XmlNode picture = xmlDoc.SelectSingleNode("//picture");
                picture.InnerText = "pt";
                qComboBoxPrintPicture.Text = "pt";

                xmlDoc.Save(url);
                MessageBox.Show("Saved successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }   



        //将输入路径写入xml
        private void buttonChangeInput_Click(object sender, EventArgs e)
        {
            string InputPath = SelectPath();
            try
            {
                string url = Application.StartupPath + @"\xml\dbconnect.xml";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(url);
                XmlNode inputPath = xmlDoc.SelectSingleNode("//inputPath");
                if (InputPath != null)
                {
                    inputPath.InnerText = InputPath;
                }
                xmlDoc.Save(url);
                LbInputPath.Text = InputPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //将输出路径写入xml
        private void buttonChangeOutput_Click(object sender, EventArgs e)
        {
            string OutputPath = SelectPath();
            try
            {
                string url = Application.StartupPath + @"\xml\dbconnect.xml";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(url);
                XmlNode outputPath = xmlDoc.SelectSingleNode("//outputPath");
                if (OutputPath != null)
                {
                    outputPath.InnerText = OutputPath;
                }
                xmlDoc.Save(url);
                LbOutputPath.Text = OutputPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //将docker路径写入xml
        private void buttonChangeDocker_Click(object sender, EventArgs e)
        {
            string DockerPath = SelectPath();
            try
            {
                string url = Application.StartupPath + @"\xml\dbconnect.xml";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(url);
                XmlNode dockerPath = xmlDoc.SelectSingleNode("//dockerPath");
                if (DockerPath != null)
                {
                    dockerPath.InnerText = DockerPath;
                }
                xmlDoc.Save(url);
                LbDockerPath.Text = DockerPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //弹出一个选择目录的对话框
        private string SelectPath() 
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.ShowDialog();
            return path.SelectedPath;
        }

        public static bool IsInteger(string s)

        {
            string pattern = @"^\d*$";
            return Regex.IsMatch(s, pattern);
        }

        private void qComboBoxPrintPicture_TextChanged(object sender, EventArgs e)
        {
            if (qComboBoxPrintPicture.Text == "pf")
            {
                qTextBoxPfNum.Enabled = true;
            }
            else
            {
                qTextBoxPfNum.Enabled = false;
            }
        }
    }
}
