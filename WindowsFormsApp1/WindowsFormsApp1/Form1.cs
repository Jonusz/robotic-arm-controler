using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private const string V = "SP ";
        private const string X = "DJ 1,";
        private const string X2 = "DJ 2,";
        private const string X3 = "DJ 3,";
        private const string X4 = "DJ 4,";
        private const string X5 = "DJ 5,";
        private const string X6 = "DJ 6,";
        private const string V1 = " ";
        private string a;
        string dataOUT;
        string dataIN;
        string toSend;
        string toBox;
        int trackBar1_Value;
        int trackBar2_Value;
        int hScrollBarValue;
        int hScrollBarValue2;
        int hScrollBarValue3;
        int hScrollBarValue4;
        int hScrollBarValue5;
        int hScrollBarValue6;
        DateTime lastScrollTime = DateTime.MinValue;
        private bool isSliderBeingDragged = false;

        public Form1()
        {
            InitializeComponent();

            // Subscribe to the ValueChanged event of the slider
           // trackBar2.ValueChanged += trackBar2_ValueChanged;

            // Subscribe to the MouseUp event to detect when the slider is released
            //trackBar2.MouseUp += TrackBar2_MouseUp;

        }


        private void UpdateValues()
        {

            // Subscribe to the ValueChanged event of the slider
           // trackBar2.ValueChanged += trackBar2_ValueChanged;

            // Subscribe to the MouseUp event to detect when the slider is released
            trackBar2.MouseUp += TrackBar2_MouseUp;

            trackBar1_Value = trackBar1.Value;
            trackBar2_Value = trackBar2.Value;
            hScrollBarValue = hScrollBar1.Value;
            hScrollBarValue2 = hScrollBar2.Value;
            hScrollBarValue3 = hScrollBar3.Value;
            hScrollBarValue4 = hScrollBar4.Value;
            hScrollBarValue5 = hScrollBar5.Value;
            hScrollBarValue6 = hScrollBar6.Value;



        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tBoxDataOut_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cBoxCOMPORT.Items.AddRange(ports);

        }

        private void btnOpen_Click(object sender, EventArgs e)

        {
            try
            {
                serialPort1.PortName = cBoxCOMPORT.Text;
                serialPort1.BaudRate = Convert.ToInt32(cBoxBAUDRATE.Text);
                serialPort1.DataBits = Convert.ToInt32(cBoxDATABITS.Text);
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cBoxSTOPBITS.Text);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cBoxPARITYBITS.Text);

                serialPort1.Open();
                progressBar1.Value = 100;
                labelStatus.Text = "Connected";
                ovalShape1.FillColor = Color.Green;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Błąd/Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                labelStatus.Text = "Disconnected";
                ovalShape1.FillColor = Color.Red;
            }




        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                progressBar1.Value = 0;

            }
            labelStatus.Text = "Disconnected";
            ovalShape1.FillColor = Color.Red;

        }

        private void btnSendData_Click(object sender, EventArgs e)
        {

            string messages = tBoxDataOut.Text;

            // Split the string into an array of strings using line breaks as the delimiter
            string[] messageArray = messages.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            // Convert the array to a vector (List<string>), if needed
            var messageVector = new System.Collections.Generic.List<string>(messageArray);

            // Print each message
            foreach (string message in messageVector)
            {
                serialPort1.Write(message + "\r");
                Thread.Sleep(300);
                // Console.WriteLine(message);
            }


            /*
            if (serialPort1.IsOpen)
            {
                dataOUT = tBoxDataOut.Text;
                // serialPort1.WriteLine(dataOUT);
                serialPort1.Write(dataOUT + "\r");
            }
            */


        }

        private void cBoxSTOPBITS_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (tBoxDataOut.Text != "")
            {
                tBoxDataOut.Text = "";
            }
        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void coToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void jogOperationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void cBoxCOMPORT_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            dataIN = serialPort1.ReadExisting();
            this.Invoke(new EventHandler(ShowData));

        }
        private void ShowData(object sender, EventArgs e)
        {
            TextBoxDataReceive.Text = dataIN;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void ShowText()
        {
            switch (listBox1.SelectedItem)
            {
                case "GC":
                    label16.Text = "Grip Close";
                    break;

                case "G0":
                    label16.Text = "Grip Open";
                    break;
                case "WH":
                    label16.Text = "Where";
                    break;
                case "DJ":
                    label16.Text = "Draw Joint:\r * [DJ joint, angle]";
                    break;
                case "SP":
                    label16.Text = "Speed(parameter)(axial interpolation): \r* [SP speed]";
                    break;
                case "DS":
                    label16.Text = "Draw Straight (Linear - XYZ move): \r* [DS 0, 0, 100]";
                    break;
                case "DW":
                    label16.Text = "Draw (Axel - XYZ move): \r* [DW 0, 0, 100]";
                    break;
                case "PD":
                    label16.Text = "Position Define: \r* [PD pos_number, X, Y, Z, A, B, C, R, A, F]";
                    break;
                case "PC":
                    label16.Text = "Position Clear: \r* [PC pos_number]";
                    break;
                case "PL":
                    label16.Text = "Position Load/Swap: \r* [PL old_pos_number, new_pos_number]";
                    break;
                case "PR":
                    label16.Text = "Position Read: \r* [PR pos_number]";
                    break;
                case "HE":
                    label16.Text = "Here(save actual position as position: pos_number): \r* [HE pos_number]";
                    break;
                case "MO":
                    label16.Text = "Move(axial interpolation): \r* [MO joint]";
                    break;
                case "MS":
                    label16.Text = "Move Straight(linear interpolation): \r* [MS joint]";
                    break;
                case "MR":
                    label16.Text = "Move R(circular interpolation):\r *[MR 1, 2, 3]";
                    break;
                case "MA":
                    label16.Text = "Move Approach(linear interpolation(1+2)): \r * [MA 1, 2]";
                    break;
                case "MC":
                    label16.Text = "Move Continuously(linear interpolation - punkty pośrednie):\r* [MC 1, 4]";
                    break;
                case "MJ":
                    label16.Text = "Move Joint(axial interpolation from actual position):\r* [MJ X, Y, Z, A, B, C]";
                    break;
                case "MP":
                    label16.Text = "Move Position(axial interpolation) - kartezjańskie absolutne: \r* [MP X, Y, Z, A, B, C]";
                    break;
                case "MT":
                    label16.Text = "Move Tool(axial interpolation - tool's layout):\r * [MT 1, -10]";
                    break;
                case "MTS":
                    label16.Text = "Move Tool Straight(linear interpolation - tool's layout):\r * [MTS 1, 20]";
                    break;
                case "CL":
                    label16.Text = "Counter Load";
                    break;
                case "CP":
                    label16.Text = "Compare Counter";
                    break;
                case "DA":
                    label16.Text = "Disable Act";
                    break;
                case "DC":
                    label16.Text = "Decrement counter";
                    break;
                case "DL":
                    label16.Text = "Delete Line";
                    break;
                case "EA":
                    label16.Text = "Enable Act";
                    break;
                case "ED":
                    label16.Text = "END";
                    break;
                case "RT":
                    label16.Text = "Return";
                    break;
                case "GO":
                    label16.Text = "Grip Open";
                    break;
                case "GP":
                    label16.Text = "Grip Pressure(parameters):\r* [GP start_force, end_force, time]";
                    break;

            }


        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                //valueListbox1 = listBox1.SelectedItem.ToString();
                tBoxCommand.Text = "";
                tBoxCommand.Text = tBoxCommand.Text + listBox1.SelectedItem.ToString();
                toSend = tBoxCommand.Text;
            }
            ShowText();

        }

        private void listBox1_Click(object sender, EventArgs e)
        {

            tBoxCommand.Text = listBox1.SelectedItem.ToString();
        }

        private void btnAddCommand_Click(object sender, EventArgs e)
        {

            if (progressBar1.Value == 100)
            {
                //toSend = Convert.ToString(listBox1.SelectedItem);

                serialPort1.Write(toSend + "\r");
               // tBoxCommand = " ";
            }
            else
            {
                MessageBox.Show("1.Choose a port, 2.Open it, 3. Send Data");
            }

            tBoxCommand.Text = " ";

        }

        private void TextBoxDataReceive_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void tBoxCommand_TextChanged(object sender, EventArgs e)
        {
            if (tBoxCommand.Text.Contains("["))
            {

                tBoxCommand.Text = tBoxCommand.Text.Remove(tBoxCommand.SelectionStart - 1, 1);
                // USUWA NAWIAS MOGE PODZIALAC WIECEJ
                // MessageBox.Show("");



            }
            else
            {
                toSend = tBoxCommand.Text;
            }
            tBoxCommand.SelectionStart = tBoxCommand.Text.Length;


        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void label6_Click_1(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label14.Text = trackBar1.Value.ToString();
            UpdateValues();

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            UpdateValues();




        }

        private async void hScrollBar1_Scroll_1(object sender, ScrollEventArgs e)
        {
            DateTime now = DateTime.Now;
            if ((now - lastScrollTime).TotalMilliseconds > 1000) // Adjust this delay as needed
            {
                lastScrollTime = now;

                UpdateValues();
                int DelayMs = 500;
                await Task.Delay(DelayMs);
                //serialPort1.Write("10 " + V + trackBar1_Value + "\r" + "20 " + X + hScrollBarValue + "\r" + "RN 10 20" + "\r");
                serialPort1.Write("10 " + V + trackBar1_Value + "\r" + "20 " + X + " " + hScrollBarValue + "\r" + "RN 10 20" + "\r");

            }


        }

        private async void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            DateTime now = DateTime.Now;
            if ((now - lastScrollTime).TotalMilliseconds > 1000) // Adjust this delay as needed
            {
                lastScrollTime = now;

                UpdateValues();
                int DelayMs = 500;
                await Task.Delay(DelayMs);
                serialPort1.Write(V + trackBar1_Value + V1 + X2 + hScrollBarValue2 + "\r");

            }
        }

        private async void hScrollBar3_Scroll(object sender, ScrollEventArgs e)
        {
            DateTime now = DateTime.Now;
            if ((now - lastScrollTime).TotalMilliseconds > 1000) // Adjust this delay as needed
            {
                lastScrollTime = now;

                UpdateValues();
                int DelayMs = 500;
                await Task.Delay(DelayMs);
                serialPort1.Write(V + trackBar1_Value + V1 + X3 + hScrollBarValue3 + "\r");

            }
        }

        private async void hScrollBar4_Scroll(object sender, ScrollEventArgs e)
        {
            DateTime now = DateTime.Now;
            if ((now - lastScrollTime).TotalMilliseconds > 1000) // Adjust this delay as needed
            {
                lastScrollTime = now;

                UpdateValues();
                int DelayMs = 500;
                await Task.Delay(DelayMs);
                serialPort1.Write(V + trackBar1_Value + V1 + X4 + hScrollBarValue4 + "\r");

            }
        }

        private async void hScrollBar5_Scroll(object sender, ScrollEventArgs e)
        {
            DateTime now = DateTime.Now;
            if ((now - lastScrollTime).TotalMilliseconds > 1000) // Adjust this delay as needed
            {
                lastScrollTime = now;

                UpdateValues();
                int DelayMs = 500;
                await Task.Delay(DelayMs);
                serialPort1.Write(V + trackBar1_Value + V1 + X5 + hScrollBarValue5 + "\r");

            }
        }

        private async void hScrollBar6_Scroll(object sender, ScrollEventArgs e)
        {
            DateTime now = DateTime.Now;
            if ((now - lastScrollTime).TotalMilliseconds > 1000) // Adjust this delay as needed
            {
                lastScrollTime = now;

                UpdateValues();
                int DelayMs = 500;
                await Task.Delay(DelayMs);
                serialPort1.Write(V + trackBar1_Value + V1 + X6 + hScrollBarValue6 + "\r");

            }
        }

        
        private async void trackBar2_Scroll(object sender, EventArgs e){

            label15.Text = trackBar2.Value.ToString();
            DateTime now = DateTime.Now;
            await Task.Delay(2000);

            /*
            if (!isSliderBeingDragged)
            {
                serialPort1.Write(V + trackBar1_Value + "\r" + X + " " + trackBar2.Value + "\r");
            }
            */

           /*
            if ((now - lastScrollTime).TotalMilliseconds > 1000) // Adjust this delay as needed
            {
                lastScrollTime = now;

                UpdateValues();
                //int DelayMs = 500;
                //await Task.Delay(DelayMs);
                serialPort1.Write(V + trackBar1_Value + "\r" + X + " " + trackBar2.Value + "\r");

            }
           */
        }
        
        /*
        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            // This event is triggered continuously as the slider value changes
            // Check if the slider is currently being dragged
            if (!isSliderBeingDragged)
            {
                // Code to execute when the slider is released
                // For example, update a label with the current value
                label15.Text = "Slider Value: " + trackBar2.Value;
                serialPort1.Write(V + trackBar1_Value + V1 + X + trackBar2_Value + "\r");
            }
        }
        */

        
        private void TrackBar2_MouseUp(object sender, MouseEventArgs e)
        {
            // Set the flag to indicate that the slider is no longer being dragged
            isSliderBeingDragged = false;

            // Additional code to execute when the slider is released, if needed
        }

        private void trackBar2_MouseDown(object sender, MouseEventArgs e)
        {
            // Set the flag to indicate that the slider is being dragged
            isSliderBeingDragged = true;
        }
        

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Write("GC" + "\r");
        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void btnGridOpen_Click(object sender, EventArgs e)
        {
            serialPort1.Write("GO" + "\r");
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void btnGridOpen_Click_1(object sender, EventArgs e)
        {
            serialPort1.Write("GO" + "\r");
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void trackBar2_MouseEnter(object sender, EventArgs e)
        {

        }

        private void hScrollBar1_QueryAccessibilityHelp(object sender, QueryAccessibilityHelpEventArgs e)
        {

        }

        private void hScrollBar1_CursorChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            serialPort1.Write("10 " + V + trackBar1.Value + "\r");
            Thread.Sleep(300);
            serialPort1.Write("20 " + X + " " + trackBar2.Value + "\r");
            Thread.Sleep(300);
            serialPort1.Write("RN 10 20" + "\r");
            Thread.Sleep(300);

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            serialPort1.Write("10 " + V + trackBar1.Value + "\r");
            Thread.Sleep(300);
            serialPort1.Write("20 " + X + " -" + trackBar2.Value + "\r");
            Thread.Sleep(300);
            serialPort1.Write("RN 10 20" + "\r");
            Thread.Sleep(300);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            serialPort1.Write("10 " + V + trackBar1.Value + "\r");
            Thread.Sleep(300);
            serialPort1.Write("20 " + X2 + " -" + trackBar2.Value + "\r");
            Thread.Sleep(300);
            serialPort1.Write("RN 10 20" + "\r");
            Thread.Sleep(300);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            serialPort1.Write("10 " + V + trackBar1.Value + "\r");
            Thread.Sleep(300);
            serialPort1.Write("20 " + X2 + " " + trackBar2.Value + "\r");
            Thread.Sleep(300);
            serialPort1.Write("RN 10 20" + "\r");
            Thread.Sleep(300);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            serialPort1.Write("10 " + V + trackBar1.Value + "\r");
            Thread.Sleep(300);
            serialPort1.Write("20 " + X3 + " " + trackBar2.Value + "\r");
            Thread.Sleep(300);
            serialPort1.Write("RN 10 20" + "\r");
            Thread.Sleep(300);
        }

        private void Joint_3_L_Click(object sender, EventArgs e)
        {
            serialPort1.Write("10 " + V + trackBar1.Value + "\r");
            Thread.Sleep(300);
            serialPort1.Write("20 " + X3 + " -" + trackBar2.Value + "\r");
            Thread.Sleep(300);
            serialPort1.Write("RN 10 20" + "\r");
            Thread.Sleep(300);
        }

        private void Joint_4_L_Click(object sender, EventArgs e)
        {
            serialPort1.Write("10 " + V + trackBar1.Value + "\r");
            Thread.Sleep(300);
            serialPort1.Write("20 " + X4 + " -" + trackBar2.Value + "\r");
            Thread.Sleep(300);
            serialPort1.Write("RN 10 20" + "\r");
            Thread.Sleep(300);
        }

        private void Joint_4_R_Click(object sender, EventArgs e)
        {
            serialPort1.Write("10 " + V + trackBar1.Value + "\r");
            Thread.Sleep(300);
            serialPort1.Write("20 " + X4 + " " + trackBar2.Value + "\r");
            Thread.Sleep(300);
            serialPort1.Write("RN 10 20" + "\r");
            Thread.Sleep(300);
        }

        private void Joint_5_L_Click(object sender, EventArgs e)
        {
            serialPort1.Write("10 " + V + trackBar1.Value + "\r");
            Thread.Sleep(300);
            serialPort1.Write("20 " + X5 + " -" + trackBar2.Value + "\r");
            Thread.Sleep(300);
            serialPort1.Write("RN 10 20" + "\r");
            Thread.Sleep(300);
        }

        private void Joint_5_R_Click(object sender, EventArgs e)
        {
            serialPort1.Write("10 " + V + trackBar1.Value + "\r");
            Thread.Sleep(300);
            serialPort1.Write("20 " + X5 + " " + trackBar2.Value + "\r");
            Thread.Sleep(300);
            serialPort1.Write("RN 10 20" + "\r");
            Thread.Sleep(300);
        }

        private void Joint_6_L_Click(object sender, EventArgs e)
        {
            serialPort1.Write("10 " + V + trackBar1.Value + "\r");
            Thread.Sleep(300);
            serialPort1.Write("20 " + X6 + " -" + trackBar2.Value + "\r");
            Thread.Sleep(300);
            serialPort1.Write("RN 10 20" + "\r");
            Thread.Sleep(300);
        }

        private void Joint_6_R_Click(object sender, EventArgs e)
        {
            serialPort1.Write("10 " + V + trackBar1.Value + "\r");
            Thread.Sleep(300);
            serialPort1.Write("20 " + X6 + " " + trackBar2.Value + "\r");
            Thread.Sleep(300);
            serialPort1.Write("RN 10 20" + "\r");
            Thread.Sleep(300);
        }

        /*
        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            serialPort1.Write(V + trackBar1_Value + V1 + X + trackBar2_Value + "\r");
        }
        */
    }
}

 