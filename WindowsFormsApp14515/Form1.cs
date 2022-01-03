using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WK.Libraries.SharpClipboardNS;
using static WK.Libraries.SharpClipboardNS.SharpClipboard;
using System.Threading;
using System.IO;
using System.Data.Common;

namespace WindowsFormsApp14515
{


    public partial class Form1 : Form




    {
        public Form1()
        {
            InitializeComponent();

        }




        public void tableout (object sender, EventArgs e)
        {
       

        }

        public int counted = 0;
        public string filetype;
        public string state;

      


        public DataTable dt = new DataTable();
        


        private void initclipboard(object sender, EventArgs e)

        {
            if (state != "started")
            {
                state = "started";
                dt.Columns.Clear();
                dt.Rows.Clear();
                var clipboard = new SharpClipboard();
                clipboard.StartMonitoring();
                clipboard.ClipboardChanged += ClipboardChanged;
                string res = Clipboard.GetText();
                label8.Text = "COPYING";
                label8.ForeColor = Color.Green;
                counted = 0;
            }
        }


        private void stopclipboard(object sender, EventArgs e)

        {

            Clipboard.Clear();
            Application.Restart();

        }


        public void ClipboardChanged(Object sender, ClipboardChangedEventArgs e)
        {


            try
            {


                if (e.ContentType == SharpClipboard.ContentTypes.Text & counted <= 49 & !(state != "started"))

                {

                    if (dt.Columns.Count == 0)
                    {

                        dt.Columns.Add();
                        dt.Columns.Add();
                        dt.Columns.Add();
                        dt.Columns.Add();
                    }

                    {
                        var clipboard = new SharpClipboard();
                        string cont = (e.Content.ToString());
                        string appinfo = (e.SourceApplication.ToString());
                        string dates = DateTime.Now.ToString();
                        int cnt = counted + 1;
                        counted = cnt;

                        var dr1 = dt.Rows.Add(cont,appinfo,dates,cnt);

                        dt.AcceptChanges();

                        label2.Text = counted.ToString();
                        label2.ForeColor = Color.Green;
                        richTextBox1.Text = cont.ToString();

                    }
                    dt.AcceptChanges();
                    
                    return;

                }

                if (counted < 49)
                {
                    return;
                }

                if (counted > 48)
                {
                    label2.Text = counted.ToString() + " (MAX)";
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        public void pause(object sender, EventArgs e)

        {
            
            Clipboard.Clear();
            richTextBox1 = null;

        }

  
        public void clearcontents (object sender, EventArgs e)
        {
            label2.Text = "0";
            label2.ForeColor = SystemColors.MenuHighlight;
            counted = 0;
            richTextBox1.Text = null;
            dt.Clear();
            counted = 0;
            
        }




        public void writetofile(object sender, EventArgs e)

        {




            if (radioButton1.Checked == true)
            {
                filetype = ".csv";
            }

            if (radioButton2.Checked == true)
            {
                filetype = ".txt";
            }


            if (dt.Rows.Count == 0)
            {
                return;
            }



            if (dt.Rows.Count > 0)

                try
                {
                    button4.Text = "Generating...";
                    button4.Enabled = false;

                    string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + filetype;
                    string newpath = Path.Combine(textBox1.Text.ToString(), ("CopiedStrings"+filename));
                    var tw = new StreamWriter((newpath), true);

                    using (tw)
                    {


                        foreach (DataRow dr in dt.Rows)

                        {

                            string copiedstring = dr.Field<string>(0);
                            string appdetails = dr.Field<string>(1);
                            string datez = dr.Field<string>(2);
                            string count = dr.Field<string>(3);



                            StringBuilder fullstring = new StringBuilder();



                            if (radioButton2.Checked == true)

                            {
                                if (checkBox1.Checked == true && checkBox2.Checked == true)
                                {
                                    fullstring.Append("---COPIED STRING " + count + "--- " + Environment.NewLine + "STRING:" + Environment.NewLine + copiedstring + Environment.NewLine + "APPLICATION DETAILS:" + Environment.NewLine + appdetails + Environment.NewLine + "DATETIME OF COPY:" + Environment.NewLine + datez + Environment.NewLine + "-----" + Environment.NewLine + "" + Environment.NewLine);
                                    tw.WriteLine(fullstring);
                                    tw.Flush();
                                }

                                if (checkBox1.Checked == true && checkBox2.Checked == false)
                                {
                                    fullstring.Append("---COPIED STRING " + count + "--- " + Environment.NewLine + "STRING:" + Environment.NewLine + copiedstring + Environment.NewLine + "APPLICATION DETAILS:" + Environment.NewLine + appdetails + Environment.NewLine + "-----" + Environment.NewLine + "" + Environment.NewLine);
                                    tw.WriteLine(fullstring);
                                    tw.Flush();
                                }


                                if (checkBox1.Checked == false && checkBox2.Checked == true)
                                {
                                    fullstring.Append("---COPIED STRING " + count + "--- " + Environment.NewLine + "STRING:" + Environment.NewLine + copiedstring + Environment.NewLine + "DATETIME OF COPY:" + Environment.NewLine + datez + Environment.NewLine + "-----" + Environment.NewLine + "" + Environment.NewLine);
                                    tw.WriteLine(fullstring);
                                    tw.Flush();
                                }

                                if (checkBox1.Checked == false && checkBox2.Checked == false)
                                {
                                    fullstring.Append("---COPIED STRING " + count + "--- " + Environment.NewLine + "STRING:" + Environment.NewLine + copiedstring + Environment.NewLine + "-----" + Environment.NewLine + "" + Environment.NewLine);
                                    tw.WriteLine(fullstring);
                                    tw.Flush();
                                }

                                
                            }

                            if (radioButton1.Checked == true)

                            {
                                if (checkBox1.Checked == true && checkBox2.Checked == true)
                                {
                                    fullstring.Append(@"""" + count + @"""" + "," + @"""" + copiedstring + @"""" + "," + @"""" + appdetails + @"""" + "," + @"""" + datez + @"""");
                                    tw.WriteLine(fullstring);
                                    tw.Flush();
                                }

                                if (checkBox1.Checked == true && checkBox2.Checked == false)
                                {
                                    fullstring.Append(@"""" + count + @"""" + "," + @"""" + copiedstring + @"""" + "," + @"""" + appdetails + @"""");
                                    tw.WriteLine(fullstring);
                                    tw.Flush();
                                }


                                if (checkBox1.Checked == false && checkBox2.Checked == true)
                                {
                                    fullstring.Append(@"""" + count + @"""" + "," + @"""" + copiedstring + @"""" + "," + @"""" + datez + @"""");
                                    tw.WriteLine(fullstring);
                                    tw.Flush();
                                }

                                if (checkBox1.Checked == false && checkBox2.Checked == false)
                                {
                                    fullstring.Append(@"""" + count + @"""" + "," + @"""" + copiedstring + @"""");
                                    tw.WriteLine(fullstring);
                                    tw.Flush();
                                }

                                
                            }

                        }


                    }



                }

                catch (Exception ex)
                {

                    if (ex.Message.Contains("Could not find"))
                    {
                        MessageBox.Show("Please check that your Filepath is valid.", "Path not valid.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        MessageBox.Show(ex.Message, "Error.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                }



            button4.Text = "File Generated";
            button4.ForeColor = System.Drawing.Color.Green;
            Thread.Sleep(1000);


            button4.Enabled = true;
            button4.Text = "Generate";
            button4.ForeColor = System.Drawing.SystemColors.Highlight;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    }
