using System;
using System.Windows.Forms;
using NeuralNetworkCSharp;
using NeuralNetworkCSharp.ActivationFunctions;
using NeuralNetworkCSharp.InputFunctions;

namespace SimpleNeuralNetworkInCSharpApp
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            txtOutput.Text = "Starting...";
            this.Cursor = Cursors.WaitCursor;
            var network = new SimpleNeuralNetwork(3);
            var layerFactory = new NeuralLayerFactory();
            network.AddLayer(layerFactory.CreateNeuralLayer(2, new RectifiedActivationFuncion(), new WeightedSumFunction()));
            network.AddLayer(layerFactory.CreateNeuralLayer(3, new RectifiedActivationFuncion(), new WeightedSumFunction()));
            network.AddLayer(layerFactory.CreateNeuralLayer(2, new RectifiedActivationFuncion(), new WeightedSumFunction()));
            network.AddLayer(layerFactory.CreateNeuralLayer(1, new SigmoidActivationFunction(0.7), new WeightedSumFunction()));
            
            network.PushExpectedValues(
                                        new double[][] {
                                        new double[] { 0 },
                                        new double[] { 1 },
                                        new double[] { 1 },
                                        new double[] { 0 },
                                        new double[] { 1 },
                                        new double[] { 0 },
                                        new double[] { 0 },
                                        });

            network.Train(
                            new double[][] {
                            new double[] { 150, 2, 0 },
                            new double[] { 1002, 56, 1 },
                            new double[] { 1060, 59, 1 },
                            new double[] { 200, 3, 0 },
                            new double[] { 300, 3, 1 },
                            new double[] { 120, 1, 0 },
                            new double[] { 80, 1, 0 },
                            }, 10000);

            network.PushInputValues(new double[] { 1054, 54, 1 });
            var outputs = network.GetOutput();
            foreach(var output in outputs)
            {
                txtOutput.Text = output.ToString();
            }
            this.Cursor = Cursors.Default;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            txtOutput.Text = "Ready.";
        }
    }
}



//Online source: 
//https://rubikscode.net/2022/07/04/implementing-simple-neural-network-in-c/?fbclid=IwAR2y1oEIllGUfsjzyjca4vlRqNTJLMmmnayhJBiwt_Jnjhh1CHEJ6pTiHUI