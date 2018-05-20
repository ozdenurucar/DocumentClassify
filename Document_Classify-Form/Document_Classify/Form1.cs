using System;
using System.Linq;
using System.Windows.Forms;

namespace Document_Classify
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static string dir = "";
        private void btn_choose_Click(object sender, EventArgs e)
        {
            if (folderbrowser.ShowDialog() == DialogResult.OK)
            {
                dir = folderbrowser.SelectedPath+"\\";
            }
            DataSet dataset = new DataSet(dir);
            NaiveBayes.Train bayes = new NaiveBayes.Train(dataset.TrainingData);
            NaiveBayes.Test test = new NaiveBayes.Test(bayes, dataset.Categories);
            test.PredictAll(dataset.TestData);
            var results = test.Results;
            var values = test.PrecisionRecallAndFMeasureForResult();
            grpbx_calculatedvalues.Visible = true;
            double true_prediction = 0;
            foreach (var res in results)
            {
                test_results.Items.Add(res.Item1+"      "+res.Item2);
                if(res.Item1==res.Item2)
                {
                    true_prediction++;
                }
            }
            double oran = Convert.ToDouble((true_prediction / results.Count())*100);
            txt_oran.Text = oran.ToString();
            foreach (var res in values)
            {
                if(res.Key=="ekonomi")
                {
                    ekonomi.Text = "Precision:%" + res.Value.Item1.ToString() + "\n" + "Recall:%" + res.Value.Item2.ToString() + "\n" + "F-Measure:%" + res.Value.Item3.ToString();
                }
                if (res.Key == "magazin")
                {
                    magazin.Text = "Precision:%" + res.Value.Item1.ToString() + "\n" + "Recall:%" + res.Value.Item2.ToString() + "\n" + "F-Measure:%" + res.Value.Item3.ToString();
                }
                if (res.Key == "saglik")
                {
                    saglik.Text = "Precision:%" + res.Value.Item1.ToString() + "\n" + "Recall:%" + res.Value.Item2.ToString() + "\n" + "F-Measure:%" + res.Value.Item3.ToString();
                }
                if (res.Key == "siyasi")
                {
                    siyasi.Text = "Precision:%" + res.Value.Item1.ToString() + "\n" + "Recall:%" + res.Value.Item2.ToString() + "\n" + "F-Measure:%" + res.Value.Item3.ToString();
                }
                if (res.Key == "spor")
                {
                    spor.Text = "Precision:%" + res.Value.Item1.ToString() + "\n" + "Recall:%" + res.Value.Item2.ToString() + "\n" + "F-Measure:%" + res.Value.Item3.ToString();
                }
            }
        }

    }
}
