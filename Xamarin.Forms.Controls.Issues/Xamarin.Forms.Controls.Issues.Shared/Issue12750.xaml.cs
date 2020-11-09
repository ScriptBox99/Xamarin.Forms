﻿using System.Collections.Generic;
using Xamarin.Forms.CustomAttributes;
using System.Collections.ObjectModel;
using System;

#if UITEST
using Xamarin.UITest;
using NUnit.Framework;
using Xamarin.Forms.Core.UITests;
#endif

namespace Xamarin.Forms.Controls.Issues
{
	[Issue(IssueTracker.Github, 12750,
		"[Bug] SwipeView in ListView on Android causes Overlapping Duplicates",
		PlatformAffected.Android)]
	public partial class Issue12750 : TestContentPage
	{
		ObservableCollection<string> _list;

		public Issue12750()
		{
#if APP
			InitializeComponent();

			_list = new ObservableCollection<string>
			{
				"one",
				"two"
			};
			Issue12750ListView.ItemsSource = _list;
#endif
		}

		protected override void Init()
		{
		}

		void OnButtonClicked(object sender, EventArgs e)
		{
			Navigation.PushModalAsync(new Issue12750DetailPage(_list));
		}
	}

	public class Issue12750DetailPage : ContentPage
	{
		ObservableCollection<string> _list;

		public Issue12750DetailPage(ObservableCollection<string> list)
		{
			_list = list;

			var layout = new StackLayout();

			var label = new Label
			{
				Text = "Add item:"
			};

			var entry = new Entry();

			var button = new Button
			{
				Text = "Add"
			};

			layout.Children.Add(label);
			layout.Children.Add(entry);
			layout.Children.Add(button);

			Content = layout;

			button.Clicked += (sender, args) =>
			{
				_list.Add(entry.Text);
				Navigation.PopModalAsync();
			};
		}
	}
}