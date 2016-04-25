using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using AstroApp.Helpers;

namespace AstroApp.UserControls
{
	public class WrapGridPanel:Panel
	{

		public static readonly DependencyProperty RatioDependencyProperty = DependencyProperty.Register("Ratio", typeof(double), typeof(WrapGridPanel), new PropertyMetadata(1.0));
		public double Ratio
		{
			get
			{
				return (double)GetValue(RatioDependencyProperty);
			}
			set
			{
				SetValue(RatioDependencyProperty, value);
			}
		}

		private readonly LayoutAdapter _layoutAdapter = new LayoutAdapter();


		protected override Size MeasureOverride(Size availableSize)
		{
			int count = Children.Count;
			if (count == 0)
			{
				return availableSize;
			}
			Layout bestGridLayout = GetBestLayout(availableSize, count);

			double elementWidth = availableSize.Width/bestGridLayout.ColumnCount;
			double elementHeight = availableSize.Height/bestGridLayout.RowCount;

			foreach (UIElement child in Children)
			{
				child.Measure(new Size(elementWidth,elementHeight));
			}
			return availableSize;
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			int count = Children.Count;
			if (count == 0)
			{
				return finalSize;
			}
			Layout bestGridLayout = GetBestLayout(finalSize, count);
			double elementWidth = finalSize.Width / bestGridLayout.ColumnCount;
			double elementHeight = finalSize.Height / bestGridLayout.RowCount;

			int row = 0, column = 0;
			foreach (UIElement child in Children)
			{
				if (column == bestGridLayout.ColumnCount)
				{
					column = 0;
					row++;
				}
				int x = (int) Math.Round(column*elementWidth);
				int y =(int)Math.Round( row*elementHeight);

				child.Arrange(new Rect(new Point(x, y), new Size(elementWidth, elementHeight)));
			
				column++;

			}
			return finalSize;
		}

		private Layout GetBestLayout(Size availableSize, int count)
		{
			
			double panelRatio = availableSize.Width / availableSize.Height;
			Layout bestGridLayout = _layoutAdapter.ComputeBestGridLayout(count, Ratio, panelRatio);
			return bestGridLayout;
		}

	}
}
