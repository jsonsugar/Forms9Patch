﻿using System.Collections;
using Xamarin.Forms;

namespace Forms9Patch
{
	/// <summary>
	/// Single picker.
	/// </summary>
	public class SinglePicker : Frame
	{
		#region Properties
		/// <summary>
		/// The item templates property.
		/// </summary>
		public static readonly BindableProperty ItemTemplatesProperty = BindableProperty.Create("ItemTemplates", typeof(DataTemplateSelector), typeof(SinglePicker), null);
		/// <summary>
		/// Gets the item templates.
		/// </summary>
		/// <value>The item templates.</value>
		public DataTemplateSelector ItemTemplates
		{
			get { return (DataTemplateSelector)GetValue(ItemTemplatesProperty); }
			private set { SetValue(ItemTemplatesProperty, value); }
		}

		/// <summary>
		/// The items source property.
		/// </summary>
		public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(SinglePicker), null);
		/// <summary>
		/// Gets or sets the items source.
		/// </summary>
		/// <value>The items source.</value>
		public IEnumerable ItemsSource
		{
			get { return (IEnumerable)GetValue(ItemsSourceProperty); }
			set { 
				SetValue(ItemsSourceProperty, value); 
			}
		}

		/// <summary>
		/// The row height property.
		/// </summary>
		public static readonly BindableProperty RowHeightProperty = BindableProperty.Create("RowHeight", typeof(int), typeof(SinglePicker), 30);
		/// <summary>
		/// Gets or sets the height of the row.
		/// </summary>
		/// <value>The height of the row.</value>
		public int RowHeight
		{
			get { return (int)GetValue(RowHeightProperty); }
			set { SetValue(RowHeightProperty, value); }
		}

		/// <summary>
		/// The index property.
		/// </summary>
		public static readonly BindableProperty IndexProperty = BindableProperty.Create("Index", typeof(int), typeof(SinglePicker), 0, BindingMode.TwoWay);
		/// <summary>
		/// Gets or sets the index.
		/// </summary>
		/// <value>The index.</value>
		public int Index
		{
			get { return (int)GetValue(IndexProperty); }
			set { SetValue(IndexProperty, value); }
		}

		/// <summary>
		/// The selected item property.
		/// </summary>
		public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create("SelectedItem", typeof(object), typeof(SinglePicker), null, BindingMode.TwoWay);
		/// <summary>
		/// Gets or sets the selected item.
		/// </summary>
		/// <value>The selected item.</value>
		public object SelectedItem
		{
			get { return GetValue(SelectedItemProperty); }
			set { SetValue(SelectedItemProperty, value); }
		}

		#endregion

		#region Fields
		readonly internal BasePicker _basePicker = new BasePicker
		{
			BackgroundColor = Color.Transparent
		};
		readonly internal Xamarin.Forms.AbsoluteLayout _absLayout = new Xamarin.Forms.AbsoluteLayout();

		readonly internal Color _overlayColor = Color.FromRgb(0.85, 0.85, 0.85);

		readonly internal ColorGradientBox _upperGradient = new ColorGradientBox
		{
			Orientation = StackOrientation.Vertical
		};

		readonly internal ColorGradientBox _lowerGradient = new ColorGradientBox
		{
			Orientation = StackOrientation.Vertical
		};

