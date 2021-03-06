﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using DataBinding.Local;
using System.Windows.Data;

namespace DataBinding
{
    public partial class ValidateProductObject : UserControl
    {
        public ValidateProductObject()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Product product = new Product("AEFS100", "Portable Defibrillator", 77,
                "Analyzes the electrical activity of a person's heart and applies an electric shock if necessary.");
            gridProductDetails.DataContext = product;            
        }

        private void cmdChange_Click(object sender, RoutedEventArgs e)
        {
            Product product = (Product)gridProductDetails.DataContext;
            product.UnitCost *= 1.1;
        }

        private void cmdCheck_Click(object sender, RoutedEventArgs e)
        {
            Product product = (Product)gridProductDetails.DataContext;
            lblInfo.Text = "Model Name: " + product.ModelName + "\nModel Number: " + product.ModelNumber + "\nUnit Cost: " + product.UnitCost;
        }

        private void Grid_BindingValidationError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Error.Exception != null)
            {
                // Validation failed due to an exception.
                lblInfo.Text = e.Error.Exception.Message;
                lblInfo.Text += "\nThe stored UnitCost is still: " + ((Product)gridProductDetails.DataContext).UnitCost.ToString();
                txtUnitCost.Focus();
            }
            else
            {
                // Validation error reported through an interface.
                Product product = (Product)gridProductDetails.DataContext;
                if (product.HasErrors)
                {                    
                    lblInfo.Text = e.Error.ErrorContent.ToString();
                    lblInfo.Text += "\nHowever, the stored ModelNumber is now: " + ((Product)gridProductDetails.DataContext).ModelNumber;
                    txtModelNumber.Focus();
                }
            }
        }

        
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblInfo.Text = "";            
        }
    }
}
