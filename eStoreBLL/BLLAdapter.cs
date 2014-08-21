#region Licence
/*
 * The MIT License
 *
 * Copyright (c) 2008-2013, Andrew Gray
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
#endregion
using eStoreDAL.DALTableAdapters;

namespace eStoreBLL {
    public class BLLAdapter {
        private static BLLAdapter _instance;
        private CategoryTableAdapter _categoriesAdapter;
        private ZoneTableAdapter _zoneAdapter;
        private WishListTableAdapter _wishListAdapter;
        private UsersTableAdapter _usersAdapter;
        private CCExpiryMonthTableAdapter _ccExpiryMonthsAdapter;
        private CCExpiryYearTableAdapter _ccExpiryYearsAdapter;
        private ProductColorTableAdapter _colorsAdapter;
        private ShipCostTableAdapter _costsAdapter;
        private ShipToCountryTableAdapter _countriesAdapter;
        private CurrencyTableAdapter _currenciesAdapter;
        private CustomerAddressTableAdapter _customerAddressAdapter;
        private OrderTableAdapter _orderAdapter;
        private OrderDetailTableAdapter _orderDetailsAdapter;
        private DepartmentTableAdapter _departmentsAdapter;
        private EmailTableAdapter _emailsAdapter;
        private ProductImageTableAdapter _imagesAdapter;
        private ProductTableAdapter _productsAdapter;
        private LinksTableAdapter _linkAdapter;
        private TestimonialsTableAdapter _testimonialAdapter;
        private ShipToModeTableAdapter _modesAdapter;
        private SavedSearchTableAdapter _savedSearchAdapter;
        private SettingsTableAdapter _settingsAdapter;
        private ProductSizeTableAdapter _sizesAdapter;
        private SupplierTableAdapter _supplierAdapter;
        
        private BLLAdapter() {
        }

        public static BLLAdapter Instance {
            get { return _instance ?? (_instance = new BLLAdapter()); }
        }

        public CategoryTableAdapter CategoryAdapter {
            get { return _categoriesAdapter ?? (_categoriesAdapter = new CategoryTableAdapter()); }
        }

        public ZoneTableAdapter ZoneAdapter {
            get { return _zoneAdapter ?? (_zoneAdapter = new ZoneTableAdapter()); }
        }

        public WishListTableAdapter WishListAdapter {
            get { return _wishListAdapter ?? (_wishListAdapter = new WishListTableAdapter()); }
        }

        public UsersTableAdapter UserAdapter {
            get { return _usersAdapter ?? (_usersAdapter = new UsersTableAdapter()); }
        }

        public CCExpiryMonthTableAdapter CcMonthAdapter {
            get { return _ccExpiryMonthsAdapter ?? (_ccExpiryMonthsAdapter = new CCExpiryMonthTableAdapter()); }
        }

        public CCExpiryYearTableAdapter CcYearAdapter {
            get { return _ccExpiryYearsAdapter ?? (_ccExpiryYearsAdapter = new CCExpiryYearTableAdapter()); }
        }

        public ProductColorTableAdapter ColorAdapter {
            get { return _colorsAdapter ?? (_colorsAdapter = new ProductColorTableAdapter()); }
        }

        public ShipCostTableAdapter CostAdapter {
            get { return _costsAdapter ?? (_costsAdapter = new ShipCostTableAdapter()); }
        }

        public ShipToCountryTableAdapter CountryAdapter {
            get { return _countriesAdapter ?? (_countriesAdapter = new ShipToCountryTableAdapter()); }
        }

        public CurrencyTableAdapter CurrencyAdapter {
            get { return _currenciesAdapter ?? (_currenciesAdapter = new CurrencyTableAdapter()); }
        }

        public CustomerAddressTableAdapter CustomerAddressAdapter {
            get { return _customerAddressAdapter ?? (_customerAddressAdapter = new CustomerAddressTableAdapter()); }
        }

        public DepartmentTableAdapter DepartmentAdapter {
            get { return _departmentsAdapter ?? (_departmentsAdapter = new DepartmentTableAdapter()); }
        }

        public EmailTableAdapter EmailAdapter {
            get { return _emailsAdapter ?? (_emailsAdapter = new EmailTableAdapter()); }
        }

        public ProductImageTableAdapter ImageAdapter {
            get { return _imagesAdapter ?? (_imagesAdapter = new ProductImageTableAdapter()); }
        }

        public LinksTableAdapter LinkAdapter {
            get { return _linkAdapter ?? (_linkAdapter = new LinksTableAdapter()); }
        }

        public TestimonialsTableAdapter TestimonialAdapter {
            get { return _testimonialAdapter ?? (_testimonialAdapter = new TestimonialsTableAdapter()); }
        }

        public ShipToModeTableAdapter ModeAdapter {
            get { return _modesAdapter ?? (_modesAdapter = new ShipToModeTableAdapter()); }
        }

        public OrderTableAdapter OrderAdapter {
            get { return _orderAdapter ?? (_orderAdapter = new OrderTableAdapter()); }
        }

        public OrderDetailTableAdapter OrderDetailAdapter {
            get { return _orderDetailsAdapter ?? (_orderDetailsAdapter = new OrderDetailTableAdapter()); }
        }

        public ProductTableAdapter ProductAdapter {
            get { return _productsAdapter ?? (_productsAdapter = new ProductTableAdapter()); }
        }

        public SavedSearchTableAdapter SavedSearchAdapter {
            get { return _savedSearchAdapter ?? (_savedSearchAdapter = new SavedSearchTableAdapter()); }
        }

        public SettingsTableAdapter SettingAdapter {
            get { return _settingsAdapter ?? (_settingsAdapter = new SettingsTableAdapter()); }
        }

        public ProductSizeTableAdapter SizeAdapter {
            get { return _sizesAdapter ?? (_sizesAdapter = new ProductSizeTableAdapter()); }
        }

        public SupplierTableAdapter SupplierAdapter {
            get { return _supplierAdapter ?? (_supplierAdapter = new SupplierTableAdapter()); }
        }
    }
}