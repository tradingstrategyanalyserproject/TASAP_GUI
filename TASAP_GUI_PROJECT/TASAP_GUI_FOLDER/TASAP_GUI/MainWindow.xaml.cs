using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        OptionRequest OptionRequest;
        bool strike_already_checked = false;
        bool stock_already_checked = false;
        bool vol_already_checked = false;
        bool logicalError = false;

        public MainWindow()

        {
            InitializeComponent();
            rblist.Add(Strike_Price);
            rblist.Add(Stock_Price);
            rblist.Add(Volatility);
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

        private float stringToFloat(string val)
        {
            float res;
            res = float.Parse(val, CultureInfo.InvariantCulture.NumberFormat);
            return res;
        }

        private void Launch_Click(object sender, RoutedEventArgs e)
        {
            // When clicking on Launch button, we call the OptionRequest Class we builded in the TASAP COM Project
            //OptionRequest OptionRequest = OptionRequestMaker.MakeOptionRequest(variable, min, max, optionType, (int)StockPriceSlider.Value, (int)StrikePriceSlider.Value, (int)MaturitySlider.Value, (int)RfRateSlider.Value, (int)VolatilitySlider.Value);
            logicalError = false;
            yParam.IsEnabled = true;
            yParam.UnselectAll();
            bool checkedval = false;

            foreach (RadioButton rb in rblist)
            {
                if (rb.IsChecked == true)
                {
                    checkedval = true;
                    switch (rb.Name)
                    {
                        case "Strike_Price": // for the strike price
                            if (stringToFloat(StrikePriceValmin.Text) >= stringToFloat(StrikePriceValmax.Text))
                            {
                                MessageBox.Show("Min value can't be lower than the max value.");
                                logicalError = true;
                                break;
                            }
                            else
                            {
                                OptionRequest = OptionRequestMaker.MakeOptionRequest("variable", "strike", StrikePriceValmin.Text, StrikePriceValmax.Text, optionType, MaturityVal.Text, RfRateVal.Text, StockPriceVal.Text, VolatilityVal.Text);
                                break;
                            }
                        case "Stock_Price":
                            if (stringToFloat(StockPriceValmin.Text) >= stringToFloat(StockPriceValmax.Text))
                            {
                                MessageBox.Show("Min value can't be lower than the max value.");
                                logicalError = true;
                                break;
                            }
                            else
                            {
                                OptionRequest = OptionRequestMaker.MakeOptionRequest("variable", "spot", StockPriceValmin.Text, StockPriceValmax.Text, optionType, MaturityVal.Text, RfRateVal.Text, StrikePriceVal.Text, VolatilityVal.Text);
                                break;
                            }
                        case "Volatility":
                            if (stringToFloat(VolatilityValmin.Text) >= stringToFloat(VolatilityValmax.Text))
                            {
                                MessageBox.Show("Min value can't be lower than the max value.");
                                logicalError = true;
                                break;
                            }
                            else
                            {
                                OptionRequest = OptionRequestMaker.MakeOptionRequest("variable", "sigma", VolatilityValmin.Text, VolatilityValmax.Text, optionType, MaturityVal.Text, RfRateVal.Text, StockPriceVal.Text, StrikePriceVal.Text);
                                break;
                            }
                    }
                }
            }

            if (!checkedval)
            {
                OptionRequest = OptionRequestMaker.MakeOptionRequest(optionType, (StockPriceVal.Text), (StrikePriceVal.Text), (MaturityVal.Text), (RfRateVal.Text), (VolatilityVal.Text));
            }

            if(!logicalError){
                answerGreecs = OptionRequest.Rfq_Handler(checkedval);

                if (!checkedval) { PayoffVal.Content = answerGreecs.greecsdico.Values.ElementAt(2).ToString(); }

                BuildChart();
            }
        }

        private void OptionType_Checked(object sender, RoutedEventArgs e)
        {
            optionType = "call";

        }
        private void OptionType_Unchecked(object sender, RoutedEventArgs e)
        {
            optionType = "put";

        }



        private void MaturityCb_Selected(object sender, EventArgs e)
        {
            string maturity = MaturityCb.Text;
            Console.WriteLine("THE SELECTED ITEM IS:" + maturity);
            if (maturity == "1 month") { MaturityVal.Text = "0.083"; }
            else if (maturity == "3 months") { MaturityVal.Text = "0.25"; }
            else if (maturity == "6 months") { MaturityVal.Text = "0.5"; }
            else if (maturity == "1 year") { MaturityVal.Text = "1.0"; }
            else if (maturity == "2 years") { MaturityVal.Text = "2.0"; }
            else if (maturity == "5 years") { MaturityVal.Text = "5.0"; }
            else if (maturity == "10 years") { MaturityVal.Text = "10.0"; }
            else if (maturity == "12 years") { MaturityVal.Text = "12.0"; }
            else if (maturity == "15 years") { MaturityVal.Text = "15.0"; }
            else if (maturity == "20 years") { MaturityVal.Text = "20.0"; }
            else if (maturity == "30 years") { MaturityVal.Text = "30.0"; }
        }

        #endregion

        #region Radio button exclusion conditions
        private void Strike_Price_CheckedChanged(object sender, EventArgs e)
        {
            strike_already_checked = Strike_Price.IsChecked.Value;
        }

        private void Strike_Price_Click(object sender, RoutedEventArgs e)
        {

            if (Strike_Price.IsChecked == true && !strike_already_checked)
            {
                strike_already_checked = true;

                foreach (RadioButton rb in rblist)
                {
                    if (rb.Name != Strike_Price.Name)
                    {
                        rb.IsChecked = false;
                    }
                }
                
            }
            else
            {
                Strike_Price.IsChecked = false;
                strike_already_checked = false;
            }
            

            StockPriceVal.IsEnabled = true;
            StockPriceValmin.IsEnabled = false;
            StockPriceValmax.IsEnabled = false;

            VolatilityVal.IsEnabled = true;
            VolatilityValmin.IsEnabled = false;
            VolatilityValmax.IsEnabled = false;

            StrikePriceVal.IsEnabled = false;
            StrikePriceValmin.IsEnabled = true;
            StrikePriceValmax.IsEnabled = true;

        }


        private void Stock_Price_CheckedChanged(object sender, EventArgs e)
        {
            stock_already_checked = Stock_Price.IsChecked.Value;
        }

        private void Stock_Price_Click(object sender, RoutedEventArgs e)
        {

            if (Stock_Price.IsChecked == true && !stock_already_checked)
            {
                stock_already_checked = true;
                foreach (RadioButton rb in rblist)
                {
                    if (rb.Name != Stock_Price.Name)
                    {
                        rb.IsChecked = false;
                    }
                }
            }
            else
            {
                Stock_Price.IsChecked = false;
                stock_already_checked = false;
            }

            StrikePriceVal.IsEnabled = true;
            StrikePriceValmin.IsEnabled = false;
            StrikePriceValmax.IsEnabled = false;

            VolatilityVal.IsEnabled = true;
            VolatilityValmin.IsEnabled = false;
            VolatilityValmax.IsEnabled = false;

            StockPriceVal.IsEnabled = false;
            StockPriceValmin.IsEnabled = true;
            StockPriceValmax.IsEnabled = true;

        }


        private void Volatility_CheckedChanged(object sender, EventArgs e)
        {
            vol_already_checked = Volatility.IsChecked.Value;
        }

        private void Volatility_Click(object sender, RoutedEventArgs e)
        {

            if (Volatility.IsChecked == true && !vol_already_checked)
            {
                vol_already_checked = true;
                foreach (RadioButton rb in rblist)
                {
                    if (rb.Name != Volatility.Name)
                    {
                        rb.IsChecked = false;
                    }
                }
            }
            else
            {
                Volatility.IsChecked = false;
                vol_already_checked = false;
            }

            StockPriceVal.IsEnabled = true;
            StockPriceValmin.IsEnabled = false;
            StockPriceValmax.IsEnabled = false;

            StrikePriceVal.IsEnabled = true;
            StrikePriceValmin.IsEnabled = false;
            StrikePriceValmax.IsEnabled = false;

            VolatilityVal.IsEnabled = false;
            VolatilityValmin.IsEnabled = true;
            VolatilityValmax.IsEnabled = true;
        }
        #endregion

        #region Chart functions

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
                MinValue = 0,
            });

            DataChart.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Greec(s)",
            }
           );
            SeriesCollection series = new SeriesCollection();
            DataChart.Series = series;
        
        }


        private List<double> ConvertForChart(string grec)
        {
            List<double> values = new List<double>();
            foreach (Object key in answerGreecs.greecsdico.Keys)
            {
                if (key.ToString().Contains(grec))
                {
                    values.Add(Convert.ToDouble(answerGreecs.greecsdico[key]));
                }
            }
            return values;
        }

        private void AddChartSerie(string grec)
        {
            DataChart.Series.Add(new LineSeries() { Title = grec, Values = new ChartValues<double>(ConvertForChart(grec)),PointGeometrySize = 2 });
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

        private void Payoff_Selected(object sender, RoutedEventArgs e)
        {
            AddChartSerie("payoff");
        }

        #endregion

        #region Greec unselection

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

        private void Payoff_Unselected(object sender, RoutedEventArgs e)
        {
            RemoveChartSeries("payoff");

        }

        #endregion
    }
}
