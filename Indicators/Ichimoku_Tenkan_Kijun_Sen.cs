// Copyright InvestDataSystems/Reuniware LLC. © 2022. All rights reserved.

using System;
using System.Diagnostics;
using System.Drawing;
using TradingPlatform.BusinessLayer;

namespace Indicator1
{
    /// <summary>
    /// An example of blank indicator. Add your code, compile it and use on the charts in the assigned trading terminal.
    /// Information about API you can find here: http://api.quantower.com
    /// Code samples: https://github.com/Quantower/Examples
    /// </summary>
	public class Indicator1 : Indicator
    {
        /// <summary>
        /// Indicator's constructor. Contains general information: name, description, LineSeries etc. 
        /// </summary>
        public Indicator1()
            : base()
        {
            // Defines indicator's name and description.
            Name = "Indicator1";
            Description = "My indicator's annotation";

            // Defines line on demand with particular parameters.
            AddLineSeries("kijun_sen", Color.CadetBlue, 1, LineStyle.Solid);

            // Defines line on demand with particular parameters.
            AddLineSeries("tenkan_sen", Color.Green, 1, LineStyle.Solid);

            // Defines line on demand with particular parameters.
            AddLineSeries("line2", Color.Honeydew, 1, LineStyle.Solid);

            // By default indicator will be applied on main window of the chart
            SeparateWindow = false;
        }

        /// <summary>
        /// This function will be called after creating an indicator as well as after its input params reset or chart (symbol or timeframe) updates.
        /// </summary>
        protected override void OnInit()
        {
            // Add your initialization code here
        }

        /// <summary>
        /// Calculation entry point. This function is called when a price data updates. 
        /// Will be runing under the HistoricalBar mode during history loading. 
        /// Under NewTick during realtime. 
        /// Under NewBar if start of the new bar is required.
        /// </summary>
        /// <param name="args">Provides data of updating reason and incoming price.</param>
        protected override void OnUpdate(UpdateArgs args)
        {
            if (Count <= 26)
                return;

            double higher26 = GetPrice(PriceType.High, 0);
            double lower26 = GetPrice(PriceType.Low, 0);
            for (int x=1;x<27;x++)
            {
                double high = GetPrice(PriceType.High, x);
                if (high > higher26)
                {
                    higher26 = high;
                }
                double low = GetPrice(PriceType.Low, x);
                if (low < lower26)
                {
                    lower26 = low;
                }
            }
            double kijun = (higher26 + lower26) / 2;

            double higher9 = GetPrice(PriceType.High, 0);
            double lower9 = GetPrice(PriceType.Low, 0);
            for (int x = 1; x < 9; x++)
            {
                double high = GetPrice(PriceType.High, x);
                if (high > higher9)
                {
                    higher9 = high;
                }
                double low = GetPrice(PriceType.Low, x);
                if (low < lower9)
                {
                    lower9 = low;
                }
            }
            double tenkan = (higher9 + lower9) / 2;


            SetValue(kijun, 0);
            SetValue(tenkan, 1);

        }
    }
}
