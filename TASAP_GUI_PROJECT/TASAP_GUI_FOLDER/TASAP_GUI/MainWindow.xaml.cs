using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TASAP_COM;

namespace TASAP_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string optionType = "put";
        public List<RadioButton> rblist = new List<RadioButton>();
        Greecs answerGreecs;
        String currentNameX;

        public MainWindow()

        {
            InitializeComponent();
            rblist.Add(STPrb);
            rblist.Add(STKrb);
            rblist.Add(Volrb);
        }


        #region Components actions 

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {

        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Launch_Click(object sender, RoutedEventArgs e)
        {
            // When clicking on Launch button, we call the OptionRequest Class we builded in the TASAP COM Project
            //OptionRequest OptionRequest = OptionRequestMaker.MakeOptionRequest(variable, min, max, optionType, (int)StockPriceSlider.Value, (int)StrikePriceSlider.Value, (int)MaturitySlider.Value, (int)RfRateSlider.Value, (int)VolatilitySlider.Value);
            //OptionRequest OptionRequest = OptionRequestMaker.MakeOptionRequest(optionType, (int)StockPriceSlider.Value, (int)StrikePriceSlider.Value, (int)MaturitySlider.Value, (int)RfRateSlider.Value, (int)VolatilitySlider.Value);
            //answerGreecs = OptionRequest.Rfq_Handler();
            PayoffVal.Content = answerGreecs.greecsdico.Values.ElementAt(2).ToString();
            BuildChart();
            AddChartSerie("gamma");
        }

        private void OptionType_Checked(object sender, RoutedEventArgs e)
        {
            optionType = "call";

        }
        private void OptionType_Unchecked(object sender, RoutedEventArgs e)
        {
            optionType = "put";

        }

        private void STPrb_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)STPrb.IsChecked)
                foreach (RadioButton rb in rblist)
                {
                    if (rb.Name != STPrb.Name)
                    {
                        rb.IsChecked = false;
                    }
                }
            StockPriceValmin.IsEnabled = false;
            StockPriceValmax.IsEnabled = false;

            VolatilityValmin.IsEnabled = false;
            VolatilityValmax.IsEnabled = false;

            StrikePriceValmin.IsEnabled = true;
            StrikePriceValmax.IsEnabled = true;

        }

        private void STKrb_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)STKrb.IsChecked)
                foreach (RadioButton rb in rblist)
                {
                    if (rb.Name != STKrb.Name)
                    {
                        rb.IsChecked = false;
                    }
                }
            StrikePriceValmin.IsEnabled = false;
            StrikePriceValmax.IsEnabled = false;

            VolatilityValmin.IsEnabled = false;
            VolatilityValmax.IsEnabled = false;

            StockPriceValmin.IsEnabled = true;
            StockPriceValmax.IsEnabled = true;

        }

        private void Volrb_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)Volrb.IsChecked)
                foreach (RadioButton rb in rblist)
                {
                    if (rb.Name != Volrb.Name)
                    {
                        rb.IsChecked = false;
                    }
                }
            StockPriceValmin.IsEnabled = false;
            StockPriceValmax.IsEnabled = false;

            StrikePriceValmin.IsEnabled = false;
            StrikePriceValmax.IsEnabled = false;

            VolatilityValmin.IsEnabled = true;
            VolatilityValmax.IsEnabled = true;
        }

        private void MaturityCb_Selected(object sender, EventArgs e)
        {
            string maturity = MaturityCb.Text;
            Console.WriteLine("THE SELECTED ITEM IS:" + maturity);
            if (maturity == "1 month") { MaturityVal.Text = "0,083"; }
            else if (maturity == "3 months") { MaturityVal.Text = "0,25"; }
            else if (maturity == "6 months") { MaturityVal.Text = "0,5"; }
            else if (maturity == "1 year") { MaturityVal.Text = "1"; }
            else if (maturity == "2 years") { MaturityVal.Text = "2"; }
            else if (maturity == "5 years") { MaturityVal.Text = "5"; }
            else if (maturity == "10 years") { MaturityVal.Text = "10"; }
            else if (maturity == "12 years") { MaturityVal.Text = "12"; }
            else if (maturity == "15 years") { MaturityVal.Text = "15"; }
            else if (maturity == "20 years") { MaturityVal.Text = "20"; }
            else if (maturity == "30 years") { MaturityVal.Text = "30"; }
        }
        #endregion


        #region Chart functions

        private string GetCheckedParam()
        {
            foreach (RadioButton rb in rblist)
            {
                if (rb.IsChecked == true)
                {
                    //Console.WriteLine(rb.Name + "zeifhjgzeyufhé" + (bool)rb.IsChecked);
                    return rb.Name;
                }

                else return null;
            }
            return null;
        }

        private void BuildChart()
        {
            //For some reason i had to clear Axis to have a clean result
            DataChart.AxisX.Clear();
            DataChart.AxisY.Clear();

            DataChart.Series.Clear();

            foreach (RadioButton rb in rblist)
            {
                if (rb.IsChecked == true)
                {
                    currentNameX = rb.Name;
                }
            }

            // Naming new Axis
            DataChart.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = currentNameX,
            });

            DataChart.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Greec(s)",
            }
           );

            SeriesCollection series = new SeriesCollection();
            series.Add(new LineSeries() { Title = "FIST", Values = new ChartValues<double> { 2, 3, 4, 5 } });
            DataChart.Series = series;
        
        }


        private List<double> ConvertForChart(string grec)
        {
            List<double> values = new List<double>();
            foreach (Object key in answerGreecs.greecsdico.Keys)
            {
                if (key.ToString() == grec+": ")
                {
                    values.Add(Convert.ToDouble(answerGreecs.greecsdico[key]));
                }
            }
            return values;
        }

        private void AddChartSerie(string grec)
        {
            DataChart.Series.Add(new LineSeries() { Title = grec, Values = new ChartValues<double>(ConvertForChart(grec))});
            Console.WriteLine("Added to the chart" + ConvertForChart(grec));
        }

        private void RemoveChartSeries(string grec)
        {
            foreach(LineSeries ls in DataChart.Series)
            {
                if (ls.Title == grec)
                {
                    Console.WriteLine("Got insode");
                    DataChart.Series.Remove(ls);
                }
            }
        }

        #endregion

        #region Greec selection

        private void DeltaCheck_Selected(object sender, RoutedEventArgs e)
        {
            AddChartSerie("delta");
        }

        private void GammaCheck_Selected(object sender, RoutedEventArgs e)
        {
            AddChartSerie("gamma");
        }

        private void VegaCheck_Selected(object sender, RoutedEventArgs e)
        {
            AddChartSerie("vega");
        }

        private void ThetaCheck_Selected(object sender, RoutedEventArgs e)
        {
            AddChartSerie("theta");
        }

        private void RhoCheck_Selected(object sender, RoutedEventArgs e)
        {
            AddChartSerie("rho");
        }

        #endregion

        private void DeltaCheck_Unselected(object sender, RoutedEventArgs e)
        {
            RemoveChartSeries("delta");
        }

        private void GammaCheck_Unselected(object sender, RoutedEventArgs e)
        {
            RemoveChartSeries("gamma");
        }

        private void VegaCheck_Unselected(object sender, RoutedEventArgs e)
        {
            RemoveChartSeries("vega");
        }

        private void ThetaCheck_Unselected(object sender, RoutedEventArgs e)
        {
            RemoveChartSeries("theta");
        }

        private void RhoCheck_Unselected(object sender, RoutedEventArgs e)
        {
            RemoveChartSeries("rho");
        }
    }
}
