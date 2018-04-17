using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System.Collections.Generic;
using System.Data;

namespace ControlLazyLoad
{
    /// <summary>
    /// 控件扩展
    /// </summary>
    public static class ControlExtension
    {
        ///// <summary>
        ///// 查找
        ///// </summary>
        ///// <typeparam name="T">返回的数据列表所属的类型IList&lt;&gt;</typeparam>
        ///// <param name="filterString">查找的关键词</param>
        ///// <param name="pageIndex">页码</param>
        ///// <param name="pageSize">每页条数</param>
        ///// <param name="count">返回符合条件的总记录数</param>
        ///// <returns></returns>
        //public delegate List<T> GetData<T>(string filterString, int pageIndex, int pageSize, out int count) where T : class;
        /// <summary>
        /// 查找
        /// </summary>
        /// <typeparam name="T">返回的数据列表所属的类型DataTable</typeparam>
        /// <param name="filterString">查找的关键词</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="count">返回符合条件的总记录数</param>
        /// <returns></returns>
        public delegate T GetData<out T>(string filterString, int pageIndex, int pageSize, out int count) where T : class;
        /// <summary>
        /// SearchLookUpEdit控件绑定数据
        /// </summary>
        /// <typeparam name="T">绑定的数据的类型</typeparam>
        /// <param name="searchLookUpEdit">控件</param>
        /// <param name="displayMember">显示值所绑定的成员</param>
        /// <param name="valueMember">实际值所绑定的成员</param>
        /// <param name="pageIndex">默认显示时的页码</param>
        /// <param name="pageSize">分页加载时每次加载数据条数</param>
        /// <param name="callBack">查找的时候实时绑定的方法，该方法返回一个根据查找的关键词过滤的数据源 (第一个参数是查找的关键词,第二个参数是页码，第三个参数是每页数据条数，第四个参数是符合条件的总的记录数，该参数使用out修饰)</param>
        public static void BindData<T>(this SearchLookUpEdit searchLookUpEdit, string displayMember, string valueMember, int pageIndex,int pageSize, GetData<List<T>> callBack) where T : class
        {
            BindEvent(searchLookUpEdit.Properties, displayMember, valueMember, pageIndex, pageSize, callBack);


            //if (callBack == null)
            //{
            //    return;
            //}
            //int originPageIndex = pageIndex;
            //List<T> dataContainer = new List<T>();
            //int count;
            //dataContainer.AddRange(callBack(null, pageIndex, pageSize, out count));
            //searchLookUpEdit.Properties.DisplayMember = displayMember;
            //searchLookUpEdit.Properties.ValueMember = valueMember;
            //searchLookUpEdit.Properties.DataSource = dataContainer;
            //var gv = searchLookUpEdit.Properties.View;
            //if (dataContainer.Count < count)
            //{
            //    gv.ColumnFilterChanged += (sender, e) =>
            //    {
            //        pageIndex = 1;
            //        dataContainer.Clear();
            //        string filterText = gv.FindFilterText;
            //        if (string.IsNullOrEmpty(filterText))
            //        {
            //            dataContainer.AddRange(callBack(null, originPageIndex, pageSize, out count));
            //        }
            //        else
            //        {
            //            dataContainer.AddRange(callBack(filterText, pageIndex, pageSize, out count));
            //        }
            //        gv.RefreshData();
            //        gv.ApplyFindFilter(filterText);
            //    };
            //    gv.TopRowChanged += (sender, e) =>
            //    {
            //        string filterText = gv.FindFilterText;
            //        int pageCount = (count + pageSize - 1) / pageSize;
            //        if (gv.IsRowVisible(gv.DataRowCount - 1) == DevExpress.XtraGrid.Views.Grid.RowVisibleState.Visible && pageIndex < pageCount)
            //        {
            //            pageIndex++;
            //            dataContainer.AddRange(callBack(filterText, pageIndex, pageSize, out count));
            //            gv.RefreshData();
            //            gv.ApplyFindFilter(filterText);
            //        }
            //    };
            //}
        }

        private static void BindEvent<T>(RepositoryItemSearchLookUpEdit properties, string displayMember, string valueMember, int pageIndex, int pageSize, GetData<T> callBack) where T : class
        {
            if (callBack == null)
            {
                return;
            }
            int originPageIndex = pageIndex;
            List<T> dataContainer = new List<T>();
            int count;
            dataContainer.AddRange(callBack(null, pageIndex, pageSize, out count));
            properties.DisplayMember = displayMember;
            properties.ValueMember = valueMember;
            properties.DataSource = dataContainer;
            var gv = properties.View;
            if (dataContainer.Count < count)
            {
                gv.ColumnFilterChanged += (sender, e) =>
                {
                    pageIndex = 1;
                    dataContainer.Clear();
                    string filterText = gv.FindFilterText;
                    if (string.IsNullOrEmpty(filterText))
                    {
                        dataContainer.AddRange(callBack(null, originPageIndex, pageSize, out count));
                    }
                    else
                    {
                        dataContainer.AddRange(callBack(filterText, pageIndex, pageSize, out count));
                    }
                    gv.RefreshData();
                    gv.ApplyFindFilter(filterText);
                };
                gv.TopRowChanged += (sender, e) =>
                {
                    string filterText = gv.FindFilterText;
                    int pageCount = (count + pageSize - 1) / pageSize;
                    if (gv.IsRowVisible(gv.DataRowCount - 1) == DevExpress.XtraGrid.Views.Grid.RowVisibleState.Visible && pageIndex < pageCount)
                    {
                        pageIndex++;
                        dataContainer.AddRange(callBack(filterText, pageIndex, pageSize, out count));
                        gv.RefreshData();
                        gv.ApplyFindFilter(filterText);
                    }
                };
            }
        }

