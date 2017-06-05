using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Xml;
using System.Xml.XPath;
using System.IO;

namespace TempertureZipApplication
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string zip = zipTextBox.Text;
                if((zipTextBox.TextLength < 5) || (zipTextBox.TextLength > 9))
                {
                    button1.Enabled = true;
                }

                WebRequest request = WebRequest.Create("http://graphical.weather.gov/xml/sample_products/browser_interface/ndfdBrowserClientByDay.php?zipCodeList="+zip+"&format=24+hourly&startDate=2012-04-21&numDays=1");
                Stream response = request.GetResponse().GetResponseStream();
                XmlReader fileReader = XmlReader.Create(response);
                XPathDocument reader = new XPathDocument(fileReader);
                XPathNavigator nav = reader.CreateNavigator();
                XmlNamespaceManager mngr = new XmlNamespaceManager(new NameTable());
                mngr.AddNamespace("xsi", "http://graphical.weather.gov/xml/DWMLgen/schema/DWML.xsd");
                if (radioButton1.Checked)
                {
                    XPathNodeIterator nodes = nav.Select("/dwml/data/parameters/temperture[@type =maximum]/value", mngr);
                    MessageBox.Show("The High Temperature for today is " + nodes.Current.Value);
                    response.Close();
                }
                else if (radioButton2.Checked)
                {
                    XPathNodeIterator nodes = nav.Select("/dwml/data/parameters/temperture[@type =minimum]/value", mngr);
                    MessageBox.Show("The Low Temperature for today is " + nodes.Current.Value);
                    response.Close();
                }
            }
            catch (Exception tefet)
            { MessageBox.Show(tefet.ToString()); }
        }
    }
}
