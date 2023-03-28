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
        public UserRole userRole = UserRole.SELLER;

        // все адаптеры
        private List<Page> allPages = new List<Page>();
        // ----------------------------

        private int currentPageIdx = 0;


        // pages
        public FactoriesPage FactoriesPage { get; } = new FactoriesPage();

        public FurniturePage FurniturePage { get; } = new FurniturePage();
        public FurnitureCarriersPage FurnitureCarriersPage { get; } = new FurnitureCarriersPage();
        public FurnitureTypesPage FurnitureTypesPage { get; } = new FurnitureTypesPage();

        public MachinesPage MachinesPage { get; } = new MachinesPage();

        public MaterialsPage MaterialsPage { get; } = new MaterialsPage();
        public MaterialsSuppliersPage MaterialsSuppliersPage { get; } = new MaterialsSuppliersPage();

        public PostPage PostPage { get; } = new PostPage();

        public ReceiptsPage ReceiptsPage { get; } = new ReceiptsPage();

        public StaffPage StaffPage { get; } = new StaffPage();

        public PagesWindow()
        {
            InitializeComponent();
        }

        public void LoadPages()
        {
            if (userRole == UserRole.ADMIN)
            {
                allPages.Add(FactoriesPage);

                allPages.Add(FurniturePage);
                allPages.Add(FurnitureCarriersPage);
                allPages.Add(FurnitureTypesPage);

                allPages.Add(MachinesPage);

                allPages.Add(MaterialsPage);
                allPages.Add(MaterialsSuppliersPage);

                allPages.Add(PostPage);

                allPages.Add(ReceiptsPage);

                allPages.Add(StaffPage);
            }
            else if (userRole == UserRole.SELLER)
            {
                allPages.Add(ReceiptsPage);
            }

            ReceiptsPage.Init();

            PagesFrame.Content = allPages[0];
        }

        public void IncrementCurrentTable(int inc)
        {
            currentPageIdx += inc;

            currentPageIdx = Math.Max(0, Math.Min(currentPageIdx, allPages.Count - 1));

            FactoriesPage.TableDataGrid.ItemsSource = FactoriesPage.Adapter.GetData();

            FurniturePage.TableDataGrid.ItemsSource = FurniturePage.Adapter.GetData();
            FurnitureCarriersPage.TableDataGrid.ItemsSource = FurnitureCarriersPage.Adapter.GetData();
            FurnitureTypesPage.TableDataGrid.ItemsSource = FurnitureTypesPage.Adapter.GetData();

            MachinesPage.TableDataGrid.ItemsSource = MachinesPage.Adapter.GetData();

            MaterialsPage.TableDataGrid.ItemsSource = MaterialsPage.Adapter.GetData();
            MaterialsSuppliersPage.TableDataGrid.ItemsSource = MaterialsSuppliersPage.Adapter.GetData();

            PostPage.TableDataGrid.ItemsSource = PostPage.Adapter.GetData();

            ReceiptsPage.TableDataGrid.ItemsSource = ReceiptsPage.Adapter.GetData();
            ReceiptsPage.AvailableFurnitureDataGrid.ItemsSource = FurniturePage.Adapter.GetData();

            StaffPage.TableDataGrid.ItemsSource = StaffPage.Adapter.GetData();

            PagesFrame.Content = allPages[currentPageIdx];
        }
    }
}