		readonly internal BoxView _upperEdge = new BoxView
		{
			BackgroundColor = Color.Gray
		};
		readonly internal BoxView _lowerEdge = new BoxView
		{
			BackgroundColor = Color.Gray
		};
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Forms9Patch.SinglePicker"/> class.
		/// </summary>
		public SinglePicker()
		{
			Padding = new Thickness(0, 0, 0, 0);

			_upperGradient.StartColor = _overlayColor;
			_upperGradient.EndColor = _overlayColor.WithAlpha(0.5);
			_lowerGradient.StartColor = _overlayColor.WithAlpha(0.5);
			_lowerGradient.EndColor = _overlayColor;

			//Xamarin.Forms.AbsoluteLayout.SetLayoutFlags(_selectionPadView, AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.YProportional);
			Xamarin.Forms.AbsoluteLayout.SetLayoutFlags(_basePicker, AbsoluteLayoutFlags.All);
			Xamarin.Forms.AbsoluteLayout.SetLayoutFlags(_upperGradient, AbsoluteLayoutFlags.All);
			Xamarin.Forms.AbsoluteLayout.SetLayoutFlags(_lowerGradient, AbsoluteLayoutFlags.All);
			Xamarin.Forms.AbsoluteLayout.SetLayoutFlags(_upperEdge, AbsoluteLayoutFlags.YProportional | AbsoluteLayoutFlags.WidthProportional);
			Xamarin.Forms.AbsoluteLayout.SetLayoutFlags(_lowerEdge, AbsoluteLayoutFlags.YProportional | AbsoluteLayoutFlags.WidthProportional);

			//Xamarin.Forms.AbsoluteLayout.SetLayoutBounds(_selectionPadView, new Rectangle(0,0.5,1.0,RowHeight));
			Xamarin.Forms.AbsoluteLayout.SetLayoutBounds(_basePicker, new Rectangle(0.5, 0.5, 1.0, 1.0));
			Xamarin.Forms.AbsoluteLayout.SetLayoutBounds(_upperGradient, new Rectangle(0.5,0.0,1.0, 0.4));
			Xamarin.Forms.AbsoluteLayout.SetLayoutBounds(_lowerGradient, new Rectangle(0.5,1.0,1.0, 0.4));
			Xamarin.Forms.AbsoluteLayout.SetLayoutBounds(_upperEdge, new Rectangle(0.5, 0.4, 1.0, 1.0));
			Xamarin.Forms.AbsoluteLayout.SetLayoutBounds(_lowerEdge, new Rectangle(0.5, 0.6, 1.0, 1.0));

			//_absLayout.Children.Add(_selectionPadView);
			_absLayout.Children.Add(_upperEdge);
			_absLayout.Children.Add(_lowerEdge);
			_absLayout.Children.Add(_basePicker);
			_absLayout.Children.Add(_upperGradient);
			_absLayout.Children.Add(_lowerGradient);

			_basePicker.SelectBy = SelectBy.Position;
			_basePicker.SetBinding(BasePicker.ItemsSourceProperty,"ItemsSource");
			_basePicker.SetBinding(BasePicker.RowHeightProperty,"RowHeight");
			_basePicker.SetBinding(BasePicker.IndexProperty,"Index");
			_basePicker.SetBinding(BasePicker.SelectedItemProperty, "SelectedItem");
			_basePicker.BindingContext = this;

			Content = _absLayout;
		}

		#region change management
		/// <summary>
		/// Ons the property changed.
		/// </summary>
		/// <param name="propertyName">Property name.</param>
		protected override void OnPropertyChanged(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);
			if (propertyName == HeightProperty.PropertyName || propertyName == RowHeightProperty.PropertyName)
			{
				var overlayPortion = ((Height - RowHeight) / 2.0) / Height;
				Xamarin.Forms.AbsoluteLayout.SetLayoutBounds(_upperGradient, new Rectangle(0.5, 0.0, 1.0, overlayPortion));
				Xamarin.Forms.AbsoluteLayout.SetLayoutBounds(_lowerGradient, new Rectangle(0.5, 1.0, 1.0, overlayPortion));
				Xamarin.Forms.AbsoluteLayout.SetLayoutBounds(_upperEdge, new Rectangle(0.5, overlayPortion, 1.0, 1.0));
				Xamarin.Forms.AbsoluteLayout.SetLayoutBounds(_lowerEdge, new Rectangle(0.5, 1-overlayPortion, 1.0, 1.0));

			}
		}


		#endregion

	}
}

