using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

using DBReader.CompanyDataSetTableAdapters;
using static DBReader.CompanyDataSet;

namespace DBReader
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class PagesWindow : Window
    {
        // ----------------------- user
        private UserRole userRole = UserRole.ADMIN;

        // все адаптеры
        private List<Page> allPages = new List<Page>();
        // ----------------------------

        private int currentPageIdx = 0;

        // other windows
        //private CreateRecordWindow createRecordWindow = new CreateRecordWindow();

        public PagesWindow()
        {
            InitializeComponent();
            //sqlConnection.Open();

            FactoriesPage factoriesPage = new FactoriesPage();
            factoriesPage.pagesWindow = this;

            FurniturePage furniturePage = new FurniturePage();
            furniturePage.pagesWindow = this;

            FurnitureCarriersPage furnitureCarriersPage = new FurnitureCarriersPage();
            furnitureCarriersPage.pagesWindow = this;

            FurnitureTypesPage furnitureTypesPage = new FurnitureTypesPage();
            furnitureTypesPage.pagesWindow = this;

            MachinesPage machinesPage = new MachinesPage();
            machinesPage.pagesWindow = this;

            MaterialsPage materialsPage = new MaterialsPage();
            materialsPage.pagesWindow = this;

            MaterialsSuppliersPage materialsSuppliersPage = new MaterialsSuppliersPage();
            materialsSuppliersPage.pagesWindow = this;

            PostPage postPage = new PostPage();
            postPage.pagesWindow = this;

            ReceiptsPage receiptsPage = new ReceiptsPage();
            receiptsPage.pagesWindow = this;

            StaffPage staffPage = new StaffPage();
            staffPage.pagesWindow = this;

            allPages.Add(factoriesPage);

            allPages.Add(furniturePage);
            allPages.Add(furnitureCarriersPage);
            allPages.Add(furnitureTypesPage);

            allPages.Add(machinesPage);

            allPages.Add(materialsPage);
            allPages.Add(materialsSuppliersPage);

            allPages.Add(postPage);

            allPages.Add(receiptsPage);

            allPages.Add(staffPage);

            PagesFrame.Content = allPages[0];
        }

        public void IncrementCurrentTable(int inc)
        {
            currentPageIdx += inc;

            currentPageIdx = Math.Max(0, Math.Min(currentPageIdx, allPages.Count - 1));

            PagesFrame.Content = allPages[currentPageIdx];
        }

        private void CreateRecordButton_Click(object sender, RoutedEventArgs e)
        {
            //createRecordWindow.CreateEditor(adapters[currentTableIdx]);

            //createRecordWindow.Show();
        }

        private void DeleteRecordButton_Click(object sender, RoutedEventArgs e)
        {
            /*
            try
            {
                foreach (DataRowView drv in dbDataGrid.SelectedItems)
                {
                    if (adapters[currentTableIdx] is PostTableAdapter postTableAdapter)
                    {
                        postTableAdapter.DeletePost((int)drv.Row[0], (string)drv.Row[1]);                   
                    }
                    else if (adapters[currentTableIdx] is StaffTableAdapter staffTableAdapter)
                    {
                        staffTableAdapter.DeleteStaff((int)drv.Row[0], (string)drv.Row[1], (int)drv.Row[2]);
                    }
                }
            } 
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            UpdateDataGrid();
            */
        }

        private void DBDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            /*
            try
            {
                dbDataGrid.RowEditEnding -= DBDataGrid_RowEditEnding;

                int lastID = (int)(dbDataGrid.SelectedItem as DataRowView).Row[0];

                dbDataGrid.CommitEdit();

                DataRowView dataRowView = e.Row.Item as DataRowView;

                if (adapters[currentTableIdx] is PostTableAdapter postTableAdapter)
                {
                    postTableAdapter.UpdatePost(lastID, (string)dataRowView.Row[1], lastID);
                }
                else if (adapters[currentTableIdx] is StaffTableAdapter staffTableAdapter)
                {
                    staffTableAdapter.UpdateStaff((string)dataRowView.Row[1], (int)dataRowView[2], lastID);
                }
            } 
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            dbDataGrid.RowEditEnding += DBDataGrid_RowEditEnding;
            */
        }

        private void EditRecordButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
