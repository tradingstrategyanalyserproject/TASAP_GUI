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

        private void Slider_DragCompletedSP(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            int strikePriceVal = (int)StrikePriceSlider.Value;
            StrikePriceVal.Text = strikePriceVal.ToString();
        }

        private void StockPriceSlider_DragCompletedSTP(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            int stockPriceVal = (int)StockPriceSlider.Value;
            StockPriceVal.Text = stockPriceVal.ToString();
        }

        private void MaturitySlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            int marturityVal = (int)MaturitySlider.Value;
            MaturityVal.Text = marturityVal.ToString();
        }

        private void RfRateSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            int rfRateVal = (int)RfRateSlider.Value;
            RfRateVal.Text = rfRateVal.ToString();
        }

        private void VolatilitySlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            int volatilityVal = (int)VolatilitySlider.Value;
            VolatilityVal.Text = volatilityVal.ToString();
        }

        private void Launch_Click(object sender, RoutedEventArgs e)
        {
            // When clicking on Launch button, we call the OptionRequest Class we builded in the TASAP COM Project
            Optionrequest optionrequest = new Optionrequest(optionType, (int)StockPriceSlider.Value, (int)StrikePriceSlider.Value, (int)MaturitySlider.Value, (int)RfRateSlider.Value, (int)VolatilitySlider.Value);
            answerGreecs = optionrequest.Rfq_Handler();
            PayoffVal.Content = answerGreecs.greecsdico.Values.ElementAt(2).ToString();
            BuildChart();
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
            if((bool)STPrb.IsChecked)
            foreach(RadioButton rb in rblist)
                {
                    if(rb.Name != STPrb.Name)
                    {
                        rb.IsChecked = false;
                    }
                }
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
        }
        #endregion


        #region Chart functions

        private string GetCheckedParam()
        {
            foreach (RadioButton rb in rblist)
            {
                if ((bool)rb.IsChecked)
                {
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

            // Naming new Axis
            DataChart.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = GetCheckedParam(),
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
                if (key.ToString() == grec)
                {
                    values.Add(Convert.ToDouble(answerGreecs.greecsdico[key]));
                }
            }
            return values;
        }

        private void AddChartSerie(string grec)
        {
            DataChart.Series.Add(new LineSeries() { Title = grec, Values = new ChartValues<double>(ConvertForChart(grec))});
        }

        #endregion
    }
}