        /// <summary>
        /// SearchLookUpEdit控件绑定数据
        /// </summary>
        /// <typeparam name="T">绑定的数据的类型</typeparam>
        /// <param name="searchLookUpEdit">控件</param>
        /// <param name="displayMember">显示值所绑定的成员</param>
        /// <param name="valueMember">实际值所绑定的成员</param>
        /// <param name="pageIndex">默认显示时的页码</param>
        /// <param name="pageSize">分页加载时每次加载数据条数</param>
        /// <param name="callBack">查找的时候实时绑定的方法，该方法返回一个根据查找的关键词过滤的数据源 (第一个参数是查找的关键词,第二个参数是页码，第三个参数是每页数据条数，第四个参数是符合条件的总的记录数，该参数使用out修饰)</param>
        public static void BindData(this SearchLookUpEdit searchLookUpEdit, string displayMember, string valueMember, int pageIndex, int pageSize, DataTable callBack) 
        {
            //if (callBack == null)
            //{
            //    return;
            //}
            //int originPageIndex = pageIndex;
            //DataTable dataContainer = new DataTable();
            //if (dataContainer.GetType().IsGenericType)
            //{

            //}
            //int count;
            //DataTable dt =  callBack(null, pageIndex, pageSize, out count);
            //if (dt != null)
            //{
            //    dataContainer = dt.Clone();
            //}
            //searchLookUpEdit.Properties.DisplayMember = displayMember;
            //searchLookUpEdit.Properties.ValueMember = valueMember;
            //searchLookUpEdit.Properties.DataSource = dataContainer;
            //var gv = searchLookUpEdit.Properties.View;
            //if (dataContainer.Rows.Count < count)
            //{
            //    gv.ColumnFilterChanged += (sender, e) =>
            //    {
            //        pageIndex = 1;
            //        dataContainer.Clear();
            //        string filterText = searchLookUpEdit.Properties.View.FindFilterText;
            //        if (string.IsNullOrEmpty(filterText))
            //        {
            //            dataContainer.AddRange(callBack(null, originPageIndex, pageSize, out count));
            //        }
            //        else
            //        {
            //            dataContainer.AddRange(callBack(filterText, pageIndex, pageSize, out count));
            //        }
            //        gv.RefreshData();
            //        gv.ApplyFindFilter(filterText);
            //    };
            //    gv.TopRowChanged += (sender, e) =>
            //    {
            //        string filterText = gv.FindFilterText;
            //        int pageCount = (count + pageSize - 1) / pageSize;
            //        if (gv.IsRowVisible(gv.DataRowCount - 1) == DevExpress.XtraGrid.Views.Grid.RowVisibleState.Visible && pageIndex < pageCount)
            //        {
            //            pageIndex++;
            //            dataContainer.AddRange(callBack(filterText, pageIndex, pageSize, out count));
            //            gv.RefreshData();
            //            gv.ApplyFindFilter(filterText);
            //        }
            //    };
            //}
        }

        public static void BindData<T>(this LookUpEdit lookUpEdit, string displayMember, string valueMember, int pageIndex, int pageSize, GetData<T> callBack) where T : class
        {
            if (callBack == null)
            {
                return;
            }
            int originPageIndex = pageIndex;
            List<T> dataContainer = new List<T>();
            int count;
            dataContainer.AddRange(callBack(null, pageIndex, pageSize, out count));
            lookUpEdit.Properties.DisplayMember = displayMember;
            lookUpEdit.Properties.ValueMember = valueMember;
            lookUpEdit.Properties.DataSource = dataContainer;
            //var gv = lookUpEdit.Properties.CreateViewInfo;
            if (dataContainer.Count < count)
            {
                //gv.ColumnFilterChanged += (sender, e) =>
                //{
                //    pageIndex = 1;
                //    dataContainer.Clear();
                //    string filterText = searchLookUpEdit.Properties.View.FindFilterText;
                //    if (string.IsNullOrEmpty(filterText))
                //    {
                //        dataContainer.AddRange(callBack(null, originPageIndex, pageSize, out count));
                //    }
                //    else
                //    {
                //        dataContainer.AddRange(callBack(filterText, pageIndex, pageSize, out count));
                //    }
                //    gv.RefreshData();
                //    gv.ApplyFindFilter(filterText);
                //};
                //gv.MouseWheel += (sender, e) =>
                //{
                //    //string filterText = gv.FindFilterText;
                //    int pageCount = (count + pageSize - 1) / pageSize;
                //    string str = gv
                //    if (gv.isrow(gv.DataRowCount - 1) == DevExpress.XtraGrid.Views.Grid.RowVisibleState.Visible && pageIndex < pageCount)
                //    {
                //        pageIndex++;
                //        dataContainer.AddRange(callBack(filterText, pageIndex, pageSize, out count));
                //        gv.RefreshData();
                //        gv.ApplyFindFilter(filterText);
                //    }
                //};
            }
        }
    }
    /// <summary>
    /// DataTable扩展
    /// </summary>
    public static class DataTableExtension
    {
        public static void AddRange(this DataTable source, DataTable destination)
        {
            if (destination != null)
            {
                for (int i = 0; i < destination.Rows.Count; i++)
                {
                    source.ImportRow(destination.Rows[i]);
                }
            }
        }
    }
}
