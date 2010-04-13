// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Extensions;
    using Infrastructure;

    public class GridDataProcessor
    {
        private readonly IGridBindingContext bindingContext;

        private bool dataSourceIsProcessed;
        private int totalCount;
        private IEnumerable processedDataSource;
        private IList<SortDescriptor> sortDescriptors;
        private IList<IFilterDescriptor> filterDescriptors;
        private IList<GroupDescriptor> groupDescriptors;

        public GridDataProcessor(IGridBindingContext bindingContext)
        {
            Guard.IsNotNull(bindingContext, "bindingContext");

            this.bindingContext = bindingContext;
        }

        public int Total
        {
            get
            {
                EnsureDataSourceIsProcessed();
                return totalCount;
            }
        }

        public IList<SortDescriptor> SortDescriptors
        {
            get
            {
                if (sortDescriptors == null)
                {
                    string sortExpression = bindingContext.GetGridParameter<string>(GridUrlParameters.OrderBy);

                    if (sortExpression != null)
                    {
                        sortDescriptors = GridDescriptorSerializer.Deserialize<SortDescriptor>(sortExpression);
                    }

                    if (sortDescriptors == null)
                    {
                        sortDescriptors = bindingContext.SortDescriptors;
                    }
                }

                return sortDescriptors;
            }
        }

        public IList<GroupDescriptor> GroupDescriptors
        {
            get
            {
                if (groupDescriptors == null)
                {
                    string groupExpression = bindingContext.GetGridParameter<string>(GridUrlParameters.GroupBy);
                    
                    if (groupExpression != null)
                    {
                        groupDescriptors = GridDescriptorSerializer.Deserialize<GroupDescriptor>(groupExpression);
                    }

                    if (groupDescriptors == null)
                    {
                        groupDescriptors = bindingContext.GroupDescriptors;
                    }
                }

                return groupDescriptors;
            }
        }

        public IList<IFilterDescriptor> FilterDescriptors
        {
            get
            {
                if (filterDescriptors == null)
                {
                    string filterExpression = bindingContext.GetGridParameter<string>(GridUrlParameters.Filter);

                    if (filterExpression != null)
                    {
                        filterDescriptors = FilterDescriptorFactory.Create(filterExpression);
                    }

                    if (filterDescriptors == null)
                    {
                        filterDescriptors = bindingContext.FilterDescriptors.Cast<IFilterDescriptor>().ToList();
                    }
                }

                return filterDescriptors;
            }
        }

        public int PageCount
        {
            get
            {
                EnsureDataSourceIsProcessed();

                int pageSize = bindingContext.PageSize;

                if ((totalCount == 0) || (pageSize == 0))
                {
                    return 1;
                }

                return (totalCount + pageSize - 1)/pageSize;
            }
        }

        public IEnumerable ProcessedDataSource
        {
            get
            {
                EnsureDataSourceIsProcessed();

                return processedDataSource;
            }
        }

        public int CurrentPage
        {
            get
            {
                return bindingContext.GetGridParameter<int?>(GridUrlParameters.CurrentPage) ?? 1;
            }
        }

        private void EnsureDataSourceIsProcessed()
        {
            if (dataSourceIsProcessed)
            {
                return;
            }

            if (bindingContext.DataSource == null)
            {
                dataSourceIsProcessed = true;
                return;
            }

            IQueryable dataSource = bindingContext.DataSource.AsQueryable();
            GridModel model = null;
            if (!bindingContext.EnableCustomBinding)
            {
                model = dataSource.ToGridModel(CurrentPage, bindingContext.PageSize, SortDescriptors, FilterDescriptors, GroupDescriptors);
            }
            else
            {
                model = dataSource.ToGroupingGridModel(GroupDescriptors, bindingContext.Total);
            }

            totalCount = model.Total;
            processedDataSource = model.Data;

            dataSourceIsProcessed = true;
        }
    }
}